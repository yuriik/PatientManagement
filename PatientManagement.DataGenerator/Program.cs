// See https://aka.ms/new-console-template for more information

using PatientManagement.API.Models;
using System.Net.Http.Json;
using System.Reflection;
using System.Xml.Linq;

class Program
{
    static async Task Main(string[] args)
    {
        using var client = new HttpClient();
        client.BaseAddress = new Uri("https://localhost:7001/");

        for (int i = 0; i < 100; i++)
        {
            var patient = GenerateRandomPatient();
            var response = await client.PostAsJsonAsync("api/patients", patient);
            Console.WriteLine($"Patient {i + 1}: {response.StatusCode}");
        }
    }

    static Patient GenerateRandomPatient()
    {
        var random = new Random();
        var surnames = new[] { "Иванов", "Петров", "Сидоров", "Смирнов" };
        var names = new[] { "Иван", "Петр", "Александр", "Михаил" };
        var patronymics = new[] { "Иванович", "Петрович", "Александрович", "Михайлович" };

        return new Patient
        {
            Name = new Name
            {
                Id = Guid.NewGuid(),
                Use = "official",
                Family = surnames[random.Next(surnames.Length)],
                Given = new List<string>
                {
                    names[random.Next(names.Length)],
                    patronymics[random.Next(patronymics.Length)]
                }
            },
            Gender = (Gender)random.Next(4),
            BirthDate = DateTime.Now.AddDays(-random.Next(365)),
            Active = random.Next(2) == 1
        };
    }
}
