
using System.Text.Json;
using static pokeAPI.APIControllers;

namespace pokeAPI
{
    internal class Helpers
    {
        // Metodos aux para garantizar validacion (si esta vacio no se actualiza)
        public static string? UpdateIfNotEmpty(string? input, string? value) =>
            string.IsNullOrWhiteSpace(input) ? value : input.Trim();

        public static double UpdateIfNotEmptyDouble(string? input, double value) {
            double InputValidated = ValidateInputDouble(input);
            return InputValidated == -1 ? value : InputValidated;
        }

        public static List<string> UpdateIfNotEmptyList(string? input, List<string> value) =>
            string.IsNullOrWhiteSpace(input) ? value : input.Split(',').Select(vl => vl.Trim()).ToList();
        
        // Manejo de excepciones
        public static void HandlerException(Exception ex)
        {
            if (ex is HttpRequestException HttpEx)
            {
                Console.WriteLine($"\nError de Peticion HTTP: {HttpEx.Message}\n Info: {HttpEx.HResult}");
                return;
            }
            if (ex is JsonException JsonEx)
            {
                Console.WriteLine($"\nError de Lectura JSON: {JsonEx.Message}\n Info: {JsonEx.HResult}");
                return;
            }
            if (ex is TaskCanceledException TaskEx)
            {
                Console.WriteLine($"\nError de Tarea Cancelada: {TaskEx.Message}\n Info: {TaskEx.HResult}");
                return;
            }
            if (ex is OperationCanceledException OpEx)
            {
                Console.WriteLine($"\nError de Operacion Cancelada: {OpEx.Message}\n Info: {OpEx.HResult}");
                return;
            }
            if (ex is TimeoutException TimeEx)
            {
                Console.WriteLine($"\nError de Tiempo Excedido: {TimeEx.Message}\n Info: {TimeEx.HResult}");
                return;
            }
            if (ex is ArgumentException ArgEx)
            {
                Console.WriteLine($"\nError de Argumento: {ArgEx.Message}\n Info: {ArgEx.HResult}");
                return;
            }
            if (ex is NullReferenceException NullEx)
            {
                Console.WriteLine($"\nError de Referencia Nula: {NullEx.Message}\n Info: {NullEx.HResult}");
                return;
            }
            Console.WriteLine($"\nError desconocido: {ex.Message}\n Info: {ex.HResult}");
        } // HandlerException
    }
}