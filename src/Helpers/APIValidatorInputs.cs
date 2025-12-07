using System.Globalization;

namespace pokeAPI
{
    internal class APIControllers
    {
        public static int ValidateInput(string? input)
        {
            if (string.IsNullOrWhiteSpace(input) || !int.TryParse(input, out int num))
            {
                // Console.WriteLine("Opcion no valida");
                // PrintWaitForPressKey();
                return -1;
            }
            return num;
        }
        public static double ValidateInputDouble(string? input)
        {
            if (string.IsNullOrWhiteSpace(input) ||
            !double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out double num))
                return -1;
            
            return num;
        }
        

    }
}