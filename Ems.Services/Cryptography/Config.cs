using System;
using System.Configuration;

namespace Ems.Service.Cryptography
{
	public class Config
	{
		public static string ClientId
		{
			get
			{
				return Read("Ems.Service.Cryptography.clientid", "635151941061126793");
			}
		}

		public static string ClientPassword
		{
			get
			{
				return Read("Ems.Service.Cryptography.clientpass", "L7IMTpeg6rA1FjDIEh6QH9fUw5t5j4AzQLoaNtK4H");
			}
		}

		public static string Salt
		{
			get
			{
				return Read("Ems.Service.Cryptography.salt", "4d563039b9e440b5a656d915099f3aaf");
			}
		}

		public static string Spice
		{
			get
			{
				return Read("Ems.Service.Cryptography.spice", "E6*-vC/RO");
			}
		}

		public static string Passphrase
		{
			get
			{
				return Read("Ems.Service.Cryptography.passphrase", "99ec6d47462344809d8258218910bd75");
			}
		}

		public static string HashAlgorithm
		{
			get
			{
				return Read("Ems.Service.Cryptography.hashalgorithm", "SHA1");
			}
		}

		public static int PasswordIterations
		{
			get
			{
				return Read("Ems.Service.Cryptography.passworditerations", 91);
			}
		}

		public static string InitVector
		{
			get
			{
				// must be exactly 16 bytes
				return Read("Ems.Service.Cryptography.initvector", "mkPfnNZJja0tB6+c");
			}
		}

		public static int KeySize
		{
			get
			{
				// 128 or 192 or 256
				return Read("Ems.Service.Cryptography.keysize", 256);
			}
		}

		public static int WSCallValidity
		{
			get
			{
				return Read("Ems.Service.Cryptography.wscallvalidity", 6);
			}
		}

		// awesome read
		private static T Read<T>(string key, T @default = default(T))
		{
			var temp = string.Empty;//ConfigurationManager.AppSettings[key];
			return !string.IsNullOrWhiteSpace(temp) ? (T)Convert.ChangeType(temp, typeof(T)) : @default;
		}
	}
}

