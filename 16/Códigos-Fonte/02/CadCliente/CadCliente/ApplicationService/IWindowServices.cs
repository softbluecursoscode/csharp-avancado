
namespace CadCliente.ApplicationService
{
    // Serviços expostos pela view
    interface IWindowServices
    {
        void PutFocusOnForm();
        bool ConfirmSave();
        bool ConfirmDelete();
        void CloseWindow();
        void UpdateBindings();
    }
}
