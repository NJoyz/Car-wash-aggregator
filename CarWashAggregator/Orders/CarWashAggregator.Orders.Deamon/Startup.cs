using CarWashAggregator.Common.Domain;
using CarWashAggregator.Common.Domain.Contracts;
using CarWashAggregator.Common.Domain.DTO.Order.Querys.Request;
using CarWashAggregator.Common.Domain.DTO.Order.Querys.Response;
using CarWashAggregator.Common.Infra;
using CarWashAggregator.Orders.Business.Handlers.QueryHandlers;
using CarWashAggregator.Orders.Business.Interfaces;
using CarWashAggregator.Orders.Business.Services;
using CarWashAggregator.Orders.Domain.Contracts;
using CarWashAggregator.Orders.Infra.Data;
using CarWashAggregator.Orders.Infra.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CarWashAggregator.Orders.Deamon
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<OrderContext>(options =>
            {
                options.UseNpgsql(_configuration.GetConnectionString(Helper.DataBaseConnectionSection));
            });

            //Services
            services.AddTransient<IOrderService, OrderService>();

            //Data
            services.AddScoped<IOrderRepository, OrderRepository>();

            //Subscriptions
            services.AddTransient<RequestByIdHandler>();

            services.AddMvc();
            BusContainer.RegisterBusService(services, _configuration);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            ConfigureEventBus(app);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }

        private void ConfigureEventBus(IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.SubscribeToQuery<RequestOrderById, ResponseOrders, RequestByIdHandler>();
        }
    }
}
