using System;
using System.Xml.Linq;
using System.Linq;

namespace Softblue
{
    class Program
    {
        static void Main()
        {
            // Obtém o objeto Type do tipo a ser utilizado.
            Type type = typeof(object);

            // Cria o XML.
            XElement xml = new XElement("type", new XAttribute("name", type.FullName),
                from m in type.GetMethods()
                select new XElement("method",
                    new XElement("name", m.Name),
                    new XElement("returnType", m.ReturnType.FullName),
                    new XElement("params",
                        from p in m.GetParameters()
                        select new XElement("param", new XAttribute("name", p.Name), p.ParameterType.FullName))));

            Console.WriteLine(xml);

            // Grava o XML no arquivo de saída.
            xml.Save("methods.xml");
        }
    }
}
