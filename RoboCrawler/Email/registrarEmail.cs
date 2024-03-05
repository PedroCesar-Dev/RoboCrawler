using System;
using System.IO;
using System.Text.RegularExpressions;

public class registrarEmail
{
    public static void Registrar()
    {
        string userInput;

        try
        {
            // Verifica se o arquivo de registro está vazio
            if (!File.Exists("emailRegistro.txt") || string.IsNullOrWhiteSpace(File.ReadAllText("emailRegistro.txt")))
            {
                Console.WriteLine("Digite seu endereço de e-mail:");

                bool validEmail = false;
                string emailPattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";

                do
                {
                    userInput = Console.ReadLine();
                    validEmail = Regex.IsMatch(userInput, emailPattern);

                    if (!validEmail)
                    {
                        Console.WriteLine("Endereço de e-mail inválido. Por favor, tente novamente:");
                    }
                } while (!validEmail);
                File.WriteAllText("emailRegistro.txt", userInput);
                Console.WriteLine("Email salvo com sucesso!");
            }
            else
            {
                userInput = File.ReadAllText("emailRegistro.txt");
                Console.WriteLine($"Email Registrado: {userInput}");
            }
        }
        catch (IOException e)
        {
            Console.WriteLine($"Erro ao manipular o arquivo: {e.Message}");
        }
    }
}
