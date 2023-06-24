using System;
using System.IO;

namespace Softblue
{
    // Classe usada para contar arquivos e diretórios.
    public static class Contador
    {
        // Conta arquivos e diretórios dentro de um diretório passado como parâmetro.
        public static Resultado ContarArquivosEDiretorios(DirectoryInfo dirInfo)
        {
            Resultado resultado = new Resultado();
            ContarArquivosEDiretorios(dirInfo, resultado);
            return resultado;
        }

        // Método que faz a contagem e é chamado recursivamente.
        static void ContarArquivosEDiretorios(DirectoryInfo dirInfo, Resultado resultado)
        {
            // Lê a quantidade de arquivos do diretório e acumula com o valor que já havia sido encontrado.
            resultado.Arquivos += dirInfo.GetFiles().Length;

            // Para cada subdiretório, faz a contagem de forma recursiva.
            foreach (DirectoryInfo subdirInfo in dirInfo.GetDirectories())
            {
                // Incrementa o número de diretórios encontrados.
                resultado.Diretorios++;

                // Conta os arquivos do diretório.
                ContarArquivosEDiretorios(subdirInfo, resultado);
            }
        }
    }

    // Classe que representa o resultado da contagem.
    public class Resultado
    {
        // Número de arquivos encontrados.
        public int Arquivos { get; set; }

        // Número de diretórios encontrados.
        public int Diretorios { get; set; }
    }
}
