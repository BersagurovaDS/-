using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinContacts
{
    public class ContactRepository
    {
        SQLiteConnection database;

        public ContactRepository(string databasePath)
        {
            database = new SQLiteConnection(databasePath);
            database.CreateTable<Contact>();
        }

        public IEnumerable<Contact> GetContacts()
        {
            return database.Table<Contact>().ToList();
        }

        public Contact GetContact(int id)
        {
            return database.Get<Contact>(id);
        }

        public int DeleteContact(int id)
        {
            return database.Delete<Contact>(id);
        }

        public int SaveContact(Contact contact)
        {
           
            if (contact.Id != 0)
            {
                database.Update(contact);
                return contact.Id;
            }
            
            return database.Insert(contact);
        }

    }
}
