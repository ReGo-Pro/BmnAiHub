using Core;
using Domain.Identity;
using Microsoft.AspNetCore.Identity;
using WebApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
                .AddNewtonsoftJson();  // This replaces the default System.Text.Json-based input and output
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging();
// TODO: Add NLog

Console.WriteLine("**************************************************************");
Console.WriteLine(AppSettings.Database.ConnectionString);
Console.WriteLine("**************************************************************");

builder.Services.AddAppServices();
builder.Services.AddStaticFilesManager(builder.Environment.WebRootPath);
builder.Services.AddPostgreSQL();
builder.Services.AddAppIdentity();
builder.Services.AddJwtAuthentication();

builder.Services.AddAppCors();

var app = builder.Build();

using (var scope = app.Services.CreateScope()) {
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

    var identityInitializer = new IdentityInitializer(roleManager, userManager);
    await identityInitializer.AddDefaultRoles();
    await identityInitializer.AddDefaultAdminUser(AppSettings.Admin.Email, AppSettings.Admin.Password);
}

// if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
// }

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseCors(AppSettings.Cors.Name);
app.UseStaticFiles();
app.UseAuthorization();
app.MapControllers();
app.MapDefaultControllerRoute();
app.Run();