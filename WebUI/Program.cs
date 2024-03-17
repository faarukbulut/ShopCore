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
    //.AddDefaultTokenProviders(); // Email doðrulama & þifre sýfýrlama için token oluþturma

builder.Services.Configure<IdentityOptions>(opt =>
{
    opt.Password.RequireDigit = false; // Þifrelerde sayýsal deðer zorunluluðu
    opt.Password.RequireLowercase = false; // Þifrelerde küçük harf zorunluluðu
    opt.Password.RequiredLength = 6; // Þifrelerde minimum karakter sayýsý
    opt.Password.RequireNonAlphanumeric = false; // Þifrelerde alfa numeric zorunluluðu
    opt.Password.RequireUppercase = false; // Þifrelerde büyük harf zorunluluðu

    opt.Lockout.MaxFailedAccessAttempts = 5; // Kullanýcýnýn kaç defa baþarýsýz giriþ yapacaðý
    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // Toplam baþarýsýz iþlem sonrasý hesap kilit süresi
    opt.Lockout.AllowedForNewUsers = true; // Yeni kullanýcýlar için kilit iþlem durumu

    opt.User.RequireUniqueEmail = true; // Benzersiz bir mail adresi zorunluluðu

    opt.SignIn.RequireConfirmedPhoneNumber = false; // Telefon numrasý onay zorunluluðu
    opt.SignIn.RequireConfirmedEmail = false; // Mail onay zorunluluðu
});

builder.Services.ConfigureApplicationCookie(opt =>
{
    opt.LoginPath = "/Account/Login";
    opt.LogoutPath = "/Account/Logout";
    opt.AccessDeniedPath = "/Account/AccessDenied";
    opt.ExpireTimeSpan = TimeSpan.FromHours(1); // Cookie saklama süresi
    opt.SlidingExpiration = true; // giriþ yapýlan süreyi her istek yapýldýðýnda tekrar arttýr.

    opt.Cookie = new CookieBuilder
    {
        HttpOnly = true, // cookielerin scriptler tarafýndan okunmamasýný sadece http ile okunmasý saðlar. Güvenlik için önemlidir.
        Name = ".ShopApp.Security.Cookie",
        SameSite = SameSiteMode.Strict // baþka bir kullanýcýnýn ayný cookie ile iþlem yapamama potansiyeli.
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
