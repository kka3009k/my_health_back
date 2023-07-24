using DotNetEnv;
using MyHealth.Data;
using MyHealth.Data.Seed;
using MyHealth.Api.Extension;

Env.Load();
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureDbContext();
builder.Services.ConfigureApi();
builder.Services.ConfigureJwtAuthentication();
builder.WebHost.UseUrls(Env.GetString("APP_URL"), Env.GetString("APP_URL_SSL"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Map("/", context =>
    {
        context.Response.Redirect("/swagger/");
        return Task.CompletedTask;
    });

await app.Services.CreateScope().ServiceProvider.GetRequiredService<MyDbContext>().SeedAsync();
await app.RunAsync();
