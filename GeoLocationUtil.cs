using Newtonsoft.Json.Linq;
using System.Text.Json;

public class GeoLocationUtil
{
    private static readonly HttpClient client = new HttpClient();
    private static string ApiKey;

    public static async Task Main(string[] args)
    {
        ApiKey = LoadApiKey("appsettings.json");

        if (args.Length == 0)
        {
            Console.WriteLine("Please provide location inputs.");
            return;
        }

        List<Task> locationTasks = new List<Task>();

        foreach (var location in args)
        {
            locationTasks.Add(ProcessLocation(location));
        }

        await Task.WhenAll(locationTasks);
    }

    static async Task ProcessLocation(string location)
    {
        string query;
        if (IsZipCode(location))
        {
            query = $"https://api.openweathermap.org/geo/1.0/zip?zip={location},US&appid={ApiKey}";
        }
        else
        {
            string[] cityState = location.Split(",");
            string city = cityState[0].Trim();
            string state = cityState.Length > 1 ? cityState[1].Trim() : "";
            query = $"https://api.openweathermap.org/geo/1.0/direct?q={Uri.EscapeDataString(city)},{Uri.EscapeDataString(state)},US&limit=1&appid={ApiKey}";
        }

        try
        {
            var response = await client.GetStringAsync(query);
            ParseGeoResponse(response, location);
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"Error fetching data for {location}: {e.Message}");
        }
    }

    static void ParseGeoResponse(string jsonResponse, string location)
    {
        // Check if the response is valid and parse accordingly
        if (IsZipCode(location))
        {
            // ZIP code response is expected to be an object
            JToken token = JToken.Parse(jsonResponse);
            if (token.Type == JTokenType.Object)
            {
                DisplayZipCodeData(location, token);
            }
            else
            {
                Console.WriteLine($"No data found for {location}");
            }
        }
        else
        {
            // City/state response is expected to be an array
            JArray jsonArray = JArray.Parse(jsonResponse);
            if (jsonArray.Count > 0)
            {
                DisplayLocationData(location, jsonArray[0]);
            }
            else
            {
                Console.WriteLine($"No data found for {location}");
            }
        }
    }

    private static void DisplayLocationData(string location, JToken data)
    {
        string name = data["name"]?.ToString();
        string latitude = data["lat"]?.ToString();
        string longitude = data["lon"]?.ToString();

        Console.WriteLine($"Location: {name}");
        Console.WriteLine($"Latitude: {latitude}, Longitude: {longitude}");
    }

    private static void DisplayZipCodeData(string location, JToken data)
    {
        string zip = data["zip"]?.ToString();
        string name = data["name"]?.ToString();
        string latitude = data["lat"]?.ToString();
        string longitude = data["lon"]?.ToString();

        Console.WriteLine($"ZIP: {zip}");
        Console.WriteLine($"Location: {name}");
        Console.WriteLine($"Latitude: {latitude}, Longitude: {longitude}");
    }

    private static bool IsZipCode(string input)
    {
        return int.TryParse(input, out _);
    }

    private static string LoadApiKey(string configFilePath)
    {
        try
        {
            string json = File.ReadAllText(configFilePath);
            var config = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(json);
            return config?["ApiSettings"]?["OpenWeatherMapApiKey"];
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading API key: {ex.Message}");
            return null;
        }
    }
}
