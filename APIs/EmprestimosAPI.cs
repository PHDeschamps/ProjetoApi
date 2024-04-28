using Microsoft.EntityFrameworkCore;
public static class EmprestimosAPI 
{
    public static void MapEmprestimosApi(this WebApplication app)
    {
        var group = app.MapGroup("/emprestimos");

        group.MapGet("/", async (BancoDeDados db) =>
            //select * from emprestimos
            await db.Emprestimos.ToListAsync()
        );

        group.MapPost("/", async (Emprestimo emprestimo, BancoDeDados db) =>
        {
            db.Emprestimos.Add(emprestimo);
            //insert into...
            await db.SaveChangesAsync();

            return Results.Created($"/emprestimos/{emprestimo.Id}", emprestimo);
        }
        );

        group.MapPut("/{id}", async (int id, Emprestimo emprestimoAlterado, BancoDeDados db) =>
        {
            //select * from emprestimos where id = ?
            var emprestimo = await db.Emprestimos.FindAsync(id);
            if (emprestimo is null)
            {
                return Results.NotFound();
            }
            emprestimo.PessoaNome = emprestimoAlterado.PessoaNome;
            emprestimo.LivroTitulo = emprestimoAlterado.LivroTitulo;

            //update....
            await db.SaveChangesAsync();

            return Results.NoContent();
        }
        );

        group.MapDelete("/{id}", async (int id, BancoDeDados db) =>
        {
            if (await db.Emprestimos.FindAsync(id) is Emprestimo emprestimo)
            {
                //Operações de exclusão
                db.Emprestimos.Remove(emprestimo);
                await db.SaveChangesAsync();
                return Results.NoContent();
            }
            return Results.NotFound();
        }
        );
    }
}