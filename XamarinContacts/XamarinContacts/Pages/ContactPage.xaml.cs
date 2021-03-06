﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using Plugin.Messaging;


namespace XamarinContacts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactPage : ContentPage
    {
        public ContactPage()
        {
            InitializeComponent();
        }

        private void SaveButton_Clicked(object sender, EventArgs e)
        {
            Contact contact = (Contact)BindingContext;
            if (!string.IsNullOrEmpty(contact.Name))
            {
                try
                {
                    App.Repository.SaveContact(contact);
                    Navigation.PopAsync();
                }
                catch (SQLiteException ex)
                {
                    // вид сообщения об ошибке: UNIQUE constraint failed: Contact.Name
                    if (ex.Message.Contains("Contact.Name"))
                    {
                        DisplayAlert("Ошибка!", "Контакт с таким именем уже есть", "Ладно");
                    }
                }
            }
            else
            {
                DisplayAlert("Ошибка!", "Имя контакта не может быть пустым", "Ладно");
            }

        }

        private void DeleteButton_Clicked(object sender, EventArgs e)
        {
            Contact contact = (Contact)BindingContext;
            App.Repository.DeleteContact(contact.Id);
            Navigation.PopAsync();
        }
        private void CancelButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
        private void MakePhoneCall(object sender, EventArgs e)
        {
            var dialer = CrossMessaging.Current.PhoneDialer;
            if (dialer.CanMakePhoneCall)
            {
                Contact contact = (Contact)BindingContext;
                dialer.MakePhoneCall(contact.Phone);
            }
        }
        private void SendMessage(object sender, EventArgs e)
        {
            var dialer = CrossMessaging.Current.SmsMessenger;
            if (dialer.CanSendSms)
            {
                Contact contact = (Contact)BindingContext;
                dialer.SendSms(contact.Phone);
            }
        }
        private void SendEmail(object sender, EventArgs e)
        {
            var dialer = CrossMessaging.Current.EmailMessenger;
            if (dialer.CanSendEmail)
            {
                Contact contact = (Contact)BindingContext;
                dialer.SendEmail(contact.Email, "", $"Привет, {contact.Name}");
            }
        }



    }
}