using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using vidibr_api.Repositorio;
using vidibr_api.Models;

namespace vidibr_api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

            // services.AddCors(options =>
            // {
            //     options.AddPolicy("AllowAll",
            //         builder =>
            //         {
            //             builder
            //             .AllowAnyOrigin() 
            //             .AllowAnyMethod()
            //             .AllowAnyHeader()
            //             .AllowCredentials();
            //         });
            // });

            services.AddDbContext<UsuarioDbContext>(option => 
            option.UseSqlServer(Configuration.GetConnectionString("ConexaoVidiBR")));
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();               
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
             app.UseCors("CorsPolicy");

            app.UseMvc();
        }
    }
}
