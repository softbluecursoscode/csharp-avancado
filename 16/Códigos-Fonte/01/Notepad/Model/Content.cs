using Softblue.Mvvm;
using System;
using System.IO;

namespace Softblue
{
    // Representa o conteúdo a ser salvo em um arquivo.
    // Herda de 'NotificationEnabled' para poder avisar a view sobre mudanças nas properties.
    class Content : Bindable
    {
        // Texto do documento.
        private string text;
        public string Text
        {
            get { return text; }
            set
            {
                SetValue(ref text, value);
            }
        }

        // Arquivo atrelado ao texto.
        private FileInfo fileInfo;
        public FileInfo FileInfo
        {
            get { return fileInfo; }
            set { SetValue(ref fileInfo, value); }
        }

        // Salva o conteúdo no arquivo.
        public void Save()
        {
            if (fileInfo != null)
            {
                File.WriteAllText(fileInfo.FullName, Text);
            }
        }

        // Carrega o conteúdo do arquivo.
        public void Load()
        {
            if (fileInfo != null)
            {
                Text = File.ReadAllText(fileInfo.FullName);
            }
        }
    }
}
