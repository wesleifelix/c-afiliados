using InfraAfiliados;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Newtonsoft.Json.Serialization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Mvc.Formatters;
//using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace ApiAfiliados
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiAfiliados", Version = "v1" });
                var securitySchema = new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };

                c.AddSecurityDefinition("Bearer", securitySchema);

                var securityRequirement = new OpenApiSecurityRequirement
                {
                    { securitySchema, new[] { "Bearer" } }
                };

                c.AddSecurityRequirement(securityRequirement);
            });


            string connec = "";
            
            if (Configuration["live"].ToLower() == "false")
            {
                connec = Configuration["ConnectionStrings:Local"];
            }
            else
            {
                connec = Configuration["ConnectionStrings:Prod"];
            }
                

            var serverVersion = new MySqlServerVersion(new Version(5, 7, 33));
            var connection = connec;
           
            services
                .AddDbContext<AfiliadosContext>(optionsFin =>
                    optionsFin.UseMySql(connection, serverVersion).EnableSensitiveDataLogging() // These two calls are optional but help
                    .EnableDetailedErrors()
            );

            services.AddControllers( options => {
                options.OutputFormatters.RemoveType<StringOutputFormatter>();
                options.OutputFormatters.RemoveType<HttpNoContentOutputFormatter>();
                //new JsonSerializerOptions
                //{
                //    Converters =
                //    {
                //        new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
                //    }
                //};
            });

            //services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

            //services.AddControllers()
            //         .AddJsonOptions(options =>
            //         {
            //             // Use the default property (Pascal) casing.
            //             options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                         
                         
            //         })
                     
            //        .AddNewtonsoftJson(options =>
            //        {
            //            // Use the default property (Pascal) casing
            //            options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            //        }
            //);

            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            services.AddControllersWithViews().AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = null);

            services.AddAuthorization(options =>
            {
                options.AddPolicy("publisher", policy => policy.RequireClaim("Store", "publisher"));
                options.AddPolicy("partiner", policy => policy.RequireClaim("Store", "partiner"));
                options.AddPolicy("financial", policy => policy.RequireClaim("Store", "financial"));
                options.AddPolicy("atendent", policy => policy.RequireClaim("Store", "atendent"));
                options.AddPolicy("super", policy => policy.RequireClaim("Store", "super"));
            });

            services.AddCors();

            services.AddMvcCore();
            services.AddMvc(options =>
            {
                 //options.Filters.AddService(typeof(RAML.WebApiExplorer.ApiExplorerDataFilter));
                 //options.Conventions.Add(new RAML.WebApiExplorer.ApiExplorerVisibilityEnabledConvention());
                 //options.Conventions.Add(new RAML.WebApiExplorer.ApiExplorerVisibilityDisabledConvention(typeof(Controllers.RamlController)));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            
           

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,   // Verifica se um token recebido ainda � v�lido
                        ValidateIssuerSigningKey = true,// Valida a assinatura de um token recebido
                        ValidIssuer = "Mercado 8",
                        ValidAudience = "M8_AFILIADOS",
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(Configuration["SecurityKey"])
                            ),


                        // Tempo de toler�ncia para a expira��o de um token (utilizado
                        // caso haja problemas de sincronismo de hor�rio entre diferentes
                        // computadores envolvidos no processo de comunica��o)
                        ClockSkew = TimeSpan.Zero

                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnTokenValidated = context =>
                        {
                            return Task.CompletedTask;
                        }
                    };

                }
            );
            /*
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,   // Verifica se um token recebido ainda é válido
                        ValidateIssuerSigningKey = true,// Valida a assinatura de um token recebido
                        ValidIssuer = "Mercado 8",
                        ValidAudience = "M8_AFILIADOS",
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(Configuration["SecurityKey"])
                            ),


                        // Tempo de tolerância para a expiração de um token (utilizado
                        // caso haja problemas de sincronismo de horário entre diferentes
                        // computadores envolvidos no processo de comunicação)
                        ClockSkew = TimeSpan.Zero

                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnTokenValidated = context =>
                        {
                            return Task.CompletedTask;
                        }
                    };

                }
                );*/
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

           /* if (env.IsDevelopment())
            {*/
                app.UseDeveloperExceptionPage();
               
            /*}*/

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiAfiliados v1"));

           
            app.UseCors(x => x
               .AllowAnyMethod()
               .AllowAnyHeader()
               .SetIsOriginAllowed(origin => true) // allow any origin
               .AllowCredentials()); // allow credentials

            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseStaticFiles();
           // app.UseHttpsRedirection();

           

        }
    }
}
