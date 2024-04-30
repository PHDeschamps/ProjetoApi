using Microsoft.EntityFrameworkCore;

public class BancoDeDados : DbContext
{
    //Configuração da conexão
    protected override void OnConfiguring(DbContextOptionsBuilder builder) 
    {builder.UseMySQL("server=localhost;port=3306;database=projeto;user=root;password=positivo");}

    //Mapeamento das tabelas
    public DbSet<Pessoa> Pessoas { get; set; }
    public DbSet<Livro> Livros { get; set; }
    public DbSet<Emprestimo> Emprestimos { get; set; }

}
