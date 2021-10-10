using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
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
                //BaseAddress = new Uri("http://192.168.0.101:8080")
                BaseAddress = new Uri("http://212.106.184.93:8080")
                
            };

            Token = "";
        }
        
        public async Task<(User user, HttpStatusCode code)> Login(string username, string password)
        {
            
            var json = await Task.Factory.StartNew(() => JsonConvert.SerializeObject(new
            {
                username,
                password
            }));

            var content = new StringContent (json, Encoding.UTF8, "application/json");

            var res = await _httpClient.PostAsync("/User/AuthenticateUser", content);
            
            
            if (!res.IsSuccessStatusCode)
                return (null,res.StatusCode);
            
            Token = res.Headers.GetValues("authtoken").FirstOrDefault();

            var resJson = await res.Content.ReadAsStringAsync();
            
            return (await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<User>(resJson)),res.StatusCode);

        }

        public User LoginSync(string username, string password)
        {
            var json = JsonConvert.SerializeObject(new
            {
                username,
                password
            });

            var content = new StringContent (json, Encoding.UTF8, "application/json");

            var res = _httpClient.PostAsync("/User/AuthenticateUser", content).Result;
            
            
            if (!res.IsSuccessStatusCode)
                return null;
            
            Token = res.Headers.GetValues("authtoken").FirstOrDefault();

            var resJson = res.Content.ReadAsStringAsync().Result;
            
            return JsonConvert.DeserializeObject<User>(resJson);
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
            var res = await _httpClient.GetAsync("/Workshop/GetWorkshops");
            
            //Token = res.Headers.GetValues("authtoken").FirstOrDefault();
            
            if (!res.IsSuccessStatusCode)
                return null;
            
            var resJson = await res.Content.ReadAsStringAsync();
            

            return await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<IEnumerable<Workshop>>(resJson));
        }

        public async Task<(QrCodeResponse, HttpStatusCode code)> RedeemQrCode(string qrCode)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            
            var parameters = new Dictionary<string, string>();
            parameters["code"] = qrCode;
            
            var res = await _httpClient.PostAsync("/QrCodeRedeem/RedeemQrCode",new FormUrlEncodedContent(parameters));

            if (!res.IsSuccessStatusCode)
                return (null,res.StatusCode);
            
            Token = res.Headers.GetValues("authtoken").FirstOrDefault();
            
            var resJson = await res.Content.ReadAsStringAsync();
            

            return (await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<QrCodeResponse>(resJson)),res.StatusCode);
        }

        public async Task<bool> SelectWorkshop(string workshopCode)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            
            var parameters = new Dictionary<string, string>();
            parameters["workshopCode"] = workshopCode;
            
            var response = await _httpClient.PutAsync("/User/SelectWorkshop",new FormUrlEncodedContent(parameters));
            Token = response.Headers.GetValues("authtoken").FirstOrDefault();
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> RequestActivationResend(string username)
        {
            var parameters = new Dictionary<string, string>();
            parameters["username"] = username;
            
            var response = await _httpClient.PutAsync("/User/RequestActivationResend",new FormUrlEncodedContent(parameters));
            
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> IssuePasswordReset(string username)
        {
            var parameters = new Dictionary<string, string>();
            parameters["username"] = username;
            
            var response = await _httpClient.PutAsync("/User/IssuePasswordReset",new FormUrlEncodedContent(parameters));

            return response.IsSuccessStatusCode;
        }
    }
}