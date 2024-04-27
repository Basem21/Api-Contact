using AutoMapper;
using ContactApi.Data;
using ContactApi.DTOs;
using ContactApi.Interfaces;
using ContactApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContactApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContacRepository _contactRepository;
        private readonly IMapper _mapper;

        public ContactController(IContacRepository contactRepository,IMapper mapper)
        {
            this._contactRepository = contactRepository;
            this._mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetContacts()
        {
           var Contacts= _contactRepository.GetAll();
            return Ok(Contacts);
        }
        [HttpGet("Id")]
        public IActionResult GetContactById(int Id)
        {
           var Contact=_contactRepository.GetById(Id);
            return Ok(Contact);

        }

        [HttpPost]
        public IActionResult CreateContact(ContactDTO dto)
        {
            if (dto == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest("Invalid Contac");

            var ContactMap = _mapper.Map<Contact>(dto);
            if (!_contactRepository.CreateContact(ContactMap))
                return BadRequest("Something Went Wrong While Saving");

            return Ok("Successfully Created");
        }
        [HttpPut]
        public IActionResult UpdateContact(int id,ContactDTO dto)
        {
            var OldContact = _contactRepository.GetById(id);

            if (OldContact == null)
                return BadRequest($"There is No Contact With Id:{id}");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var ContactMapper = _mapper.Map<Contact>(dto);

            if (!_contactRepository.UpdateContact(id, ContactMapper))
                return BadRequest("There is Something Went Wrong While Updating Data");

            return Ok("Successfully Updated");
        }

        [HttpDelete("id")]
        public IActionResult DeleteContact(int id)
        {
            var ContactToDelete = _contactRepository.GetById(id);

            if (ContactToDelete == null)
                return BadRequest($"There is No Contact With Id:{id}");

            if (!_contactRepository.DeleteContact(id))
                return BadRequest("There is Something Went Wrong While Deleting Contact");

            return Ok("Deleted Successfully");
        }
    }
}
