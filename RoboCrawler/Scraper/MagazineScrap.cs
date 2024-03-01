using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

public class MagazineScrap 
{
    public string ObterPreco(string descricaoProduto, int produtoID)
    {
        try
        {
            // Inicializa o ChromeDriver
            using (IWebDriver driver = new ChromeDriver())
            {
                // Abre a página
                driver.Navigate().GoToUrl($"https://www.magazineluiza.com.br/busca/{descricaoProduto}");
                
                // Aguarda um tempo fixo para permitir que a página seja carregada (você pode ajustar conforme necessário)
                System.Threading.Thread.Sleep(5000);

                // Encontra o elemento que possui o atributo data-testid
                IWebElement priceElement = driver.FindElement(By.CssSelector("[data-testid='price-value']"));
                Console.WriteLine(priceElement.Text);

                // Verifica se o elemento foi encontrado
                if (priceElement != null)
                {
                    // Obtém o preço do primeiro produto
                    string MagazinePreco = priceElement.Text;

                    // Registra o log com o ID do produto
                    MagazineLog("0001", "Pedro", DateTime.Now, "Web Scraping - Magazine Luiza", "Sucesso", produtoID);



                    // Retorna o preço
                    return MagazinePreco;
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
            { CodRobot = CodRobot, UserName = UserName, LogDate = logDate, StateDescription = StateDescription, ResultFeedBack = ResultFeedBack, ProductID = ProductID };
            context.Logs.Add(log);
            context.SaveChanges();
        }

    }
}
