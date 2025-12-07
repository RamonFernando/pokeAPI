
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using static pokeAPI.APIControllers;
using static pokeAPI.GetRequestAPI;
using static pokeAPI.SearchByName;
using static pokeAPI.Helpers;
using static pokeAPI.Views;
using static pokeAPI.SearchById;
using static pokeAPI.SearchByHeight;
using static pokeAPI.SearchByType;
using static pokeAPI.SearchByMass;
using static pokeAPI.APILoadJson;
using static pokeAPI.APIRemoveFavoriteList;
using static pokeAPI.SearchByMoves;
using static pokeAPI.APIDeletePokemonDELETE;

namespace pokeAPI
{
    internal class App
    {
        internal async Task Run()
        {
            APILoadFavoriteList();
            while (true)
            {
                try
                {
                    PrintMainMenu();
                    int input = ValidateInput(Console.ReadLine());

                    if (input == -1 || input < 0 || input > 11)
                    {
                        Console.WriteLine("Entrada no valida");
                        PrintWaitForPressKey();
                        continue;
                    }

                    switch (input)
                    {
                        case 1:
                            Console.WriteLine("Cargando API...");
                            await GetPokemons();
                            break;
                        case 2:
                            // Buscar por Id
                            await RequestSearchById();
                            break;
                        case 3:
                            // Buscar por nombre
                            await RequestSearchByName();
                            break;
                        case 4:
                            // Filtrar por tipo
                            await RequestSearchByType();
                            break;
                        case 5:
                            // Filtrar por altura
                            await RequestSearchByHeight();
                            break;
                        case 6:
                            // Filtrar por peso
                            await RequestSearchByWeight();
                            break;
                        case 7:
                            // Filtrar por movimientos
                            await RequestSearchByMoves();
                            break;
                        case 8:
                            // Borrar pokemon de Lista favoritos
                            RequestRemoveFavoriteList();
                            break;
                        case 9:
                            // Mostrar Lista API
                            PrintFavoriteList();
                            break;
                        case 10:
                            // Actualizar Pokemon (PUT)
                            break;
                        case 11:
                            // Eliminar Pokemon (DELETE)
                            await RequestDeletePokemonDELETE();
                            break;
                        case 0:
                            Console.WriteLine("\nSaliendo del programa...");
                            Environment.Exit(0);
                            // PrintWaitForPressKey();
                            // return;
                            break;
                        default:
                            Console.WriteLine("\nOpcion no valida");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    HandlerException(ex);
                }
            }
            
        } // Main
    }
}