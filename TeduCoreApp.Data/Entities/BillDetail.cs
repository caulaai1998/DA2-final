using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TeduCoreApp.Infrastructure.SharedKernel;

namespace TeduCoreApp.Data.Entities
{
    [Table("BillDetails")]
    public class BillDetail : DomainEntity<int>
    {
        public BillDetail() { }

        public BillDetail(int id, int billId, int productId, int quantity, decimal price)
        {
            Id = id;
            BillId = billId;
            ProductId = productId;
            Quantity = quantity;
            Price = price;
            
        }

        public BillDetail(int billId, int productId, int quantity, decimal price)
        {
            BillId = billId;
            ProductId = productId;
            Quantity = quantity;
            Price = price;
         
        }
        public int BillId { set; get; }

        public int ProductId { set; get; }

        public int Quantity { set; get; }

        public decimal Price { set; get; }

        [ForeignKey("BillId")]
        public virtual Bill Bill { set; get; }

        [ForeignKey("ProductId")]
        public virtual Product Product { set; get; }

       
    }
}
