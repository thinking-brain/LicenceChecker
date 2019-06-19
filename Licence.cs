using System;

namespace LicenceChecker
{
    [Serializable]
    public class Licence
    {
        public string Suscriptor { get; set; }

        public string Application { get; set; }

        public DateTime ExpirationDate { get; set; }

        public byte[] LicenceHash { get; set; }

        public string GetKey
        {
            get
            {
                return $"S:{Suscriptor} A:{Application} Fv:{ExpirationDate.ToShortDateString()}";
            }
        }
    }
}