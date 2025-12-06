using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using static pokeAPI.Program;
using static pokeAPI.APIControllers;
using static pokeAPI.Views;


namespace pokeAPI
{
    internal class SearchByMoves
    {
        public static async Task RequestSearchByMoves()
        {
            Console.WriteLine("Introduce el movimiento del pokemon a buscar: ");
            string? moves = Console.ReadLine();
            
            Console.WriteLine($"Buscando movimiento: {moves}");
            var result = await SearchByMovesAsync(moves);

            if (result is null)
            {
                Console.WriteLine("Error al obtener o parsear los datos de la API.");
                PrintWaitForPressKey();
                return;
            }
            if(result.Count() == 0)
            {
                Console.WriteLine($"No se encontro ningun Pokemon con el movimiento: {moves}.");
                PrintWaitForPressKey();
                return;
            }

            Console.WriteLine($"Lista de pokemons con el movimiento {moves}: ");
            PrintPokemonsList(result);
            PrintWaitForPressKey();
        }
        public static async Task<List<Pokemon>?> SearchByMovesAsync(string? moves)
        {
            var url = $"{BASE_URL}";

            // 1. Peticion HTTP y validacion de respuesta
            HttpResponseMessage response = await client.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest) return null;
                if(response.StatusCode == System.Net.HttpStatusCode.NotFound) return null;
                Console.WriteLine($"Error : \nCodigo: {response.StatusCode} \nInfo: {response.ReasonPhrase}");
                PrintWaitForPressKey();
                return null;
            }

            // 2. Lectura del JSON y validacion
            var json = await response.Content.ReadAsStringAsync();
            var pokemonList = JsonSerializer.Deserialize<List<Pokemon>>(json);
            if(json is null) return null;
            
            // 3. Busqueda del Pokemon por movimientos
            var filteredPokemons = pokemonList?
                .Where(pkm => pkm.Moves != null && pkm.Moves[0].Contains(moves ?? "", StringComparison.OrdinalIgnoreCase)
                ).ToList();
            
            // 4. Mostramos el resultado
            return (filteredPokemons?.Count > 0) ? filteredPokemons : null;
        }
    }
}