using System;
using System.Collections.Generic;
using System.Reflection;

namespace Softblue
{
    class Program
    {
        static void Main()
        {
            // Solicita o nome da classe.
            Console.Write("Nome da classe: ");
            string className = Console.ReadLine();

            // Obtém o Type associado à classe.
            Type type = Type.GetType(className);

            // Se o Type for null, a classe não existe.
            if (type == null)
            {
                Console.WriteLine("A classe " + className + " não foi encontrada");
                return;
            }

            // Procura pelo construtor padrão. Se não existir, não prossegue.
            ConstructorInfo constructor = type.GetConstructor(Type.EmptyTypes);
            if (constructor == null)
            {
                Console.WriteLine("Esta classe não pode ser instanciada porque não possui um construtor sem parâmetros");
                return;
            }

            // Obtém a lista de métodos que atendem aos critérios.
            List<MethodInfo> methods = ListMethods(type);

            // Escreve os métodos na tela.
            int count = 1;
            foreach (MethodInfo method in methods)
            {
                Console.WriteLine(count++ + ") " + method);
            }

            // Pede para o usuário escolher um método.
            Console.Write("\nQual método você deseja chamar? ");

            int num;
            try
            {
                num = int.Parse(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("O número digitado não é válido");
                return;
            }

            // Recupera o método escolhido.
            MethodInfo methodToCall = methods[num - 1];

            // Cria um array de parâmetros, com tamanho igual ao número de parâmetros do método.
            string[] values = new string[methodToCall.GetParameters().Length];
            foreach (ParameterInfo parameter in methodToCall.GetParameters())
            {
                // Armazena no array o valor de cada parâmetro digitado pelo usuário.
                Console.Write("Valor do parâmetro {0} ({1}): ", parameter.Position + 1, parameter.ParameterType);
                values[parameter.Position] = Console.ReadLine();
            }

            // Cria uma instância do objeto.
            object obj = Activator.CreateInstance(type);

            // Chama o método.
            Console.WriteLine("\nExecução do método: ");
            methodToCall.Invoke(obj, values);
        }

        // Retorna uma lista de métodos de determinado tipo.
        static List<MethodInfo> ListMethods(Type type)
        {
            List<MethodInfo> list = new List<MethodInfo>();

            // Obtém a lista de métodos.
            MethodInfo[] methods = type.GetMethods();

            foreach (MethodInfo method in type.GetMethods())
            {
                bool add = true;

                // Só considera métodos não estáticos e públicos.
                if (!method.IsStatic && method.IsPublic)
                {
                    foreach (ParameterInfo parameter in method.GetParameters())
                    {
                        // Itera sobre os parâmetros do método para checar se todos são do tipo string.
                        if (parameter.ParameterType != typeof(string))
                        {
                            add = false;
                            break;
                        }
                    }
                }
                else
                {
                    add = false;
                }

                if (add)
                {
                    // Se o método atende aos critérios, adiciona na lista.
                    list.Add(method);
                }
            }

            return list;
        }
    }

    class Teste
    {
        public void M1(string x, string y)
        {
            Console.WriteLine("M1(): " + x + " - " + y);
        }

        public void M2()
        {
            Console.WriteLine("M2()");
        }
    }
}