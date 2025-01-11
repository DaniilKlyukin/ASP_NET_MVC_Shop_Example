using ASP_NET_MVC_Shop_Example.Data;
using ASP_NET_MVC_Shop_Example.Services;
using Microsoft.EntityFrameworkCore;

namespace ASP_NET_MVC_Shop_Example
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // �������� ������ ����������� �� ����� ������������
            string connection = builder.Configuration.GetConnectionString("DefaultConnection");

            // ��������� �������� ApplicationContext � �������� ������� � ����������
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlite(connection);
            });

            builder.Services.AddSingleton<IStateStorage, PreviousStateStorage>();
            builder.Services.AddSingleton<SortStateService>();

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            app.UseHttpsRedirection();
            app.UseStaticFiles(); // ��� ���������� ��� �������� ����������� ������, ����� ��� CSS

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Products}/{action=Index}/{id?}");

            app.Run();
        }
    }
}