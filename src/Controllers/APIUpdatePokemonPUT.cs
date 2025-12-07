
using System.Net.Http;
using System.Text;
using System.Text.Json;

using static pokeAPI.Program;
using static pokeAPI.Views;
using static pokeAPI.APIControllers;
using static pokeAPI.SearchById;
using static pokeAPI.Helpers;
using static pokeAPI.HttpClientService;


namespace pokeAPI
{
    public class APIUpdatePokemonPUT
    {
        public static async Task RequestUpdatePokemonPUT()
        {
            // 1. Peticion al usuario y validacion de entrada
            Console.WriteLine("\n----- Actualizar Pokemon -----");
            Console.Write("Introduce el id del pokemon a actualizar: ");
            int id = ValidateInput(Console.ReadLine());
            if (id == -1 || id <= 0)
            {
                Console.WriteLine("Id no valido o inexistente.");
                PrintWaitForPressKey();
                return;
            }

            // 2. Buscamos el pokemon y validamos
            Console.WriteLine($"\nBuscando Pokemon con ID {id} ...");
            var pokemon = await SearchByIdAsync(id);
            if (pokemon == null)
            {
                Console.WriteLine($"No se encontro ningun Pokemon con el ID: {id}.");
                PrintWaitForPressKey();
                return;
            }

            PrintPokemon(pokemon);

            // 3. Solicitamos los nuevos datos
            Console.WriteLine("\nIntroduce los nuevos datos del Pokemon:");
            Console.Write("Nombre: ");
            pokemon.Name = UpdateIfNotEmpty(Console.ReadLine(), pokemon.Name);

            Console.Write("Altura: ");
            pokemon.Height = UpdateIfNotEmptyDouble(Console.ReadLine(), pokemon.Height);

            Console.Write("Peso: ");
            pokemon.Mass = UpdateIfNotEmptyDouble(Console.ReadLine(), pokemon.Mass);

            Console.Write("Movimientos: Dejar vacio para no modificar: ");

            pokemon.Moves = UpdateIfNotEmptyList(Console.ReadLine(), pokemon.Moves);
            
            Console.Write("Tipo: ");
            pokemon.Type = UpdateIfNotEmptyList(Console.ReadLine(), pokemon.Moves);

            // PUT
            // 4. Actualizamos el Pokemon
            string result = await UpdatePokemonPUT(id, pokemon);
            if (string.IsNullOrWhiteSpace(result))
            {
                Console.WriteLine($"No se ha podido actualizar el Pokemon con el ID: {id}.");
                PrintWaitForPressKey();
                return;
            }

            // 5. Convertimos el JSON devuelto en un objeto Pokemon y validamos
            var pokemonUpdate = JsonSerializer.Deserialize<Pokemon>(result);
            if (pokemonUpdate == null)
            {
                Console.WriteLine($"Error al obtener o parsear los datos de la API.");
                PrintWaitForPressKey();
                return;
            }

            // 6. Mostramos el Pokemon actualizado
            Console.WriteLine($"Pokemon {pokemonUpdate.Id}: '{pokemonUpdate.Name}' actualizado correctamente.");
            PrintPokemon(pokemonUpdate);
            PrintWaitForPressKey();
        }

        public static async Task<string> UpdatePokemonPUT(int id, object data)
        {
            // 1. Peticion HTTP del JSON
            var url = $"{BASE_URL}/{id}";
            string json = JsonSerializer.Serialize(data);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // 2. Peticion HTTP y validacion de respuesta
            var response = await client.PutAsync(url, content);
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Error al actualizar el Pokemon {id}: \nCodigo: {response.StatusCode}\nInfo: {response.ReasonPhrase}");
                PrintWaitForPressKey();
                return "";
            }

            // 3. Lectura del JSON y return del Pokemon
            Console.WriteLine($"Pokemon {id} actualizado correctamente.");
            PrintWaitForPressKey();
            var pokemon = await response.Content.ReadAsStringAsync();
            return pokemon; // Devuelve el pokemon actualizado (objeto JSON)
        }

        
    } // APIUpdatePokemonPUT
}
