using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Bitad2021.Models;

namespace Bitad2021.Data
{
    public class BitadServiceRest : IBitadService
    {
        
        private HttpClient _httpClient = new HttpClient()
        {
            //TODO: TAKE ADDRESS OUT TO ENV OR SOME JSON
            BaseAddress = new Uri("http://localhost:8080")
        };

        private string _token = "";
        
        public async Task<User> Login(string login, string password)
        {
            throw new System.NotImplementedException();
        }

        public async Task<User> Register(string login, string password, string email)
        {
            throw new System.NotImplementedException();
        }

        public async Task<User> GetUser()
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Agenda>> GetAllAgendas()
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Sponsor>> GetAllSponsors()
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Workshop>> GetAllWorkshops()
        {
            throw new System.NotImplementedException();
        }
    }
}