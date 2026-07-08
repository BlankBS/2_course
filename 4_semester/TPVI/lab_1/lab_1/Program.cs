using Microsoft.AspNetCore.HttpLogging; //подклчение пространства имен для логирования HTTP-запросов

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args); // создание экземпляра WebApplicationBuilder с переданными аргументами
        builder.Services.AddHttpLogging(options => // добавление службы логирования HTTP-запросов
        {
            options.LoggingFields = HttpLoggingFields.All; // указвание, что нужно логировать все поля HTTP-запросов
        });

        builder.Services.AddRazorPages(); // добавление службы-контейнера
        
        var app = builder.Build(); // создание экземпляра программы
        app.UseHttpLogging(); // включение логирования HTTP-запросов
        app.MapGet("/asp", () => "Мое первое ASPA"); // вывод приветственного сообщения
        
        // настройка конвейера обработки HTTP-запросов
        if(!app.Environment.IsDevelopment()) // среда не является средой разработки
        {
            app.UseExceptionHandler("/Error"); // использование обработчика исключений для перенаправления на страницу ошибок
            app.UseHsts(); // включение HTTP Strict Transport Security (HSTS) для повышения безопасности
        }

        app.UseHttpsRedirection(); // перенаправление HTTP-запросов на HTTPS
        app.UseStaticFiles(); // включение поддержки статических файлов
        app.UseRouting(); // включение маршрутизатора 
        app.UseAuthorization(); // включение авторизации
        app.MapRazorPages(); // настройка маршрутизации для Razor Pages

        app.Run(); // запуск приложения
    }
}