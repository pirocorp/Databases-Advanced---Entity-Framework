namespace _11._Visitor
{
    /// <summary>
    /// The 'ConcreteElement' class
    /// </summary>
    public class Employee : Element
    {
        private string _name;
        private double _income;
        private int _vacationDays;

        public Employee(string name, double income, int vacationDays)
        {
            this._name = name;
            this._income = income;
            this._vacationDays = vacationDays;
        }

        public string Name
        {
            get => this._name;
            set => this._name = value;
        }

        public double Income
        {
            get => this._income;
            set => this._income = value;
        }

        public int VacationDays
        {
            get => this._vacationDays;
            set => this._vacationDays = value;
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
