using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class Program
{
    public static async Task Main(string[] args)
    {
        string userName = "Alice";
        string userPassword = "alicepass";

        await AuthenticateUser(userName, userPassword);
    }

    public static async Task AuthenticateUser(string name, string password)
    {
        var url = "http://localhost:5000/api/authenticate";
        var httpClient = new HttpClient();

        var payload = new
        {
            name = name,
            password = password
        };

        var jsonPayload = JsonSerializer.Serialize(payload);
        var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

        try
        {
            var response = await httpClient.PostAsync(url, content);
            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Response content: {responseContent}");

            Console.WriteLine(response.StatusCode);
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"Произошла ошибка при отправке запроса: {e.Message}");
        }
    }
}

public class ResponseData
{
    public bool Success { get; set; }
    public string Message { get; set; }
}
