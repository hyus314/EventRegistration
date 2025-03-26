//using Microsoft.AspNetCore.DataProtection;

//namespace EventRegistration.Security
//{
//    public class WorkerProtector
//    {
//        private readonly IDataProtector dataProtector;
//        public WorkerProtector(IDataProtector dataProtector)
//        {
//            this.dataProtector = dataProtector;
//        }
//        public string Encrypt(int id)
//        {
//            return dataProtector.Protect(id.ToString());
//        }

//        public int Decrypt(string id)
//        {
//            var decryptedString = dataProtector.Unprotect(id);
//            return int.Parse(decryptedString);
//        }
//    }
//}
