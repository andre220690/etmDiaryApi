namespace etmDiaryApi.Models
{
    public class ApplicationDbContext : Dbcontext
    {
        public DbS


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>()
                    .HasMany(c => c.Students)
                    .WithMany(s => s.Courses)
                    .UsingEntity(j => j.ToTable("Enrollments"));
        }
    }
}
