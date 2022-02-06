using Microsoft.EntityFrameworkCore;
using Pelicula.DM;
using Peliculas.API.Helper;
using Peliculas.BM.ActorBM;
using Peliculas.BM.ActorBM.Interface;
using Peliculas.BM.GeneroBM;
using Peliculas.BM.GeneroBM.Interface;
using Peliculas.BM.Helper.AlmacenarArchivos.Interfaces;
using Peliculas.BM.Helper.GuardarArchivos;
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
            services.AddHttpContextAccessor();

            //Servicio de automapper
            services.AddAutoMapper(typeof(AutoMapperProfile));


            //Inyección de dependencias
            services.AddTransient<IBMGenero, BMGenero>();
            services.AddTransient<IBMActor, BMActor>();

            //Helper
            services.AddTransient<IAlmacenadorArchivos, AlmacenadorArchivosAzure>();
            //services.AddTransient<IAlmacenadorArchivos, AlmacenadorArchivosLocal>();

        }




        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}
