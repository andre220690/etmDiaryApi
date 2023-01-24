using Microsoft.EntityFrameworkCore;

namespace etmDiaryApi.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Board> Boards { get; set; }
        public DbSet<Condition> Conditions { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Partner> Partners { get; set; }
        public DbSet<Stick> Sticks { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Theme> Themes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<FavoritSticks> FavoritSticks { get; set; }
        public DbSet<FavoritTasks> FavoritTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Stick>()
                .HasOne(c => c.User)
                .WithMany(c => c.Sticks);//???????

            modelBuilder.Entity<Task>()
                .HasOne(c => c.User)
                .WithMany(c => c.Tasks);

        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option) : base(option)
        {
            //Здесь заполнение БД
            Database.EnsureDeleted();
            Database.EnsureCreated();
            Console.WriteLine("DB Created");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //
            //optionsBuilder.UseMySql("server=localhost;user=root;password=123456789;database=usersdb;",
            //    new MySqlServerVersion(new Version(8, 0, 25)));
        }

    }
}
