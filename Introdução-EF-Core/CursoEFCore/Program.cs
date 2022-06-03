using System;
using System.Collections.Generic;
using System.Linq;
using CursoEFCore.Domain;
using CursoEFCore.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace CursoEFCore
{
    class Program
    {
        static void Main(string[] args)
        {
            // using var db = new Data.Configurations.ApplicationContext();

            // // db.Database.Migrate();

            // var existe = db.Database.GetAppliedMigrations().Any();
            // if (existe)
            // {
            //     //
            // }
            // InserirDados();
            // InserirDadosEmMassa();      
            // ConsultarDados();     
            // CadastrarPedido();     
            // ConsultarPedidoCarregamentoAdiantado();
            AtualizarDados();
            // RemoverRegistro();
        }
        private static void RemoverRegistro()
        {
            using var db = new Data.Configurations.ApplicationContext();
            // var cliente = db.Clientes.Find(6);// utiliza a chave primaria 
            var cliente = new Cliente { Id = 7 };
            // db.Clientes.Remove(cliente);
            // db.Remove(cliente);
            db.Entry(cliente).State = EntityState.Deleted;

            db.SaveChanges();
        }

        private static void AtualizarDados()
        {
            using var db = new Data.Configurations.ApplicationContext();
            var cliente = db.Clientes.Find(8);
            cliente.Nome = "Cliente Alterado Passo 1";
            // Cenário Desconectado é quando os dados não estão instânciados ainda

            // var cliente = new Cliente
            // {
            //     Id = 8
            // };
            // var clienteDesconectado = new
            // {
            //     Nome = "Cliente Desconectado Passo 3",
            //     Telefone = "1284164646"
            // };
            // db.Attach(cliente);
            // db.Entry(cliente).CurrentValues.SetValues(clienteDesconectado);
            // db.Clientes.Update(cliente);// o EF Core começa a rastrear as entidades
            // e quando chama o método update ele sobrescreve todas as propriedade internamente e informa que todas as propriedades sofreram alterações
            db.SaveChanges(); // usando apenas o SaveChanges só vai alterar algum campo na base de dados se for mudado...
            // como atualizar apenas os campos que eu quero sem te me, pois desta forma é atualizada todas as colunas novamente essa um abordagem ruim
            // 
            
            // //pode usar o método para restreamento que é colocadop antes do SaveChange
            // db.Entry(cliente).State = EntityState.Modified;




        }
        private static void ConsultarPedidoCarregamentoAdiantado()// Usando carregamento adiantado
        {
            using var db = new Data.Configurations.ApplicationContext();
            var pedidos = db.Pedidos.Include(p => p.Itens)
                                    .ThenInclude(p => p.Produto)
                                    .ToList(); //pode Include("Itens") com uma string que o EF Core também conseguirar rastrear

            Console.WriteLine(pedidos.Count);
        }


        private static void CadastrarPedido()
        {
            using var db = new Data.Configurations.ApplicationContext();

            var cliente = db.Clientes.FirstOrDefault();
            var produto = db.Produtos.FirstOrDefault();

            var pedido = new Pedido
            {
                ClienteId = cliente.Id,
                IniciadoEM = DateTime.Now,
                FinalizadoEm = DateTime.Now,
                Observacoes = "Pedido Teste",
                Status = StatusPedido.Analise,
                TipoFrete = TipoFrete.SemFrete,
                Itens = new List<PedidoItem>
                {
                    new PedidoItem
                    {
                        ProdutoId = produto.Id,
                        Desconto =0,
                        Quantidade = 1,
                        Valor = 10
                    }
                }
            };

            db.Pedidos.Add(pedido);


            db.SaveChanges();

        }



        private static void ConsultarDados()
        {
            using var db = new Data.Configurations.ApplicationContext();
            // var consultarPorSintaxe = (from c in db.Clientes where c.Id>0 select c).ToList();
            var consultaPorMetodo = db.Clientes.Where(p => p.Id > 0)
                                               .OrderBy(p =>p.Id)
                                               .ToList();
            // var consultaPorMetodo = db.Clientes.AsNoTracking().Where(p=>p.Id>0).ToList();

            foreach (var cliente in consultaPorMetodo)
            {
                Console.WriteLine($"Consultando Cliente:  {cliente.Id}");
                // db.Clientes.Find(cliente.Id);//primeiro procura na meméria depois vai procurar na base de dados 
                db.Clientes.FirstOrDefault(p => p.Id == cliente.Id);
            }
        }

        private static void InserirDadosEmMassa()
        {
            var produto = new Produto
            {
                Descricao = "Produto Teste",
                CodigoBarras = "12345674891231",
                Valor = 10m,
                Ativo = true
            };
            var cliente = new Cliente
            {
                Nome = "Matheus",
                CEP = "99999999",
                Cidade = "Cachoeiro de Itapemririm",
                Estado = "SE",
                Telefone = "99999999999"
            };
            var listaClientes = new[]
            {
                new Cliente
                {
                    Nome = "teste 1",
                    CEP = "99999999",
                    Cidade = "Cachoeiro de Itapemririm",
                    Estado = "SE",
                    Telefone = "99999999999"
                },
                new Cliente
                {
                    Nome = "teste 2",
                    CEP = "99999999",
                    Cidade = "Cachoeiro de Itapemririm",
                    Estado = "SE",
                    Telefone = "99999999999"
                },
            };

            using var db = new Data.Configurations.ApplicationContext();
    // Inserindo registros em massa em seu banco de dados . na versão anterior não era possivel você tinha uma lsta de 10 produtos para pessistir em seu banco de dados, então o EF fazia 10 interações com o banco isso fazia que degrada-se a performace da sua aplicação.

    // pacote de log


            // db.AddRange(produto, cliente);
            db.Set<Cliente>().AddRange(listaClientes);
            // db.Clientes.AddRange(listaClientes);
            var registros = db.SaveChanges();
            Console.WriteLine($"Total de Registro(s): {registros}");
        }


        private static void InserirDados()
        {
        //     var produto = new Produto
        //     {
        //         Descricao = "Produto Teste",
        //         CodigoBarras = "12345674891231",
        //         Valor = 10m,
        //         Ativo = true
        //     };

        //    using var db = new Data.Configurations.ApplicationContext();
        //    /// o indicado são essas duas primeiras
        //     db.Produtos.Add(produto); //-- atravez de uma propriedade espostas em seu contexto
        //     db.Set<Produto>().Add(produto); //ou atravéz do método set -- método genérico para esta adicionando
        //     db.Entry(produto).State = EntityState.Added;// forçando rastreamento de uma determinada entidade
        //     db.Add(produto);// própria instacia do próprio context - ira ter basicamente todos os metrodos a cima --- ele precisa de uma sobrecarga para descobri 
        //     //o dados não foi para o banco de dados ele apenas começou a ser rastreado em memooria com o EF core

        //     var registro = db.SaveChanges();//ele retorna a quantidade de registros que foram afetados em minha base de dados
        //     // é nescessário chamar esse SaveChanges para que o EF core intenda que eu quero pegar tudo que esta sendo rastreado, pelo EF core que está em memória
        //     //pegue essas informações e gere as instruções nescessárias para serem executadas no meu banco de dados, então todads as alterações de adicionar, 
        //     //remover ou atualizar ela só são de fato pessistida na nossa base de dados ou executados na nossa base de dados quando é chamado o método saveChanges 
        //     Console.WriteLine($"Total de Registro(s): {registro}");

        // oque é rastreado ou trackeado pelo EF Core é a istância do o
        //bjeto produto ele começa a rastrear aquela instacia especifica e não uma para cada método que eu chamar.
        // Eu tenho apenas uma instacia rastreada pelo EF Core
        
        }
    }
}
