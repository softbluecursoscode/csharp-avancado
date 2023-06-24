using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Win32;
using System.IO;
using Softblue.Properties;

namespace Softblue
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IWindowServices
    {
        public MainWindow()
        {
            InitializeComponent();

            // Atribui a referência de IWindowServices (a própria janela) ao ViewModel.
            // Dessa forma o ViewModel pode chamar os serviços disponíveis na interface.
            viewModel.WindowServices = this;
        }

        // Evento de mudança no texto.
        private void txtContent_TextChanged(object sender, TextChangedEventArgs e)
        {
            viewModel.TextChanged = true;
        }

        // Exibe diálogo de "Sobre";
        public void ShowAboutDialog()
        {
            AboutWindow aboutWindow = new AboutWindow();
            aboutWindow.Owner = this;
            aboutWindow.ShowDialog();
        }

        // Mostrar diálogo para salvar alterações.
        public DialogResult ShowSaveChangesDialog()
        {
            // Pergunta se o usuário deseja salvar as alterações.
            MessageBoxResult result = MessageBox.Show(this, "Deseja salvar suas alterações?", Settings.Default.ApplicationName, MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

            // Mapeia a escolha para um dos valores do enum DialogResult.
            switch (result)
            {
                case MessageBoxResult.Yes: return Softblue.DialogResult.Yes;
                case MessageBoxResult.No: return Softblue.DialogResult.No;
                case MessageBoxResult.Cancel: return Softblue.DialogResult.Cancel;
            }

            return Softblue.DialogResult.Unknown;
        }

        // Mostrar diálogo cara carregar arquivo.
        public System.IO.FileInfo ShowOpenFileDialog()
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            bool? result = openDialog.ShowDialog();

            if (result.HasValue && result.Value)
            {
                // Retorna um FileInfo representando o arquivo escolhido.
                return new FileInfo(openDialog.FileName);
            }

            return null;
        }
        
        // Mostrar diálogo para salvar arquivo.
        public FileInfo ShowSaveFileDialog()
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Documentos de texto (*.txt)|*.txt|Todos os arquivos (*.*)|*.*";

            bool? result = saveDialog.ShowDialog();

            if (result.HasValue && result.Value)
            {
                string filePath = saveDialog.FileName;

                // Adiciona a extensão 'txt' se nenhuma foi fornecida.
                if (!filePath.Contains("."))
                {
                    filePath += ".txt";
                }

                // Retorna um FileInfo representando o arquivo escolhido.
                return new FileInfo(filePath);
            }

            return null;
        }

        // Evento de janela fechando.
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Verifica se realmente é para sair. Se não for, cancela o fechamento da janela.
            if (!viewModel.AskToSave())
            {
                e.Cancel = true;
            }
        }

        // Evento de mudança na posição do cursor.
        private void txtContent_SelectionChanged(object sender, RoutedEventArgs e)
        {
            // Calcula a linha e a coluna atual do cursor.
            int row = txtContent.GetLineIndexFromCharacterIndex(txtContent.CaretIndex);
            int col = txtContent.CaretIndex - txtContent.GetCharacterIndexFromLineIndex(row);

            // Atribui a nova posição no ViewModel.
            viewModel.CursorRow = row + 1;
            viewModel.CursorColumn = col + 1;
        }

        // Fechar janela.
        public void CloseWindow()
        {
            Close();
        }
    }
}