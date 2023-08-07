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

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
    pattern: "{controller=Cliente}/{action=InicialView}", // O padrão da rota que você deseja mapear
    defaults: new { controller = "Cliente", action = "InicialView" }
);


// Rota para inserir um cliente
app.MapControllerRoute(
    name: "inserirCliente",
    pattern: "{controller=Cliente}/{action=InserirCliente}/{cpf}/{nome}/{senha}/{email}",
    defaults: new { action = "InserirCliente" },
    constraints: new { httpMethod = new HttpMethodRouteConstraint("POST") });

// Rota para atualizar um cliente
app.MapControllerRoute(
    name: "atualizarCliente",
    pattern: "{controller=Cliente}/{action=AtualizarCliente}/{idCliente}/{cpf}/{nome}/{senha}/{email}",
    defaults: new { action = "AtualizarCliente" },
    constraints: new { httpMethod = new HttpMethodRouteConstraint("PUT") }
);

// Rota para excluir um cliente
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

/*
using Microsoft.EntityFrameworkCore;
using System.Security.Policy;
using System.Text;

namespace Aniquota.Domain.Models
{
    class Program
    {
        static void Main(string[] args)
        {
            InsertData();
            //PrintData();
        }

        private static void InsertData()
        {
            using (var context = new Contexto())
            {
                // Creates the database if not exists
                context.Database.EnsureCreated();

                /*
                // Adds a publisher
                var publisher = new Publisher
                {
                    Name = "Mariner Books"
                };
                context.Publisher.Add(publisher);

                // Adds some books
                context.Book.Add(new Book
                {
                    ISBN = "978-0544003415",
                    Title = "The Lord of the Rings",
                    Author = "J.R.R. Tolkien",
                    Language = "English",
                    Pages = 1216,
                    Publisher = publisher
                });
                context.Book.Add(new Book
                {
                    ISBN = "978-0547247762",
                    Title = "The Sealed Letter",
                    Author = "Emma Donoghue",
                    Language = "English",
                    Pages = 416,
                    Publisher = publisher
                });

                // Saves changes
                context.SaveChanges();
                *//*
            }
        }
*/
/*private static void PrintData()
{
    // Gets and prints all books in database
    using (var context = new Contexto())
    {
        var books = context.Book
          .Include(p => p.Publisher);
        foreach (var book in books)
        {
            var data = new StringBuilder();
            data.AppendLine($"ISBN: {book.ISBN}");
            data.AppendLine($"Title: {book.Title}");
            data.AppendLine($"Publisher: {book.Publisher.Name}");
            Console.WriteLine(data.ToString());
        }
    }
}

}
}
*/