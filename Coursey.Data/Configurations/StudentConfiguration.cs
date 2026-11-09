using Coursey.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Coursey.Data.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasData(
                new Student
                {
                    Id = 1,
                    FirstName = "Ivan",
                    LastName = "Petrov",
                    Email = "ivan.petrov@example.com",
                    BirthDate = new DateOnly(2007, 3, 15),
                    FavouriteSubject = "Mathematics"
                },
                new Student
                {
                    Id = 2,
                    FirstName = "Maria",
                    LastName = "Georgieva",
                    Email = "maria.georgieva@example.com",
                    BirthDate = new DateOnly(2006, 8, 22),
                    FavouriteSubject = "Computer Science"
                },
                new Student
                {
                    Id = 3,
                    FirstName = "Dimitar",
                    LastName = "Ivanov",
                    Email = "dimitar.ivanov@example.com",
                    BirthDate = new DateOnly(2007, 11, 5),
                    FavouriteSubject = "Physics"
                }
            );
        }
    }
}