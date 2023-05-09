using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using AlWaddahClinic.Client;
using MudBlazor.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddTransient<AuthorizationMessageHandler>();
builder.Services.AddScoped<AuthenticationStateProvider, JwtAuthenticationStateProvider>();

builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddHttpClient("AlWaddahClinic.ServerAPI", client =>
{
    client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
}).AddHttpMessageHandler<AuthorizationMessageHandler>();

// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("AlWaddahClinic.ServerAPI"));

builder.Services.AddMudServices();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();

