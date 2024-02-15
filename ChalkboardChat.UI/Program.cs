
using ChalkboardChat.App.Models;
using ChalkboardChat.Data.AppDbContext;
using ChalkboardChat.Data.AuthDbContext;
using ChalkboardChat.Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Lägg till DbContext för identity users
builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("UsersDB"), b => b.MigrationsAssembly("ChalkboardChat.UI")));

// Konfigurera identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AuthDbContext>(); // Använd AuthDbContext för att lagra identity users

// Lägg till DbContext för Messages
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("MessagesDB"), b => b.MigrationsAssembly("ChalkboardChat.UI")));



// Lägg till repository med dependency injection 
builder.Services.AddScoped<IRepositoryMessage, MessageRepository>();


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

app.UseAuthorization();

app.MapRazorPages();

app.Run();