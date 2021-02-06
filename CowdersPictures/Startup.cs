using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SignalR.Mvc;

namespace CowdersPictures
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
            services.AddCors(); // Make sure you call this previous to AddMvc

            services.AddSignalR();

            //services.AddAuthentication(options =>
            //    {
            //        options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //        options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //        options.DefaultChallengeScheme = "Vipps";
            //    })
            //    .AddCookie()
            //    .AddOAuth("Vipps", options =>
            //    {
            //        options.ClientId = Configuration["Vipps:ClientId"];
            //        options.ClientSecret = Configuration["Vipps:ClientSecret"];
            //        options.CallbackPath = new PathString("/signin-vipps");

            //        options.AuthorizationEndpoint = "'https://apitest.vipps.no/access-management-1.0/access/oauth2/auth";
            //        options.TokenEndpoint = "'https://apitest.vipps.no/access-management-1.0/access/oauth2/token";
            //        options.UserInformationEndpoint = "https://apitest.vipps.no/vipps-userinfo-api/userinfo";

            //        //options.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
            //        //options.ClaimActions.MapJsonKey(ClaimTypes.Name, "name");
            //        //options.ClaimActions.MapJsonKey("urn:vipps:login", "login");
            //        //options.ClaimActions.MapJsonKey("urn:vipps:url", "html_url");
            //        //options.ClaimActions.MapJsonKey("urn:vipps:avatar", "avatar_url");

            //        options.Events = new OAuthEvents
            //        {
            //            OnCreatingTicket = async context =>
            //            {
            //                var request = new HttpRequestMessage(HttpMethod.Get, context.Options.UserInformationEndpoint);
            //                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", context.AccessToken);

            //                var response = await context.Backchannel.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, context.HttpContext.RequestAborted);
            //                response.EnsureSuccessStatusCode();

            //                var user = JObject.Parse(await response.Content.ReadAsStringAsync());

            //                context.RunClaimActions();
            //                //context.RunClaimActions(user);
            //            }
            //        };
            //    }
            //);

            services.AddControllers();
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen();

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(
                    options => options.WithOrigins("https://localhost:44318", "https://blue-tree-069c24103.azurestaticapps.net").AllowAnyMethod()
                );

            app.UseHttpsRedirection();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Issues API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ChatHub>("/chat");
                endpoints.MapControllers();
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
