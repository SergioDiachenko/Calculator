namespace Calculator.ViewModels
{
    public interface IDialogViewModel
    {
        string Message { get; set; }

        void Close();
        void OpenWindow(string message);
    }
}