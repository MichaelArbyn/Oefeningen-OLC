using Encryption_oplossing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption
{
    /// <summary>
    /// Provides functionality to encrypt and decrypt text by reversing the input string.
    /// </summary>
    public class Reverse :IEncryption
    {
        // Name of the algorithm
        public static string Name => "Achterstevooren";

        /// <summary>
        /// Decrypts the specified cyphertext
        /// </summary>
        /// <param name="input">The string to decrypt.</param>
        /// <returns>The decrypted string.</returns>
        public string Decrypt(string input)
        {
            return ReverseString(input);
        }
        /// <summary>
        /// Encrypts the specified plaintext
        /// </summary>
        /// <param name="input"> text to encrypt</param>
        /// <returns>The encrypted text </returns>
        public string Encrypt(string input)
        {
            return ReverseString(input);
        }

        // Reverses the input string
        private string ReverseString(string input)
        {
            string result = "";
            for(int i = input.Length-1; i>=0; i--)
            {
                char c = input[i];
                result += c;
            }
            return result;
        }
    }
}
