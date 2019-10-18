using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TeduCoreApp.Data.Enums;
using TeduCoreApp.Infrastructure.SharedKernel;

namespace TeduCoreApp.Data.Entities
{
    [Table("Publishers")]
    public class Publisher : DomainEntity<int>
    {
        public Publisher()
        {

        }

        public Publisher(string name, int? parentId, int sortOrder, Status status)
        {
            NamePublisher = name;
            ParentId = parentId;
            SortOrder = sortOrder;
            Status = status;
        }

        public Publisher(int id, string name, int? parentId, int sortOrder, Status status)
        {
            Id = id;
            NamePublisher = name;
            ParentId = parentId;
            SortOrder = sortOrder;
            Status = status;
        }

        public string NamePublisher { get; set; }
        public int? ParentId { get; set; }
        public int SortOrder { get; set; }

        public Status Status { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
