using Coursey.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Coursey.Data.Configurations
{
    public class CourseStudentConfiguration : IEntityTypeConfiguration<CourseStudent>
    {
        public void Configure(EntityTypeBuilder<CourseStudent> builder)
        {
            builder.HasData(
                new CourseStudent { CourseId = 1, StudentId = 1 },
                new CourseStudent { CourseId = 1, StudentId = 2 },
                new CourseStudent { CourseId = 2, StudentId = 3 }
            );
        }
    }
}