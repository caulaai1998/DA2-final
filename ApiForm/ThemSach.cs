using ApiForm.Model.Data;
using ApiForm.Model.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApiForm
{
    public partial class ThemSach : Form
    {

        AuthorM tacgiaDAO;
        ProductCategoryM chudeDAO;
        PublisherM nxbDAO;
        ProductM productM;

        List<Product> listSach;
        List<Author> listTacGia;
        List<ProductCategory> listChuDe;
        List<Publisher> listNXB;
        public ThemSach()
        {
            InitializeComponent();
            LoadData();
        }

        void LoadData()
        {
            productM = new ProductM();
            listSach = productM.GetSachs();
            tacgiaDAO = new AuthorM();
            chudeDAO = new ProductCategoryM();
            nxbDAO = new PublisherM();
            listTacGia = tacgiaDAO.GetTgs();
            listChuDe = chudeDAO.GetCtgs();
            listNXB = nxbDAO.GetNXBs();

            cbTacgia.DataSource = listTacGia;
            cbTacgia.DisplayMember = "TenTacGia";
            cbTacgia.ValueMember = "IdTacGia";

            cbChuDe.DataSource = listChuDe;
            cbChuDe.DisplayMember = "TenChuDe";
            cbChuDe.ValueMember = "IdChuDe";

            cbNXB.DataSource = listNXB;
            cbNXB.DisplayMember = "TenNXB";
            cbNXB.ValueMember = "IdNXB";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                double gia;
                int soluong;
                if (!String.IsNullOrEmpty(txtBookName.Text) && int.TryParse(nSoLuong.Value.ToString(), out soluong) && double.TryParse(nPrice.Value.ToString(), out gia))
                {
                    Product sach = new Product();
                    sach.Name = txtBookName.Text;
                    sach.Price = gia;
                    sach.Quantity = soluong;
                    sach.flag = true;
                    sach.AuthorId = int.Parse(cbTacgia.SelectedValue.ToString());
                    sach.CategoryId = int.Parse(cbChuDe.SelectedValue.ToString());
                    sach.PublisherId = int.Parse(cbNXB.SelectedValue.ToString());
                    sach.Description = txtDes.Text;
                    //sach.HinhAnh=

                    productM = new ProductM();
                    if (productM.Add(sach) != null)
                        this.Close();
                    else
                        MessageBox.Show("Lỗi");

                }
            }
            catch (Exception ex) { }
        }
    }
}
