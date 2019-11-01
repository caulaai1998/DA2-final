using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.Enums;
using TeduCoreApp.Utilities.Constants;

namespace TeduCoreApp.Data.EF
{
    public class DbInitializer
    {
        private readonly AppDbContext _context;
        private UserManager<AppUser> _userManager;
        private RoleManager<AppRole> _roleManager;
        public DbInitializer(AppDbContext context, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task Seed()
        {
            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new AppRole()
                {
                    Name = "Admin",
                    NormalizedName = "Admin",
                    Description = "Top manager"
                });
                await _roleManager.CreateAsync(new AppRole()
                {
                    Name = "Staff",
                    NormalizedName = "Staff",
                    Description = "Staff"
                });
                await _roleManager.CreateAsync(new AppRole()
                {
                    Name = "Customer",
                    NormalizedName = "Customer",
                    Description = "Customer"
                });
            }
            if (!_userManager.Users.Any())
            {
                await _userManager.CreateAsync(new AppUser()
                {
                    UserName = "admin",
                    FullName = "Administrator",
                    Email = "admin@gmail.com",
                    Balance = 0,
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                    Status = Status.Active
                }, "123654$");
                var user = await _userManager.FindByNameAsync("admin");
                await _userManager.AddToRoleAsync(user, "Admin");
            }
            if (!_context.Contacts.Any())
            {
                _context.Contacts.Add(new Contact()
                {
                    Id = CommonConstants.DefaultContactId,
                    Address = "No 36 Lane 133 Nguyen Phong Sac Cau Giay",
                    Email = "pandashop@gmail.com",
                    Name = "Panda Shop",
                    Phone = "0942 324 543",
                    Status = Status.Active,
                    Website = "http://pandashop.com",
                    Lat = 21.0435009,
                    Lng = 105.7894758
                });
            }
            if (_context.Functions.Count() == 0)
            {
                _context.Functions.AddRange(new List<Function>()
                {
                    new Function() {Id = "HOME", Name = "Trang chủ",ParentId = null,SortOrder = 0,Status = Status.Active,URL = "/",IconCss = "fa-desktop"  },
                    new Function() {Id = "DASHBOARD", Name = "Biểu đồ thống kê",ParentId = "HOME",SortOrder = 1,Status = Status.Active,URL = "/admin/home",IconCss = "fa-dashboard"  },

                 new Function() {Id = "SYSTEM", Name = "Hệ thống",ParentId = null,SortOrder = 1,Status = Status.Active,URL = "/",IconCss = "fa-desktop"  },
                    new Function() {Id = "ROLE", Name = "Quyền",ParentId = "SYSTEM",SortOrder = 1,Status = Status.Active,URL = "/admin/role/index",IconCss = "fa-home"  },

                    new Function() {Id = "USER", Name = "Người dùng",ParentId = "SYSTEM",SortOrder =3,Status = Status.Active,URL = "/admin/user/index",IconCss = "fa-home"  },

                    new Function() {Id = "PRODUCT",Name = "Quản lý sản phẩm",ParentId = null,SortOrder = 2,Status = Status.Active,URL = "/",IconCss = "fa-chevron-down"  },
                    new Function() {Id = "PRODUCT_CATEGORY",Name = "Thể loại",ParentId = "PRODUCT",SortOrder =1,Status = Status.Active,URL = "/admin/productcategory/index",IconCss = "fa-chevron-down"  },
                    new Function() {Id = "PRODUCT_LIST",Name = "Sản phẩm",ParentId = "PRODUCT",SortOrder = 2,Status = Status.Active,URL = "/admin/product/index",IconCss = "fa-chevron-down"  },
                    new Function() {Id = "BILL",Name = "Hóa đơn",ParentId = "PRODUCT",SortOrder = 3,Status = Status.Active,URL = "/admin/bill/index",IconCss = "fa-chevron-down"  },
                });
            }

            if (_context.Footers.Count(x => x.Id == CommonConstants.DefaultFooterId) == 0)
            {
                string content = "Footer";
                _context.Footers.Add(new Footer()
                {
                    Id = CommonConstants.DefaultFooterId,
                    Content = content
                });
            }

            if (_context.Colors.Count() == 0)
            {
                List<Color> listColor = new List<Color>()
                {
                    new Color() {Name="Black", Code="#000000" },
                    new Color() {Name="White", Code="#FFFFFF"},
                    new Color() {Name="Red", Code="#ff0000" },
                    new Color() {Name="Blue", Code="#1000ff" },
                };
                _context.Colors.AddRange(listColor);
            }
            if (_context.AdvertistmentPages.Count() == 0)
            {
                List<AdvertistmentPage> pages = new List<AdvertistmentPage>()
                {
                    new AdvertistmentPage() {Id="home", Name="Home",AdvertistmentPositions = new List<AdvertistmentPosition>(){
                        new AdvertistmentPosition(){Id="home-left",Name="Bên trái"}
                    } },
                    new AdvertistmentPage() {Id="product-cate", Name="Product category" ,
                        AdvertistmentPositions = new List<AdvertistmentPosition>(){
                        new AdvertistmentPosition(){Id="product-cate-left",Name="Bên trái"}
                    }},
                    new AdvertistmentPage() {Id="product-detail", Name="Product detail",
                        AdvertistmentPositions = new List<AdvertistmentPosition>(){
                        new AdvertistmentPosition(){Id="product-detail-left",Name="Bên trái"}
                    } },

                };
                _context.AdvertistmentPages.AddRange(pages);
            }


