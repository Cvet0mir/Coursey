using Coursey.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Coursey.Data.Configurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasData(
                new Course
                {
                    Id = 1,
                    Name = "Advanced Mathematics",
                    Subject = "Mathematics",
                    StartDate = new DateTime(2026, 1, 10),
                    EndDate = new DateTime(2026, 3, 10),
                    Difficulty = "Advanced",
                    TeacherId = 1
                },
                new Course
                {
                    Id = 2,
                    Name = "Introduction to C#",
                    Subject = "Programming",
                    StartDate = new DateTime(2026, 2, 1),
                    EndDate = new DateTime(2026, 4, 1),
                    Difficulty = "Beginner",
                    TeacherId = 2
                },
                new Course
                {
                    Id = 3,
                    Name = "Classical Mechanics",
                    Subject = "Physics",
                    StartDate = new DateTime(2026, 3, 1),
                    EndDate = new DateTime(2026, 5, 1),
                    Difficulty = "Intermediate",
                    TeacherId = 3
                }
            );
        }
    }
}