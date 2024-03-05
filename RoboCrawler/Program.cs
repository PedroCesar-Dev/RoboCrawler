using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

class Program 
{
    static List<Produto> produtosVerificados = new List<Produto>();
    static void Main(string[] args)
    {
        registrarEmail.Registrar();
        int interval = 300000;

        Timer timer = new Timer(verificar, null, 0, interval);


        while (true)
        {
            Thread.Sleep(Timeout.Infinite);
        }
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
                                    WriteLog("9563", "Pedro", DateTime.Now, "API Consulta - 001-site 1.ktempurl.com/api/v1/produto/getall", "Sucesso", produto.Id);

                                    MercadoScrap mercadoScraper = new MercadoScrap();
                                    var mercadoLivre = mercadoScraper.ObterPreco(produto.Nome, produto.Id);
                                    
                                    MagazineScrap magazineScraper = new MagazineScrap();
                                    var magazineLuiza = magazineScraper.ObterPreco(produto.Nome, produto.Id);                                    
                                    
                                    var compareReturn = Compare.comparePrice(mercadoLivre.preco, magazineLuiza.preco, produto.Id);
                                    string Destino = File.ReadAllText("emailRegistro.txt");
                                    Email.EnviarEmail(produto.Nome, mercadoLivre.preco, produto.Nome, magazineLuiza.preco, compareReturn, mercadoLivre.hrefUrl, magazineLuiza.hrefUrl, Destino);
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
        List<Produto> produtos = JsonConvert.DeserializeObject<List<Produto>>(responseData);
        return produtos;
    }

    static bool ProdutoJaRegistrado(int ProductID)
    {
        using (var context = new CrawlerContext())
        {
            return context.Logs.Any(log => log.iDlOG == ProductID && log.CodigoRobo == "9563");
        }
    }
    
    static void WriteLog(string CodRobot, string UserName, DateTime logDate, string StateDescription, string ResultFeedBack, int ProductID)
    {
        using (var context = new CrawlerContext())
        {
            var log = new Log
            { CodigoRobo = CodRobot,UsuarioRobo = UserName,DateLog = logDate, Etapa = StateDescription, InformacaoLog = ResultFeedBack, IdProdutoAPI = ProductID };          
            context.Logs.Add(log);
            context.SaveChanges();
        }
    }
}