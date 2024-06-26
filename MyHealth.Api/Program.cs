using DotNetEnv;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.FileProviders;
using MyHealth.Api.Extension;
using MyHealth.Api.Middlewares;
using MyHealth.Api.Seed;
using MyHealth.Api.Static;
using MyHealth.Data;

Env.Load();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.DateFormatHandling = Newtonsoft.Json.DateFormatHandling.IsoDateFormat;
    options.SerializerSettings.DateFormatString = "yyyy'-'MM'-'dd'T'HH':'mm':'ssZ";
});

FirebaseApp.Create(new AppOptions { Credential = GoogleCredential.FromFile("firebase_key.json") });
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureDbContext();
builder.Services.ConfigureApi();
builder.Services.ConfigureJwtAuthentication();
builder.WebHost.UseUrls(Env.GetString("APP_URL"), Env.GetString("APP_URL_SSL"));
builder.Services.AddRazorPages();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI(x =>
{
    x.ShowCommonExtensions();
    x.ShowExtensions();
    x.DisplayRequestDuration();
    x.EnableDeepLinking();
    x.EnableFilter();
});

app.MapControllers();

app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Constants.FileStoragePath),
    RequestPath = new PathString("/file_storage")
});
app.UseRouting();
app.UseCors(options => options.WithOrigins("0.0.0.0").AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
app.UseMiddleware<ExceptionMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.MapDefaultControllerRoute();

app.Map("/", context =>
    {
        context.Response.Redirect("/swagger/");
        return Task.CompletedTask;
    });

await app.Services.CreateScope().ServiceProvider.GetRequiredService<MyDbContext>().SeedAsync();
await app.RunAsync();
