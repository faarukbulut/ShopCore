using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Concrete;
using Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using WebUI.Middlewares;
using WebUI.Repositories.Abstract;
using WebUI.Repositories.Concrete;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Services.AddMvc();
builder.Services.AddDbContext<Context>();

builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<Context>();
    //.AddDefaultTokenProviders(); // Email do�rulama & �ifre s�f�rlama i�in token olu�turma

builder.Services.Configure<IdentityOptions>(opt =>
{
    opt.Password.RequireDigit = false; // �ifrelerde say�sal de�er zorunlulu�u
    opt.Password.RequireLowercase = false; // �ifrelerde k���k harf zorunlulu�u
    opt.Password.RequiredLength = 6; // �ifrelerde minimum karakter say�s�
    opt.Password.RequireNonAlphanumeric = false; // �ifrelerde alfa numeric zorunlulu�u
    opt.Password.RequireUppercase = false; // �ifrelerde b�y�k harf zorunlulu�u

    opt.Lockout.MaxFailedAccessAttempts = 5; // Kullan�c�n�n ka� defa ba�ar�s�z giri� yapaca��
    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // Toplam ba�ar�s�z i�lem sonras� hesap kilit s�resi
    opt.Lockout.AllowedForNewUsers = true; // Yeni kullan�c�lar i�in kilit i�lem durumu

    opt.User.RequireUniqueEmail = true; // Benzersiz bir mail adresi zorunlulu�u

    opt.SignIn.RequireConfirmedPhoneNumber = false; // Telefon numras� onay zorunlulu�u
    opt.SignIn.RequireConfirmedEmail = false; // Mail onay zorunlulu�u
});

builder.Services.ConfigureApplicationCookie(opt =>
{
    opt.LoginPath = "/Account/Login";
    opt.LogoutPath = "/Account/Logout";
    opt.AccessDeniedPath = "/Account/AccessDenied";
    opt.ExpireTimeSpan = TimeSpan.FromHours(1); // Cookie saklama s�resi
    opt.SlidingExpiration = true; // giri� yap�lan s�reyi her istek yap�ld���nda tekrar artt�r.

    opt.Cookie = new CookieBuilder
    {
        HttpOnly = true, // cookielerin scriptler taraf�ndan okunmamas�n� sadece http ile okunmas� sa�lar. G�venlik i�in �nemlidir.
        Name = ".ShopApp.Security.Cookie",
        SameSite = SameSiteMode.Strict // ba�ka bir kullan�c�n�n ayn� cookie ile i�lem yapamama potansiyeli.
    };

});

builder.Services.AddScoped<IMailRepository, MailRepository>();

//

builder.Services.AddScoped<ICategoryRepository, CategoryRepositoy>();
builder.Services.AddScoped<ICategoryService, CategoryManager>();

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderManager>();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductManager>();

builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<ICartService, CartManager>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Default/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.CustomStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Default}/{action=Index}/{id?}");

app.Run();
