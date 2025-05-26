using humanResourceManager.Datas.RoleAndPermission;
using Microsoft.EntityFrameworkCore;

namespace humanResourceManager.Datas
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions options): base(options) { }


        #region DbSet 
        public DbSet<Employees> Employees { get; set; }
        public DbSet<Attendance> Attendance { get; set; }
        public DbSet<Contracts> Contracts { get; set; }
        public DbSet<Departments> Departments { get; set; }
        public DbSet<Payroll> Payroll { get; set; }
        public DbSet<Positions> Positions { get; set; }
        public DbSet<Users> Users { get; set; }

		public DbSet<Permission> Permissions { get; set; }

        public DbSet<Rolee> Roles { get; set; }


		#endregion
	}
}
