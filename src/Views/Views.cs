using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static pokeAPI.Program;
using static pokeAPI.APIControllers;
using static pokeAPI.Helpers;
using static pokeAPI.SavePokemonList;
using static pokeAPI.APIAddFavoriteList;

namespace pokeAPI
{
    internal class Views
    {
        public static void PrintMainMenu()
        {
            Console.Clear();
            Console.WriteLine("**=======================================**");
            Console.WriteLine($"  Bienvenido a la API de {NameApi}");
            Console.WriteLine("===========================================");
            Console.WriteLine("         MENU PRINCIPAL");
            Console.WriteLine("===========================================");
            Console.WriteLine("1. Mostrar API");
            Console.WriteLine("2. Buscar (Id)");
            Console.WriteLine("3. Buscar (Nombre y Add a Favoritos)");
            Console.WriteLine("4. Mostrar Tipo");
            Console.WriteLine("5. Mostrar Altura");
            Console.WriteLine("6. Mostrar Peso");
            Console.WriteLine("7. Mostrar Movimientos");
            Console.WriteLine("8. Borrar Pokemon de Lista Favoritos");
            Console.WriteLine("9. Mostrar Lista API");
            Console.WriteLine("10. Actualizar Pokemon (PUT)");
            Console.WriteLine("11. Delete Pokemon (DELETE)");
            Console.WriteLine("0. Salir");
            Console.WriteLine("**=======================================**");
            Console.Write("Introduce una opcion: ");
        }

        public static void PrintWaitForPressKey()
        {
            Console.WriteLine("\nPresiona cualquier tecla para continuar...");
            Console.ReadKey(); // true para no mostrar la tecla presionada
        }
        public static void PrintMenuOptions(){
            Console.Write("\nDesea agregar a la lista de favoritos? (S/N)");
            string? opc = Console.ReadLine();
            opc = opc?.Trim().ToLower();
            
            if(string.IsNullOrWhiteSpace(opc) || opc != "s")
            {

                Console.WriteLine("Pokemon no agregado a la lista de favoritos.");
                PrintWaitForPressKey();
                return;
            }
            // Console.WriteLine("Pokemon agregado a la lista de favoritos");
            // PrintWaitForPressKey();
            // return;
        }

        // Mostrar lista de Pokemons
        public static void PrintPokemonsList(List<Pokemon> pokemons)
        {
            int count = pokemons.Count;
            Console.WriteLine("--------------------------------");
            Console.WriteLine($"Total Pokemons encontrados: {count}");
            
            foreach (var item in pokemons)
            {
                Console.WriteLine(
                    $"\nId: {item.Id}" +
                    $"\nNombre: {item.Name}" +
                    $"\nTipo: {string.Join(", ", item.Type)}" +
                    $"\nAltura: {item.Height} Metros." +
                    $"\nPeso: {item.Mass} Kg." +
                    $"\nMovimientos: {string.Join("," , item.Moves)}"
                );
            }
        }

        // Mostrar 1 solo pokemon
        public static void PrintPokemon(Pokemon pokemon)
        {
            Console.WriteLine(
                $"ID: {pokemon.Id}" +
                $"\nNombre: {pokemon.Name}" +
                $"\nTipo: {string.Join(", ", pokemon.Type)}" +
                $"\nAltura: {pokemon.Height} mt/s" +
                $"\nPeso: {pokemon.Mass} kg/s" +
                $"\nMovimientos: {string.Join(", ", pokemon.Moves)}"
            );
        }

        // Mostrar lista de favoritos
        public static void PrintFavoriteList()
        {
            if (pokemonsFavoriteList.Count == 0)
            {
                Console.WriteLine("No hay Pokemons en la lista de favoritos.");
                PrintWaitForPressKey();
                return;
            }
            Console.WriteLine($"\nLista de Pokemons Favoritos: {pokemonsFavoriteList.Count}");
            Console.WriteLine("=========================================");
            
            for (int i = 0; i < pokemonsFavoriteList.Count; i++)
            {
                var pokemon = pokemonsFavoriteList[i];
                Console.WriteLine(
                $"\n{i+1}º Pokemon: " +
                $"\n--------------------------" +
                $"\nId: {pokemon.Id}" +
                $"\nNombre: {pokemon.Name}" +
                $"\nTipo: {string.Join(", ", pokemon.Type)}" +
                $"\nAltura: {pokemon.Height} Mt" +
                $"\nPeso: {pokemon.Mass} Kg" +
                $"\nMovimientos: {string.Join(", ", pokemon.Moves)}"
                );
            }
            PrintWaitForPressKey();
        }
        
    }
}