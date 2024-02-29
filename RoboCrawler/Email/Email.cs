using System.Net;
using System.Net.Mail;

public class Email
{
    public static void EnviarEmail(string nomeProdutoMercadoLivre, string precoProdutoMercadoLivre, string nomeProdutoMagazineLuiza, string precoProdutoMagazineLuiza, int compareReturn)
    {
        string smtpServer = "smtp-mail.outlook.com";
        int port = 587;
        string login = "pedrocesar.senai.teste@outlook.com";
        string senha = "c6o36-u52b30";

        using (SmtpClient client = new SmtpClient(smtpServer, port))
        {
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(login, senha);
            client.EnableSsl = true;

            if (compareReturn == 1)
            {
                MailMessage mensagem = new MailMessage(login, "pedro9563@hotmail.com")
                {
                    Subject = "Resultado da comparação de preços",
                    Body = $"Mercado Livre\n" +
                          $"Produto: {nomeProdutoMercadoLivre}\nPreço: {precoProdutoMercadoLivre}\n\n\n\n" +
                          $"Magazine Luiza" +
                          $"Produto: {nomeProdutoMagazineLuiza}\nPreço: {precoProdutoMagazineLuiza}\n\n\n\n" +
                          $"Melhor compra:\n" +
                          $"Mercado Livre"

                };
                client.Send(mensagem);
            }
            else if (compareReturn == 2)
            {
                MailMessage mensagem = new MailMessage(login, "pedro9563@hotmail.com")
                {
                    Subject = "Resultado da comparação de preços",
                    Body = $"Mercado Livre\n" +
                          $"Produto: {nomeProdutoMercadoLivre}\nPreço: {precoProdutoMercadoLivre}\n\n\n\n" +
                          $"Magazine Luiza" +
                          $"Produto: {nomeProdutoMagazineLuiza}\nPreço: {precoProdutoMagazineLuiza}\n\n\n\n" +
                          $"Melhor compra:\n" +
                          $"Magazine Luiza - clique aqui"

                };
                client.Send(mensagem);
            }
            else if (compareReturn == 3)
            {
                MailMessage mensagem = new MailMessage(login, "pedro9563@hotmail.com")
                {
                    Subject = "Resultado da comparação de preços",
                    Body = $"Mercado Livre\n" +
                          $"Produto: {nomeProdutoMercadoLivre}\nPreço: {precoProdutoMercadoLivre}\n\n\n\n" +
                          $"Magazine Luiza" +
                          $"Produto: {nomeProdutoMagazineLuiza}\nPreço: {precoProdutoMagazineLuiza}\n\n\n\n" +
                          $"Melhor compra:\n" +
                          $"Ambos possuem o mesmo preço"

                };
                client.Send(mensagem);
            }           
        }
        Console.WriteLine(nomeProdutoMercadoLivre);
        Console.WriteLine(precoProdutoMercadoLivre);
        Console.WriteLine(nomeProdutoMagazineLuiza);
        Console.WriteLine(precoProdutoMagazineLuiza);
    }
}