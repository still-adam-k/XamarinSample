using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cirrious.MvvmCross.ViewModels;
using Sample.Core.Services;

namespace Sample.Core.ViewModel
{
    public class CalculatorViewModel : MvxViewModel
    {
        private ICalculator _calculator;

        public CalculatorViewModel(ICalculator calculator)
        {
            No1 = 1;
            No2 = 1;
            _calculator = calculator;
            _result = "Result is two-way binded to property";
        }

        public int No1 { get; set; }
        public int No2 { get; set; }

        public string _result ;
        public string Result
        {
            get { return _result; }
            set { _result = value; RaisePropertyChanged(() => Result); } 
        }
         
        public IMvxCommand CalculateCommand
        {
            get
            {
                return new MvxCommand(Calculate);
            }
        }

        public void Calculate()
        {
            Result = _calculator.Add(No1, No2).ToString();
        }
    }
}
