using Microsoft.AspNetCore.DataProtection;

namespace pdfviewer
{
    public class CipherService : ICipherService
    {
        private readonly IDataProtectionProvider _dataProtectionProvider;
        private const string Key = "my-very-long-key-of-no-exact-size";

        public CipherService(IDataProtectionProvider dataProtectionProvider)
        {
            _dataProtectionProvider = dataProtectionProvider;
        }

        public string Encrypt(string input)
        {
            //TODO
            return input;
        }

        public string Decrypt(string cipherText)
        {
            //TODO
            return cipherText;
        }
    }
}