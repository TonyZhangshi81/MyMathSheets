using System;
using System.Text;

namespace MyMathSheets.CommonLib.Util.Security
{
	/// <summary>
	/// 實現BASE64加密
	/// </summary>
	public sealed class Base64
	{
		/// <summary>
		/// Base64加密
		/// </summary>
		/// <param name="encode">加密採用的編碼方式</param>
		/// <param name="source">待加密的明文</param>
		/// <returns>加密后的字符串</returns>
		public static string EncodeBase64(Encoding encode, string source)
		{
			byte[] bytes = encode.GetBytes(source);
			try
			{
				return Convert.ToBase64String(bytes);
			}
			catch
			{
				return source;
			}
		}

		/// <summary>
		/// Base64加密，採用UTF8編碼方式加密
		/// </summary>
		/// <param name="source">待加密的明文</param>
		/// <returns>加密后的字符串</returns>
		public static string EncodeBase64(string source)
		{
			return EncodeBase64(Encoding.UTF8, source);
		}

		/// <summary>
		/// Base64加密，採用UTF8編碼方式加密
		/// </summary>
		/// <param name="bytes">待加密的二進制字符串</param>
		/// <returns>加密后的字符串</returns>
		public static string EncodeBase64(byte[] bytes)
		{
			return Convert.ToBase64String(bytes);
		}

		/// <summary>
		/// Base64解密
		/// </summary>
		/// <param name="encode">解密採用的解密方式，注意和加密時採用的編碼方式一致</param>
		/// <param name="result">待解密的密文</param>
		/// <returns>解密后的字符串</returns>
		public static string DecodeBase64(Encoding encode, string result)
		{
			byte[] bytes = DecodeBase64(result);
			try
			{
				return encode.GetString(bytes);
			}
			catch
			{
				return result;
			}
		}

		/// <summary>
		/// Base64解密，採用UTF8的編碼方式解密
		/// </summary>
		/// <param name="result">待解密的密文</param>
		/// <returns>解密后的字符串</returns>
		public static string DecodeBase64String(string result)
		{
			return DecodeBase64(Encoding.UTF8, result);
		}

		/// <summary>
		/// Base64解密，採用UTF8的編碼方式解密
		/// </summary>
		/// <param name="result">待解密的密文</param>
		/// <returns>解密后的二進制字符串</returns>
		public static byte[] DecodeBase64(string result)
		{
			return Convert.FromBase64String(result);
		}
	}
}