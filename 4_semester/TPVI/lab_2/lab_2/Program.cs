public class Program
{
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddRazorPages();
        var app = builder.Build();

        app.UseWelcomePage("/aspnetcore");
        app.UseDefaultFiles();
        app.UseStaticFiles();
        app.MapGet("/aspnetcore", () => "Hello, World!");

        app.Run();
    }
}