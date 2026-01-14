using System.Linq;

namespace Encryption
{
    /// <summary>
    /// Provides functionality to encrypt and decrypt text using the ROT13 substitution cipher.
    /// </summary>
    public class Rot13 : IEncryptionAlgorithm
    {
        public static string Name => "ROT13";

        /// <summary>
        /// Encrypts the specified plaintext using ROT13.
        /// </summary>
        /// <param name="input">The text to encrypt.</param>
        /// <returns>The encrypted text.</returns>
        public string Encrypt(string input)
        {
            return Transform(input);
        }

        /// <summary>
        /// Decrypts the specified cyphertext using ROT13.
        /// </summary>
        /// <param name="input">The text to decrypt.</param>
        /// <returns>The decrypted text.</returns>
        public string Decrypt(string input)
        {
            return Transform(input);
        }

        private string Transform(string input)
        {
            if (input == null) return string.Empty;

            char Shift(char c)
            {
                if (c >= 'a' && c <= 'z')
                    return (char)('a' + (c - 'a' + 13) % 26);

                if (c >= 'A' && c <= 'Z')
                    return (char)('A' + (c - 'A' + 13) % 26);

                return c;
            }

            return new string(input.Select(Shift).ToArray());
        }
    }
}