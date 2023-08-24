using Blazored.LocalStorage;
using DotNetEnv;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using MyHealth.Admin.Handlers;
using MyHealth.Admin.Services;
using MyHealth.Data;

namespace MyHealth.Admin
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Env.Load();

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddScoped<UserStateService>();
            builder.Services.AddScoped(sp =>
                new HttpClient(new CustomHttpHandler(builder.Services.BuildServiceProvider().GetRequiredService<UserStateService>()))
                {
                    BaseAddress = new Uri(builder.Configuration["BackendUrl"]),
                });

            var connectionString =
            $"Server={Env.GetString("DB_HOST")};Port={Env.GetString("DB_PORT")};Database={Env.GetString("DB_DATABASE")};User ID={Env.GetString("DB_USERNAME")};Password={Env.GetString("DB_PASSWORD")};Include Error Detail=true";

            builder.Services.AddDbContextFactory<MyDbContext>(opt =>
    opt.UseNpgsql(connectionString));

            builder.WebHost.UseUrls(Env.GetString("APP_URL"), Env.GetString("APP_URL_SSL"));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}