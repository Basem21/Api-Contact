using ContactApi.Data;
using ContactApi.Interfaces;
using ContactApi.Models;

namespace ContactApi.Repositories
{
    public class ContactRepository : IContacRepository
    {
        private readonly ApplicationDbContext _context;

        public ContactRepository(ApplicationDbContext context)
        {
            this._context = context;
        }
        IEnumerable<Contact> IContacRepository.GetAll()
        {
            var Contacts= _context.Contacts.ToList();
            return Contacts;
        }
        Contact IContacRepository.GetById(int id)
        {
            var Contact=_context.Contacts.Where(C=>C.Id==id).FirstOrDefault();
            return Contact;
        }
        bool IContacRepository.CreateContact(Contact contact)
        {
            _context.Contacts.Add(contact);
            return Save();
        }
        bool IContacRepository.UpdateContact(int id, Contact contact)
        {
            var OldContact=_context.Contacts.Where(C=>C.Id == id).FirstOrDefault();

            OldContact.FullName = contact.FullName;
            OldContact.Email = contact.Email;
            OldContact.Address = contact.Address;
            OldContact.PhoneNumber = contact.PhoneNumber;

            _context.Contacts.Update(OldContact);
            return Save();
        }

        bool IContacRepository.DeleteContact(int id)
        {
            var ContactToDelete=_context.Contacts.Where(C=>C.Id==id).FirstOrDefault();
            _context.Contacts.Remove(ContactToDelete);
            return Save();
        }


        public bool Save()
        {
            var Saved=_context.SaveChanges();
            return Saved>0?true:false;
        }
    }
}
