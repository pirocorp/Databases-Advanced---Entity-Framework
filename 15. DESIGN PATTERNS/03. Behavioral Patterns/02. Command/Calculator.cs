namespace _02._Command
{
    using System;

    /// <summary>
    /// The 'Receiver' class
    /// </summary>
    public class Calculator
    {
        private int _current = 0;

        public void Operation(char @operator, int operand)
        {
            switch (@operator)
            {
                case '+': this._current += operand; break;
                case '-': this._current -= operand; break;
                case '*': this._current *= operand; break;
                case '/': this._current /= operand; break;
            }

            Console.WriteLine("Current value = {0,3} (following {1} {2})", this._current, @operator, operand);
        }
    }
}
