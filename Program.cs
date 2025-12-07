
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using static pokeAPI.App;


namespace pokeAPI
{
    public class Program
    {
        public static string BASE_URL = "http://localhost:4000/pokemons";
        public static string NameApi => "Pokemons";
        static void Main(string[] args)
        {
            App app = new App();
            app.Run().GetAwaiter().GetResult();
            
        } // Main
        
    }
}