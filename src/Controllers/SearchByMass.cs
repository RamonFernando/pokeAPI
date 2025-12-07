using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

using static pokeAPI.Program;
using static pokeAPI.APIControllers;
using static pokeAPI.Pokemon;
using static pokeAPI.Views;
using static pokeAPI.HttpClientService;

namespace pokeAPI
{
    internal class SearchByMass
    {
        public static async Task RequestSearchByWeight()
        {
            // 1. Pedimos el peso al usuario y validamos la entrada
            Console.Write("\nIntroduce el peso del pokemon a buscar: ");
            string? input = Console.ReadLine();
            double mass = ValidateInputDouble(input);
            if (mass == -1)
            {
                Console.WriteLine("\nEntrada no valida o peso inexistente.");
                PrintWaitForPressKey();
                return;
            }

            // 2. Bucamos el peso del pokemon y validamos
            var weight = await SearchByMassAsync(mass);
            Console.WriteLine($"\nBuscando Pokemon con peso {mass}Kg ...");
            if (weight == null)
            {
                Console.WriteLine($"Error al obtener o parsear los datos de la API.");
                PrintWaitForPressKey();
                return;
            }
            if (weight.Count == 0)
            {
                Console.WriteLine($"No se encontro ningun Pokemon con peso {mass} kg.");
                PrintWaitForPressKey();
                return;
            }

            // 3. Mostramos los resultados
            Console.WriteLine("----- Pokemons encontrados -----");
            PrintPokemonsList(weight);
            PrintWaitForPressKey();
        }
        public static async Task<List<Pokemon>?> SearchByMassAsync(double mass)
        {
            var url = $"{BASE_URL}";

            // 1. Peticion HTTP y validacion de respuesta
            HttpResponseMessage response = await client.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                if(response.StatusCode == System.Net.HttpStatusCode.BadRequest) return null;
                if(response.StatusCode == System.Net.HttpStatusCode.NotFound) return null;
                Console.WriteLine($"Error : \nCodigo: {response.StatusCode} \nInfo: {response.ReasonPhrase}");
                PrintWaitForPressKey();
                return null;
            }

            // 2. Lectura del JSON
            var json = await response.Content.ReadAsStringAsync();
            var pokemonList = JsonSerializer.Deserialize<List<Pokemon>>(json);
            if(pokemonList is null) return null;
            
            // 3. Busqueda del Pokemon por peso
            var filteredPokemons = pokemonList.Where(pkm => pkm != null && pkm.Mass == mass).ToList();
            return (filteredPokemons.Count > 0) ? filteredPokemons : null;
        }
    }
}