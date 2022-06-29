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
                UploadBooks(serviceScope.ServiceProvider.GetService<LibraryDbContext>());
            }
        }

        private static void UploadBooks(LibraryDbContext context)
        {
            if (!context.Books.Any())
            {
                context.Books.AddRange(
                    new Book() { Name = "Red", Pages = 100, Copies = 10 },
                    new Book() { Name = "Blue", Pages = 200, Copies = 20 },
                    new Book() { Name = "Black", Pages = 300, Copies = 50 }
                );

                context.SaveChanges();
            }
        }
    }
}
