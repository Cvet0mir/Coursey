using Coursey.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Coursey.Data.Configurations
{
    public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.HasData(
                new Teacher
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Smith",
                    MainSubject = "Mathematics",
                    YearsExperience = 12,
                    Address = "12 Main Street",
                    Age = 42
                },
                new Teacher
                {
                    Id = 2,
                    FirstName = "Emily",
                    LastName = "Johnson",
                    MainSubject = "Programming",
                    YearsExperience = 8,
                    Address = "25 Oak Avenue",
                    Age = 35
                },
                new Teacher
                {
                    Id = 3,
                    FirstName = "Michael",
                    LastName = "Brown",
                    MainSubject = "Physics",
                    YearsExperience = 20,
                    Address = "8 Green Road",
                    Age = 48
                }
            );
        }
    }
}