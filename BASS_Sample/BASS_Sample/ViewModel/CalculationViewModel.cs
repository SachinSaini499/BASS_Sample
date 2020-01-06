using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace BASS_Sample.ViewModel
{
    public class CalculationViewModel : INotifyPropertyChanged
    {
        public CalculationViewModel()
        {
            CommandAdd = new Command(Add_Click);
        }

        public ICommand CommandAdd;
        //{
        //    get 
        //    {
        //        return new Command(() =>
        //        {
        //            Result = FirstNumber + SecondNumber;
        //        });
        //    }
        //}


    private void Add_Click(object obj)
        {
            Result = _firstNumber + _secondNumber;
        }

        
             
             
         
       
        private int _firstNumber;
        public int FirstNumber
        {
            get { return _firstNumber; }
            set { _firstNumber = value; OnPropertyChanged(nameof(FirstNumber)); }
        }

        private int _secondNumber;
        public int SecondNumber
        {
            get { return _secondNumber; }
            set { _secondNumber = value; OnPropertyChanged(nameof(SecondNumber)); }
        }

        private int _result;
        public int Result
        {
            get { return _result; }
            set { _result = value; OnPropertyChanged(nameof(Result)); }
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
