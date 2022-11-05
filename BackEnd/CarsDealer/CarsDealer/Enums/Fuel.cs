namespace CarsDealer.Enums
{
    public enum Fuel : byte
    {
        Gasoline = 0,
        Diesel = 1,
        Electric = 2,
        GasGasoline = 3,
        Hybrid = 4,
    }

    public static class ConvertFuelToString
    {
        public static string ConvertFuel(this Fuel fuel)
        {
            switch (fuel)
            {
                case Fuel.GasGasoline: return "Gas/Gasoline";
                case Fuel.Diesel: return "Diesel";
                case Fuel.Electric: return "Electric";
                case Fuel.Gasoline: return "Gasoline";
                case Fuel.Hybrid: return "Hybrid";
                default: return null;
            }
        }
    }
}
