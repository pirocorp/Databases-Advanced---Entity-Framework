namespace P05_CompanyRoster
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class StartUp
    {
        public static void Main()
        {
            var employees = new List<Employee>();
            var n = int.Parse(Console.ReadLine());

            ReadEmployees(n, employees);

            var highestAverageSalaryDepartment = employees
                .GroupBy(e => e.Department)
                .ToDictionary(x => x.Key, x => x.ToList())
                .OrderByDescending(x => x.Value.Average(e => e.Salary))
                .First();

            Console.WriteLine($"Highest Average Salary: {highestAverageSalaryDepartment.Key}");

            var orderedEmployees = highestAverageSalaryDepartment
                .Value
                .OrderByDescending(x => x.Salary)
                .ToArray();

            foreach (var employee in orderedEmployees)
            {
                Console.WriteLine(employee);
            }

        }

        private static void ReadEmployees(int n, List<Employee> employees)
        {
            for (var i = 0; i < n; i++)
            {
                var inputArgs = Console.ReadLine().Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries);

                var name = inputArgs[0];
                var salary = decimal.Parse(inputArgs[1]);
                var position = inputArgs[2];
                var department = inputArgs[3];

                Employee newEmployee = null;

                if (inputArgs.Length == 4)
                {
                    newEmployee = new Employee(name, salary, position, department);
                }
                else if (inputArgs.Length == 5)
                {
                    var isParsed = int.TryParse(inputArgs[4], out var age);

                    if (isParsed)
                    {
                        newEmployee = new Employee(name, salary, position, department, age);
                    }
                    else
                    {
                        var email = inputArgs[4];
                        newEmployee = new Employee(name, salary, position, department, email);
                    }
                }
                else if (inputArgs.Length == 6)
                {
                    var isParsed = int.TryParse(inputArgs[4], out var parsedAge);

                    var age = -1;
                    var email = string.Empty;

                    if (isParsed)
                    {
                        age = parsedAge;
                        email = inputArgs[5];
                    }
                    else
                    {
                        age = int.Parse(inputArgs[5]);
                        email = inputArgs[4];
                    }

                    newEmployee = new Employee(name, salary, position, department, email, age);
                }

                employees.Add(newEmployee);
            }
        }
    }
}
