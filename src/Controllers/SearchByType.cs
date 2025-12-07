using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

using static pokeAPI.Program;
using static pokeAPI.APIControllers;
using static pokeAPI.Views;
using static pokeAPI.HttpClientService;


namespace pokeAPI
{
    internal class SearchByType
    {
        public static async Task RequestSearchByType()
        {
            Console.Write("Introduce el tipo(Planta, Agua, etc) del Pokemon a buscar: ");
            string? type = Console.ReadLine();

            Console.WriteLine($"\nBuscando Pokemon {type} ...");
            var result = await SearchByTypeAsync(type);
            
            if (result == null)
            {
                Console.WriteLine($"Error al obtener o parsear los datos de la API.");
                PrintWaitForPressKey();
                return;
            }
            if (result.Count == 0)
            {
                Console.WriteLine($"No se encontraron Pokemons con el nombre de '{type}'.");
                PrintWaitForPressKey();
                return;
            }
            Console.WriteLine($"\nLista de pokemones para el tipo '{type}':");
            PrintPokemonsList(result);
            PrintWaitForPressKey();
        } // End of RequestSearchByName
        
        public static async Task<List<Pokemon>?> SearchByTypeAsync(string? type)
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
                    pkm.Type != null &&
                    pkm.Type[0].Contains(type ?? "", StringComparison.OrdinalIgnoreCase) // busqueda parcial y case insensitive
                ).ToList();
            
            return result;
        } // End of SearchByTypeAsync
    }
}