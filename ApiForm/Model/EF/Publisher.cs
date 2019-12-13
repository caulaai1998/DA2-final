using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiForm.Model.EF
{
    class Publisher
    {
        public int Id { get; set; }

        public string NamePublisher { get; set; }

        public int SortOrder { get; set; }
    }
}
