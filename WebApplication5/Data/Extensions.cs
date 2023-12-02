using Microsoft.EntityFrameworkCore;
using WebApplication5.Models;

namespace WebApplication5.Data
{
    public static class Extension
    {
        public static void CreateDevDbIfNotExists(this IHost host)
        {    
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<StackOverflowCloneContext>();
                //context.Database.EnsureCreated();
                DevDbInitializer.Initialize(context);
            } 
        }

        public static void MigrateSchema(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<StackOverflowCloneContext>();

                foreach (var item in context.Database.GetMigrations())
                {
                    Console.WriteLine(item);
                }
                context.Database.Migrate();

                if (!context.Tag.Any())
                {
                    List<Tag> tags = new List<Tag>()
                    {
                        new Tag() { Title = ".NET" },
                        new Tag() { Title = "Python" },
                        new Tag() { Title = "Java" },
                        new Tag() { Title = "Javascript" },
                        new Tag() { Title = "Go" },
                        new Tag() { Title = "Rust" },
                        new Tag() { Title = "C++" },
                        new Tag() { Title = "C" },
                        new Tag() { Title = "ReactJS" },
                        new Tag() { Title = "R" },
                        new Tag() { Title = "HTML" },
                        new Tag() { Title = "SQL" },
                        new Tag() { Title = "Mobile" },
                        new Tag() { Title = "Linux" },
                        new Tag() { Title = "Windows" },
                        new Tag() { Title = "Web" },
                    };
                    context.Tag.AddRange(tags);
                    context.SaveChanges();
                }
            }
        }
    }
}
