using Microsoft.EntityFrameworkCore;
public static class LivrosAPI 
{
    public static void MapLivrosApi(this WebApplication app)
    {
        var group = app.MapGroup("/livros");

        group.MapGet("/", async (BancoDeDados db) =>
            //select * from livros
            await db.Livros.ToListAsync()
        );

        group.MapPost("/", async (Livro livro, BancoDeDados db) =>
        {
            db.Livros.Add(livro);
            //insert into...
            await db.SaveChangesAsync();

            return Results.Created($"/livros/{livro.Id}", livro);
        }
        );

        group.MapPut("/{id}", async (int id, Livro livroAlterado, BancoDeDados db) =>
        {
            //select * from livros where id = ?
            var livro = await db.Livros.FindAsync(id);
            if (livro is null)
            {
                return Results.NotFound();
            }
            livro.Titulo = livroAlterado.Titulo;
            livro.Autor = livroAlterado.Autor;
            livro.Genero = livroAlterado.Genero;

            //update....
            await db.SaveChangesAsync();

            return Results.NoContent();
        }
        );

        group.MapDelete("/{id}", async (int id, BancoDeDados db) =>
        {
            if (await db.Livros.FindAsync(id) is Livro livro)
            {
                //Operações de exclusão
                db.Livros.Remove(livro);
                await db.SaveChangesAsync();
                return Results.NoContent();
            }
            return Results.NotFound();
        }
        );
    }
}