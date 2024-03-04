using System;
using System.Net;
using System.Net.Mail;
using HtmlAgilityPack;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using static System.Net.WebRequestMethods;

public class Email
{
    public static void EnviarEmail(string nomeProdutoMercadoLivre, string precoProdutoMercadoLivre, string nomeProdutoMagazineLuiza, string precoProdutoMagazineLuiza, int compareReturn)
    {
        HtmlWeb web = new HtmlWeb();
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
                var urlMod = nomeProdutoMercadoLivre.Replace(' ', 'þ');
                var url = $"https://lista.mercadolivre.com.br/{urlMod}";
                
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
                            <a href='{hyperlink}'>Clique aqui: Mercado Livre</a>                         
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
                var urlMod = nomeProdutoMagazineLuiza.Replace(' ', '+');
                var url = $"https://lista.mercadolivre.com.br/{urlMod}";
                
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
                            <a href='{hyperlink}'>Clique aqui: Magazine Luiza</a>                         
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
                            <a href='{hyperlinkMercado}'>Clique aqui: Mercado Livre</a>  
                            <a href='{hyperlinkMagazine}'>Clique aqui: Magazine Luiza</a>                      
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