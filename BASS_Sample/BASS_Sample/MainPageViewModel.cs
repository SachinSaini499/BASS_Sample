using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace BASS_Sample
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private string _name;
        private string _orgName;
        private string _msg;
        private string _selectedGender;
        private ObservableCollection<User> _lstUser;
        public event PropertyChangedEventHandler PropertyChanged;
        ObservableCollection<string> _lstGender;
        FireBaseHelper firebaseHelper;
        public string abbrevation = "";
        public ICommand CommandSave { get; private set; }
        public ICommand CommandUpdate { get; private set; }
        public ICommand OnEdit { get; set; }
        public ICommand OnDelete { get; set; }
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }
        public string OrgName
        {
            get { return _orgName; }
            set { _orgName = value; OnPropertyChanged(nameof(OrgName)); }
        }
        public string msg
        {
            get { return _msg; }
            set { _msg = value; OnPropertyChanged(nameof(msg)); }
        }
        
        public string SelectedGender
        {
            get { return _selectedGender; }
            set { _selectedGender = value; OnPropertyChanged(nameof(SelectedGender)); }
        }
        public ObservableCollection<string> lstGender
        {
            get { return _lstGender; }
            set { _lstGender = value; OnPropertyChanged(nameof(lstGender)); }
        }
        public ObservableCollection<User> lstUser
        {
            get { return _lstUser; }
            set { _lstUser = value; OnPropertyChanged(nameof(lstUser)); }
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MainPageViewModel(int u)
        {
            CommandSave = new Command(SaveCommand_ClickAsync);
        }



            public MainPageViewModel()
        {
            lstUser = new ObservableCollection<User>();
            firebaseHelper = new FireBaseHelper();
            CommandSave = new Command(SaveCommand_ClickAsync);
            CommandUpdate = new Command(UpdateCommand_ClickAsync);
            OnEdit = new Command(EditCommand_ClickAsync);
            OnDelete = new Command(DeleteCommand_ClickAsync);
            ShowAllRecordAsync();
            LoadGender();
        }

        private void LoadGender()
        {
            lstGender = new ObservableCollection<string>();
            lstGender.Add("Male");
            lstGender.Add("Female");
        }

        private void UpdateCommand_ClickAsync(object obj)
        {
            UpdateDataAsync();
            MessagingCenter.Send<Xamarin.Forms.Application, string>(App.Current, "HideUpdate", "test");
        }

        private async System.Threading.Tasks.Task UpdateDataAsync()
        {
            abbrevation = getPersonAbbrevation(SelectedGender);
            await firebaseHelper.UpdatePerson(Name, OrgName, abbrevation);
            Name = string.Empty;
            OrgName = string.Empty;
            await ShowAllRecordAsync();
        }

        private string getPersonAbbrevation(string sex)
        {
            if (sex == "Male")
                return "Mr.";
            else if (sex == "Female")
                return "Ms.";
            else return "";
        }

        private void DeleteCommand_ClickAsync(object obj)
        {
            firebaseHelper.DeletePerson((obj as User).Name);
            ShowAllRecordAsync();

        }

        private void EditCommand_ClickAsync(object obj)
        {
            abbrevation = getPersonAbbrevation(SelectedGender);
            User user = new User();
            user.Name = (obj as User).Name;
            user.OrgName = (obj as User).OrgName;
            user.Gender = abbrevation;
            MessagingCenter.Send<Xamarin.Forms.Application, User>(App.Current, "ShowUpdate", user);
        }

        private void SaveCommand_ClickAsync(object obj)
        {
            AddDataAsync();
        }

        private async System.Threading.Tasks.Task AddDataAsync()
        {
            abbrevation = getPersonAbbrevation(SelectedGender);
            await firebaseHelper.AddPerson(Name, OrgName, abbrevation);
            Name = string.Empty;
            OrgName = string.Empty;
            msg = "Added";
            await ShowAllRecordAsync();
        }

        private async System.Threading.Tasks.Task ShowAllRecordAsync()
        {

            var allPersons = await firebaseHelper.GetAllPersons();
            lstUser = new ObservableCollection<User>(allPersons);
        }
    }
}
