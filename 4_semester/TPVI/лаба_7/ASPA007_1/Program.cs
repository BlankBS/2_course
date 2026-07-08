using ASPA007_1;
using DAL_Celebrity_MSSQL;
using static ASPA007_1.CelebritiesAPIExtensions;
using static ASPA007_1.CelebrityAPI;
using static ASPA007_1.MiddlewareErrorHandler;

internal class Program
{
	private static void Main(string[] args)
	{
        string CS = "Server=(localdb)\\MSSQLLocalDB;Database=LES01;Trusted_Connection=True;";
        Init init = new Init(CS);
        Init.Execute(delete: true, create: true);

        var builder = WebApplication.CreateBuilder(args);

		builder.AddCelebritiesConfiguration();
		builder.AddCelebritiesServices();
        //cshtml 
        builder.Services.AddRazorPages();
		builder.Services.AddRazorPages(options =>
		{
			options.Conventions.AddPageRoute("/Celebrities", "/");
			options.Conventions.AddPageRoute("/NewCelebrity", "/0");
			options.Conventions.AddPageRoute("/Celebrity", "/Celebrities/{id:int:min(1)}");
			options.Conventions.AddPageRoute("/Celebrity", "/{id:int:min(1)}");
		}

		);

		var app = builder.Build();
		app.UseStaticFiles();
		app.UseASPErrorHandler();

		if (!app.Environment.IsDevelopment())
		{
			app.UseExceptionHandler("/Error");
		}

		app.UseRouting();
		app.UseAuthorization();
		app.MapRazorPages();

		app.MapCelebrities();
		app.MapLifeevents();
		app.MapPhotoCelebrities();

		app.Run();
	}
}