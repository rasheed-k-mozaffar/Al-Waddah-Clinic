using Microsoft.AspNetCore.ResponseCompression;
using AlWaddahClinic.Server.Extensions;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContextAndIdentity(builder.Configuration);
builder.Services.AddJwtBearerAuthentication(builder.Configuration);

builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IPatientsRepository, PatientsRepository>();
builder.Services.AddScoped<IHealthRecordsRepository, HealthRecordsRepository>();
builder.Services.AddScoped<IAppointmentsRepository, AppointmentsRepository>();
builder.Services.AddScoped<IClinicRepository, ClinicRepository>();
builder.Services.AddScoped<IPaymentsRepository, PaymentsRepository>();

builder.Services.AddScoped<IAiRepository, AiRepository>();

builder.Services.AddHttpClient();

builder.Services.AddScoped(sp =>
{
    var options = new AlWaddahClinic.Server.Options.IdentityOptions();
    var httpContextAccessor = sp.GetService<IHttpContextAccessor>();
    if(httpContextAccessor != null)
    {
        var httpContext = httpContextAccessor.HttpContext;
        if (httpContext.User.Identity.IsAuthenticated)
        {
            options.ClinicId = httpContext.User.FindFirst("ClinicId").Value;
            options.UserId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }

    return options;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();


app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();

