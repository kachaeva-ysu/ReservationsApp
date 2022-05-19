using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using Serilog;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace ReservationWebAPI
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
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().WithExposedHeaders("location", "content-type");
                });
            });
            services.AddMvc().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<VillaFilterParameters>());
            services.AddMvc(options =>
            {
                options.Filters.Add(new ExceptionHandlingFilter());
                options.SuppressAsyncSuffixInActionNames = false;
            });
            services.AddControllers();
            services.AddSwaggerGen();
            services.AddScoped<IVillaHandler, VillaHandler>();
            services.AddScoped<IUserHandler, UserHandler>();
            services.AddScoped<IUserActionHandler, UserActionHandler>();
            services.AddScoped<IAuthorizationHandler, AuthorizationHandler>();
            services.AddScoped<IAuthorizationActionHandler, AuthorizationActionHandler>();
            services.AddScoped<IReservationHandler, ReservationHandler>();
            services.AddScoped<IReservationActionHandler, ReservationActionHandler>();
            services.AddScoped<IAccessHandler, AccessHandler>();
            services.AddScoped<IAppRepository, AppContext>();
            services.AddScoped<IGoogleAuthorizationHandler>(_ => new GoogleAuthorizationHandler(Configuration["GoogleClientId"], Configuration["GoogleHostedDomain"]));
            services.AddScoped<ITokenAuthorizationHandler>(_ => new TokenAuthorizationHandler(Configuration["SecretKey"]));
            services.AddScoped<IPasswordHandler>(_ => new PasswordHandler(Configuration["PasswordSalt"]));
            services.AddScoped<IUserInfoFromToken, UserInfoFromToken>();
            services.AddDbContext<AppContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseSerilogRequestLogging();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthorization();

            app.UseStaticFiles(new StaticFileOptions {
                FileProvider = new PhysicalFileProvider(
            Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles")),
                RequestPath = "/StaticFiles"
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

