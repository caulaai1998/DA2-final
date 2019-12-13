using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using ApiForm.Model.EF;
using Newtonsoft.Json;
using ApiForm.Model.Data;
using ApiForm.Model.Detail;

namespace ApiForm.Model.Data
{
    class ProductM
    {
        HttpClient client;
        List<Product> listproduct;

        public ProductM()
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
            HttpResponseMessage response = client.GetAsync("api/product").Result;
            if (response.IsSuccessStatusCode)
            {
                listproduct = JsonConvert.DeserializeObject<List<Product>>(response.Content.ReadAsStringAsync().Result);
            }
        }
        public List<Product> GetSachs()
        {
            Load();
            return listproduct;
        }

        public List<ProductDT> ToListDTO(List<Product> list)
        {
            AuthorM tgDao = new AuthorM();
            List<Author> listTacGia = tgDao.GetTgs();

            ProductCategoryM cdDao = new ProductCategoryM();
            List<ProductCategory> listChuDe = cdDao.GetCtgs();

            PublisherM nxbDao = new PublisherM();
            List<Publisher> listNXB = nxbDao.GetNXBs();


            List<ProductDT> listSachDTO = new List<ProductDT>();

            foreach (Product sach in list)
            {
                ProductDT tmp = new ProductDT();
                tmp.IdSach = sach.Id;
                tmp.TenSach = sach.Name;
                tmp.HinhAnh = sach.Image;
                tmp.Gia = sach.Price;
                tmp.MoTa = sach.Description;
                tmp.SoLuong = sach.Quantity;
                tmp.TenTacGia = listTacGia.FirstOrDefault(x => x.Id == sach.AuthorId).AuthorName;
                tmp.TenChuDe = listChuDe.FirstOrDefault(x => x.Id == sach.CategoryId).Name;
                tmp.TenNXB = listNXB.FirstOrDefault(x => x.Id == sach.PublisherId).NamePublisher;
                listSachDTO.Add(tmp);
            }
            return listSachDTO;
        }
        public Product Add(Product sach)
        {
            Load();
            if (!String.IsNullOrEmpty(sach.Name) && !String.IsNullOrEmpty(sach.CategoryId.ToString()) && !String.IsNullOrEmpty(sach.AuthorId.ToString()) && !String.IsNullOrEmpty(sach.PublisherId.ToString()) && sach.Price >= 1000 && sach.Quantity > 0)
            {
                if (!listproduct.Any(x => x.Name == sach.Name &&
                     x.AuthorId == sach.AuthorId &&
                     x.CategoryId == sach.CategoryId &&
                     x.PublisherId == sach.PublisherId))
                {
                    HttpResponseMessage response = client.PostAsJsonAsync("api/product", sach).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        Product rs = sach;
                        return rs;
                    }
                }
            }
            return null;
        }
        public bool Update(int id, Product sach)
        {
            Load();
            if (listproduct.Any(x => x.Id == id))
            {
                if (!String.IsNullOrEmpty(sach.Name) && !String.IsNullOrEmpty(sach.CategoryId.ToString()) && !String.IsNullOrEmpty(sach.AuthorId.ToString()) && !String.IsNullOrEmpty(sach.PublisherId.ToString()) && sach.Quantity > 0)
                {
                    HttpResponseMessage response = client.PutAsJsonAsync("api/product/" + id, sach).Result;
                    if (response.IsSuccessStatusCode)
                        return true;
                    else
                        return false;
                }
            }
            return false;
        }
        public bool Remove(int id)
        {
            Load();
            if (listproduct.Any(x => x.Id == id))
            {
                HttpResponseMessage response = client.DeleteAsync("api/product/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
