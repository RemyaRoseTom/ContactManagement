using System.Data;

namespace ContactManager.Models
{
    public interface IContactManagerRepository
    {
        Contact CreateContact(Contact contactToCreate);
        void DeleteContact(Contact contactToDelete);
        Contact EditContact(Contact contactToUpdate);
        Contact GetContact(int id);
        DataTable ListContacts();
    }
}
