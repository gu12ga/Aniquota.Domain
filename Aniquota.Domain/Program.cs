using Aniquota.Domain;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<Contexto>();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(180);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Cliente}/{action=LoginView}/{id?}");

app.MapControllerRoute(
    name: "inicialView",
    pattern: "{controller=Cliente}/{action=InicialView}",
    defaults: new { controller = "Cliente", action = "InicialView" }
);

app.MapControllerRoute(
    name: "inserirCliente",
    pattern: "{controller=Cliente}/{action=InserirCliente}/{cpf}/{nome}/{senha}/{email}",
    defaults: new { action = "InserirCliente" },
    constraints: new { httpMethod = new HttpMethodRouteConstraint("POST") });

app.MapControllerRoute(
    name: "atualizarCliente",
    pattern: "{controller=Cliente}/{action=AtualizarCliente}/{idCliente}/{cpf}/{nome}/{senha}/{email}",
    defaults: new { action = "AtualizarCliente" },
    constraints: new { httpMethod = new HttpMethodRouteConstraint("PUT") }
);


app.MapControllerRoute(
    name: "excluirCliente",
    pattern: "{controller=Cliente}/{action=ExcluirCliente}/{idCliente}",
    defaults: new { action = "ExcluirCliente" },
    constraints: new { httpMethod = new HttpMethodRouteConstraint("DELETE") }
);

app.MapControllerRoute(
    name: "login",
    pattern: "{controller=Cliente}/{action=Login}/{cpf}/{senha}",
    defaults: new { action = "Login" },
    constraints: new { httpMethod = new HttpMethodRouteConstraint("GET") }
);

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var context = serviceProvider.GetRequiredService<Contexto>();

    // Creates the database if not exists
    //context.Database.EnsureCreated();
    context.Database.Migrate();
}

app.Run();