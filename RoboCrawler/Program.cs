using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

class Program 
{
    static List<Produto> produtosVerificados = new List<Produto>();
    static void Main(string[] args)
    {
        int interval = 6000;

        Timer timer = new Timer(verificar, null, 0, interval);

        Console.WriteLine("Pressione qualquer tecla para sair...");
        Console.ReadKey();
    }
    static async void verificar(object state)
    {
            string username = "11164448";
            string password = "60-dayfreetrial";
            string url = "http://regymatrix-001-site1.ktempurl.com/api/v1/produto/getall";

            try {
                using (HttpClient client = new HttpClient())
                {
                    var byteArray = Encoding.ASCII.GetBytes($"{username}:{password}");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseData = await response.Content.ReadAsStringAsync();                    
                        List<Produto> novosProdutos = ObterNovosProdutos(responseData);
                        foreach (Produto produto in novosProdutos)
                        {
                            if (!produtosVerificados.Exists(p => p.Id == produto.Id))
                            {
                                Console.WriteLine($"Novo Produto encontrado: ID {produto.Id}, Nome: {produto.Nome}");
                                produtosVerificados.Add(produto);
                                if(!ProdutoJaRegistrado(produto.Id))
                                {
                                    WriteLog("0001", "Pedro", DateTime.Now, "API Consulta - 001-site 1.ktempurl.com/api/v1/produto/getall", "Sucesso", produto.Id);

                                    MercadoScrap mercadoScraper = new MercadoScrap();
                                    mercadoScraper.ObterPreco(produto.Nome, produto.Id);

                                    MagazineScrap magazineScraper = new MagazineScrap();
                                    magazineScraper.ObterPreco(produto.Nome, produto.Id);
                                }
                                
                            }
                        }
                    } else {
                        Console.WriteLine($"Erro: {response.StatusCode}");
                    }
                }
            } catch (Exception ex) {
                Console.WriteLine($"Erro ao fazer a requisição: {ex.Message}");
            }
    }
    static List<Produto> ObterNovosProdutos(string responseData)
    {
        // Desserializar os dados da resposta para uma lista de produtos
        List<Produto> produtos = JsonConvert.DeserializeObject<List<Produto>>(responseData);
        return produtos;
    }

    // Método para verificar se o produto já foi registrado no banco de dados
    static bool ProdutoJaRegistrado(int ProductID)
    {
        using (var context = new CrawlerContext())
        {
            return context.Logs.Any(log => log.IdLog == ProductID);
        }
    }
    
    static void WriteLog(string CodRobot, string UserName, DateTime logDate, string StateDescription, string ResultFeedBack, int ProductID)
    {
        using (var context = new CrawlerContext())
        {
            var log = new Log
            { CodRobot = CodRobot,UserName = UserName,LogDate = logDate, StateDescription = StateDescription, ResultFeedBack = ResultFeedBack, ProductID = ProductID };          
            context.Logs.Add(log);
            context.SaveChanges();
        }
    }
}