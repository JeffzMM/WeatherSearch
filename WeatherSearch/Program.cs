using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

class Program
{
    private static readonly string ChaveApi = "####################";
    private static readonly string UrlApi = "http://api.openweathermap.org/data/2.5/weather";

    static async Task Main(string[] args)
    {
        Console.WriteLine("Bem-vindo!");
        Console.Write("Digite o nome da cidade brasileira: ");
        string cidade = Console.ReadLine();

        if (string.IsNullOrEmpty(cidade))
        {
            Console.WriteLine("O nome da cidade não pode estar vazio.");
            return;
        }

        try
        {
            var dadosClimaticos = await ObterDadosClimaticosAsync(cidade);
            ExibirDadosClimaticos(dadosClimaticos);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ocorreu um erro: {ex.Message}");
        }
    }

    private static async Task<JObject> ObterDadosClimaticosAsync(string cidade)
    {
        using var cliente = new HttpClient();
        var resposta = await cliente.GetStringAsync($"{UrlApi}?q={cidade}&appid={ChaveApi}&units=metric&lang=pt");
        return JObject.Parse(resposta);
    }

    private static void ExibirDadosClimaticos(JObject dadosClimaticos)
    {
        var principal = dadosClimaticos["main"];
        var clima = dadosClimaticos["weather"][0];
        var temperatura = principal["temp"];
        var descricao = clima["description"];
        var cidade = dadosClimaticos["name"];

        Console.WriteLine($"Cidade: {cidade}");
        Console.WriteLine($"Temperatura: {temperatura}°C");
        Console.WriteLine($"Descrição: {descricao}");
    }
}
