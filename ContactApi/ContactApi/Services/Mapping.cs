using AutoMapper;
using ContactApi.DTOs;
using ContactApi.Interfaces;
using ContactApi.Models;
using ContactApi.Repositories;

namespace ContactApi.Services
{
    public class Mapping:Profile
    {
        public Mapping()
        {
            CreateMap<Contact,ContactDTO>().ReverseMap();
        }
    }
}
