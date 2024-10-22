using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;

class Program
{
    private const string Salt = "WTMI_7_16_9_dev_salt_change_please";

    static void Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Uso: app <senha> <hash_para_comparar>");
            return;
        }

        string senha = args[0];
        string hashRecebido = args[1];

        string hashGerado = CalculateMD5Hash(Salt + senha);

        Console.WriteLine($"Hash Gerado: {hashGerado}");

        if (hashGerado == hashRecebido)
        {
            Console.WriteLine("Os hashes são iguais!");

            string novoHash = GerarNovoHashComPasswordHasher(senha);
            Console.WriteLine($"Novo Hash Gerado com IPasswordHasher: {novoHash}");
        }
        else
        {
            Console.WriteLine("Os hashes são diferentes.");
        }
    }

    public static string CalculateMD5Hash(string input)
    {
        using (MD5 md5 = MD5.Create())
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("x2"));
            }

            return sb.ToString();
        }
    }

    static string GerarNovoHashComPasswordHasher(string senha)
    {
        var passwordHasher = new PasswordHasher<object>();
        string hash = passwordHasher.HashPassword(null, senha);
        return hash;
    }
}
