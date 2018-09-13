using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace MyMathSheets.CommonLib.Util
{
	/// <summary>
	/// 
	/// </summary>
	public static class ReflectionUtil
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static IEnumerable<Assembly> GetReferencedAssemblies(Assembly assembly)
        {
            yield return assembly;

            foreach(var an in assembly.GetReferencedAssemblies())
            {
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
        /// 
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <returns></returns>
        public static Assembly GetAssembly(AssemblyName assemblyName)
        {
            if(!assemblyName.FullName.StartsWith("Tony."))
            {
                return null;
            }

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
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
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
