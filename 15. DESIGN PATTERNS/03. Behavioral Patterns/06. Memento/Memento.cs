namespace _06._Memento
{
    /// <summary>
    /// The 'Memento' class
    /// </summary>
    public class Memento
    {
        public Memento(string name, string phone, double budget)
        {
            this.Name = name;
            this.Phone = phone;
            this.Budget = budget;
        }

        public string Name { get; }

        public string Phone { get; }

        public double Budget { get; }
    }
}
