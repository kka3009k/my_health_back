using DotNetEnv;
using MyHealth.Data;
using MyHealth.Data.Seed;
using MyHealth.Api.Extension;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using FirebaseAdmin.Auth;
using Firebase.Auth;

Env.Load();
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
FirebaseApp.Create(new AppOptions { Credential = GoogleCredential.FromJson("\"{\\\"type\\\":\\\"service_account\\\",\\\"project_id\\\":\\\"myhealth-b189d\\\",\\\"private_key_id\\\":\\\"8c23cd36a27646fb223c7d762ce94c44fea63fb8\\\",\\\"private_key\\\":\\\"-----BEGINPRIVATEKEY-----\\\\nMIIEvQIBADANBgkqhkiG9w0BAQEFAASCBKcwggSjAgEAAoIBAQC/ycFrnu/CrFMj\\\\nZyU4ShByCCMt+qO/FGT0QzugEHpyIlhvOONRAxQkEXGvQjHf5L52aferTB1n++8W\\\\ngkJRLgHybDya5+EBYbSJBj/JcZb/PPcilnNsd4HDQgvaWZfFwJLa22elj1YkYzWI\\\\nZkVgjd44XXg4F7i3XAwgBUgBX/dspmcT8FXl7YvpJsk1fwK++LPRL+wp7r/V6O5i\\\\n3zTeyw6OOEhsLFUvQL5yefT5n/nnkJP8lefwqIMJ69dpLEL69Vgc7r5oG4eu3GtM\\\\nJmAcss0OKzhww2R/rdEm3z/+hMSGzKoc6H7LYcaFnFX7qIvVG+nRu+Gi36p99Uec\\\\nAhm7R7LVAgMBAAECggEADBfoQuWEP9XG7koQPkLpU49wzHLsBV3/WamtplOzrOO+\\\\niNDhAJg9mmKhNQhrBa+yVNwsulfz2L05n8U58k2A4OzzS19IwGINVMCYoBayC0ko\\\\nWj17bMECZTISdkPE/rq8Z+GC4R0vNN8unnm4cTSw6QP30y2nv6QlvZUnq7KF8wtV\\\\nLJ3PRMCBifS5ZO6Ti6otnLLGBdFZCTPZYhkfzmQ0Loj+3A2aieTCSHDzi27D9/4+\\\\ngZIMAKvkF5WbvHogIiG0x3xpWihltIJlvDULcbgD5Vdjet0Qbrmr4siE3mpvIX91\\\\nJ4dG7VWz1/FkXxrpo7HbQyukYepTz2YAUOzg9agjYQKBgQDhwHouyoEzD709mnoI\\\\n87FR0PWUZD4ZGp+6Gy2P74EXZOmVsU6rDS2ieJAU4lELagQU7cgpuFKKI7c1qC0W\\\\nIRe5x91ZIO3mQHEhXlrLvSD0o+RsSgZoCRSyV4C21ISS1AemGwk4e8qQpdZ23cMd\\\\nvzCzILhC9bJ9dmqOry02bYo+ZQKBgQDZfElHUPXSlFLxj+684l8fahqkjRPeurxM\\\\nWaCdUGw32ej8ceymenS6dSzvVmIUYvW+9/j6a4LuHy9weA/FOiSN72ZGPOEiIk9F\\\\n7UBXlgZE/OPI4ii58baZx5HZq2Qs3t7+Txg1JPZh6P0UJPi+HaGvHdzWTVxlsqdk\\\\nF4hbunPjsQKBgQC4zGc4hmzc77VqCFp9mX8+Cl/96VEsG69FGZpiiRyTmffcohhT\\\\nzaXdqfPIJLtTLKXKvBtui4SFsFb5hYHi65QQcJuxqlMUeQwi1KpevaOMn90NCEvW\\\\nPhjDJP4orC8aQpdAUkFqC2v2nMrC4yYl46xp9g7gQWCrc5Qm2R+ZvnG8QQKBgHOa\\\\nkQkLi9+HYB9vNqPIYG++YrMdGnbGI9khuzJj0WJOvn2RwQ0tAmcqadw+upvDjoUY\\\\nIoxaIZqZkQnjh956bXvUyTSEn9cZDbJJzm3AHU0Gb74UGTndtgZAAtMFO5ZuUXI0\\\\ncNWu9BhPVFck3+OOtKb05LI1JOwZ7shRifXYQknBAoGAa4dVuV2RZ7bz8og72UR5\\\\naAEXBVumCj8Chl8nstTK+sAdAjiSSK8Z2vrWjInHgdAAyW/z3WpE76SzKvc/uOaG\\\\n/UUDMRci/Vv/C8Nr3Ae+/6AI3cT1C9CzJtiaSG0prvw4aKrqJds5HSBdw7YhJ6yA\\\\np5HOGHUlPSLVqS4w/D42oCk=\\\\n-----ENDPRIVATEKEY-----\\\\n\\\",\\\"client_email\\\":\\\"firebase-adminsdk-n5y07@myhealth-b189d.iam.gserviceaccount.com\\\",\\\"client_id\\\":\\\"112382546750509503895\\\",\\\"auth_uri\\\":\\\"https://accounts.google.com/o/oauth2/auth\\\",\\\"token_uri\\\":\\\"https://oauth2.googleapis.com/token\\\",\\\"auth_provider_x509_cert_url\\\":\\\"https://www.googleapis.com/oauth2/v1/certs\\\",\\\"client_x509_cert_url\\\":\\\"https://www.googleapis.com/robot/v1/metadata/x509/firebase-adminsdk-n5y07%40myhealth-b189d.iam.gserviceaccount.com\\\",\\\"universe_domain\\\":\\\"googleapis.com\\\"}\"") });
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
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseStaticFiles();
app.UseCoreAdminCustomAuth((serviceProvider) => Task.FromResult(true));
app.MapDefaultControllerRoute();
app.Map("/", context =>
    {
        context.Response.Redirect("/swagger/");
        return Task.CompletedTask;
    });

await app.Services.CreateScope().ServiceProvider.GetRequiredService<MyDbContext>().SeedAsync();
await app.RunAsync();
