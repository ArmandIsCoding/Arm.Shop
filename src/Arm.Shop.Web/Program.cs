using Arm.Shop.Core.Services;
using Arm.Shop.Data.Models;
using Arm.Shop.Data.Services;
using Arm.Shop.Web.Components;
using Microsoft.EntityFrameworkCore;

namespace Arm.Shop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 🔗 Registrar DbContext con la cadena de conexión desde appsettings.json
            builder.Services.AddDbContext<ArmShopDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IProductoService, ProductoService>();

            // Agregar servicios de Blazor Server
            builder.Services
                .AddRazorComponents()
                .AddInteractiveServerComponents();

            var app = builder.Build();

            // Configuración del pipeline HTTP
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}