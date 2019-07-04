using System;
using System.Collections.Generic;
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
		public static IEnumerable<Assembly> GetReferencedAssemblies(Assembly assembly)
        {
            yield return assembly;

            foreach(var an in assembly.GetReferencedAssemblies())
            {
				// 獲取指定名稱下的程序集
				var target = GetAssembly(an);
                if(target != null)
                {
                    foreach(var asm in GetReferencedAssemblies(target))
                    {
                        yield return asm;
                    }
                }
            }
        }

		/// <summary>
		/// 返回指定名稱下的程序集
		/// </summary>
		/// <param name="assemblyName">程序集名稱</param>
		/// <returns>程序集對象</returns>
		public static Assembly GetAssembly(AssemblyName assemblyName)
        {
			// 只返回指定特征名稱的程序集對象(程序集名稱以“MyMathSheets.”開頭)
			if (!assemblyName.FullName.StartsWith("MyMathSheets."))
            {
                return null;
            }

			// 獲取當前應用程序中的所有程序集
            var assembly = AppDomain.CurrentDomain.GetAssemblies()
                .FirstOrDefault(asm => asm.GetName().Name.Equals(assemblyName.Name));
            if(assembly != null)
            {
                return assembly;
            }

            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            var dllName = assemblyName.Name + ".dll";
            if(!File.Exists(Path.Combine(basePath, dllName)))
            {
                basePath = Path.Combine(basePath, "bin");
            }
            var dllPath = Path.Combine(basePath, dllName);
            return File.Exists(dllPath) ? AppDomain.CurrentDomain.Load(assemblyName) : null;
        }

		/// <summary>
		/// 屬性反射
		/// 在指定對象中返回指定名稱的屬性值
		/// </summary>
		/// <param name="source">對象實例</param>
		/// <param name="propertyName">屬性名稱</param>
		/// <returns>屬性值</returns>
		public static object GetProperty(object source, string propertyName)
        {
            var pi = source.GetType().GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            if(pi == null)
            {
                throw new MissingMemberException(source.GetType().FullName, propertyName);
            }

            var mi = pi.GetGetMethod(true);
            if(mi == null)
            {
                throw new MissingMethodException(source.GetType().FullName, string.Format("get_{0}", propertyName));
            }

            return mi.Invoke(source, new object[0]);
        }
    }
}
