namespace Cars.Data.Models
{
    using System.Collections.Generic;

    public class Make
    {
        public Make()
        {
            this.Cars = new HashSet<Car>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Car> Cars { get; set; }
    }   
}