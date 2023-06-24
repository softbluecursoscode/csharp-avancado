using System;
using System.Reflection;

namespace Softblue
{
    class Program
    {
        static void Main()
        {
            // Obtém o assembly System.dll.
            Assembly assembly = Assembly.GetAssembly(typeof(object));

            // Itera sobre todos os tipos definidos no assembly.
            foreach (Type type in assembly.GetTypes())
            {
                // Se o tipo for uma classe e tiver o atributo [Obsolete] definido, mostra na tela.
                if (type.GetCustomAttribute<ObsoleteAttribute>() != null && type.IsClass)
                {
                    Console.WriteLine(type.FullName);
                }
            }

            Console.WriteLine("------------");

            // Itera sobre todos os tipos definidos no assembly.
            foreach (Type type in assembly.GetTypes())
            {
                if (type.IsClass)
                {
                    // Se o tipo for uma classe, itera sobre os métodos.
                    foreach(MethodInfo method in type.GetMethods())
                    {
                        // Se o método tiver o atributo [Obsolete] definido, mostra na tela as informações do método (nome e classe que o definiu).
                        if (method.GetCustomAttribute<ObsoleteAttribute>() != null)
                        {
                            Console.WriteLine("{0}() -> {1}", method.Name, method.DeclaringType.FullName);
                        }
                    }
                }
            }
        }
    }
}
