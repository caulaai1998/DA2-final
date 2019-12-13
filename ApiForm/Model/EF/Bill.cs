using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiForm.Model.EF
{
    class Bill
    {
        public int Id { get; set; }

        public string CustomerName { set; get; }

        public string CustomerAddress { set; get; }

        public string CustomerMobile { set; get; }

        public string CustomerMessage { set; get; }

        public DateTime DateCreate { get; set; }
    }
}
