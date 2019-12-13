using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiForm.Model.EF
{
    class ProductCategory
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int SortOrder { set; get; }
    }
}
