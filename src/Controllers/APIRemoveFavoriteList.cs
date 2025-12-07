
using static pokeAPI.Program;
using static pokeAPI.APIControllers;
using static pokeAPI.Views;
using static pokeAPI.SavePokemonList;
using static pokeAPI.APILoadJson;
using static pokeAPI.APISaveJson;

namespace pokeAPI
{
    public class APIRemoveFavoriteList
    {

        public static void RequestRemoveFavoriteList()
        {
            if (pokemonsFavoriteList.Count == 0)
            {
                Console.WriteLine("No hay Pokemons en la lista de favoritos.");
                PrintWaitForPressKey();
                return;
            }
            Console.Write("Introduce el Indice del pokemon a borrar: ");
            int index = ValidateInput(Console.ReadLine()) -1;
            if (index < 0 || index >= pokemonsFavoriteList.Count())
            {
                Console.WriteLine("Indice no valido o inexistente.");
                PrintWaitForPressKey();
                return;
            }
            Console.Write($"\nEstas seguro que deseas borrar el pokemon con Indice: {index+1} '{pokemonsFavoriteList[index].Name}' de la lista de favoritos? (S/N): ");
            string? opc = Console.ReadLine()?.Trim().ToLower();
            
            if(string.IsNullOrWhiteSpace(opc) || opc != "s")
            {
                Console.WriteLine($"Pokemon {index+1}: {pokemonsFavoriteList[index].Name} no se ha borrado de la lista de favoritos.");
                PrintWaitForPressKey();
                return;
            }
            RemoveFavoriteList(index);
            APISaveFavoriteList();
            PrintWaitForPressKey();
        }

        private static void RemoveFavoriteList(int index)
        {
            if (pokemonsFavoriteList.Count == 0)
            {
                Console.WriteLine("No hay Pokemons en la lista de favoritos.");
                PrintWaitForPressKey();
                return;
            }
            if (index < 0 || index >= pokemonsFavoriteList.Count())
            {
                Console.WriteLine($"No se encontro ningun Pokemon con el Indice: {index}.");
                PrintWaitForPressKey();
                return;
            }
            var pokemon = pokemonsFavoriteList[index];
            pokemonsFavoriteList.RemoveAt(index);
            Console.WriteLine($"Pokemon con Indice {index+1} '{pokemon.Name}' borrado de la lista de favoritos.");
            return;
        }
    }
}