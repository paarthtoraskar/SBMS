using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using SBMS.Models;
using WebMatrix.WebData;

namespace SBMS
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            if (!WebSecurity.Initialized)
            {
                WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "Username", true);
            }

            Database.SetInitializer(new UsersContextDbInitializer());
            var usersContext = new UsersContext();
            usersContext.Database.Initialize(true);

            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<SBMSDbContext>());

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
        }

        protected void Session_Start()
        {
            //Guid cartId = Guid.NewGuid();
            //Session["CartId"] = cartId.ToString();

            //decimal totalCartPayment = 0;
            //Session["TotalCartPayment"] = totalCartPayment;

            //List<Product> products = new List<Product>();
            //Session["ProductsInCart"] = products;

            Session["CartId"] = Guid.NewGuid();
            Session["TotalCartPayment"] = new decimal(0.0);
            Session["ProductsInCart"] = new List<Product>();
        }

        protected void Session_End()
        {
            //Session.RemoveAll();

            Session["TotalCartPayment"] = 0.0;
            //List<Product> products = Session["ProductsInCart"] as List<Product>;
            //products.Clear();
            //Session["ProductsInCart"] = products;
            (Session["ProductsInCart"] as List<Product>).Clear();
        }
    }

    public class UsersContextDbInitializer : DropCreateDatabaseIfModelChanges<UsersContext>
    {
        protected override void Seed(UsersContext userContext)
        {
            if (!WebSecurity.Initialized)
            {
                WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "Username", true);
            }

            if (!Roles.RoleExists("admin"))
            {
                Roles.CreateRole("admin");
            }
            if (!WebSecurity.UserExists("admin"))
            {
                WebSecurity.CreateUserAndAccount("admin", "admin00");
            }
            if (!Roles.GetRolesForUser("admin").Contains("admin"))
            {
                Roles.AddUsersToRoles(new[] {"admin"}, new[] {"admin"});
            }
            base.Seed(userContext);
        }
    }
}