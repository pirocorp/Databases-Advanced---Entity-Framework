namespace P03_Mankind
{
    using System;

    public class StartUp
    {
        public static void Main()
        {
            var studentArgs = Console.ReadLine().Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries);
            var studentFirstName = studentArgs[0];
            var studentLastName = studentArgs[1];
            var studentFacultyNumber = studentArgs[2];

            var workerArgs = Console.ReadLine().Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            var workerFirstName = workerArgs[0];
            var workerLastName = workerArgs[1];
            var workerSalary = decimal.Parse(workerArgs[2]);
            var workerWorkingHours = decimal.Parse(workerArgs[3]);

            Student student = null;
            Worker worker = null;

            try
            {
                student = new Student(studentFirstName, studentLastName, studentFacultyNumber);
                worker = new Worker(workerFirstName, workerLastName, workerSalary, workerWorkingHours);
            }
            catch (ArgumentException ae)
            {
                Console.WriteLine(ae.Message);
                return;
            }

            Console.WriteLine(student);
            Console.WriteLine();
            Console.WriteLine(worker);
        }
    }
}