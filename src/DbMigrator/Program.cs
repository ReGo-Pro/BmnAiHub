using Core;
using data;
using Domain.Identity;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("***********************************************************");
Console.WriteLine(AppSettings.Database.ConnectionString);
Console.WriteLine("***********************************************************");

try {
    var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>().UseNpgsql(AppSettings.Database.ConnectionString);
    using (var context = new AppDbContext(optionsBuilder.Options)) {
        if ((await context.Database.GetPendingMigrationsAsync()).Any()) {
            await context.Database.MigrateAsync();
            Console.WriteLine("All migrations are applied successfully.");
        }
        else {
            Console.WriteLine("No pending migrations found. The database is already up to date.");
        }


        var userEntity = context.Entry(new User());
    }
}
catch (Exception ex) {
    // TOOD: log the exception
    Console.WriteLine(ex.Message);
    throw;
}