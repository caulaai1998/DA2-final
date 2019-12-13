using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiForm.Model.Detail
{
    class ProductDT
    {
        public int IdSach { get; set; }//0

        [DisplayName("Tên sách")]
        public string TenSach { get; set; }//1
        public string HinhAnh { get; set; }//2

        [DisplayName("Giá")]
        public double Gia { get; set; }//3

        public string MoTa { get; set; }//4

        [DisplayName("Số lượng")]
        public int SoLuong { get; set; }//5

        [DisplayName("Lượt xem")]
        public string TenTacGia { get; set; }//6
        public string TenChuDe { get; set; }//7
        public string TenNXB { get; set; }//8
    }
}
