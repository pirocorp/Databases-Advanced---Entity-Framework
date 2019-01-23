// ReSharper disable InconsistentNaming
namespace Cars.Data.Models
{
    using System;

    [Flags]
    public enum FuelType
    {
        Petrol = 1,
        Diesel = 2,
        LPG = 4,
        CNG = 8,
        Electric = 16
    }
}