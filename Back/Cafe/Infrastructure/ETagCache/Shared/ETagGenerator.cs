
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Cafe.Infrastructure.ETagCache.Shared
{
	public class ETagGenerator
	{
		public string GenerateRandom()
		{
			//string length 44
			Span<byte> bytes = new(new byte[20]);
			RandomNumberGenerator.Fill(bytes);
			return $"\"{WebEncoders.Base64UrlEncode(bytes)}\"";
		}
		public string Generate(byte[] targetData)
		{
			byte[] bytes = SHA1.Create().ComputeHash(targetData);
			string encodedNewStateHash = $"\"{WebEncoders.Base64UrlEncode(bytes)}\"";

			return encodedNewStateHash;
		}
		public string Generate<T>(T targetData)
		{
			byte[] bytesTargetData = Encoding.ASCII.GetBytes(JsonSerializer.Serialize(
				targetData));

			return Generate(bytesTargetData);
		}
	}
}
