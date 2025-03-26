namespace EventRegistration.Security
{
    using Microsoft.AspNetCore.DataProtection;
    public class EventProtector
    {
        private readonly IDataProtector dataProtector;
        public EventProtector(IDataProtector dataProtector)
        {
            this.dataProtector = dataProtector;
        }
        public string Encrypt(int id)
        {
            return dataProtector.Protect(id.ToString());
        }

        public int Decrypt(string id)
        {
            var decryptedString = dataProtector.Unprotect(id);
            return int.Parse(decryptedString);
        }
    }
}
