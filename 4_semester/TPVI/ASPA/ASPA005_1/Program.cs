using DAL004;
using Microsoft.AspNetCore.Diagnostics;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();

        using (IRepository repository = Repository.Create(@"D:\BSTU\2_course\4_semester\TPVI\lab_4\DAL004\ASPA004_3\Celebrities"))
        {
            app.UseExceptionHandler("/Celebrities/Error");

            app.MapGet("/Celebrities", () => repository.getAllCelebrities());

            app.MapGet("/Celebrities/{id:int}", (int id) =>
            {
                Celebrity? celebrity = repository.getCelebrityById(id);
                if (celebrity == null) throw new FoundByIdException($"Celebrity Id = {id}");
                return celebrity;
            });

            app.MapPost("/Celebrities", (Celebrity celebrity) =>
            {
                int? id = repository.addCelebrity(celebrity);
                if (id == null) throw new AddCelebrityException("/Celebrities error, id == null");
                if (repository.SaveChanges() <= 0) throw new SaveException("/Celebrities error, SaveChanges() <= 0");
                return new Celebrity((int)id, celebrity.Firstname, celebrity.Surname, celebrity.PhotoPath);
            })
            .AddEndpointFilter(async (context, next) =>
            {
                Celebrity? celebrity = context.GetArgument<Celebrity>(0);
                if (celebrity == null) throw new AbsurdeException("POST /Celebrities error, Server Error");
                if (celebrity.Surname == null || celebrity.Surname.Length < 2) throw new ConflictException("POST /Celebrities error, Surname is wrong");
                return await next(context);
            })
            .AddEndpointFilter(async (context, next) =>
            {
                Celebrity? celebrity = context.GetArgument<Celebrity>(0);
                if (celebrity == null) throw new AbsurdeException("POST /Celebrities error, Server Error");
                if (repository.doesSurnameExists(celebrity.Surname)) throw new ConflictException("POST / Celebrities error, Surname is doubled");
                return await next(context);
            })
            .AddEndpointFilter(async (context, next) =>
            {
                Celebrity? celebrity = context.GetArgument<Celebrity>(0);
                if (celebrity == null) throw new AbsurdeException("POST /Celebrities error, Server Error");
                var basePath = "D:\\BSTU\\2_course\\4_semester\\TPVI\\ASPA\\Ñelebrities";
                var fileName = Path.GetFileName(celebrity.PhotoPath);
                var filePath = Path.Combine(basePath, fileName);
                if (!File.Exists(filePath))
                {
                    context.HttpContext.Response.Headers.Append("X-Celebrity", $"Not found = {fileName}");
                }
                return await next(context);
            });
            app.MapDelete("/Celebrities/{id:int}", (int id) =>
            {
                if (!repository.delCelebrityById(id)) throw new DelByIdException($"DELETE /Celebrities error, ID = {id}");
                return Results.Ok($"Celebrity with Id = {id} deleted");
            });

            app.MapPut("/Celebrities/{id:int}", (int id, Celebrity celebrity) =>
            {
                int? newId = repository.updCelebrityById(id, celebrity);
                if (newId == null) throw new UpdException($"Id={id}");

                return new Celebrity((int)newId, celebrity.Firstname, celebrity.Surname, celebrity.PhotoPath);
            });

            app.MapFallback((HttpContext ctx) => Results.NotFound(new { error = $"path {ctx.Request.Path} not supported" }));

            app.Map("/Celebrities/Error", (HttpContext ctx) =>
            {
                Exception? ex = ctx.Features.Get<IExceptionHandlerFeature>()?.Error;
                IResult rc = Results.Problem(detail: "Panic", instance: app.Environment.EnvironmentName, title: "ASPA004", statusCode: 500);
                if (ex != null)
                {
                    if (ex is ConflictException) rc = Results.Conflict(ex.Message);
                    if (ex is AbsurdeException) rc = Results.BadRequest(ex.Message);
                    if (ex is UpdatedException) rc = Results.NotFound(ex.Message);
                    if (ex is DelByIdException) rc = Results.NotFound(ex.Message);
                    if (ex is FileNotFoundException fileNotFound) rc = Results.Problem(detail: $"Could not find file '{fileNotFound.FileName}'", instance: app.Environment.EnvironmentName, title: "ASPA004", statusCode: 500);
                    if (ex is FoundByIdException) rc = Results.NotFound(ex.Message);
                    if (ex is BadHttpRequestException) rc = Results.BadRequest(ex.Message);
                    if (ex is SaveException) rc = Results.Problem(title: "ASPA004/SaveChanges", detail: ex.Message, instance: app.Environment.EnvironmentName, statusCode: 500);
                    if (ex is AddCelebrityException) rc = Results.Problem(title: "ASPA004/addCelebrity", detail: ex.Message, instance: app.Environment.EnvironmentName, statusCode: 500);
                }
                return rc;
            });

            app.Run();
        }

    }
}
