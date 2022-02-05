using Microsoft.EntityFrameworkCore;
using Pelicula.DM;
using Peliculas.Soporte.Mapper;

namespace Peliculas.API
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //Configuración del EntityFramework
            services.AddDbContext<ApplicationDbContext>(opciones => opciones.UseSqlServer(configuration.GetConnectionString("defualtConnection")));

            services.AddControllers();
            services.AddEndpointsApiExplorer();

            //Servicio de automapper
            services.AddAutoMapper(typeof(AutoMapperProfile));
        }




        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}
