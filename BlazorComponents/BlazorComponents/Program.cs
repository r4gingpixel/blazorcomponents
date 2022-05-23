using BlazorComponents;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

var host = builder.Build();
//var jsRuntime = host.Services.GetRequiredService<IJSRuntime>();
builder.Services.AddScoped(sp => new Components.ComponentsJSInterop(host.Services.GetRequiredService<IJSRuntime>()));
host = builder.Build();
await host.RunAsync();
