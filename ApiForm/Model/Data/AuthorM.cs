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
    class AuthorM
    {
        HttpClient client;
        List<Author> listTacGia;
        public AuthorM()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44344/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string authInfo = "htd:htd";
            authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authInfo);
            Load();
        }
        void Load()
        {
            HttpResponseMessage response = client.GetAsync("api/Author").Result;
            if (response.IsSuccessStatusCode)
            {
                listTacGia = JsonConvert.DeserializeObject<List<Author>>(response.Content.ReadAsStringAsync().Result);
            }
        }

        public List<Author> GetTgs()
        {
            Load();
            return listTacGia;
        }
    }
}
