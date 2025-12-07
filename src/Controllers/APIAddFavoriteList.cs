using static pokeAPI.Program;
using static pokeAPI.Views;
using static pokeAPI.SavePokemonList;
using static pokeAPI.APISaveJson;

namespace pokeAPI
{
    internal class APIAddFavoriteList
    {
        public static void AddPokemonToFavoriteList(Pokemon pokemon)
        {
            if (pokemonsFavoriteList.Any(pkm => pkm.Id == pokemon.Id))
            {
                Console.WriteLine($"El pokemon '{pokemon.Name}' ya existe en la lista de favoritos.");
                PrintWaitForPressKey();
                return;
            }
            Console.WriteLine($"Agregando '{pokemon.Name}' a la lista de favoritos...");
            pokemonsFavoriteList.Add(pokemon);
            return;
        }
    }
}