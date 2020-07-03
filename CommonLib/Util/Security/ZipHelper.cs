using System;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace MyMathSheets.CommonLib.Util.Security
{
	/// <summary>
	/// 字符串壓縮工具類
	/// </summary>
	public sealed class ZipHelper
	{
		/// <summary>
		/// 將傳入字符串以GZip算法壓縮后，返回Base64編碼字符
		/// </summary>
		/// <param name="parameter">需要壓縮的字符串</param>
		/// <returns>壓縮后的Base64編碼的字符串</returns>
		public static string GZipCompressString(string parameter)
		{
			if (string.IsNullOrEmpty(parameter))
			{
				return string.Empty;
			}
			byte[] data = Encoding.UTF8.GetBytes(parameter);
			byte[] zippedData = Compress(data);
			return Base64.EncodeBase64(zippedData);
		}

		/// <summary>
		/// GZip壓縮
		/// </summary>
		/// <param name="data">未壓縮的字符串字符集</param>
		/// <returns>已壓縮的字符串字符集</returns>
		/// <exception cref="ArgumentNullException"><paramref name="data"/>為NULL的情況</exception>
		public static byte[] Compress(byte[] data)
		{
			Guard.ArgumentNotNull(data, "data");

			MemoryStream ms = new MemoryStream();
			GZipStream compressedzipStream = new GZipStream(ms, CompressionMode.Compress, true);

			compressedzipStream.Write(data, 0, data.Length);
			compressedzipStream.Close();
			return ms.ToArray();
		}

		/// <summary>
		/// 將加密壓縮后的字符串以GZip算法解壓縮
		/// </summary>
		/// <param name="zippedString">經GZip壓縮后的字符串</param>
		/// <returns>解壓后的字符串</returns>
		public static string GZipDecompressString(string zippedString)
		{
			if (string.IsNullOrEmpty(zippedString))
			{
				return string.Empty;
			}
			byte[] zippedData = Base64.DecodeBase64(zippedString);
			return Encoding.UTF8.GetString(Decompress(zippedData));
		}

		/// <summary>
		/// ZIP解壓
		/// </summary>
		/// <param name="zippedData">經GZip壓縮后的字符串字符集</param>
		/// <returns>解壓后的字符串</returns>
		public static byte[] Decompress(byte[] zippedData)
		{
			MemoryStream ms = new MemoryStream(zippedData);
			GZipStream compressedzipStream = new GZipStream(ms, CompressionMode.Decompress);
			MemoryStream outBuffer = new MemoryStream();
			byte[] block = new byte[1024];
			while (true)
			{
				int bytesRead = compressedzipStream.Read(block, 0, block.Length);
				if (bytesRead <= 0)
					break;
				else
					outBuffer.Write(block, 0, bytesRead);
			}
			compressedzipStream.Close();
			return outBuffer.ToArray();
		}
	}
}