using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BASS_Sample
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel();
            MessagingCenter.Subscribe<Xamarin.Forms.Application, User>(App.Current, "ShowUpdate", (sender, args) => {
                Device.BeginInvokeOnMainThread(() =>
                {
                    btnSave.IsVisible = false;
                    btnUpdate.IsVisible = true;
                    EntryName.Text = args.Name;
                    EntryOrg.Text = args.OrgName;
                    EntryName.IsReadOnly = true;
                });
            });
            MessagingCenter.Subscribe<Xamarin.Forms.Application, string>(App.Current, "HideUpdate", (sender, args) => {
                Device.BeginInvokeOnMainThread(() =>
                {
                    EntryName.IsReadOnly = false; ;
                    btnSave.IsVisible = true;
                    btnUpdate.IsVisible = false;
                   
                });
            });
        }

       
    }
}
