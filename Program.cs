using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Roamy;
using Roamy.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//Replace with environment variable before deploying to Azure
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7104") });
builder.Services.AddSingleton<TripManager>(); //instantiates one TripManager object for the duration of the program. Different from Transient and Scope.

await builder.Build().RunAsync();
