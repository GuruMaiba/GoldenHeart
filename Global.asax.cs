using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using GoldenHeart.Models;

namespace GoldenHeart
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            if (db.Roles.FirstOrDefault(r => r.Name == "Admin") == null)
            {
                var role = new Role { Name = "Admin" };
                db.Roles.Add(role);
                role = new Role { Name = "Doc" };
                db.Roles.Add(role);
                db.SaveChanges();
            }

            if (db.Services.FirstOrDefault(s => s.Name == "Онкология") == null)
            {
                var service = new Service { Name = "Онкология" };
                db.Services.Add(service);
                service = new Service { Name = "Неврология" };
                db.Services.Add(service);
                db.SaveChanges();
            }

            if (db.Docs.FirstOrDefault(t => t.Email == "gold@gmail.com") == null)
            {
                var pass = "147258369Zz";
                pass = Hash.GetHashString(pass);
                var role = db.Roles.FirstOrDefault(r => r.Name == "Admin");
                var service = db.Services.FirstOrDefault(s => s.Name == "Онкология");
                db.Docs.Add(new User
                {
                    Email = "gold@gmail.com",
                    HashPassword = pass,
                    Role = role,
                    FirstName = "Всея",
                    Name = "Золотое",
                    LastName = "Сердце",
                    Birthday = "Незаконно рождённый",
                    Phone = "7 (777) 777-77-77",
                    Avatar = "add-doctor.png",
                    BeginLeave = DateTime.Now,
                    EndLeave = DateTime.Now,
                    Certificate = "Суперский",
                    Education = "Выше звёзд",
                    InstitutionWorks = "До начала времён",
                    Position = "Администратор",
                    Service = null
                });
                db.SaveChanges();
            }

            db.Dispose();

        }
    }
}
