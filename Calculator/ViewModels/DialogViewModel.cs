 using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.ViewModels
{
    public class DialogViewModel : Screen, IDialogViewModel
    {
        // Fields
        private readonly IWindowManager _windowManager;
        private string _message;
        private string _displayTitle = "Warning!";

        // Properties  
        public string DisplayTitle
        {
            get { return _displayTitle; }
            set
            {
                if (value.Equals(_displayTitle)) return;
                _displayTitle = value;
                NotifyOfPropertyChange(() => DisplayTitle);
            }
        }
           
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                NotifyOfPropertyChange(() => Message);
            }
        }

        // Constructors
        public DialogViewModel(IWindowManager windowManager, string message)
        {
            _windowManager = windowManager;
            _message = message;
        }

        public DialogViewModel(IWindowManager windowManager)
            : this(windowManager, "") { }

        // Methods
        public void OpenWindow(string message)
        {
            Message = message;
            _windowManager.ShowWindow(new DialogViewModel(_windowManager, message));
        }

        public void Close()
        {
            TryClose();
        }
    }
}
