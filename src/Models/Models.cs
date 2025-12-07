using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace pokeAPI
{
    public class Pokemon
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("type")]
        public List<string> Type { get; set; } = new ();

        [JsonPropertyName("height")]
        public double Height { get; set; }
        [JsonPropertyName("mass")]
        public double Mass { get; set; }

        [JsonPropertyName("moves")]
        public List<string> Moves { get; set; } = new ();
    }
    public class SavePokemonList
    {
        public static List<Pokemon> pokemonsFavoriteList = new List<Pokemon> ();
    }
    
}
