namespace UsersApp.Security
{

    ///<summary>
    ///This class contains methods that perform 
    ///encryption and decryption logic.
    ///</summary>>

    public static class EncryptionUtil
	{
		/// <summary>
		/// Encrypts a password when a new 
		/// gamemaster is created.
		/// </summary>
		/// <param name="clearText">(string)The password to be encrypted</param>
		/// <returns>(string) the encrypted password</returns>
		public static string  Encrypt (string clearText)
		{
			var encryptedPassword = BCrypt.Net.BCrypt.HashPassword(clearText);
			return encryptedPassword;
		}

		/// <summary>
		/// Checks whether a password of gamemaster is valid
		/// </summary>
		/// <param name="plainText">(string) the provided password</param>
		/// <param name="cipherText">(string) the encrypted saves password</param>
		/// <returns>(bool) True if valid false if not</returns>
		public static bool IsValidPassword(string plainText, string cipherText)
		{
			var isValid = BCrypt.Net.BCrypt.Verify(plainText, cipherText);

			return isValid;
		}
	}
}
