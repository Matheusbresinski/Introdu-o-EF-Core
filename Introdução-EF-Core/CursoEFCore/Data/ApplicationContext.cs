using System;
using System.Linq;
using CursoEFCore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CursoEFCore.Data.Configurations
{
    public class ApplicationContext : DbContext
    {
        private static readonly ILoggerFactory _logger = LoggerFactory.Create(p=>p.AddConsole());
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        //EnableSensitiveDataLogging todas as informações são senciveis, por padrao ele não exibi os valores dos parametros que estão sendo gerados nele --- para gente consegui ver o valor apenas ablitando com esse método
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
             optionsBuilder
                .UseLoggerFactory(_logger)
                .EnableSensitiveDataLogging()
                .UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CursoEFCore;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False",
                p=>p.EnableRetryOnFailure(maxRetryCount: 2,//quantidade de tentativas
                                          maxRetryDelay: TimeSpan.FromSeconds(5),//a cada tentativa aguarde 5 segundos// quais são os códigos dos erros adicionais -- colocado apena o padrão // tentara conectar por 6 vez até completar 1 minuto EnableRetryOnFailure(vazio) //Aplicação Resiliente
                                          errorNumbersToAdd: null).MigrationsHistoryTable("curso_ef_core"));// removendo o nome padrão ba tabela do banco. 
                                         
        }     
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
            // MapearPropriedadesEsquecidas(modelBuilder);
        }

        private void MapearPropriedadesEsquecidas(ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                var properties = entity.GetProperties().Where(p=>p.ClrType == typeof(string));// lista de propriedade de entidades

                // verificando se essa propriedade foi ou na configurada ou não "CONFIGURANDO TODAS AS PROPRIEDADE QUE ESQUECEU NO BANCO"
                foreach (var property in properties)
                {
                    if(string.IsNullOrEmpty(property.GetColumnType())//verifa se foi feito o caminho e o tamanho da coluna 
                        && !property.GetMaxLength().HasValue)
                    {
                        // property.SetMaxLength(100);
                        property.SetColumnType("VARCHAR(100)");

                    }
                }
            

            }    
        }   
    }
}