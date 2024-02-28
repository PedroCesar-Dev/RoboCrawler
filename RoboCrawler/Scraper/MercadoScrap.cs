using HtmlAgilityPack;
using System;

public class MercadoScrap
{
    public string ObterPreco(string descricaoProduto, int produtoID)
    {
        // URL da pesquisa no Mercado Livre com base na descrição do produto
        string url = $"https://lista.mercadolivre.com.br/{descricaoProduto}";

        try
        {
            // Inicializa o HtmlWeb do HtmlAgilityPack
            HtmlWeb web = new HtmlWeb();

            // Carrega a página de pesquisa do Mercado Livre
            HtmlDocument document = web.Load(url);

            // Encontra o elemento que contém o preço do primeiro produto            
            HtmlNode firstProductPriceNode = document.DocumentNode.SelectSingleNode("//span[@class='andes-money-amount__fraction']");
            Console.WriteLine(firstProductPriceNode.InnerText);

            // Verifica se o elemento foi encontrado
            if (firstProductPriceNode != null)
            {
                // Obtém o preço do primeiro produto
                string firstProductPrice = firstProductPriceNode.InnerText.Trim();

                // Registra o log com o ID do produto
                RegistrarLog("0001", "Pedro", DateTime.Now, "Scraping Mercado Livre", "Sucesso", produtoID);

                // Retorna o preço
                return firstProductPrice;
            }
            else
            {
                Console.WriteLine("Preço não encontrado.");

                // Registra o log com o ID do produto
                RegistrarLog("0001", "Pedro", DateTime.Now, "Scraping Mercado Livre", "Preço não encontrado", produtoID);

                return null;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao acessar a página: {ex.Message}");

            // Registra o log com o ID do produto
            RegistrarLog("0001", "Pedro", DateTime.Now, "Scraping Mercado Livre", $"Erro: {ex.Message}", produtoID);

            return null;
        }
    }

    private void RegistrarLog(string CodRobot, string UserName, DateTime logDate, string StateDescription, string ResultFeedBack, int ProductID)
    {
        using (var context = new CrawlerContext())
        {
            var log = new Log
            { CodRobot = CodRobot, UserName = UserName, LogDate = logDate, StateDescription = StateDescription, ResultFeedBack = ResultFeedBack, ProductID = ProductID };
            context.Logs.Add(log);
            context.SaveChanges();
        }
    }
}
