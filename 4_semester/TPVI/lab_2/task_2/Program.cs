using Microsoft.Extensions.FileProviders;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();

        DefaultFilesOptions defaultFilesOptions = new DefaultFilesOptions();
        defaultFilesOptions.DefaultFileNames.Clear();
        defaultFilesOptions.DefaultFileNames.Add("Neumann.html");

        app.UseDefaultFiles(defaultFilesOptions);

        app.UseStaticFiles();

        app.UseStaticFiles(new StaticFileOptions()
        {
            FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Picture")),

            RequestPath = new PathString("/staticPicture")
        });

        app.UseWelcomePage("/aspnetcore");

        app.Run();
    }
}