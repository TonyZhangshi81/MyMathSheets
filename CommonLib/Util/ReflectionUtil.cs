using MyMathSheets.CommonLib.Configurations;
using MyMathSheets.CommonLib.Plugin;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;

namespace MyMathSheets.CommonLib.Util
{
	/// <summary>
	/// 反射通用方法類
	/// </summary>
	public static class ReflectionUtil
	{
		/// <summary>
		/// 指定程序集中被引用的程序集集合（使用遞歸方法實現）
		/// </summary>
		/// <param name="assembly">程序集對象</param>
		/// <returns>被引用的程序集集合</returns>
		/// <remarks>
		/// 索引器返回程序集對象的順序依次是: A1, A2, B1, A3
		/// A1 ->(參照)-> A2 ->(參照)-> A3
		///				  B1
		/// </remarks>
		/// <exception cref="ArgumentNullException"><paramref name="assembly"/>為NULL的情況</exception>
		public static IEnumerable<Assembly> GetReferencedAssemblies(Assembly assembly)
		{
			yield return assembly;

			Guard.ArgumentNotNull(assembly, "assembly");

			foreach (var an in assembly.GetReferencedAssemblies())
			{
				// 獲取指定名稱下的程序集
				var target = GetAssembly(an);
				if (target != null)
				{
					foreach (var asm in GetReferencedAssemblies(target))
					{
						yield return asm;
					}
				}
			}
		}

		/// <summary>
		/// 返回指定名稱下的程序集對象
		/// </summary>
		/// <param name="assemblyName">程序集名稱</param>
		/// <returns>程序集對象</returns>
		/// <exception cref="ArgumentNullException"><paramref name="assemblyName"/>為NULL的情況</exception>
		public static Assembly GetAssembly(AssemblyName assemblyName)
		{
			Guard.ArgumentNotNull(assemblyName, "assemblyName");

			// 只返回指定特征名稱的程序集對象(程序集名稱以“MyMathSheets.”開頭)
			if (!assemblyName.FullName.StartsWith("MyMathSheets.", StringComparison.CurrentCultureIgnoreCase))
			{
				return null;
			}

			// 獲取當前應用程序中的所有程序集
			Assembly assembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(asm => asm.GetName().Name.Equals(assemblyName.Name, StringComparison.CurrentCultureIgnoreCase));
			if (assembly != null)
			{
				return assembly;
			}

			var basePath = ConfigurationUtil.ApplicationRootPath;
			// 相對路徑的對應
			if (basePath.StartsWith("~/") || basePath.StartsWith("~\\"))
			{
				basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory.TrimEnd('/', '\\'), basePath.Substring(2));
			}

			var dllName = assemblyName.Name + ".dll";
			var dllPath = Path.Combine(basePath, dllName);
			if (!File.Exists(dllPath))
			{
				return null;
			}

			return AppDomain.CurrentDomain.Load(assemblyName);
		}

		/// <summary>
		/// 返回指定題型識別ID的程序集對象
		/// </summary>
		/// <param name="topicIdentifier">題型識別ID</param>
		/// <returns>指定題型的程序集對象</returns>
		public static Assembly GetAssembly(string topicIdentifier)
		{
			var plugins = PluginHelper.GetManager().InitPluginsModuleList.ToList();
			if (!plugins.Any())
			{
				return null;
			}

			var plugin = plugins.Where(d => d.TopicIdentifier.EndsWith(topicIdentifier, StringComparison.CurrentCultureIgnoreCase));
			// 如果指定題型不存在或者插件DLL文件不存在
			if (!plugin.Any() || !File.Exists(plugin.First().FullName))
			{
				return null;
			}
			return Assembly.LoadFile(plugin.First().FullName);
		}

		/// <summary>
		/// 屬性反射
		/// 在指定對象中返回指定名稱的屬性值
		/// </summary>
		/// <param name="source">對象實例</param>
		/// <param name="propertyName">屬性名稱</param>
		/// <returns>屬性值</returns>
		/// <exception cref="ArgumentNullException"><paramref name="source"/>為NULL的情況</exception>
		public static object GetProperty(object source, string propertyName)
		{
			Guard.ArgumentNotNull(source, "source");

			var pi = source.GetType().GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			if (pi == null)
			{
				throw new MissingMemberException(source.GetType().FullName, propertyName);
			}

			var mi = pi.GetGetMethod(true);
			if (mi == null)
			{
				throw new MissingMethodException(source.GetType().FullName, string.Format(CultureInfo.CurrentCulture, "get_{0}", propertyName));
			}

			return mi.Invoke(source, new object[0]);
		}
	}
}