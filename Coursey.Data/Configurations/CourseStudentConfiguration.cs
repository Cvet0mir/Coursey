using Coursey.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Coursey.Data.Configurations
{
    public class CourseStudentConfiguration : IEntityTypeConfiguration<CourseStudent>
    {
        public void Configure(EntityTypeBuilder<CourseStudent> builder)
        {
            builder
                .HasKey(cs => new { cs.CourseId, cs.StudentId });

            builder
                .HasOne(cs => cs.Course)
                .WithMany(c => c.CourseStudents)
                .HasForeignKey(cs => cs.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(cs => cs.Student)
                .WithMany(s => s.CourseStudents)
                .HasForeignKey(cs => cs.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(
                new CourseStudent { CourseId = 1, StudentId = 1 },
                new CourseStudent { CourseId = 1, StudentId = 2 },
                new CourseStudent { CourseId = 2, StudentId = 3 }
            );
        }
    }
}