            if (_context.Slides.Count() == 0)
            {
                List<Slide> slides = new List<Slide>()
                {
                    new Slide() {Name="Slide 1",Image="/client-side/images/slider/slide-1.jpg",Url="#",DisplayOrder = 0,GroupAlias = "top",Status = true },
                    new Slide() {Name="Slide 2",Image="/client-side/images/slider/slide-2.jpg",Url="#",DisplayOrder = 1,GroupAlias = "top",Status = true },
                    new Slide() {Name="Slide 3",Image="/client-side/images/slider/slide-3.jpg",Url="#",DisplayOrder = 2,GroupAlias = "top",Status = true },

                };
                _context.Slides.AddRange(slides);
            }


            if (_context.Sizes.Count() == 0)
            {
                List<Size> listSize = new List<Size>()
                {
                    new Size() { Name="XXL" },
                    new Size() { Name="XL"},
                    new Size() { Name="L" },
                    new Size() { Name="M" },
                    new Size() { Name="S" },
                    new Size() { Name="XS" }
                };
                _context.Sizes.AddRange(listSize);
            }

            if (_context.Authors.Count() == 0)
            {
                List<Author> listAuthor = new List<Author>()
                {
                    new Author() {AuthorName="Xuân Diệu", SortOrder = 1, ParentId=null, Status = Status.Active},
                    new Author() {AuthorName="Hàn Mặc Tử", SortOrder = 2, ParentId=null, Status = Status.Active},
                    new Author() {AuthorName="Nguyễn Du", SortOrder = 3, ParentId=null, Status = Status.Active},
                    new Author() {AuthorName="Xuân Quỳnh", SortOrder = 1, ParentId=null, Status = Status.Active}
                };
                _context.Authors.AddRange(listAuthor);
            }

            if (_context.Publishers.Count() == 0)
            {
                List<Publisher> listPublisher = new List<Publisher>()
                {
                    new Publisher() {NamePublisher="Kim Đồng", SortOrder = 1, ParentId=null, Status = Status.Active},
                    new Publisher() {NamePublisher="NXB Lao động", SortOrder = 2, ParentId=null, Status = Status.Active},
                };
                _context.Publishers.AddRange(listPublisher);
            }


