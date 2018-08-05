using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Ems.Service.Cryptography
{
	public static class Hashbrowns
	{
		private const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz";

		/// <summary>
		/// Use this function for hashing password.
		/// </summary>

		public static string Hash(string plaintext, string salt = null)
		{
			return Hashbrowns.Hash<SHA256Managed>(plaintext, salt);
		}

		private static string Hash<TAlgorithm>(string plaintext, string salt = null) where TAlgorithm : HashAlgorithm, new()
		{
			var algorithm = Activator.CreateInstance<TAlgorithm>();

			if (string.IsNullOrWhiteSpace(salt))
			{
				salt = Config.Salt;
			}

			var data = Encoding.UTF8.GetBytes(plaintext + salt);
			return Convert.ToBase64String(algorithm.ComputeHash(data));
		}

		public static string RandomString(int length)
		{
			var random = new Random();
			return new string(
				Enumerable
				.Repeat(Hashbrowns.chars, length)
				.Select(o => o[random.Next(o.Length)])
				.ToArray()
			);
		}
	}
}

