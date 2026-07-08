using DAL004;
using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

Repository.JSONFileName = "Celebrities.json";
// Используем репозиторий (создаем его один раз для простоты примера)
var repository = Repository.Create("Celebrities");

app.UseExceptionHandler("/Celebrities/Error");

// GET: Получить всех
app.MapGet("/Celebrities", () => repository.getAllCelebrities());

// GET: Получить одного по ID
app.MapGet("/Celebrities/{id:int}", (int id) => {
    var celebrity = repository.getCelebrityById(id);
    if (celebrity == null) throw new FoundByIdException($"Celebrity Id = {id}");
    return Results.Ok(celebrity);
});

// POST: Добавить
app.MapPost("/Celebrities", (Celebrity celebrity) => {
    int? id = repository.addCelebrity(celebrity);
    if (id == null) throw new AddCelebrityException("/Celebrities error, id == null");
    if (repository.SaveChanges() <= 0) throw new SaveException("/Celebrities error, SaveChanges <= 0");
    return Results.Ok(repository.getCelebrityById((int)id));
});

// Обработка ошибок (RFC 7231)
app.Map("/Celebrities/Error", (HttpContext ctx) => {
    var feature = ctx.Features.Get<IExceptionHandlerFeature>();
    var ex = feature?.Error;
    IResult rc = Results.Problem(
       detail: "Panic",
       instance: app.Environment.EnvironmentName, 
       title: "ASPA004",
       statusCode: 500
   );

    if (ex != null)
    {
        if (ex is FoundByIdException)
        {
            rc = Results.Problem(detail: ex.Message, statusCode: 404);
        }
    }

    return rc;
});

app.MapFallback((HttpContext ctx) =>
{
    return Results.NotFound(new
    {
        error = $"path {ctx.Request.Path} not supported"
    });
});
app.Run();

public class FoundByIdException : Exception
{
    public FoundByIdException(string message) : base($"Found by Id: {message}") { }
}
public class SaveException : Exception
{
    public SaveException(string message) : base($"SaveChanges error: {message}") { }
}
public class AddCelebrityException : Exception
{
    public AddCelebrityException(string message) : base($"AddCelebrityException error: {message}") { }
}