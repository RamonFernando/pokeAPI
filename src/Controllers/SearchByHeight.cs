
using System.Text.Json;
using System.Text.Json.Serialization;

using static pokeAPI.Program;
using static pokeAPI.APIControllers;
using static pokeAPI.Helpers;

using static pokeAPI.Views;
using static pokeAPI.HttpClientService;

namespace pokeAPI
{
    internal class SearchByHeight
    {
        public static async Task RequestSearchByHeight()
        {
            // Solicitamos y validamos la entrada del usuario
            Console.Write($"Introduzca la altura del pokemon a buscar: ");
            string? input = Console.ReadLine();
            double height = ValidateInputDouble(input);
            if(height == -1)
            {
                Console.WriteLine("Altura no valida o inexistente.");
                PrintWaitForPressKey();
                return;
            }

            // Realizamos la busqueda a la API y validamos resultados
            Console.WriteLine($"\nBuscando Pokemon con altura {height} ...");
            var pokemonList = await SearchByHeightAsync(height);
            if (pokemonList is null) {
                Console.WriteLine($"Error al obtener o parsear los datos de la API.");
                PrintWaitForPressKey();
                return;
            }
            if(pokemonList.Count == 0) {
                Console.WriteLine($"No se encontro ningun Pokemon con altura {height} M.");
                PrintWaitForPressKey();
                return;
            }
            
            // Mostramos los resultados
            Console.WriteLine("\n----- Pokemons encontrados -----");
            PrintPokemonsList(pokemonList);
            PrintWaitForPressKey();
        }

        public static async Task<List<Pokemon>?> SearchByHeightAsync(double height)
        {
            var url = $"{BASE_URL}";
            HttpResponseMessage response = await client.GetAsync(url);
            if(!response.IsSuccessStatusCode) {
                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest) return null;
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound) return null;
                return null;
            }
            
            var json = await response.Content.ReadAsStringAsync();
            var pokemonsList = JsonSerializer.Deserialize<List<Pokemon>>(json);
            if(pokemonsList is null || pokemonsList.Count == 0) return null;
            
            var filteredPokemons = pokemonsList.Where(pkm => pkm.Height == height).ToList();
            return (filteredPokemons.Count > 0) ? filteredPokemons : null;
        }
    }
}