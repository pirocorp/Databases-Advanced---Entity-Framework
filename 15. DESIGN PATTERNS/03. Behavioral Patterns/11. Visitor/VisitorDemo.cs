namespace _11._Visitor
{
    public static class VisitorDemo
    {
        public static void Main()
        {
            // Setup employee collection
            var employees = new Employees();
            employees.Attach(new Clerk());
            employees.Attach(new Director());
            employees.Attach(new President());

            // Employees are 'visited'
            employees.Accept(new IncomeVisitor());
            employees.Accept(new VacationVisitor());
        }
    }
}
