using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace AutoRun
{
    public static class AutoRunner
    {
        public static List<Result> Run()
        {
            Assembly assembly = Assembly.GetCallingAssembly();
            Type[] types = assembly.GetTypes();

            List<Result> results = new List<Result>();
            Stopwatch timer = new Stopwatch();

            foreach(Type type in types)
            {
                if (type.GetCustomAttribute<RunClassAttribute>() != null)
                {
                    MethodInfo[] methods = type.GetMethods();

                    foreach (MethodInfo method in methods)
                    {
                        if (method.GetCustomAttribute<RunMethodAttribute>() != null && method.IsStatic)
                        {
                            timer.Start();
                            method.Invoke(null, null);
                            timer.Stop();

                            Result result = new Result(type, method, timer.Elapsed);
                            results.Add(result);
                            timer.Reset();
                        }
                    }
                }
            }

            return results;
        }
    }
}
