using System.Windows;
using System.Windows.Controls;

namespace Encryption
{
    /// <summary>
    /// Main window for the encryption application.
    /// Provides a user interface for encrypting and decrypting text using various algorithms.
    /// </summary>
    public partial class MainWindow : Window
    {
        private IEncryptionAlgorithm algorithm;

        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// Sets up the encryption algorithms.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            algorithm = new Reverse(); // default
        }

        /// <summary>
        /// Handles the Encrypt button click event.
        /// Encrypts the input text using the selected algorithm and displays the result.
        /// </summary>
        /// <param name="sender">The button that triggered the event.</param>
        /// <param name="e">Event arguments.</param>
        private void btnEncrypt_Click(object sender, RoutedEventArgs e)
        {
            string input = txtInput.Text;
            txtOutput.Text = algorithm.Encrypt(input);
        }

        /// <summary>
        /// Handles the Decrypt button click event.
        /// Decrypts the input text using the selected algorithm and displays the result.
        /// </summary>
        /// <param name="sender">The button that triggered the event.</param>
        /// <param name="e">Event arguments.</param>
        private void btnDecrypt_Click(object sender, RoutedEventArgs e)
        {
            string cypher = txtOutput.Text;
            txtInput.Text = algorithm.Decrypt(cypher);
        }

        /// <summary>
        /// Handles the radio button selection changed event.
        /// Updates the UI or performs actions when a different encryption algorithm is selected.
        /// </summary>
        /// <param name="sender">The radio button that triggered the event.</param>
        /// <param name="e">Event arguments.</param>
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton radioButton)
            {
                if (radioButton.Name == "rbReverse")
                    algorithm = new Reverse();

                if (radioButton.Name == "rbName1")
                    algorithm = new Rot13();

                // rbName2 kan later een ander algoritme krijgen
            }
        }
    }
}