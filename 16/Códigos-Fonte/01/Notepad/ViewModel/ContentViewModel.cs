// É importante que o ViewModel não faça qualquer referência à camada view.
using Softblue.Mvvm;
using System;
using System.IO;

namespace Softblue
{
    // ViewModel que faz a ponte entre a view MainWindow e o model Content.
    class ContentViewModel : Bindable
    {
        // Comandos
        public Command NewFileCommand { get; set; }
        public Command OpenFileCommand { get; set; }
        public Command SaveFileCommand { get; set; }
        public Command SaveAsFileCommand { get; set; }
        public Command ExitCommand { get; set; }
        public Command ShowAboutDialogCommand { get; set; }
        
        // Referência a um WindowService.
        public IWindowServices WindowServices { get; set; }

        // Exibição da barra de rolagem horizontal.
        public bool ShowHorizontalScrollBar
        {
            get
            {
                return !LineWrap;
            }
        }
        
        // Modelo.
        private Content content;
        public Content Content
        {
            get { return content; }
            set { SetValue(ref content, value); }
        }

        // Opção de quebra de linha.
        private bool lineWrap;
        public bool LineWrap
        {
            get { return lineWrap; }
            set
            {
                SetValue(ref lineWrap, value);

                // Avisa o WPF que a property que exibe a barra de rolagem horizontal deve ser lida novamente.
                OnPropertyChanged("ShowHorizontalScrollBar");

                // Grava a nova seleção nas settings.
                Settings.Default.LineWrap = value;
            }
        }

        // Linha do cursor.
        private int cursorRow;
        public int CursorRow
        {
            get { return cursorRow; }
            set
            {
                cursorRow = value;

                // Avisa o WPF que o texto da barra de status deve ser lido novamente.
                OnPropertyChanged("StatusBarText");
            }
        }

        // Coluna do cursor.
        private int cursorColumn;
        public int CursorColumn
        {
            get { return cursorColumn; }
            set
            {
                cursorColumn = value;

                // Avisa o WPF que o texto da barra de status deve ser lido novamente.
                OnPropertyChanged("StatusBarText");
            }
        }

        // Texto da barra de status.
        public string StatusBarText
        {
            get
            {
                return string.Format("Linha: {0}  |  Coluna: {1}", CursorRow, CursorColumn);
            }
        }

        // Título da janela.
        public string WindowTitle
        {
            get
            {
                if (Content.FileInfo == null)
                {
                    return Settings.Default.ApplicationName;
                }

                return string.Format("{0} - {1}", content.FileInfo.Name, Settings.Default.ApplicationName);
            }
        }

        // Indica se o texto teve alteração.
        private bool textChanged;
        public bool TextChanged
        {
            get { return textChanged; }
            set
            {
                textChanged = value;
            }
        }

        // Construtor.
        public ContentViewModel()
        {
            // Cria o modelo.
            Content = new Content();
            
            // Lê a quebra de linha a partir dos settings.
            LineWrap = Settings.Default.LineWrap;

            // Instancia os comandos.
            NewFileCommand = new Command(NewFile);
            OpenFileCommand = new Command(OpenFile);
            SaveFileCommand = new Command(SaveFile);
            SaveAsFileCommand = new Command(SaveAsFile);
            ExitCommand = new Command(Exit);
            ShowAboutDialogCommand = new Command(ShowAboutDialog);

            // Define o cursor na linha 1 e coluna 1.
            CursorRow = 1;
            CursorColumn = 1;
        }

        // Exibe o diálogo "Sobre"
        private void ShowAboutDialog()
        {
            if (WindowServices != null)
            {
                WindowServices.ShowAboutDialog();
            }
        }

        // Cria um novo arquivo.
        private void NewFile()
        {
            if (AskToSave())
            {
                // Cria um novo modelo.
                Content = new Content();
                
                // Reinicia a interface.
                Reset();
            }
        }

        // Sai da aplicação.
        private void Exit()
        {
            WindowServices.CloseWindow();
        }

        // Abre um arquivo.
        private void OpenFile()
        {
            if (AskToSave())
            {
                FileInfo fileInfo = WindowServices.ShowOpenFileDialog();

                if (fileInfo != null)
                {
                    // Carrega os dados do arquivo no modelo.
                    content.FileInfo = fileInfo;
                    content.Load();

                    // Reinicia a interface.
                    Reset();
                }
            }
        }

        // Salva um arquivo.
        private void SaveFile()
        {
            if (Content.FileInfo != null)
            {
                // Se já existe um arquivo associado no modelo, salva o conteúdo e reinicia a interface.
                Content.Save();
                Reset();
            }
            else
            {
                // Se não existe um arquivo, chama a funcionalidade de 'Salvar Como'.
                SaveAsFile();
            }
        }

        private void SaveAsFile()
        {
            bool saved;
            SaveAsFile(out saved);
        }

        // Salva um arquivo (Salvar como).
        // Retorna um booleano indicando se o arquivo foi salvo.
        private void SaveAsFile(out bool saved)
        {
            FileInfo savedFile = WindowServices.ShowSaveFileDialog();
            if (savedFile != null)
            {
                // Salva em arquivo e reinicia a interface.
                Content.FileInfo = savedFile;
                Content.Save();
                Reset();
                saved = true;
            }
            else
            {
                saved = false;
            }
        }

        // Reinicia a interface.
        private void Reset()
        {
            // Indica que não houve alteração de texto.
            TextChanged = false;

            // Avisa o WPF que a barra de título da janela deve ser lida novamente.
            OnPropertyChanged("WindowTitle");
        }

        // Pergunta se o conteúdo atual na tela deve ser salvo.
        // Este método retorna 'true' se a ação que estava sendo tomada ser continuar sendo executada,
        // ou 'false' se ela deve ser cancelada.
        public bool AskToSave()
        {
            if (!TextChanged)
            {
                // Se não há mudança de texto, não há o que salvar.
                return true;
            }

            // Exibe a caixa de diálogo.
            DialogResult result = WindowServices.ShowSaveChangesDialog();

            if (result == DialogResult.No)
            {
                // Se não é para salvar o documento, não salva e indica que a ação pode continuar sendo processada.
                return true;
            }
            else if (result == DialogResult.Cancel)
            {
                // Se foi solicitado o cancelamento, a ação que estava sendo tomada deve ser cancelada.
                return false;
            }

            if (Content.FileInfo != null)
            {
                // Se já existe um arquivo, chama o salvamento.
                SaveFile();
                return true;
            }
            else
            {
                // Se não existe, chama o salvar como.
                bool saved;
                SaveAsFile(out saved);
                if (saved)
                {
                    // Se o arquivo foi salvo, continua a ação que estava sendo executada.
                    return true;
                }

                // Se o arquivo não foi salvo, para a ação que estava sendo executada.
                return false;
            }
        }
    }
}
