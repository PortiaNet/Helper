namespace PortiaNet.Helper.SecurityHelper
{
    public class EncryptDecryptException : Exception
    {
        public EncryptDecryptException()
        {

        }

        public EncryptDecryptException(string message) : base(message)
        {
        }

        public EncryptDecryptException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
