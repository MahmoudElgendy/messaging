// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

 HttpClient client = new HttpClient();
string url = "http://localhost:7001/WeatherForecast";
HttpResponseMessage response = await client.GetAsync(url);
response.EnsureSuccessStatusCode(); // Throws if not 2xx

string responseBody = await response.Content.ReadAsStringAsync();
Console.WriteLine(responseBody);
Console.ReadLine();