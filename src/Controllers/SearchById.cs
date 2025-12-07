using System.Text.Json;
using System.Net;

using static pokeAPI.Pokemon;
using static pokeAPI.APIControllers;
using static pokeAPI.Program;
using static pokeAPI.Views;
using static pokeAPI.HttpClientService;




namespace pokeAPI
{
    internal class SearchById
    {
        public static async Task RequestSearchById()
        {
            Console.Write("Introduce el ID del Pokemon a buscar: ");
            string? input = Console.ReadLine();
            int id = ValidateInput(input);
            if (id == -1 || id <= 0)
            {
                Console.WriteLine("Id no valido o inexistente.");
                PrintWaitForPressKey();
                return;
            }
            Console.WriteLine($"\nBuscando Pokemon con ID {id} ...");
            var pokemon = await SearchByIdAsync(id);
            if (pokemon == null)
            {
                Console.WriteLine($"No se encontro ningun Pokemon con el ID: {id}.");
                PrintWaitForPressKey();
                return;
            }

            PrintPokemon(pokemon);
            PrintWaitForPressKey();
        } // End of RequestSearchById

        public static async Task<Pokemon?> SearchByIdAsync(int id)
        {
            var url = $"{BASE_URL}/{id}";

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
            var pokemon = JsonSerializer.Deserialize<Pokemon>(json);
            if (pokemon == null)return null;
            
            // 4. Busqueda del Pokemon por ID
            return pokemon.Id == id ? pokemon : null;
            
        } // End of SearchByIdAsync
    }
}