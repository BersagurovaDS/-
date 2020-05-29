using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinContacts
{
    public partial class App : Application
    {
        const string DATABASE_NAME = "contacts.db";
        static ContactRepository repository;

        public static ContactRepository Repository
        {
            get
            {
                
                if (repository == null)
                {
                    string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                    repository = new ContactRepository(Path.Combine(path, DATABASE_NAME));
                }
                return repository;
            }
        }
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
