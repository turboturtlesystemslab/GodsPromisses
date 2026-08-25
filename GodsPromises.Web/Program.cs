var builder = WebApplication.CreateBuilder(args);

// Razor Pages aktivieren
builder.Services.AddRazorPages();

var app = builder.Build();

// HTTPS
app.UseHttpsRedirection();

// Dateien aus wwwroot ausliefern
app.UseStaticFiles();

// Routing
app.UseRouting();

// Autorisierung
app.UseAuthorization();

// Razor Pages aktivieren
app.MapRazorPages();

app.Run();