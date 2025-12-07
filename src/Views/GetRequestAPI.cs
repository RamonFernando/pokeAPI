
using System.Text.Json;
using static pokeAPI.Program;
using static pokeAPI.APIControllers;
using static pokeAPI.HttpClientService;
using static pokeAPI.Views;


namespace pokeAPI
{
    internal class GetRequestAPI
    {

        public static async Task GetPokemons()
        {
            HttpResponseMessage response = await client.GetAsync(BASE_URL);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
            }

            string json = response.Content.ReadAsStringAsync().Result;
            var pokeAPI = JsonDocument.Parse(json).RootElement;

            foreach (var pokemon in pokeAPI.EnumerateArray())
            {
                Console.WriteLine(
                    $"\nID: {pokemon.GetProperty("id").GetInt32()}" +
                    $"\nNombre: {pokemon.GetProperty("name").GetString()}" +
                    $"\nTipo: {string.Join(", ", pokemon.GetProperty("type").EnumerateArray().Select(t => t.GetString()))}" +
                    $"\nAltura: {pokemon.GetProperty("height").GetDouble()} mt/s" +
                    $"\nPeso: {pokemon.GetProperty("mass").GetDouble()} kg/s" +
                    $"\nMovimientos: {string.Join(", ", pokemon.GetProperty("moves").EnumerateArray().Select(m => m.GetString()))}"
                );
            }
            PrintWaitForPressKey();
        }
    }
}