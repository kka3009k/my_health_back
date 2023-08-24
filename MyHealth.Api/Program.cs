using DotNetEnv;
using MyHealth.Data;
using MyHealth.Api.Extension;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using FirebaseAdmin.Auth;
using Firebase.Auth;
using Google;
using Microsoft.Extensions.FileProviders;
using MyHealth.Api.Static;
using MyHealth.Api.Seed;

Env.Load();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
FirebaseApp.Create(new AppOptions { Credential = GoogleCredential.FromFile("firebase_key.json") });
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureDbContext();
builder.Services.ConfigureApi();
builder.Services.ConfigureJwtAuthentication();
builder.WebHost.UseUrls(Env.GetString("APP_URL"), Env.GetString("APP_URL_SSL"));
builder.Services.AddRazorPages();
builder.Services.AddCoreAdmin();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();

//app.UseHttpsRedirection();
app.MapControllers();

app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Constants.FileStoragePath),
    RequestPath = new PathString("/file_storage")
});
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseCoreAdminCustomAuth((serviceProvider) => Task.FromResult(true));
app.MapDefaultControllerRoute();

app.Map("/", context =>
    {
        context.Response.Redirect("/swagger/");
        return Task.CompletedTask;
    });

await app.Services.CreateScope().ServiceProvider.GetRequiredService<MyDbContext>().SeedAsync();
await app.RunAsync();
