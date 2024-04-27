using ContactApi.Models;

namespace ContactApi.Interfaces
{
    public interface IContacRepository
    {
        IEnumerable<Contact> GetAll();
        Contact GetById(int id);
        bool CreateContact(Contact contact);
        bool UpdateContact(int id,Contact contact);
        bool DeleteContact(int id);
        bool Save();
    }
}
