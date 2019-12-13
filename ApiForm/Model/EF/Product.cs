using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiForm.Model.EF
{
    class Product
    {
        public int Id { get; set; }//0

        public string Name { get; set; }//1

        public int CategoryId { get; set; }//2

        public string Image { get; set; }//3

        public double Price { get; set; }//4

        public decimal OriginalPrice { get; set; }//5

        public string Description { get; set; }//6

        public int AuthorId { get; set; }//7

        public int PublisherId { get; set; }//8

        public int Quantity { get; set; }//9

        public bool Status { get; set; }

    }
}

