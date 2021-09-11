using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CEI_MVC_CORE_Proj.Models;
using Microsoft.AspNetCore.Identity;

namespace CEI_MVC_CORE_Proj.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, string, IdentityUserClaim<string>, UserRoleRel, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ProductTagRel>().HasKey(sc => new { sc.FK_ProductId, sc.FK_TagId });

            builder.Entity<ProductTagRel>()
                .HasOne<Product>(sc => sc.Product)
                .WithMany(s => s.ProductTagRels)
                .HasForeignKey(sc => sc.FK_ProductId);


            builder.Entity<ProductTagRel>()
                .HasOne<Tag>(sc => sc.Tag)
                .WithMany(s => s.ProductTagRels)
                .HasForeignKey(sc => sc.FK_TagId);


            /*APP USER ROLE Relation*/
            builder.Entity<UserRoleRel>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(ro => ro.UserRoleRel)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(ro => ro.UserRoleRel)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            /*Composite key for payment method*/
            builder.Entity<PaymentMethod>()
                     .HasKey(c => new { c.FK_ProductId, c.Method });
            /*Composite key for product images*/
            builder.Entity<ProductImage>()
                    .HasKey(c => new { c.FK_ProductId, c.Path });

            /* set deleting behavior */
            //builder.Entity<AppUser>().HasMany<RequestToAdmin>(c => c.Requests).WithOne(ro => ro.User).OnDelete(DeleteBehavior.Cascade);
            //builder.Entity<AppUser>().HasMany<Product>(c => c.Products).WithOne(o => o.Vendor).OnDelete(DeleteBehavior.Cascade);

            /********************** Seeding the database ***************************/
            string adminId = Guid.NewGuid().ToString();
            string vendorId = Guid.NewGuid().ToString();
            string customerId1 = Guid.NewGuid().ToString();
            string customerId2 = Guid.NewGuid().ToString();
            //1- Roles
            builder.Entity<AppRole>().HasData(new List<AppRole>
            {
                    new AppRole {
                        Id = "1",
                        Name = "Admin",
                        NormalizedName = "ADMIN"
                    },
                    new AppRole {
                        Id = "2",
                        Name = "Vendor",
                        NormalizedName = "VENDOR"
                    },
            }.ToArray());
            //2- Users
            var hasher = new PasswordHasher<AppUser>();
            builder.Entity<AppUser>().HasData(new List<AppUser> {
                new AppUser
                {
                    Id = adminId, // primary key
                    UserName = "admin",
                    FirstName = "admin",
                    LastName = "admin",
                    Email = "admin@domain.com",
                    ProfilePictureLink="/images/UserImages/admin.jpg",
                    NormalizedUserName = "ADMIN",
                    PasswordHash = hasher.HashPassword(null, "P@ssw0rd"),
                    SecurityStamp = "SecurityStamp"
                },
                new AppUser
                {
                    Id = vendorId, // primary key
                    UserName = "vendor",
                    FirstName = "vendor",
                    LastName = "vendor",
                    Email = "vendor@domain.com",
                    ProfilePictureLink = "/images/UserImages/vendor.jpg",
                    NormalizedUserName = "VENDOR",
                    PasswordHash = hasher.HashPassword(null, "P@ssw0rd"),
                    SecurityStamp = "SecurityStamp"
                },
                  new AppUser
                {
                    Id = customerId1, // primary key
                    UserName = "customer1",
                    FirstName = "customer",
                    LastName = "customer",
                    Email = "customer1@domain.com",
                    ProfilePictureLink = "/images/UserImages/customer1.jpg",
                    NormalizedUserName = "CUSTOMER1",
                    PasswordHash = hasher.HashPassword(null, "P@ssw0rd"),
                    SecurityStamp = "SecurityStamp"
                },
                new AppUser
                {
                    Id = customerId2, // primary key
                    UserName = "customer2",
                    FirstName = "customer",
                    LastName = "customer",
                    Email = "customer2@domain.com",
                    ProfilePictureLink = "/images/UserImages/customer2.jpg",
                    NormalizedUserName = "CUSTOMER2",
                    PasswordHash = hasher.HashPassword(null, "P@ssw0rd"),
                    SecurityStamp = "SecurityStamp"
                }
          }.ToArray());
            //3- Users Role Relation
            builder.Entity<UserRoleRel>().HasData(new List<UserRoleRel> {
                new UserRoleRel
                {
                    RoleId = "1", // for admin username
                    UserId = adminId  // for admin role
                },
                 new UserRoleRel
                 {
                     RoleId = "2", // for vendor role
                     UserId = adminId  // for admin username
                 },
                new UserRoleRel
                {
                    RoleId = "2", // for vendor username
                    UserId = vendorId  // for vendor role
                }
            }.ToArray());
            //4- Categories
            builder.Entity<Category>().HasData(new List<Category> {
                new Category { Id = 1, Name = "Arts & Crafts" },
                new Category { Id = 2, Name = "Automotive" },
                new Category { Id = 3, Name = "Baby" },
                new Category { Id = 4, Name = "Books" },
                new Category { Id = 5, Name = "Eletronics" },
                new Category { Id = 6, Name = "Women's Fashion" },
                new Category { Id = 7, Name = "Men's Fashion" },
                new Category { Id = 8, Name = "Health & Household" },
                new Category { Id = 9, Name = "Home & Kitchen" },
                new Category { Id = 10, Name = "Military Accessories" },
                new Category { Id = 11, Name = "Movies & Television" },
                new Category { Id = 12, Name = "Sports & Outdoors" },
                new Category { Id = 13, Name = "Tools & Home Improvement" },
                new Category { Id = 14, Name = "Toys & Games" },
                new Category { Id = 15, Name = "Others" }
            }.ToArray());
            //5- tags
            builder.Entity<Tag>().HasData(new List<Tag> { 
                new Tag { Id = 1, Name = "AllPurpose" },
                new Tag { Id = 2, Name = "TagExample" }
                }.ToArray());

            //6- Products
            Random r = new Random();
            //6-a create the list of products
            List<string> dic = new List<string> { "MaintainIT", "KinderZoo", "Mixed Feelings", "Norse Nurse", "Mineral Massage", "Media Pet", "EdWeb", "Candy Floss", "Transcoder", "Pubisphere", "BrainWire", "Package Fax", "Chaucer’s Choice", "Supplex", "Dutch Oven Gretel", "PoshPet Collars", "MailPlus", "Hairs of the Dog", "Cherry Potter", "Comfidant", "Comvex", "Old Plywood (Scotch)", "Penultimate Sharpie", "Orange Bounce", "machPRINT", "Ad Liberace", "Apptrans", "Nuclear Winter Squash", "Eargo", "Brand Aid", "Personal Mars Rover Vehicle" };
            Product[] pros = new Product[12];
            ProductTagRel[] tagRel = new ProductTagRel[12];
            ProductImage[] img = new ProductImage[24];
            PaymentMethod[] payment = new PaymentMethod[24];

            for (int i = 0; i < pros.Length; i++) {
                int currentID = i+1;
                img[2 * i] = new ProductImage { FK_ProductId = currentID, Path = $"/images/ProductImages/{i}_0.jpg" };
                img[2 * i + 1] = new ProductImage { FK_ProductId = currentID, Path = $"/images/ProductImages/{i}_1.jpg" };

                payment[2*i] = new PaymentMethod { FK_ProductId=currentID, Method = (Methods)r.Next(0, 2) };
                payment[2*i+1] = new PaymentMethod { FK_ProductId = currentID, Method = (Methods)r.Next(3, 5)};

                tagRel[i] = new ProductTagRel { FK_ProductId = currentID, FK_TagId = r.Next(1, 2)  };


                pros[i] = new Product
                {
                    Id =currentID ,
                    FK_VendorId = vendorId,
                    FK_CategoryId =r.Next(1,14),
                    Description = "Placeholder Product for shwoing",
                    Name = dic[i] ,
                    Price = 300 + i,
                    OfferPrice = r.Next(5, 200) < 100? 300 + i: 100 ,
                    Quantity = (uint) r.Next(8,100) ,
                };
            }
            //6-b add the list of products, payment methods and imgs
            builder.Entity<Product>().HasData(pros);
            builder.Entity<ProductImage>().HasData(img);
            builder.Entity<PaymentMethod>().HasData(payment);
            builder.Entity<ProductTagRel>().HasData(tagRel);

            //7- adding requests and transactions
            builder.Entity<RequestToAdmin>().HasData(new List<RequestToAdmin>
            {
                new RequestToAdmin{Id=1, FK_UserId= vendorId , Status= RequestStatus.Pending , Type= RequestType.AddNewCategory , Data = "Summer Sports Wear"   },
                new RequestToAdmin{Id=2, FK_UserId= customerId1, Status= RequestStatus.Pending , Type= RequestType.RequestVendorRole , Data = "This user is requesting vendor role"   },
                new RequestToAdmin{Id=3, FK_UserId= customerId2, Status= RequestStatus.Pending , Type= RequestType.RequestVendorRole , Data = "This user is requesting vendor role"   }
            }.ToArray());

            DateTime date = DateTime.Now;
            long timestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            string CheckOutId = timestamp.ToString() + "_" + customerId1.Substring(0, 8);
            string CheckOutId2 = timestamp.ToString() + "_" + customerId2.Substring(0, 8);
            builder.Entity<Transaction>().HasData(new List<Transaction>
            {
                new Transaction {Id=1,  FK_CustomerId= customerId1, Status= TransactionStatus.Pending , CheckOutId=CheckOutId , DateTime= date , PaymentMethod = Methods.Cheque, FK_ProductId = 1 , Quantity=3 },
                new Transaction {Id=2,  FK_CustomerId= customerId1, Status= TransactionStatus.Pending , CheckOutId=CheckOutId , DateTime= date , PaymentMethod = Methods.Paypal, FK_ProductId = 2 , Quantity=1 },
                new Transaction {Id=3,  FK_CustomerId= customerId2, Status= TransactionStatus.Pending , CheckOutId=CheckOutId2 , DateTime= date , PaymentMethod = Methods.MasterCard, FK_ProductId = 5 , Quantity=3 },
                new Transaction {Id=4,  FK_CustomerId= customerId2, Status= TransactionStatus.Pending , CheckOutId=CheckOutId2 , DateTime= date , PaymentMethod = Methods.DirectBankTransfer, FK_ProductId = 2 , Quantity=4 },
            }.ToArray());
        }
            
        //Entities:
        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<RequestToAdmin> Requests { get; set; }

        public virtual DbSet<Tag> Tags { get; set; }

        public virtual DbSet<Transaction> Transactions { get; set; }


        //Multivalued Attributes:
        public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }
        public virtual DbSet<ProductImage> ProductImages { get; set; }

        //ManyToManyRels
        public virtual DbSet<ProductTagRel> ProductTagRels { get; set; }
    }
}
