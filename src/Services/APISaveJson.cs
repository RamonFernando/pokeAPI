
using System.Text.Json;

using static pokeAPI.Views;
using static pokeAPI.Helpers;
using static pokeAPI.SavePokemonList;

namespace pokeAPI
{
    public class APISaveJson
    {
        public static void APISaveFavoriteList()
        {
            try
            {
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "favoriteListPoke.json");

                var json = JsonSerializer.Serialize(pokemonsFavoriteList, new JsonSerializerOptions { WriteIndented = true });

                File.WriteAllText(filePath, json);
                // Console.WriteLine("Operacion realizada con exito");
                // Console.WriteLine(json);

            }
            catch (Exception ex)
            {
                HandlerException(ex);
                PrintWaitForPressKey();
            }
        } // APISaveFavoriteList
    }
}