using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json;
using RenovaHrEmploymentAPI.Contracts;
using RenovaHrEmploymentAPI.Services;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using RenovaHrEmploymentAPI.Controllers;

namespace RenovaHrEmploymentAPI
{
    class Startup
    {
        public static bool isDevEnv = false;
        private static Startup _instance;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            if (_instance == null)
            {
                _instance = this;
            }
        }

        public static Startup GetInstance()
        {
            return _instance;
        }


        public string GetConnectionString()
        {
            return Configuration.GetConnectionString("DefaultConnection");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region DB Connection 
            services.AddDbContext<ModelContext>(options =>
                options.UseOracle(
                    Configuration.GetConnectionString("DefaultConnection")));

            RenovaCommon.Helpers.DatabaseHelper.ConnectionString = Configuration.GetConnectionString("DefaultConnection");
            RenovaCommon.Helpers.DatabaseHelper.Schema = Configuration["Schema"];

            #endregion
            // configure strongly typed settings object
            #region Cros Policy
            services.AddCors(o =>
            {
                o.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());



                o.AddPolicy(name: "TestEnv",
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:4200");
                      });
            });
            #endregion


            services.AddMvcCore().AddApiExplorer();

            #region Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Renova HR Employment API",
                    Version = "v1",
                    Description = "Endpoint List"
                });
                // Use method name as operationId
                c.CustomOperationIds(apiDesc =>
                {
                    return apiDesc.TryGetMethodInfo(out MethodInfo methodInfo) ? methodInfo.Name : null;
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme.",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}

                    }
                });

                //////Add Operation Specific Authorization///////
                // c.OperationFilter<AuthOperationFilter>();
                ////////////////////////////////////////////////
                c.CustomSchemaIds((type) => type.FullName);
                // c.TagActionsBy(api => api.HttpMethod);
                // c.EnableAnnotations();
                //  c.OrderActionsBy((apiDesc) => $"{apiDesc.ActionDescriptor.RouteValues["controller"]}_{apiDesc.HttpMethod}");
                // c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                var xfile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

                ControllerHelpers.BuildXmlFile(xfile);
                var xpath = Path.Combine(AppContext.BaseDirectory, xfile);
                c.IncludeXmlComments(xpath);


            });

            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes($"{Configuration["Jwt:Key"]}"))
                };
            });
            #endregion

            #region Active Service List


            services.AddCors(corsOption => corsOption.AddPolicy(
              "ReportingRestPolicy",
              corsBuilder =>
              {
                  corsBuilder.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
              }));

            services.AddSingleton<ILoggerService, LoggerService>();

            #endregion

            services.AddControllers().AddNewtonsoftJson(op =>
                op.SerializerSettings.ReferenceLoopHandling =
                    Newtonsoft.Json.ReferenceLoopHandling.Ignore).AddXmlSerializerFormatters();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                Startup.isDevEnv = true;
                app.UseDeveloperExceptionPage();
                //app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });


            app.UseSwagger();


            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("swagger/v1/swagger.json", "Renova API");
                c.RoutePrefix = "";

            });
            var useHttps = "Y";
            try
            {
                useHttps = Configuration["UseHTTPS"];
            }
            catch
            {
                useHttps = "Y";
            }

            if ("Y".Equals(useHttps))
            {
                app.UseHttpsRedirection();
            }


            //app.UseCors("CorsPolicy");
            app.UseCors("ReportingRestPolicy");

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
