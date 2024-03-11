using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

public class MagazineScrap 
{
    public ScrapMagazine ObterPreco(string descricaoProduto, int produtoID)
    {
        try
        {
            // Inicializa o ChromeDriver
            using (IWebDriver driver = new ChromeDriver())
            {
                // Abre a página
                driver.Navigate().GoToUrl($"https://www.magazineluiza.com.br/busca/{descricaoProduto}");                               
                System.Threading.Thread.Sleep(5000);
                IWebElement priceElement = driver.FindElement(By.CssSelector("[data-testid='price-value']"));
                IWebElement urlElement = driver.FindElement(By.CssSelector("[data-testid='product-card-container']"));
                Console.WriteLine(priceElement.Text);

                if (priceElement != null)
                {
                    ScrapMagazine magazine = new ScrapMagazine();
                    magazine.preco = priceElement.Text;
                    magazine.hrefUrl = urlElement.GetAttribute("href");
                    MagazineLog("0001", "Pedro", DateTime.Now, "Web Scraping - Magazine Luiza", "Sucesso", produtoID);

                    return magazine;
                }
                else
                {
                    Console.WriteLine("Preço não encontrado.");

                    // Registra o log com o ID do produto
                    MagazineLog("0001", "Pedro", DateTime.Now, "Web Scraping - Magazine Luiza", "Preço não encontrado", produtoID);

                    return null;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao acessar a página: {ex.Message}");

            // Registra o log com o ID do produto
            MagazineLog("0001", "Pedro", DateTime.Now, "Web Scraping Magazine - Luiza", $"Erro: {ex.Message}", produtoID);

            return null;
        }
    }

    private static void MagazineLog(string CodRobot, string UserName, DateTime logDate, string StateDescription, string ResultFeedBack, int ProductID)
    {

        using (var context = new CrawlerContext())
        {
            var log = new Log
            { CodigoRobo = CodRobot, UsuarioRobo = UserName, DateLog = logDate, Etapa = StateDescription, InformacaoLog = ResultFeedBack, IdProdutoAPI = ProductID };
            context.LOGROBO.Add(log);
            context.SaveChanges();
        }

    }
}
