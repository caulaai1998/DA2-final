using System;
using System.Collections.Generic;
using System.Text;
using TeduCoreApp.Application.ViewModels.Product;
using TeduCoreApp.Data.Enums;

namespace TeduCoreApp.Application.ViewModels
{
    public class PublisherViewModel
    {
        public int Id { get; set; }

        public string NamePublisher { get; set; }
        public int? ParentId { get; set; }

        public int SortOrder { get; set; }
        public Status Status { get; set; }

        public virtual ICollection<ProductViewModel> Products { get; set; }
    }
}
