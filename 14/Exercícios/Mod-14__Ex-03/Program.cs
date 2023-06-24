using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Softblue
{
    class Program
    {
        static void Main()
        {
            // Solicita o caminho do diretório.
            Console.Write("Digite o caminho de um diretório do seu computador: ");
            string diretorio = Console.ReadLine();

            if (!Directory.Exists(diretorio))
            {
                Console.WriteLine("O diretório " + diretorio + " não existe");
                return;
            }

            DirectoryInfo dirInfo = new DirectoryInfo(diretorio);

            // Cria o objeto que representa o diretório.
            Itens itens = new Itens() { NomeDiretorio = dirInfo.FullName };
            
            // Lista os arquivos do diretório e adiciona em Itens.
            foreach(FileInfo arquivo in dirInfo.GetFiles())
            {
                Item item = new Item() { Nome = arquivo.Name, IsArquivo = true, Tamanho = arquivo.Length };
                itens.Lista.Add(item);
            }

            // Lista os diretórios do diretório e adiciona em Itens.
            foreach (DirectoryInfo dir in dirInfo.GetDirectories())
            {
                Item item = new Item() { Nome = dir.Name, IsArquivo = false };
                itens.Lista.Add(item);
            }

            // Serializar o objeto 'itens' para XML. O nome do arquivo é igual ao nome do diretório com extensão XML.
            XmlSerializer xmlSer = new XmlSerializer(typeof(Itens));
            using (FileStream fs = new FileStream(dirInfo.Name + ".xml", FileMode.Create, FileAccess.Write))
            {
                xmlSer.Serialize(fs, itens);
            }
        }
    }

    // Classe que agrupa itens (arquivos e diretórios).
    [XmlRoot("ArquivosEDiretorios")]
    public class Itens
    {
        [XmlAttribute]
        public string NomeDiretorio { get; set; }

        [XmlElement("Item")]
        public List<Item> Lista { get; set; }

        public Itens()
        {
            Lista = new List<Item>();
        }
    }

    // Classe que representa um item (arquivo ou diretório).
    [XmlRoot]
    public class Item
    {
        public string Nome { get; set; }
        
        [XmlAttribute]
        public bool IsArquivo { get; set; }

        public long Tamanho { get; set; }
    }
}
