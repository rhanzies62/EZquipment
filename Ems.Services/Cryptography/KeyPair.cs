using System.Security.Cryptography;

namespace Ems.Service.Cryptography
{

    public class KeyPair
    {
        public KeyPair() { }
        public string Public { get; set; }
        public string Private { get; set; }
    }
}
