using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace ComputationalStrategy.Main.Util
{
	/// <summary>
	/// 
	/// </summary>
	public static class JsonExtension
	{
		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="obj"></param>
		/// <returns></returns>
		public static string GetJsonByObject<T>(this T obj)
		{
			if (null == obj)
			{
				return string.Empty;
			}

			// Instantiation DataContractJsonSerializer object, needs to be serialized object type
			DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));

			// Instantiate a memory stream, used to store the serialized data
			using (MemoryStream stream = new MemoryStream())
			{
				// Using WriteObject serialized objects
				serializer.WriteObject(stream, obj);

				// Write the memory stream
				byte[] dataBytes = new byte[stream.Length];
				stream.Position = 0;
				stream.Read(dataBytes, 0, (int)stream.Length);

				// Through the UTF8 format is converted to a string
				return Encoding.UTF8.GetString(dataBytes);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="jsonString"></param>
		/// <returns></returns>
		public static T GetObjectByJson<T>(this string jsonString)
		{
			try
			{
				// Instantiation DataContractJsonSerializer object, needs to be serialized object type
				DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));

				// Keep the Json into the memory stream
				using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
				{
					// Use the ReadObject methods deserialized into objects
					return (T)serializer.ReadObject(stream);
				}
			}
			catch (Exception)
			{
				return default(T);
			}

		}
	}
}
