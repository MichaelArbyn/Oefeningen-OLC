using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption_oplossing
{
    /// <summary>
    /// Provides functionality to encrypt and decrypt text
    /// </summary>
    internal interface IEncryption
    {
        /// <summary>
        /// Name of the algorithm
        /// </summary>
        static string Name => "Encryption Algorithm";

        /// <summary>
        /// Decrypts the specified cyphertext
        /// </summary>
        /// <param name="input">The string to decrypt.</param>
        /// <returns>The decrypted string.</returns>
        string Encrypt(string input);

        /// <summary>
        /// Encrypts the specified plaintext
        /// </summary>
        /// <param name="input"> text to encrypt</param>
        /// <returns>The encrypted text </returns>
        string Decrypt(string input);

    }
}
