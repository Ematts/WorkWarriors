using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WorkWarriors.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        public string Screen_Name { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }


    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Homeowner> Homeowners { get; set; }
        public DbSet<File> Files { get; set; }
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<WorkWarriors.Models.Contractor> Contractors { get; set; }

        public System.Data.Entity.DbSet<WorkWarriors.Models.ServiceRequest> ServiceRequests { get; set; }

        //public System.Data.Entity.DbSet<WorkWarriors.Models.BidAcceptance> BidAcceptances { get; set; }

        public System.Data.Entity.DbSet<WorkWarriors.Models.ContractorAcceptedBids> ContractorAcceptedBids { get; set; }

        public System.Data.Entity.DbSet<WorkWarriors.Models.HomeownerComfirmedBids> HomeownerComfirmedBids { get; set; }

        public System.Data.Entity.DbSet<WorkWarriors.Models.CompletedBids> CompletedBids { get; set; }
    }
}