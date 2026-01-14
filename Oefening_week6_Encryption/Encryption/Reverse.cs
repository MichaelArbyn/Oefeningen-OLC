using System.Linq;

namespace Encryption
{
    /// <summary>
    /// Provides functionality to encrypt and decrypt text by reversing the input string.
    /// </summary>
    public class Reverse : IEncryptionAlgorithm
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
            if (input == null) return string.Empty;
            return new string(input.Reverse().ToArray());
        }
    }
}