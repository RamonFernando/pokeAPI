using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Text.Json;

using static pokeAPI.Program;
using static pokeAPI.APIControllers;
using static pokeAPI.APISaveJson;
using static pokeAPI.Views;
using static pokeAPI.APIAddFavoriteList;
using static pokeAPI.HttpClientService;


namespace pokeAPI
{
    internal class SearchByName
    {
        public static async Task RequestSearchByName()
        {
            Console.Write("Introduce el nombre del Pokemon a buscar: ");
            string? name = Console.ReadLine();

            Console.WriteLine($"\nBuscando Pokemon {name} ...");
            var result = await SearchByNameAsync(name);
            Console.WriteLine($"\nLista de resultados para '{name}':");
            if (result == null)
            {
                Console.WriteLine($"Error al obtener o parsear los datos de la API.");
                PrintWaitForPressKey();
                return;
            }
            if (result.Count == 0)
            {
                Console.WriteLine($"No se encontraron Pokemons con el nombre de '{name}'.");
                PrintWaitForPressKey();
                return;
            }

            PrintPokemonsList(result);
            Console.Write($"\n¿Deseas agregar a '{result[0].Name}' a la lista de favoritos? (S/N): ");
            string? opc = Console.ReadLine();
            opc = opc?.Trim().ToLower();
            
            if(string.IsNullOrWhiteSpace(opc) || opc != "s")
            {
                Console.WriteLine($"Pokemon '{result[0].Name}' no agregado a la lista de favoritos.");
                PrintWaitForPressKey();
                return;
            }
            
            AddPokemonToFavoriteList(result[0]);
            APISaveFavoriteList();
            PrintWaitForPressKey();
            return;
        } // End of RequestSearchByName

        public static async Task<List<Pokemon>?> SearchByNameAsync(string? name)
        {
            var url = $"{BASE_URL}";

            // 1. Peticion HTTP y validacion de respuesta
            HttpResponseMessage response = await client.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest) return null;
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound) return null;
                Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                PrintWaitForPressKey();
                return null;
            }

            // 2. Lectura del JSON
            var json = await response.Content.ReadAsStringAsync();

            // 3. Parseo del JSON y validacion
            var pokemonsList = JsonSerializer.Deserialize<List<Pokemon>>(json);
            if (pokemonsList == null) return null;

            // 4. Busqueda del Pokemon por nombre
            var result = pokemonsList
                .Where( pkm =>
                    pkm.Name != null &&
                    pkm.Name.Contains(name ?? "", StringComparison.OrdinalIgnoreCase) // busqueda parcial y case insensitive
                ).ToList();
            
            return result;
        } // End of SearchByNameAsync
    }
}
