using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Okta.Sdk;
using OktaTools.PC_DevInt;

namespace OktaTools
{
    public class OktaController
    {
        private static HttpClient httpClient;
        private static OktaController oktaController = new OktaController();
        private static Secretkey secretkey;
        private const string OktaProfileSwitcher = @" (OktaProfileSwitcher)";
        private const string testPassword = @"Optimus234!";

        public OktaController()
        {
            var uri = new Uri($@"http://dev0079:8080");
            var credentialsCache = new CredentialCache { { uri, "NTLM", CredentialCache.DefaultNetworkCredentials } };
            var handler = new HttpClientHandler { Credentials = credentialsCache };
            httpClient = new HttpClient(handler) { BaseAddress = uri, Timeout = TimeSpan.FromMinutes(10) };
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        }
        internal async Task<IUser> GetUserByOktaIDorLogin(string oktaIDorLogin)
        {
            try
            {
                var client = Okta_Client.Get();
                var user = await client.Users.GetUserAsync(oktaIDorLogin);
                return user;
            }
            catch (Exception)
            {
                return null;
            }

        }
        internal async Task<List<IUser>> GetUsersByContactID(string contactId, Secretkey secretkey)
        {
            var publicId = Obfuscation.Obfuscate(contactId, secretkey);
            var client = Okta_Client.Get();
            var search = $@"profile.Public_ID eq ""{publicId}""";
            var users = await client.Users.ListUsers(search:search).ToListAsync();                
            return users;
        }
        internal async Task<List<IUser>> GetUsersByPublicID(string publicId)
        {
            var client = Okta_Client.Get();
            var search = $@"profile.Public_ID eq ""{publicId}""";
            var users = await client.Users.ListUsers(search: search).ToListAsync();
            return users;
        }
        internal async Task<List<IUser>> GetUsersByGroupId(string oktaGroupId)
        {
            var client = Okta_Client.Get();
            var group = await client.Groups.GetGroupAsync(oktaGroupId);
            return await group.Users.Where(u => 
                !u.Profile.Login.EndsWith("@alphero.com") 
                && !u.Profile.LastName.EndsWith(OktaProfileSwitcher) 
                && !u.Profile.Login.Contains("@automation")  
                && (u.Profile["Public_ID"]?.ToString() ?? "") != ""
                && (u.Profile["Search_email"]?.ToString() ?? "") != ""
                    ).Take(10).ToListAsync();
        }

