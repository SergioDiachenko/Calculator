using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

//[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace Calculator.ViewModels
{
    public class ShellViewModel : Screen
    {
        #region Fields

        private bool _repeat;
        private string _displayTitle = "Calculator";
        private double _firstNum;
        private double _secondNum;
        private double _result;
        private string _screen = "0";
        private string _operation = "";
        private readonly IWindowManager _windowManager;
        private readonly IDialogViewModel _dialog;
        private log4net.ILog log;

        //private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        #region Properties

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

        public double FirstNum
        {
            get { return _firstNum; }
            set { _firstNum = value; }
        }

        public double SecondNum
        {
            get { return _secondNum; }
            set { _secondNum = value; }
        }

        public double Result
        {
            get { return _result; }
            set { _result = value; }
        }

        public string Screen
        {
            get { return _screen; }
            set
            {
                _screen = value;
                NotifyOfPropertyChange(() => Screen);
            }
        }

        public string Operation
        {
            get { return _operation; }
            set { _operation = value; }
        }

        public bool Repeat
        {
            get { return _repeat; }
            set { _repeat = value; }
        }

        #endregion

        #region Constructor

        public ShellViewModel(IWindowManager windowManager, IDialogViewModel dialog, log4net.ILog log)
        {
            _windowManager = windowManager;
            _dialog = dialog;
            this.log = log;
        }

        public ShellViewModel(IWindowManager windowManager, IDialogViewModel dialog)
            : this(windowManager, dialog, null)
        {
            //_windowManager = windowManager;
            //_dialog = dialog;
        }

        #endregion

        #region Methods
        private void AddNumber(int number)
        {
            if (Operation == "")
            {
                FirstNum = FirstNum * 10 + number;
                Screen = FirstNum.ToString();
            }
            else
            { 
                SecondNum = SecondNum * 10 + number;
                Screen = SecondNum.ToString();
            }
        }

        private void Calculate()
        {
            switch (Operation)
            {
                case "+":
                    Result = FirstNum + SecondNum;
                    break;
                case "-":
                    Result = FirstNum - SecondNum;
                    break;
                case "*":
                    Result = FirstNum * SecondNum;
                    break;
                case "/":
                    Result = FirstNum / SecondNum;
                    break;
            }
        }

        private void LogResult()
        {
            if (double.IsInfinity(Result))
            {
                log.Error(string.Format("{0} {1} {2} = Error! You can not divide by zero", FirstNum, Operation, SecondNum));

                _dialog.OpenWindow("You can not divide by zero");
            }
            else
            {
                log.Error(string.Format("{0} {1} {2} = {3}. Successfull operation", FirstNum, Operation, SecondNum, Result));
            }
        }

        public void ShowResult()
        {
            Calculate();
            Screen = double.IsInfinity(Result) ? "ERROR" : Result.ToString();
            LogResult();
            ClearPartial();
        }

        public void Clear()
        {
            FirstNum = 0;
            SecondNum = 0;
            Result = 0;
            Operation = "";
            Screen = Result.ToString();
        }

        private void ClearPartial()
        {
            if (Repeat)
            {               
                FirstNum = Result;
                Repeat = false;              
            }
            else
            {
                Operation = "";
                FirstNum = !double.IsInfinity(Result) ? Result : 0;
            }

            SecondNum = 0;
            Result = 0;
        }

        #endregion

        #region Methods: represents calculator operations
        public void Divide()
        {
            if (SecondNum != 0)
            {
                Repeat = true;
                ShowResult();
            }

            Operation = "/";
        }

        public void Multiply()
        {
            if (SecondNum != 0)
            {
                Repeat = true;
                ShowResult();
            }

            Operation = "*";
        }

        public void Substract()
        {
            if (SecondNum != 0)
            {
                Repeat = true;
                ShowResult();
            }

            Operation = "-";
        }

        public void Add()
        {
            if (SecondNum != 0)
            {
                Repeat = true;
                ShowResult();
            }

            Operation = "+";
        }

        #endregion

        #region Methods: Input number buttons
        public void PressZero()
        {
            AddNumber(0);
        }

        public void PressOne()
        {
            AddNumber(1);
        }

        public void PressTwo()
        {
            AddNumber(2);
        }

        public void PressThree()
        {
            AddNumber(3);
        }

        public void PressFour()
        {
            AddNumber(4);
        }

        public void PressFive()
        {
            AddNumber(5);
        }

        public void PressSix()
        {
            AddNumber(6);
        }

        public void PressSeven()
        {
            AddNumber(7);
        }

        public void PressEight()
        {
            AddNumber(8);
        }

        public void PressNine()
        {
            AddNumber(9);

        }
        #endregion 

        #region Methods: Disabled buutons. Not implemented calculator functions. 
        public bool CanChangeArithmeticSign()
        {
            return false;
        }

        public void ChangeArithmeticSign()
        {
            throw new NotImplementedException();
        }

        public bool CanPercentageEntry()
        {
            return false;
        }

        public void PercentageEntry()
        {
            throw new NotImplementedException();
        }

        public bool CanPressComma()
        {
            return false;
        }
        public void PressComma()
        {
            throw new NotImplementedException();
        }
        #endregion     
    }
}
