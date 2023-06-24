using System;

namespace Chat.Client
{
    // Serviços para o ChatViewModel se comunicar com a tela do chat.
    interface IChatWindowServices
    {
        void RunOnUIThread(Action action);
        void ScrollMessagesToEnd();
        void CloseWindow();
    }
}
