using System.Text.Json;
using Lasmart.DAL;
using Lasmart.DAL.Repositories.Implementation;
using Lasmart.DAL.Repositories.Interfaces;
using Lasmart.Services.Implementation;
using Lasmart.Services.Interfaces;
using Lastmart.Domain.DBModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Lastmart.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Init db
            services.AddDbContext<ApplicationDbContext>(builder =>
            {
                builder.UseInMemoryDatabase("DOTS_DB");
            });
            
            // Init repos
            services.AddScoped<IBaseRepository<DotModel>, DotRepository>();
            services.AddScoped<IBaseRepository<CommentModel>, CommentRepository>();
            
            // Init services
            services.AddScoped<IDotService, DotService>();
            services.AddScoped<ICommentService, CommentService>();
            
            services.AddLogging();
            services.AddControllers().AddJsonOptions(jsonOptions =>
            {
                jsonOptions.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            });
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Lasmart.WebApi", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Lasmart.WebApi v1"));
            }

            app.UseCors(corsBuilder =>
            {
                corsBuilder.AllowAnyHeader();
                corsBuilder.AllowAnyMethod();
                corsBuilder.AllowAnyOrigin();
            });
            
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}