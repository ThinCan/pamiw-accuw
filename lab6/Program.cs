using lab6;
using lab6.Shared;
using lab6.Shared.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var httpclient = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };
var config = await new ConfiguratorReader(httpclient).getConfig();
var bookservice = new BookService(httpclient, config);
builder.Services.AddScoped(sp => httpclient);
builder.Services.AddScoped(sp => config);
builder.Services.AddScoped(sp => bookservice);

await builder.Build().RunAsync();
