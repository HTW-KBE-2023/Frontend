using Frontend.Data;

using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddBootstrapBlazor();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();

app.UseRouting();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
