using System;
using System.Data.Entity;
using System.Globalization;
using System.Security.Claims;
using System.Security.Principal;
//using GoldenHeart.Migrations;

namespace GoldenHeart.Models
{
    public static class IdentityExtensions
    {
        public static T GetUserId<T>(this IIdentity identity) where T : IConvertible
        {
            if (identity == null)
            {
                throw new ArgumentNullException("identity");
            }
            var ci = identity as ClaimsIdentity;
            if (ci != null)
            {
                var id = ci.FindFirst(ClaimTypes.NameIdentifier);
                if (id != null)
                {
                    return (T)Convert.ChangeType(id.Value, typeof(T), CultureInfo.InvariantCulture);
                }
            }
            return default(T);
        }
        public static string GetUserRole(this IIdentity identity)
        {
            if (identity == null)
            {
                throw new ArgumentNullException("identity");
            }
            var ci = identity as ClaimsIdentity;
            string role = "";
            if (ci != null)
            {
                var id = ci.FindFirst(ClaimsIdentity.DefaultRoleClaimType);
                if (id != null)
                    role = id.Value;
            }
            return role;
        }
    }

    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Docs { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Reply> Replies { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Reception> Receptions { get; set; }
        public DbSet<DeleteTime> DeleteTime { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<MessageStaff> MessagesStaff { get; set; }

        public ApplicationDbContext() : base("ServerDbConnection")
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}