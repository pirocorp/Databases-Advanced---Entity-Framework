namespace _02._Command
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The 'Invoker' class
    /// </summary>
    public class User
    {
        private readonly Calculator _calculator;
        private readonly List<Command> _commands;
        private int _current;

        public User()
        {
            this._calculator = new Calculator();
            this._commands = new List<Command>();
            this._current = 0;
        }

        public void Redo(int levels)
        {
            Console.WriteLine("\n---- Redo {0} levels ", levels);
            
            for (var i = 0; i < levels; i++)
            {
                if (this._current < this._commands.Count - 1)
                {
                    var command = this._commands[this._current++];
                    command.Execute();
                }
            }
        }

        public void Undo(int levels)
        {
            Console.WriteLine("\n---- Undo {0} levels ", levels);
            
            for (var i = 0; i < levels; i++)
            {
                if (this._current > 0)
                {
                    var command = this._commands[--this._current];
                    command.UnExecute();
                }
            }
        }

        public void Compute(char @operator, int operand)
        {
            // Create command operation and execute it
            var command = new CalculatorCommand(this._calculator, @operator, operand);
            command.Execute();

            // Add command to undo list
            this._commands.Add(command);
            this._current++;
        }
    }
}
