using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace SBMS.Models
{
    public class SBMSDbContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        //
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, add the following
        // code to the Application_Start method in your Global.asax file.
        // Note: this will destroy and re-create your database with every model change.
        //
        // System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<SBMS.Models.SBMSDbContext>());
        //static bool initialized = false;

        //public SBMSDbContext()
        //  : base("name=SBMSDbContext")
        //{
        //  //if (!initialized && Products.Local.Count == 0)
        //  //{
        //  //  InitializeOnStartup(Products);
        //  //  this.SaveChanges();
        //  //  initialized = true;
        //  //}
        //}

        public SBMSDbContext()
            : base("name=SBMSDbContext")
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<SupportRequest> SupportRequests { get; set; }

        public DbSet<Cart> Carts { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<ContractProposal> Contracts { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Milestone> Milestones { get; set; }

        public DbSet<ProjectIssue> ProjectIssues { get; set; }

        public DbSet<CustomerIssue> CustomerIssues { get; set; }

        public DbSet<MeetingAndEvent> MeetingAndEvents { get; set; }

        public void InitializeProductsOnStartup()
        {
            Fill(Path(), Products);
        }

        public string Path()
        {
            return HttpContext.Current.Server.MapPath("~\\App_Data\\ProductData.xml");
        }

        public bool Fill(string path, DbSet<Product> products)
        {
            try
            {
                XDocument doc = XDocument.Load(path);
                IEnumerable<XElement> product = from prod in doc.Elements("ProductCatalog").Elements("Product")
                    select prod;
                foreach (XElement elem in product)
                {
                    var newProduct = new Product();
                    newProduct.Name = elem.Element("Name").Value;
                    newProduct.Icon = elem.Element("Icon").Value;
                    newProduct.PunchLine = elem.Element("PunchLine").Value;
                    newProduct.Message = elem.Element("Message").Value;
                    newProduct.Price = decimal.Parse(elem.Element("Price").Value);
                    products.Add(newProduct);
                }
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}