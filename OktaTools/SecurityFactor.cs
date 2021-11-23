namespace OktaTools
{
    public class SecurityFactor
    {
        public string factorType { get; set; }
        public string provider { get; set; }
        public SecurityFactorProfile profile { get; set; }

        public class SecurityFactorProfile
        {
            public string question { get; set; }
            public string answer { get; set; }
        }
    }
}
