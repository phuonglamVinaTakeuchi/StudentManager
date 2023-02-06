using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentManager.Core.Data;

namespace StudentManager.Areas.Identity.Data;

public class StudentManagerContext : IdentityDbContext<AppUser>
{
    public DbSet<Grade> Grades { get; set; }
	public DbSet<Student> Students { get; set; }

	public StudentManagerContext(DbContextOptions<StudentManagerContext> options)
        : base(options)
    {
		
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
	    base.OnModelCreating(builder);

        //builder.Entity<StudentGrade>()
        //    .HasKey(sc => new { StudentsId = sc.StudentId, GradesId = sc.GradeId });

        builder.Entity<Student>()
	        .HasMany(s => s.Grades)
	        .WithMany(c => c.Students)
			.UsingEntity(j => j.ToTable("StudentGrade"));

        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
        builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
    }
}

public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<AppUser>
{
	public void Configure(EntityTypeBuilder<AppUser> builder)
  {
    builder.Property(u => u.FirstName).HasMaxLength(255);
    builder.Property(u => u.LastName).HasMaxLength(255);

  }
}
