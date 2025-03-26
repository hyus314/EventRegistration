using Microsoft.Extensions.Configuration;

namespace EventRegistration.Common.Utilities
{
    public class EmailApiKeysRetriever
    {
        private readonly IConfiguration configuration;
        private string publicKey;
        private string privateKey;
        public EmailApiKeysRetriever(IConfiguration configuration)
        {
            this.configuration = configuration;
            publicKey = this.configuration.GetSection("MailJetAPIKeys")["PublicKey"]!;
            privateKey = this.configuration.GetSection("MailJetAPIKeys")["SecretKey"]!;

        }
        public  string GetPublicKey()
        {
            return publicKey;
        }

        public  string GetPrivateKey()
        {
            return privateKey;
        }

    }
}
