using Frontend.Data;
using Keycloak.AuthServices.Authentication;
using System.Text;
using Swashbuckle.AspNetCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
//using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddBootstrapBlazor();

var services = builder.Services;
var configuration = builder.Configuration;
services.AddKeycloakAuthentication(configuration);

//RegisterHttpClient(builder, services);

services.AddEndpointsApiExplorer();

var openIdConnectUrl = $"{configuration["Keycloak:auth-server-url"]}"
 + "realms/"
 + $"{configuration["Keycloak:realm"]}/"
 + ".well-known/openid-configuration";

services.AddSwaggerGen(c =>
{
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Auth",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.OpenIdConnect,
        OpenIdConnectUrl = new Uri(openIdConnectUrl),
        Scheme = "bearer",
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Id = "Bearer",
            Type = ReferenceType.SecurityScheme
        }
    };
    c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {securityScheme, Array.Empty<string>()}
    });
});

builder.Services.AddOidcAuthentication(options =>
{
    options.ProviderOptions.MetadataUrl = "http://localhost:8080/realms/Test/.well-known/openid-configuration";
    options.ProviderOptions.Authority = "http://localhost:8080/realms/Test";
    options.ProviderOptions.ClientId = "test-client";
    options.ProviderOptions.ResponseType = "id_token token";

    options.UserOptions.NameClaim = "preferred_username";
    options.UserOptions.RoleClaim = "roles";
    options.UserOptions.ScopeClaim = "scope";
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger().UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();

app.UseRouting();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.UseAuthentication();
app.UseAuthorization();

app.Run();

await app.RunAsync();

/*
static void RegisterHttpClient(
    WebApplicationBuilder builder,
    IServiceCollection services)
{
    var httpClientName = "Default";

    services.AddHttpClient(httpClientName,
        client => client.BaseAddress = new Uri("http://localhost:53189/"))
            .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

    services.AddScoped(
        sp => sp.GetRequiredService<IHttpClientFactory>()
            .CreateClient(httpClientName));
}
*/