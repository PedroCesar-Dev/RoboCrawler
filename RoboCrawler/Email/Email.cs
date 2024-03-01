using System;
using System.Net;
using System.Net.Mail;
using HtmlAgilityPack;
using static System.Net.WebRequestMethods;

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
                var newurl = nomeProdutoMercadoLivre.Replace(' ','+');


                string url = $"https://lista.mercadolivre.com.br/{newurl}";
                HtmlWeb web = new HtmlWeb();                
                HtmlDocument document = web.Load(url);          
                var hyperlink = document.DocumentNode.SelectSingleNode("//*[@id=\":R4ajb:\"]/div/div/div[2]/a").Attributes["href"];               


                MailMessage mensagem = new MailMessage(login, "pedro9563@hotmail.com")
                {
                    Subject = "Resultado da comparação de preços",
                    Body =
                    $@"<html>
                      <body>
                            <h1>Mercado Livre</h1>
                            <p>Produto: {nomeProdutoMercadoLivre}</p>
                            <p>Preço: {precoProdutoMercadoLivre}</p>
                        
                            <h1>Magazine Luiza</h1>
                            <p>Produto: {nomeProdutoMagazineLuiza}</p>
                            <p>Preço: {precoProdutoMagazineLuiza}</p>

                            
                            <h1>Melhor compra</h1>
                            <a href='https://lista.mercadolivre.com.br/{hyperlink}'>Clique aqui: Mercado Livre</a>                         
                      </body>
                      </html>
                     "

                };
                mensagem.IsBodyHtml = true;
                client.Send(mensagem);
                Console.WriteLine("Email enviado com sucesso");
            }
            else if (compareReturn == 2)
            {
                var newurl = nomeProdutoMagazineLuiza.Replace(' ', '+');
                string url = $"https://www.magazineluiza.com.br/busca/{newurl}";
                HtmlWeb web = new HtmlWeb();
                HtmlDocument document = web.Load(url);                
                var hyperlink = document.DocumentNode.SelectSingleNode("//*[@id=\"__next\"]/div/div[8]/div/ul/li[1]/a").Attributes["href"];

                MailMessage mensagem = new MailMessage(login, "pedro9563@hotmail.com")
                {
                    Subject = "Resultado da comparação de preços",
                    Body =
                    $@"<html>
                      <body>
                            <h1>Mercado Livre</h1>
                            <p>Produto: {nomeProdutoMercadoLivre}</p>
                            <p>Preço: {precoProdutoMercadoLivre}</p>
                        
                            <h1>Magazine Luiza</h1>
                            <p>Produto: {nomeProdutoMagazineLuiza}</p>
                            <p>Preço: {precoProdutoMagazineLuiza}</p>

                            
                            <h1>Melhor compra</h1>
                            <a href='https://www.magazineluiza.com.br/busca/{hyperlink}'>Clique aqui: Magazine Luiza</a>                         
                      </body>
                      </html>
                     "

                };
                mensagem.IsBodyHtml = true;
                client.Send(mensagem);
                Console.WriteLine("Email enviado com sucesso");
            }
            else if (compareReturn == 3)
            {
                var newurlMercado = nomeProdutoMercadoLivre.Replace(' ', '+');
                var newurlMagazine = nomeProdutoMagazineLuiza.Replace(' ', '+');
                string urlMercado = $"https://lista.mercadolivre.com.br/{newurlMercado}";
                string urlMagazine = $"https://www.magazineluiza.com.br/busca/{newurlMagazine}";
                HtmlWeb web = new HtmlWeb();
                HtmlDocument documentMercado = web.Load(urlMercado);
                HtmlDocument documentMagazine = web.Load(urlMagazine);
                var hyperlinkMercado = documentMercado.DocumentNode.SelectSingleNode("//*[@id=\":R4ajb:\"]/div/div/div[2]/a").Attributes["href"];
                var hyperlinkMagazine = documentMagazine.DocumentNode.SelectSingleNode("//*[@id=\"__next\"]/div/div[8]/div/ul/li[1]/a").Attributes["href"];
                MailMessage mensagem = new MailMessage(login, "pedro9563@hotmail.com")
                {
                    Subject = "Resultado da comparação de preços",
                    Body =
                    $@"<html>
                      <body>
                            <h1>Mercado Livre</h1>
                            <p>Produto: {nomeProdutoMercadoLivre}</p>
                            <p>Preço: {precoProdutoMercadoLivre}</p>
                        
                            <h1>Magazine Luiza</h1>
                            <p>Produto: {nomeProdutoMagazineLuiza}</p>
                            <p>Preço: {precoProdutoMagazineLuiza}</p>

                            
                            <h1>Melhor compra</h1>
                            <p>Aviso: Ambas compras possuem o mesmo preço</p>
                            <a href='https://lista.mercadolivre.com.br/{hyperlinkMercado}'>Clique aqui: Mercado Livre</a>  
                            <a href='https://www.magazineluiza.com.br/busca/{hyperlinkMagazine}'>Clique aqui: Magazine Luiza</a>                      
                      </body>
                      </html>
                     "

                };
                mensagem.IsBodyHtml = true;
                client.Send(mensagem);
                Console.WriteLine("Email enviado com sucesso");
            }           
        }
        Console.WriteLine(nomeProdutoMercadoLivre);
        Console.WriteLine(precoProdutoMercadoLivre);
        Console.WriteLine(nomeProdutoMagazineLuiza);
        Console.WriteLine(precoProdutoMagazineLuiza);
    }
}