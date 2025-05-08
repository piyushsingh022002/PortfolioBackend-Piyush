using Backend.Data;
using Microsoft.EntityFrameworkCore;
using Hangfire;
using Backend.Services;
using Hangfire.SqlServer;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>{
    options.AddPolicy(name:MyAllowSpecificOrigins,
    policy=>{
        policy.WithOrigins("http://localhost:3000","https://portfolio-frontend-piyush.vercel.app")
        .AllowAnyHeader()
        .AllowAnyMethod();

    });
});
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IQueryService, QueryService>();



// Add Hangfire services
// builder.Services.AddHangfire(config =>
// {
//     config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
//           .UseSimpleAssemblyNameTypeSerializer()
//           .UseRecommendedSerializerSettings()
//           .UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection"));
// });
// builder.Services.AddHangfireServer();

// ✅ Register Hangfire services
builder.Services.AddHangfire(configuration =>
    configuration.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                 .UseSimpleAssemblyNameTypeSerializer()
                 .UseRecommendedSerializerSettings()
                 .UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection"), new SqlServerStorageOptions
                 {
                     CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                     SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                     QueuePollInterval = TimeSpan.Zero,
                     UseRecommendedIsolationLevel = true,
                     DisableGlobalLocks = true
                 }));

// ✅ Add Hangfire Server (to process jobs)
builder.Services.AddHangfireServer();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

// app.MapStaticAssets();

// app.MapControllerRoute(
//     name: "default",
//     pattern: "{controller=Home}/{action=Index}/{id?}")
//     .WithStaticAssets();
app.UseHangfireDashboard("/hangfire");

app.Run();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    try
    {
        db.Database.OpenConnection();
        Console.WriteLine("✅ Connection successful!");
        db.Database.CloseConnection();
    }
    catch (Exception ex)
    {
        Console.WriteLine("❌ Connection failed: " + ex.Message);
    }
}

