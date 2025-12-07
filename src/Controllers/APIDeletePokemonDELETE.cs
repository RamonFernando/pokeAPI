using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using static pokeAPI.Program;
using static pokeAPI.Views;
using static pokeAPI.SearchById;
using static pokeAPI.APIControllers;
using static pokeAPI.HttpClientService;

namespace pokeAPI
{
    public class APIDeletePokemonDELETE
    {

        public static async Task RequestDeletePokemonDELETE()
        {
            Console.WriteLine("\n----- Eliminar Pokemon -----");
            Console.Write("Introduce el ID del Pokemon a eliminar: ");

            int id = ValidateInput(Console.ReadLine());
            if (id <= 0)
            {
                Console.WriteLine("ID no válido.");
                PrintWaitForPressKey();
                return;
            }

            // 1. Buscamos el Pokémon
            Console.WriteLine($"\nBuscando Pokemon con ID {id} ...");

            var pokemon = await SearchByIdAsync(id);
            if (pokemon == null)
            {
                Console.WriteLine($"No se encontró ningún Pokemon con el ID {id}.");
                PrintWaitForPressKey();
                return;
            }

            // 2. Mostrar Pokémon encontrado
            PrintPokemon(pokemon);

            // 3. Confirmación
            Console.Write("\n¿Seguro que deseas eliminar este Pokemon? (s/n): ");
            string confirm = Console.ReadLine()!.Trim().ToLower();

            if (confirm != "s")
            {
                Console.WriteLine("Operacion cancelada.");
                PrintWaitForPressKey();
                return;
            }

            // 4. Ejecutamos DELETE
            string result = await ExecuteDelete(id);

            if (string.IsNullOrWhiteSpace(result))
            {
                Console.WriteLine($"No se pudo eliminar el Pokémon con ID {id}.");
                PrintWaitForPressKey();
                return;
            }

            Console.WriteLine($"Pokemon {id} eliminado correctamente.");
            PrintWaitForPressKey();
        }

        private static async Task<string> ExecuteDelete(int id)
        {
            var url = $"{BASE_URL}/{id}";

            var response = await client.DeleteAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Error al eliminar el Pokemon {id}: " +
                                $"\nCodigo: {response.StatusCode}" +
                                $"\nInfo: {response.ReasonPhrase}");
                return "";
            }

            // Devolvemos contenido JSON devuelto por el servidor
            return await response.Content.ReadAsStringAsync();
        }
    }
}
