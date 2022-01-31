using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Service;
using WebAPI.Service.Interfaces;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;

namespace WebAPI
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
      string connectionString = "mongodb://localhost:27017/heroesapp";
      var connection = new MongoUrlBuilder(connectionString);
      MongoClient client = new MongoClient(connectionString);
      IMongoDatabase database = client.GetDatabase(connection.DatabaseName);

      services.AddControllers();
      services.AddCors();
      services.AddSingleton<IMongoDatabase>(database);
      services.AddSingleton<IHeroService, HeroService>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			//app.UseHttpsRedirection();

			app.UseRouting();
      app.UseCors(builder => builder.AllowAnyOrigin()
                                 .AllowAnyHeader()
                                 .AllowAnyMethod());


			//app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
