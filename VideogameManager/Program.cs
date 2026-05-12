using Microsoft.EntityFrameworkCore;
using VideogameManager.Data;
using VideogameManager.Services;
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddScoped<GameService>(); //anteriormente AddSingleton
builder.Services.AddDbContext<GameStoreContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.MapGet("/", context =>
{
    context.Response.Redirect("/Games/Index");
    return Task.CompletedTask;
});
    
app.Run();
