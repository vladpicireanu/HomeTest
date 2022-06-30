using Domain;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.PreparationDb
{
    public static class PrepDb
    {
        public static void Population(IApplicationBuilder app)
        {
            using ( var serviceScope = app.ApplicationServices.CreateScope())
            {
                UploadData(serviceScope.ServiceProvider.GetService<LibraryDbContext>());
            }
        }

        private static void UploadData(LibraryDbContext context)
        {
            if (!context.Books.Any())
            {
                context.Books.AddRange(
                    new Book() { Name = "Red", Pages = 100, Copies = 10 },
                    new Book() { Name = "Blue", Pages = 200, Copies = 20 },
                    new Book() { Name = "Black", Pages = 300, Copies = 50 }
                );
            }

            if (!context.Users.Any())
            {
                context.Users.AddRange(
                    new User() { FirstName = "john", LastName = "User1", Email = "test@test.com" },
                    new User() { FirstName = "Emma", LastName = "User2" },
                    new User() { FirstName = "Rick", LastName = "User3" }
                );
            }

            if (!context.Rents.Any())
            {
                context.Rents.AddRange(
                    new Rent() { BookId = 1, UserId = 1, StartDate = new DateTimeOffset(DateTime.Now.AddDays(-5)) },
                    new Rent() { BookId = 2, UserId = 3, StartDate = new DateTimeOffset(DateTime.Now) },
                    new Rent()
                    {
                        BookId = 1,
                        UserId = 1,
                        StartDate = new DateTimeOffset(DateTime.Now.AddDays(-3)),
                        StopDate = new DateTimeOffset(DateTime.Now.AddDays(-1))
                    },
                    new Rent()
                    {
                        BookId = 3,
                        UserId = 2,
                        StartDate = new DateTimeOffset(DateTime.Now.AddDays(-8)),
                        StopDate = new DateTimeOffset(DateTime.Now.AddDays(-4))
                    }
                );
            }

            context.SaveChanges();
        }
    }
}
