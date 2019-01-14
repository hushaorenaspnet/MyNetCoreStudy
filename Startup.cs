using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyNetCoreStudy.IService;
using MyNetCoreStudy.Service;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace MyNetCoreStudy
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc()
                    .AddRazorPagesOptions(options =>
                    {
                        options.Conventions.Add(new GlobalTemplatePageRouteModelConvention());
                        options.Conventions.Add(new GlobalHeaderPageApplicationModelConvention());
                        options.Conventions.Add(new GlobalPageHandlerModelConvention());

                        options.Conventions.AddFolderRouteModelConvention("/OtherPages", model =>
                        {
                            //OtherPages文件夹下的页面，都用此路由模板。
                            var selectorCount = model.Selectors.Count;
                            for (var i = 0; i < selectorCount; i++)
                            {
                                var selector = model.Selectors[i];
                                model.Selectors.Add(new SelectorModel
                                {
                                    AttributeRouteModel = new AttributeRouteModel
                                    {
                                        //用于处理路由匹配,指定路由处理顺序。按顺序处理的路由 (-1、 0、 1、 2、 … n)
                                        Order = 2,
                                        Template = AttributeRouteModel.CombineTemplates
                                        (selector.AttributeRouteModel.Template, "{otherPagesTemplate?}")
                                    }
                                });
                            }
                        });

                        options.Conventions.AddPageRouteModelConvention("/About", model =>
                        {
                            //About页面,用此路由模板。
                            var selectorCount = model.Selectors.Count;
                            for (var i = 0; i < selectorCount; i++)
                            {
                                var selector = model.Selectors[i];
                                model.Selectors.Add(new SelectorModel
                                {
                                    AttributeRouteModel = new AttributeRouteModel
                                    {
                                        Order = 2,
                                        Template = AttributeRouteModel.CombineTemplates
                                        (selector.AttributeRouteModel.Template, "{aboutTemplate?}")
                                    }
                                });
                            }
                        });

                    })
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);







            services.AddTransient<IOperationTransient, Operation>();
            services.AddScoped<IOperationScoped, Operation>();
            services.AddSingleton<IOperationSingleton, Operation>();
            services.AddSingleton<IOperationSingletonInstance>(new Operation(Guid.Empty));

            services.AddTransient<OperationService, OperationService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc();
           
        }
    }
}
