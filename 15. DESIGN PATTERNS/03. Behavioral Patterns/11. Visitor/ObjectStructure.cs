namespace _11._Visitor
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The 'ObjectStructure' class
    /// </summary>
    public class Employees
    {
        private readonly List<Employee> _employees;

        public Employees()
        {
            this._employees = new List<Employee>();
        }

        public void Attach(Employee employee)
        {
            this._employees.Add(employee);
        }

        public void Detach(Employee employee)
        {
            this._employees.Remove(employee);
        }

        public void Accept(IVisitor visitor)
        {
            foreach (var employee in this._employees)
            {
                employee.Accept(visitor);
            }

            Console.WriteLine();
        }
    }
}
