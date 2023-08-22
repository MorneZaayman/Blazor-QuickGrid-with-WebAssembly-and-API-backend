using Microsoft.EntityFrameworkCore;
using QuickGrid.API.Data;

namespace QuickGrid.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite());

            builder.Services.AddControllers();

            
            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseCors(x => x
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed(origin => true));
            }

            app.UseHttpsRedirection();

            app.MapControllers();

            app.Run();
        }
    }
}