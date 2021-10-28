namespace CarDealer.Models
{
    public class PartCar
    {
        public int PartId { get; set; }
        public Part Part { get; set; }

        public int CarId { get; set; }
        public Car Car { get; set; }

        public override string ToString()
        {
            return $"C:{this.CarId} P:{this.PartId}";
        }
    }
}