        internal async Task DeleteUserByOktaIDorLogin(string item, OktaUsersList oktaUsersList, bool updateList = true)
        {
            try
            {
                var user = await oktaController.GetUserByOktaIDorLogin(item);
                if (user != null)
                {
                    var isDeleted = await DeleteUser(user);
                    if (isDeleted && updateList)
                    {
                        //RemoveUserFromList(user, oktaUsersList);
                        AddUserToList(user, oktaUsersList);
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError($@"time: {DateTime.Now:s} Delete error: {ex.Message}");
            }
        }
        internal async Task DeleteUserByPublicID(string publicId, OktaUsersList oktaUsersList, bool updateList = true)
        {
            try
            {
                var users = await oktaController.GetUsersByPublicID(publicId);

                foreach (var user in users)
                {
                    var isDeleted = await DeleteUser(user);
                    if (isDeleted && updateList)
                    {
                        //RemoveUserFromList(user, oktaUsersList);
                        AddUserToList(user, oktaUsersList);
                    }
                }
            }

            catch (Exception ex)
            {
                Trace.TraceError($@"time: {DateTime.Now:s} Delete error: {ex.Message}");
            }
        }


        internal async Task DeleteUserByContactId(string contactId, OktaUsersList oktaUsersList, Secretkey secretkey, bool userFromList, bool updateList = true)
        {
            try
            {
                List<IUser> users = new List<IUser>();
                if (userFromList)
                {
                    var publicId = Obfuscation.Obfuscate(contactId, secretkey);
                    users = GetUsers(oktaUsersList, publicId);
                }
                else
                {
                    users = await GetUsers(contactId, secretkey);
                }

                foreach (var user in users)
                {
                    var isDeleted = await DeleteUser(user);
                    if (isDeleted && updateList)
                    {
                        //RemoveUserFromList(user, oktaUsersList);
                        AddUserToList(user, oktaUsersList);
                    }
                }
            }

            catch (Exception ex)
            {
                Trace.TraceError($@"time: {DateTime.Now:s} Delete error: {ex.Message}");
            }
        }

        private async Task<List<IUser>> GetUsers(string contactId, Secretkey secretkey)
        {
            var users = await oktaController.GetUsersByContactID(contactId, secretkey);
            return users.ToList();
        }

        private async Task<bool> DeleteUser(IUser user)
        {
            if (user.Profile.Login.EndsWith("@alphero.com")) return false;
            if (user.Profile.LastName.EndsWith(OktaProfileSwitcher)) return false;
            if (user.Profile.Login.Contains(@"automation")) return false;
            if ((user.Profile["Public_ID"]?.ToString() ?? "") == "") return false;

            try
            {

                if (user.Status != "DEPROVISIONED")
                {
                    await user.DeactivateAsync();
                }
                await user.DeactivateOrDeleteAsync();

                return true;
            }
            catch (Exception ex)
            {
                Trace.TraceError($@"time: {DateTime.Now:s} Delete error: {ex.Message}");
                return false;
            }

        }

        private void RemoveUserFromList(IUser user, OktaUsersList oktaUsersList)
        {
            switch (oktaUsersList)
            {
                case OktaUsersList.FormImportOktaUsers:
                    FormImportOktaUsers.OktaUsers.Remove(user);
                    break;

                case OktaUsersList.FormDeleteOktaUserByID:
                    FormDeleteOktaUserByID.OktaUsers.Remove(user);
                    break;

                default: break;
            }
        }
        private void AddUserToList(IUser user, OktaUsersList oktaUsersList)
        {
            switch (oktaUsersList)
            {
                case OktaUsersList.FormDeleteOktaUserByID:
                    FormDeleteOktaUserByID.DeletedOktaUsers.Add(user);
                    break;

                case OktaUsersList.FormImportOktaUsers:
                    FormImportOktaUsers.ImportedOktaUsers.Add(user);
                    break;

                case OktaUsersList.FormResyncOktaUsers:
                    FormResyncOktaUsers.ResyncedOktaUsers.Add(user);
                    break;

                default: break;
            }
        }


        internal async Task<List<IUser>> DeleteUserByEnvironment(OktaUsersList oktaUsersList)
        {
            var deletedOktaUsers = new List<IUser>();
            var oktaUsersToDelete = GetOktaUsersToDelete(oktaUsersList);

            foreach (var user in oktaUsersToDelete)
            {
                await DeleteUser(user);
                deletedOktaUsers.Add(user);
            }

            return deletedOktaUsers;
        }

        internal async Task<string> ResyncOktaProfile(ContactAccountDetail contactAccount, string boomi)
        {
            secretkey = Obfuscation.GetSecretKey(boomi);
            
            var oktaProfile = new OktaProfile(contactAccount, boomi, null, secretkey);
            var client = Okta_Client.Get();

            var payload = new UpdateOktaUserProfile(oktaProfile);
            
            var contactId = contactAccount.ContactID;
            var users = await oktaController.GetUsersByContactID(contactId, secretkey);

            var user = users.FirstOrDefault();
            var oktaId = user.Id;
            payload.profile.login = user.Profile.Login;
            payload.profile.email = user.Profile.Email;
            
            //if (users.Count() > 1)
            //{
            //    user = FindUserWithMatchingPrefix(users, newLogin);
            //}

            var updateOktaUser = new Okta.Sdk.HttpRequest
            {
                Uri = $@"api/v1/users/{oktaId}",                
                Payload = payload
            };

            try
            {
                user = await client.PutAsync<User>(updateOktaUser);
                if (user != null)
                {
                    var login = user.Profile.Login;
                    Trace.TraceInformation($@"time: {DateTime.Now:s} login: {login}");
                    return user.Id;
                }
                return null;
            }

            catch (Exception ex)
            {
                Trace.TraceError($@"time: {DateTime.Now:s} contactId: {contactAccount.ContactID} error: {ex.Message}");
                return null;
            }
        }

        internal async Task ResyncUserByContactId(string contactId, OktaUsersList oktaUsersList, Secretkey secretkey, bool updateList)
        {
            try
            {
                var users = await GetUsers(contactId, secretkey);
                foreach (var user in users)
                {
                    var isDeleted = await DeleteUser(user);
                    var isResynced = await ResyncUser(user);
                    if (isResynced && updateList)
                    {
                        AddUserToList(user, oktaUsersList);
                    }
                }
            }

            catch (Exception ex)
            {
                Trace.TraceError($@"time: {DateTime.Now:s} Delete error: {ex.Message}");
            }
        }

        private async Task<bool> ResyncUser(IUser user)
        {
            if (user.Profile.Login.EndsWith("@alphero.com")) return false;
            if (user.Profile.LastName.EndsWith(OktaProfileSwitcher)) return false;
            if (user.Profile.Login.Contains(@"automation")) return false;
            if ((user.Profile["Public_ID"]?.ToString() ?? "") == "") return false;

            try
            {

                if (user.Status != "DEPROVISIONED")
                {
                    await user.DeactivateAsync();
                }
                await user.DeactivateOrDeleteAsync();

                return true;
            }
            catch (Exception ex)
            {
                Trace.TraceError($@"time: {DateTime.Now:s} Delete error: {ex.Message}");
                return false;
            }

        }

        private List<IUser> GetOktaUsersToDelete(OktaUsersList oktaUsersList)
        {
            switch (oktaUsersList)
            {
                case OktaUsersList.FormDeleteOktaUsers:
                    return FormDeleteOktaUsers.OktaUsersToDelete;
                case OktaUsersList.FormDeleteOktaUsersAllUnlinked:
                    return FormDeleteOktaUsersAllUnlinked.OktaUsersToDelete;

                default: return null;
            }
        }

        private List<IUser> GetUsers(OktaUsersList oktaUsersList, string publicId)
        {
            switch(oktaUsersList)
            {
                case OktaUsersList.FormImportOktaUsers:
                    return FormImportOktaUsers.OktaUsers.Where(u => u.Profile["Public_ID"]?.ToString() == publicId).ToList();
                case OktaUsersList.FormDeleteOktaUserByID:
                    return FormDeleteOktaUserByID.OktaUsers.Where(u => u.Profile["Public_ID"]?.ToString() == publicId).ToList();
                case OktaUsersList.FormDeleteOktaUsers:
                    return FormDeleteOktaUsers.OktaUsers.Where(u => u.Profile["Public_ID"]?.ToString() == publicId).ToList();
                case OktaUsersList.FormUpdateOktaUserLogin:
                    return FormUpdateOktaUserLogin.OktaUsers.Where(u => u.Profile["Public_ID"]?.ToString() == publicId).ToList();
                default: return null;
            }
        }


        internal async Task<bool> UpdateUserLogin(string contactId, string newLogin, Secretkey secretkey)
        {
            var users = await GetUsers(contactId, secretkey);

            var user = users.FirstOrDefault();

            if (users.Count() > 1)
            {
                user = FindUserWithMatchingPrefix(users, newLogin);                
            }

            if (user != null)
            {
                user.Profile.Login = newLogin;
                user.Profile.Email = newLogin;
                await user.UpdateAsync();
                return true;
            }
            return false;
        }

        private IUser FindUserWithMatchingPrefix(List<IUser> users, string newLogin)
        {
            var testEnvPrefixed = Regex.IsMatch(newLogin, @"^(test[abcdef]\.)");
            if (testEnvPrefixed)
            {
                var prefix = newLogin.Substring(0, 6);
                return users.FirstOrDefault(u => u.Profile.Login.StartsWith(prefix) 
                    && !u.Profile.LastName.EndsWith(OktaProfileSwitcher)
                    && !u.Profile.Login.EndsWith("@alphero.com")
                );

            }
            return null;
        }
        internal async Task<string> CreateOktaProfile(string contactId, string boomi)
        {
            try
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();

                var jsonString = $@"{{ContactId:'{contactId}'}}";
                var jsonObject = JObject.Parse(jsonString);
                var content = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json");
                var url = $@"{boomi}/Okta/CreateUser";
                var result = await httpClient.PostAsync(url, content);
                var resultString = await result.Content.ReadAsStringAsync();
                dynamic createdUser = JsonConvert.DeserializeObject(resultString);
                var login = createdUser.profile.login.Value;
                stopwatch.Stop();
                var elapsed = stopwatch.ElapsedMilliseconds;

                Trace.TraceInformation($@"time: {DateTime.Now:s} login: {login} elapsed: {elapsed}");
                return login;

            }
            catch (Exception ex)
            {
                Trace.TraceError($@"time: {DateTime.Now:s} contactId: {contactId} error: {ex.Message}");
                return null;
            }

        }
        internal async Task<string> CreateOktaProfile(ContactAccountDetail contactAccount, string boomi, string chosenSecondaryEmail)
        {
            secretkey = Obfuscation.GetSecretKey(boomi);
            var oktaProfile = new OktaProfile(contactAccount, boomi, chosenSecondaryEmail, secretkey);
            var client = Okta_Client.Get();

            var payload = new CreateOktaUserProfile(oktaProfile);

            var createOktaUser = new Okta.Sdk.HttpRequest
            {
                Uri = $@"api/v1/users/?activate=false",
                Payload = payload
            };

            try
            {
                var user = await client.PostAsync<User>(createOktaUser);
                if (user != null)
                {
                    var answer = string.IsNullOrEmpty(oktaProfile.PrimaryAccountNumber)
                        ? oktaProfile.AccountNumbers.FirstOrDefault().ToString()
                        : oktaProfile.PrimaryAccountNumber;

                    await user.AddFactorAsync(new AddSecurityQuestionFactorOptions
                    {
                        Question = @"favorite_security_question",
                        Answer = answer
                    });

                    var login = user.Profile.Login;
                    Trace.TraceInformation($@"time: {DateTime.Now:s} login: {login}");
                    return user.Id;
                }
                return null;                
            }

            catch (Exception ex)
            {
                Trace.TraceError($@"time: {DateTime.Now:s} contactId: {contactAccount.ContactID} error: {ex.Message}");
                return null;
            }
        }

        internal async Task<string> SetOktaProfileSwitcher(ContactAccountDetail contactAccount, string boomi, string login, string firstname, string lastname, string primaryEmail, string secondaryEmail)
        {
            var user = await oktaController.GetUserByOktaIDorLogin(login);
            if (user == null)
            {
                return await CreateOktaProfileSwitcher(contactAccount, boomi, login, firstname, lastname, primaryEmail, secondaryEmail);
            }
            else
            {
                return await UpdateOktaProfileSwitcher(user, contactAccount, boomi, login, firstname, lastname, primaryEmail, secondaryEmail);
            }
        }

        private async Task<string> CreateOktaProfileSwitcher(ContactAccountDetail contactAccount, string boomi, string login, string firstname, string lastname, string primaryEmail, string secondaryEmail)
        {
            secretkey = Obfuscation.GetSecretKey(boomi);
            var oktaProfile = new OktaProfile(contactAccount, boomi, secondaryEmail, secretkey);
            var client = Okta_Client.Get();
            
            var payload = new CreateOktaUserProfile(oktaProfile, testPassword);
            payload.profile.confirmDetails = false;
            payload.profile.login = login;
            payload.profile.email = string.IsNullOrEmpty(primaryEmail) ? login : primaryEmail;
            
            if (!string.IsNullOrEmpty(secondaryEmail))
            {
                payload.profile.secondEmail = secondaryEmail;
            }
            if (!string.IsNullOrEmpty(firstname))
            {
                payload.profile.firstName = firstname;
            }
            if (!string.IsNullOrEmpty(lastname))
            {
                payload.profile.lastName = lastname;
            }
            if (!payload.profile.lastName.EndsWith(OktaProfileSwitcher))
            {
                payload.profile.lastName += OktaProfileSwitcher;
            }

            var createOktaUser = new Okta.Sdk.HttpRequest
            {
                Uri = $@"api/v1/users/?activate=true",
                Payload = payload
            };

            try
            {
                var user = await client.PostAsync<User>(createOktaUser);
                if (user != null)
                {
                    var answer = string.IsNullOrEmpty(oktaProfile.PrimaryAccountNumber)
                        ? oktaProfile.AccountNumbers.FirstOrDefault().ToString()
                        : oktaProfile.PrimaryAccountNumber;

                    await user.AddFactorAsync(new AddSecurityQuestionFactorOptions
                    {
                        Question = @"favorite_security_question",
                        Answer = answer
                    }); 
                    
                    login = user.Profile.Login;
                    Trace.TraceInformation($@"time: {DateTime.Now:s} login: {login}");
                    return user.Id;
                }
                return null;
            }

            catch (Exception ex)
            {
                Trace.TraceError($@"time: {DateTime.Now:s} contactId: {contactAccount.ContactID} error: {ex.Message}");
                return null;
            }
        }

        private async Task<string> UpdateOktaProfileSwitcher(IUser user, ContactAccountDetail contactAccount, string boomi, string login, string firstname, string lastname, string primaryEmail, string secondaryEmail)
        {
            var client = Okta_Client.Get();
            secretkey = Obfuscation.GetSecretKey(boomi);

            var oktaProfile = new OktaProfile(contactAccount, boomi, secondaryEmail, secretkey);            
            var payload = new UpdateOktaUserProfile(oktaProfile);
            payload.profile.confirmDetails = false;
            payload.profile.login = login;
            payload.profile.email = user.Profile.Email;
            payload.profile.secondEmail = user.Profile.SecondEmail;
            payload.profile.firstName = user.Profile.FirstName;
            payload.profile.lastName = user.Profile.LastName;

            if (!string.IsNullOrEmpty(primaryEmail))
            {
                payload.profile.email = primaryEmail;
            }
            if (!string.IsNullOrEmpty(secondaryEmail))
            {
                payload.profile.secondEmail = secondaryEmail;
            }
            if (!string.IsNullOrEmpty(firstname))
            {
                payload.profile.firstName = firstname;
            }
            if (!string.IsNullOrEmpty(lastname))
            {
                payload.profile.lastName = lastname;
            }
            if (!payload.profile.lastName.EndsWith(OktaProfileSwitcher))
            {
                payload.profile.lastName += OktaProfileSwitcher;
            }

            var oktaId = user.Id;
            var updateOktaUser = new Okta.Sdk.HttpRequest
            {
                Uri = $@"api/v1/users/{oktaId}",
                Payload = payload
            };

            try
            {
                user = await client.PutAsync<User>(updateOktaUser);
                if (user != null)
                {
                    // code block only needed if changing password
                    //var changePasswordOptions = new ChangePasswordOptions();
                    //changePasswordOptions.CurrentPassword = @"Optimus123!";
                    //changePasswordOptions.NewPassword = @"Optimus234!";
                    //await user.ChangePasswordAsync(changePasswordOptions);

                    login = user.Profile.Login;
                    Trace.TraceInformation($@"time: {DateTime.Now:s} login: {login}");
                    return user.Id;
                }
                return null;
            }

            catch (Exception ex)
            {
                Trace.TraceError($@"time: {DateTime.Now:s} contactId: {contactAccount.ContactID} error: {ex.Message}");
                return null;
            }
        }
    }
}
