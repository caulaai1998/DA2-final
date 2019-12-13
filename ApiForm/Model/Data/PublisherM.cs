using ApiForm.Model.EF;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ApiForm.Model.Data
{
    class PublisherM
    {
        HttpClient client;
        List<Publisher> listNXB;
        public PublisherM()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44344/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string authInfo = "cus1:cus1";
            authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authInfo);
            Load();
        }
        void Load()
        {
            HttpResponseMessage response = client.GetAsync("api/NXB").Result;
            if (response.IsSuccessStatusCode)
            {
                listNXB = JsonConvert.DeserializeObject<List<Publisher>>(response.Content.ReadAsStringAsync().Result);
            }
        }

        public List<Publisher> GetNXBs()
        {
            Load();
            return listNXB;
        }
    }
}

