using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Bitad2021.Models;
using Newtonsoft.Json;

namespace Bitad2021.Data
{
    public class BitadServiceRest : IBitadService
    {

        private HttpClient _httpClient;

        private string Token { get; set; }

        public BitadServiceRest()
        {
            _httpClient = new HttpClient()
            {
                //TODO: TAKE ADDRESS OUT TO ENV OR SOME JSON
                //BaseAddress = new Uri("http://10.0.2.2:8080")
                BaseAddress = new Uri("http://192.168.0.101:8080")
                
            };

            Token = "";
        }
        
        public async Task<User> Login(string username, string password)
        {
            
            var json = await Task.Factory.StartNew(() => JsonConvert.SerializeObject(new
            {
                username,
                password
            }));

            var content = new StringContent (json, Encoding.UTF8, "application/json");

            var res = await _httpClient.PostAsync("/User/AuthenticateUser", content);
            
            
            if (!res.IsSuccessStatusCode)
                return null;
            
            Token = res.Headers.GetValues("authtoken").FirstOrDefault();

            var resJson = await res.Content.ReadAsStringAsync();
            
            return await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<User>(resJson));

        }

        public async Task<User> Register(string email, string firstName,string lastName, string username, string password)
        {
            var json = await Task.Factory.StartNew(() => JsonConvert.SerializeObject(new
            {
                email,
                firstName,
                lastName,
                username,
                password
            }));
            

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var res = await _httpClient.PostAsync("/User/RegisterUser", content);

            if (!res.IsSuccessStatusCode)
                return null;

            var resJson = await res.Content.ReadAsStringAsync();

            return await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<User>(resJson));
            
        }

        public async Task<User> GetUser()
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Agenda>> GetAllAgendas()
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            var res = await _httpClient.GetAsync("/Agenda/GetAgendas");
            
            //Token = res.Headers.GetValues("authtoken").FirstOrDefault();
            
            if (!res.IsSuccessStatusCode)
                return null;
            
            var resJson = await res.Content.ReadAsStringAsync();
            

            return await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<IEnumerable<Agenda>>(resJson));
        }

        public async Task<IEnumerable<Sponsor>> GetAllSponsors()
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Workshop>> GetAllWorkshops()
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            var res = await _httpClient.GetAsync("/Agenda/GetWorkshops");
            
            //Token = res.Headers.GetValues("authtoken").FirstOrDefault();
            
            if (!res.IsSuccessStatusCode)
                return null;
            
            var resJson = await res.Content.ReadAsStringAsync();
            

            return await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<IEnumerable<Workshop>>(resJson));
        }

        public async Task<QrCodeResponse> RedeemQrCode(string qrCode)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            
            var parameters = new Dictionary<string, string>();
            parameters["code"] = qrCode;
            
            var res = await _httpClient.PostAsync("/QrCodeRedeem/RedeemQrCode",new FormUrlEncodedContent(parameters));
            
            Token = res.Headers.GetValues("authtoken").FirstOrDefault();
            
            if (!res.IsSuccessStatusCode)
                return null;
            
            var resJson = await res.Content.ReadAsStringAsync();
            

            return await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<QrCodeResponse>(resJson));
        }
    }
}