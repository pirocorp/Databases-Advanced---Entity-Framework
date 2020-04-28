namespace _03._Interpreter
{
    using System;
    using System.Collections.Generic;

    public static class InterpreterDemo
    {
        public static void Main()
        {
            var roman = "MCMXXVIII";
            var context = new Context(roman);

            // Build the 'parse tree'
            var tree = new List<Expression>();
            tree.Add(new ThousandExpression());
            tree.Add(new HundredExpression());
            tree.Add(new TenExpression());
            tree.Add(new OneExpression());

            // Interpret
            foreach (var exp in tree)
            {
                exp.Interpret(context);
            }

            Console.WriteLine($"{roman} = {context.Output}");
        }
    }
}
