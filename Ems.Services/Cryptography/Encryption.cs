using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Ems.Service.Cryptography
{
	public class Encryption
	{
		/// <summary>
		/// Only use this function if you are going to decrypt the data later.
		/// </summary>
		public static string Encrypt(string plaintext)
		{
			return Encrypt(plaintext, Config.Salt);
		}
		/// <summary>
		/// Only use this function if you are going to decrypt the data later.
		/// </summary>
		public static string Encrypt(string plaintext, string key)
		{
			var initvectorBytes = Encoding.ASCII.GetBytes(Config.InitVector);
			var saltvalueBytes = Encoding.ASCII.GetBytes(key);
			var data = Encoding.UTF8.GetBytes(plaintext);

			var password = new PasswordDeriveBytes(Config.Passphrase, saltvalueBytes, Config.HashAlgorithm, Config.PasswordIterations);
			var keyBytes = password.GetBytes(Config.KeySize / 8);

			var encryptor = new RijndaelManaged { Mode = CipherMode.CBC }.CreateEncryptor(keyBytes, initvectorBytes);

			var stream = new MemoryStream();
			var cryptostream = new CryptoStream(stream, encryptor, CryptoStreamMode.Write);

			cryptostream.Write(data, 0, data.Length);
			cryptostream.FlushFinalBlock();

			var cipher = Convert.ToBase64String(stream.ToArray());

			stream.Close();
			cryptostream.Close();
            
			return cipher;
		}

		public static string Decrypt(string cipher)
		{
			return Decrypt(cipher, Config.Salt);
		}

		public static string Decrypt(string cipher, string key)
		{
            

			var initvectorBytes = Encoding.ASCII.GetBytes(Config.InitVector);
			var saltvalueBytes = Encoding.ASCII.GetBytes(key);
			var data = Convert.FromBase64String(cipher);

			var password = new PasswordDeriveBytes(Config.Passphrase, saltvalueBytes, Config.HashAlgorithm, Config.PasswordIterations);
			var keyBytes = password.GetBytes(Config.KeySize / 8);

			var decryptor = new RijndaelManaged { Mode = CipherMode.CBC }.CreateDecryptor(keyBytes, initvectorBytes);

			var stream = new MemoryStream(data);
			var cryptostream = new CryptoStream(stream, decryptor, CryptoStreamMode.Read);

			var plaintextbytes = new byte[data.Length];
			int bytecount = cryptostream.Read(plaintextbytes, 0, plaintextbytes.Length);

			stream.Close();
			cryptostream.Close();

			return Encoding.UTF8.GetString(plaintextbytes, 0, bytecount);
		}
	}
}

