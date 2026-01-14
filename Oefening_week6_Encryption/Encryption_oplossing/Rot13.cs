using Encryption_oplossing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption
{
    /// <summary>
    /// Provides functionality to encrypt and decrypt text using ROT13 cipher.
    /// </summary>
    public class Rot13 : IEncryption
    {
        // Name of the algorithm
        public static string Name => "ROT13";

        // Dictionary for ROT13 character mapping
        private static readonly Dictionary<char, char> rot13Map = new Dictionary<char, char>
        {
            {'a', 'n'}, {'b', 'o'}, {'c', 'p'}, {'d', 'q'}, {'e', 'r'}, {'f', 's'}, {'g', 't'},
            {'h', 'u'}, {'i', 'v'}, {'j', 'w'}, {'k', 'x'}, {'l', 'y'}, {'m', 'z'},
            {'n', 'a'}, {'o', 'b'}, {'p', 'c'}, {'q', 'd'}, {'r', 'e'}, {'s', 'f'}, {'t', 'g'},
            {'u', 'h'}, {'v', 'i'}, {'w', 'j'}, {'x', 'k'}, {'y', 'l'}, {'z', 'm'}
        };

        /// <summary>
        /// Decrypts the specified cyphertext
        /// </summary>
        /// <param name="input">The string to decrypt.</param>
        /// <returns>The decrypted string.</returns>
        public string Decrypt(string input)
        {
            return RotateString(input);
        }

        /// <summary>
        /// Encrypts the specified plaintext
        /// </summary>
        /// <param name="input"> text to encrypt</param>
        /// <returns>The encrypted text </returns>
        public string Encrypt(string input)
        {
            return RotateString(input);
        }

        // Applies ROT13 transformation to the input string
        private string RotateString(string input)
        {
            string result = "";
            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];
                char lowerC = char.ToLower(c);              
                if (rot13Map.ContainsKey(lowerC))
                {
                    char rotated = rot13Map[lowerC];
                    result += rotated;
                }
                else
                {
                    result += c;
                }
            }
            return result;
        }
    }
}
