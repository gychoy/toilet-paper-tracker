using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ToiletPaperTracker.Core.Interfaces;
using ToiletPaperTracker.Core;

namespace ToiletPaperTracker.BlazorWASM
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });


            builder.Services.AddSingleton<IToiletService, ToiletService>();
            builder.Services.AddSingleton<IToiletRepository, ToiletRepository>();

            await builder.Build().RunAsync();
        }
    }
}
