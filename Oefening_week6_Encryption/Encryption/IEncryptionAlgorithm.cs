using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption
{
    /// <summary>
    /// Interface voor encryptie-algoritmes.
    /// </summary>
    public interface IEncryptionAlgorithm
    {
        string Encrypt(string input);
        string Decrypt(string input);
    }
}