using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiForm.Model.EF
{
    class Author
    {
        public int Id { get; set; }

        public string AuthorName { get; set; }

        public int SortOrder { get; set; }

        public bool Status { get; set; }
    }
}
