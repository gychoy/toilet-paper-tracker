using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using ToiletPaperTracker.Core;
using ToiletPaperTracker.Core.Interfaces;

namespace ToiletPaperTracker.BlazorWASM
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddTransient<IToiletService, ToiletService>();
            builder.Services.AddTransient<IToiletRepository, ToiletRepository>();

            await builder.Build().RunAsync();
        }
    }
}
