﻿using System.Data;
using System.Text.RegularExpressions;

namespace ContactManager.Models
{
    public class ContactManagerService: IContactManagerService
    {
        private IValidationDictionary _validationDictionary;
        private IContactManagerRepository _repository;

        public ContactManagerService(IValidationDictionary validationDictionary)
             : this(validationDictionary, new ContactManagerRepository())
        { }


        public ContactManagerService(IValidationDictionary validationDictionary, IContactManagerRepository repository)
        {
            _validationDictionary = validationDictionary;
            _repository = repository;
        }
        public bool ValidateContact(Contact contactToValidate)
        {
            if (contactToValidate.FirstName.Trim().Length == 0)
                _validationDictionary.AddError("FirstName", "First name is required.");
            if (contactToValidate.LastName.Trim().Length == 0)
                _validationDictionary.AddError("LastName", "Last name is required.");
            if (contactToValidate.Phone.Length > 0 && !Regex.IsMatch(contactToValidate.Phone, @"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}"))
                _validationDictionary.AddError("Phone", "Invalid phone number.");
            if (contactToValidate.Email.Length > 0 && !Regex.IsMatch(contactToValidate.Email, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
                _validationDictionary.AddError("Email", "Invalid email address.");
            return _validationDictionary.IsValid;
        }
        public bool CreateContact(Contact contactToCreate)
        {
            if (!ValidateContact(contactToCreate))
                return false;

            // Database logic
            try
            {
                _repository.CreateContact(contactToCreate);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool EditContact(Contact contactToEdit)
        {
            if (!ValidateContact(contactToEdit))
                return false;
            // Database logic
            try
            {
                _repository.EditContact(contactToEdit);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool DeleteContact(Contact contactToDelete)
        {
            try
            {
                _repository.DeleteContact(contactToDelete);
            }
            catch
            {
                return false;
            }
            return true;
        }
        public Contact GetContact(int id)
        {
            return _repository.GetContact(id);
        }
      

        public DataTable ListContacts()
        {
            return _repository.ListContacts();
        }

    }
}