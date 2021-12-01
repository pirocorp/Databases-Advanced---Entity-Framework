namespace SoftJail.Data.Models
{
    using System.Collections.Generic;

    public class Department
    {
        public Department()
        {
            this.Cells = new List<Cell>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Cell> Cells { get; set; }   
    }
}
