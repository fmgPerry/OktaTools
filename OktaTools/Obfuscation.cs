using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace OktaTools
{
    public static class Obfuscation
    {
        private static List<string> elementsToObfuscate = new List<string>() { "ID", "AccountHolderID", "ContactID", "MainDriver", "PolicyPeriodID", "FixedId", "Entry", "JobID", "ContactId", "CoverableID", "MainContact", "ReportedBy", "JobNumber", "FixedID", "SliceDate" };
        private static List<string> elementsToReverseObfuscate = new List<string>() { "ID", "FixedID", "FixedId", "JobNumber", "JobID", "MainDriver", "SliceDate" };
        private static string fmgapi_undefined = "fmgapi_undefined";
        private static string Secret;
        //private static Secretkey secretkey;
        private static Regex contactIdRegex = new Regex(@"^[a-zA-Z0-9]{4,}:[0-9]+$");


        public static object Obfuscate(object thisObject)
        {
            ObfuscateObject(thisObject);
            return thisObject;
        }
        public static object ReverseObfuscate(object thisObject)
        {
            ReverseObfuscateObject(thisObject);
            return thisObject;
        }
        private static object ObfuscateObject(object thisObject)
        {
            if (thisObject != null)
            {
                if (thisObject.GetType().IsArray)
                {
                    foreach (var singleObject in thisObject as IEnumerable)
                    {
                        ObfuscateIt(singleObject);
                    }
                }
                else
                {
                    ObfuscateIt(thisObject);
                }
            }
            return thisObject;
        }

        private static void ObfuscateIt(object singleObject)
        {
            var properties = singleObject.GetType().GetProperties().ToList();
            foreach (var property in properties)
            {
                var propertyType = property.PropertyType;
                if (propertyType.IsArray || propertyType.Namespace != "System")
                {
                    var result = ObfuscateObject(property.GetValue(singleObject));
                    property.SetValue(singleObject, result);
                }
                if (elementsToObfuscate.Contains(property.Name))
                {
                    // Obfuscate
                    var result = Obfuscate(property.GetValue(singleObject)?.ToString(), Secretkey.test);
                    property.SetValue(singleObject, result);
                }
            }
        }

        private static object ReverseObfuscateObject(object thisObject)
        {
            if (thisObject != null)
            {
                if (thisObject.GetType().IsArray)
                {
                    foreach (var singleObject in thisObject as IEnumerable)
                    {
                        ReverseObfuscateIt(singleObject);
                    }
                }
                else
                {
                    ReverseObfuscateIt(thisObject);
                }
            }
            return thisObject;
        }

        private static void ReverseObfuscateIt(object thisObject)
        {
            var properties = thisObject.GetType().GetProperties().ToList();
            foreach (var property in properties)
            {
                var propertyType = property.PropertyType;
                if (propertyType.IsArray || propertyType.Namespace != "System")
                {
                    var result = ReverseObfuscateObject(property.GetValue(thisObject));
                    property.SetValue(thisObject, result);
                }
                if (elementsToReverseObfuscate.Contains(property.Name))
                {
                    // Reverse Obfuscate
                    var result = ReverseObfuscate(property.GetValue(thisObject)?.ToString());
                    property.SetValue(thisObject, result);
                }
            }
        }

        public static string Obfuscate(string text, Secretkey secretkey)
        {
            if (text == fmgapi_undefined || text == null)
            {
                return text;
            }

            SetSecret(secretkey);

            var encrypted = encrypt(text);
            var utf8Encoded = System.Text.Encoding.UTF8.GetBytes(encrypted);
            Array.Reverse(utf8Encoded);
            var bigInteger = new BigInteger(utf8Encoded);
            var base36Encoded = Base36.Encode(bigInteger);

            return base36Encoded;
        }

        internal static string TryReverseObfuscatePublicId(string Public_ID, OktaGroup chosenOktagroup)
        {
            try
            {
                if (chosenOktagroup == OktaGroup.prod)
                {
                    return Obfuscation.ReverseObfuscate(Public_ID, Secretkey.p);
                }
                else
                {
                    
                    var contactId = Obfuscation.ReverseObfuscate(Public_ID, Secretkey.test)?.Trim();
                    if (!contactIdRegex.IsMatch(contactId))
                    {
                        contactId = Obfuscation.ReverseObfuscate(Public_ID, Secretkey.pp)?.Trim();
                        if (!contactIdRegex.IsMatch(contactId))
                        {
                            return Public_ID;
                        }
                    }
                    return contactId;
                }
            }
            catch (Exception)
            {
                // try again with preprod secretKey
                try
                {
                    var contactId = Obfuscation.ReverseObfuscate(Public_ID, Secretkey.pp)?.Trim();
                    if (!contactIdRegex.IsMatch(contactId))
                    {
                        return Public_ID;
                    }
                    return contactId;
                }
                catch (Exception ex)
                {
                    // just return obfuscated
                    return Public_ID;
                }
            }
        }

        internal static string ReverseObfuscate(string obfuscatedText, Secretkey secretkey = Secretkey.test)
        {
            if (obfuscatedText == fmgapi_undefined || obfuscatedText == null)
            {
                return obfuscatedText;
            }

            SetSecret(secretkey);

            var base36Decoded = Base36.Decode(obfuscatedText).ToByteArray();
            Array.Reverse(base36Decoded);
            var base36DecodedString = System.Text.Encoding.UTF8.GetString(base36Decoded);
            var decrypted = decrypt(base36DecodedString);

            return decrypted;
        }

        private static void SetSecret(Secretkey secretkey)
        {
            switch(secretkey)
            {
                case Secretkey.test : Secret = Form1.secrets.secretKeyValue.test; break;
                case Secretkey.pp : Secret = Form1.secrets.secretKeyValue.preProd; break;
                case Secretkey.p : Secret = Form1.secrets.secretKeyValue.prod; break;
                default: Secret = Form1.secrets.secretKeyValue.test; break;
            }
        }

        public static Secretkey GetSecretKey(string boomi)
        {
            switch (boomi)
            {
                case "prod":
                    return Secretkey.p;                    
                case "preprod":
                    return Secretkey.pp;                    
                default:
                    return Secretkey.test;                    
            }
        }

        private static AesManaged CreateAes()
        {
            var secret = Secret;
            for (var i = 0; i < 4; i++)
            {
                secret += secret;

            }
            var secretKey = secret.Substring(0, 16);

            var aes = new AesManaged();
            aes.Key = System.Text.Encoding.UTF8.GetBytes(secretKey);
            aes.IV = System.Text.Encoding.UTF8.GetBytes(secretKey);
            return aes;
        }

        
        public static string encrypt(string text)
        {
            using (AesManaged aes = CreateAes())
            {
                ICryptoTransform encryptor = aes.CreateEncryptor();
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                            sw.Write(text);
                        return Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
        }
        public static string decrypt(string text)
        {
            using (var aes = CreateAes())
            {
                ICryptoTransform decryptor = aes.CreateDecryptor();
                using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(text)))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader reader = new StreamReader(cs))
                        {
                            return reader.ReadToEnd();
                        }
                    }
                }

            }
        }
        public static class Base36
        {
            private const string Digits = "0123456789abcdefghijklmnopqrstuvwxyz";

            public static BigInteger Decode(string value)
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Empty value.");
                bool negative = false;
                if (value[0] == '-')
                {
                    negative = true;
                    value = value.Substring(1, value.Length - 1);
                }
                if (value.Any(c => !Digits.Contains(c)))
                    throw new ArgumentException("Invalid value: \"" + value + "\".");
                BigInteger decoded = 0L;
                for (var i = 0; i < value.Length; ++i)
                    decoded += Digits.IndexOf(value[i]) * BigInteger.Pow(Digits.Length, value.Length - i - 1);
                return negative ? decoded * -1 : decoded;

            }

            public static string Encode(BigInteger value)
            {
                if (value == long.MinValue)
                {
                    //hard coded value due to error when getting absolute value below: "Negating the minimum value of a twos complement number is invalid.".
                    return "-1Y2P0IJ32E8E8";
                }
                bool negative = value < 0;
                value = BigInteger.Abs(value);
                string encoded = string.Empty;
                do
                    encoded = Digits[(int)(value % Digits.Length)] + encoded;
                while ((value /= Digits.Length) != 0);
                return negative ? "-" + encoded : encoded;
            }            
        }

        public static class Base64
        {
            public static string Encode(string text)
            {
                var bytes = System.Text.Encoding.UTF8.GetBytes(text);
                return Convert.ToBase64String(bytes);
            }
            public static string Decode(string text)
            {
                var bytes = Convert.FromBase64String(text);
                return System.Text.Encoding.UTF8.GetString(bytes);
            }
        }
    }
}
