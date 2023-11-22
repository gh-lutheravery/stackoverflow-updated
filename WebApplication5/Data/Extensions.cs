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
    }
}