            if (_context.ProductCategories.Count() == 0)
            {
                List<ProductCategory> listProductCategory = new List<ProductCategory>()
                {
                   new ProductCategory() {  Name="Men shirt",SeoAlias="men-shirt",ParentId = null,Status=Status.Active,SortOrder=1,
                        Products = new List<Product>()
                        {
                             new Product(){Name = "Sản phẩm 1", DateCreated=DateTime.Now,Image="~/admin-side/assets/images/No-image-found.jpg",SeoAlias = "san-pham-1",Price = 1000,Status = Status.Active,OriginalPrice = 1000},
                            new Product(){Name = "Sản phẩm 5",DateCreated=DateTime.Now,Image="~/admin-side/assets/images/No-image-found.jpg",SeoAlias = "san-pham-5",Price = 1000,Status = Status.Active,OriginalPrice = 1000},
                            new Product(){Name = "Sản phẩm 2", DateCreated=DateTime.Now,Image="~/admin-side/assets/images/No-image-found.jpg",SeoAlias = "san-pham-2",Price = 1000,Status = Status.Active,OriginalPrice = 1000},
                            new Product(){Name = "Sản phẩm 3",DateCreated=DateTime.Now,Image="~/admin-side/assets/images/No-image-found.jpg",SeoAlias = "san-pham-3",Price = 1000,Status = Status.Active,OriginalPrice = 1000},
                            new Product(){Name = "Sản phẩm 4",DateCreated=DateTime.Now,Image="~/admin-side/assets/images/No-image-found.jpg",SeoAlias = "san-pham-4",Price = 1000,Status = Status.Active,OriginalPrice = 1000},
                        }
                    },
                     new ProductCategory() { Name="Women shirt",SeoAlias="women-shirt",ParentId = null,Status=Status.Active ,SortOrder=2,
                        Products = new List<Product>()
                        {
         new Product(){Name = "Sản phẩm 6",DateCreated=DateTime.Now,Image="~/admin-side/assets/images/No-image-found.jpg",SeoAlias = "san-pham-6",Price = 1000,Status = Status.Active,OriginalPrice = 1000},
                            new Product(){Name = "Sản phẩm 7",DateCreated=DateTime.Now,Image="~/admin-side/assets/images/No-image-found.jpg",SeoAlias = "san-pham-7",Price = 1000,Status = Status.Active,OriginalPrice = 1000},
                            new Product(){Name = "Sản phẩm 8",DateCreated=DateTime.Now,Image="~/admin-side/assets/images/No-image-found.jpg",SeoAlias = "san-pham-8",Price = 1000,Status = Status.Active,OriginalPrice = 1000},
                            new Product(){Name = "Sản phẩm 9",DateCreated=DateTime.Now,Image="~/admin-side/assets/images/No-image-found.jpg",SeoAlias = "san-pham-9",Price = 1000,Status = Status.Active,OriginalPrice = 1000},
                            new Product(){Name = "Sản phẩm 10",DateCreated=DateTime.Now,Image="~/admin-side/assets/images/No-image-found.jpg",SeoAlias = "san-pham-10",Price = 1000,Status = Status.Active,OriginalPrice = 1000},
                        }},
                   new ProductCategory() {Name="Men shoes",SeoAlias="men-shoes",ParentId = null,Status=Status.Active ,SortOrder=3,
                        Products = new List<Product>()
                        {
                             new Product(){Name = "Sản phẩm 11",DateCreated=DateTime.Now,Image="~/admin-side/assets/images/No-image-found.jpg",SeoAlias = "san-pham-11",Price = 1000,Status = Status.Active,OriginalPrice = 1000},
                            new Product(){Name = "Sản phẩm 12",DateCreated=DateTime.Now,Image="~/admin-side/assets/images/No-image-found.jpg",SeoAlias = "san-pham-12",Price = 1000,Status = Status.Active,OriginalPrice = 1000},
                            new Product(){Name = "Sản phẩm 13",DateCreated=DateTime.Now,Image="~/admin-side/assets/images/No-image-found.jpg",SeoAlias = "san-pham-13",Price = 1000,Status = Status.Active,OriginalPrice = 1000},
                            new Product(){Name = "Sản phẩm 14",DateCreated=DateTime.Now,Image="~/admin-side/assets/images/No-image-found.jpg",SeoAlias = "san-pham-14",Price = 1000,Status = Status.Active,OriginalPrice = 1000},
                            new Product(){Name = "Sản phẩm 15", DateCreated=DateTime.Now,Image="~/admin-side/assets/images/No-image-found.jpg",SeoAlias = "san-pham-15",Price = 1000,Status = Status.Active,OriginalPrice = 1000},
                        }},
                    new ProductCategory() { Name="Woment shoes",SeoAlias="women-shoes",ParentId = null,Status=Status.Active,SortOrder=4,
                        Products = new List<Product>()
                        {
                            new Product(){Name = "Sản phẩm 16", DateCreated=DateTime.Now, Image="~/admin-side/assets/images/No-image-found.jpg",SeoAlias = "san-pham-16",Price = 1000,Status = Status.Active,OriginalPrice = 1000},
                            new Product(){Name = "Sản phẩm 17", DateCreated=DateTime.Now,Image="~/admin-side/assets/images/No-image-found.jpg",SeoAlias = "san-pham-17",Price = 1000,Status = Status.Active,OriginalPrice = 1000},
                            new Product(){Name = "Sản phẩm 18",DateCreated=DateTime.Now,Image="~/admin-side/assets/images/No-image-found.jpg",SeoAlias = "san-pham-18",Price = 1000,Status = Status.Active,OriginalPrice = 1000},
                            new Product(){Name = "Sản phẩm 19",DateCreated=DateTime.Now,Image="~/admin-side/assets/images/No-image-found.jpg",SeoAlias = "san-pham-19",Price = 1000,Status = Status.Active,OriginalPrice = 1000},
                            new Product(){Name = "Sản phẩm 20", DateCreated=DateTime.Now,Image="~/admin-side/assets/images/No-image-found.jpg",SeoAlias = "san-pham-20",Price = 1000,Status = Status.Active,OriginalPrice = 1000},
                        }}
                };
                _context.ProductCategories.AddRange(listProductCategory);
            }

            if (!_context.SystemConfigs.Any(x => x.Id == "HomeTitle"))
            {
                _context.SystemConfigs.Add(new SystemConfig()
                {
                    Id = "HomeTitle",
                    Name = "Home's title",
                    Value1 = "Tedu Shop home",
                    Status = Status.Active
                });
            }
            if (!_context.SystemConfigs.Any(x => x.Id == "HomeMetaKeyword"))
            {
                _context.SystemConfigs.Add(new SystemConfig()
                {
                    Id = "HomeMetaKeyword",
                    Name = "Home Keyword",
                    Value1 = "shopping, sales",
                    Status = Status.Active
                });
            }
            if (!_context.SystemConfigs.Any(x => x.Id == "HomeMetaDescription"))
            {
                _context.SystemConfigs.Add(new SystemConfig()
                {
                    Id = "HomeMetaDescription",
                    Name = "Home Description",
                    Value1 = "Home tedu",
                    Status = Status.Active
                });
            }
            await _context.SaveChangesAsync();

        }
    }
}
