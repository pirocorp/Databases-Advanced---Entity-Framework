namespace _02._Command
{
    using System;

    /// <summary>
    /// The 'ConcreteCommand' class
    /// </summary>
    public class CalculatorCommand : Command
    {
        private char _operator;
        private int _operand;
        private readonly Calculator _calculator;

        public CalculatorCommand(Calculator calculator,
            char @operator, int operand)
        {
            this._calculator = calculator;
            this._operator = @operator;
            this._operand = operand;
        }

        public char Operator
        {
            set => this._operator = value;
        }

        public int Operand
        {
            set => this._operand = value;
        }

        public override void Execute()
        {
            this._calculator.Operation(this._operator, this._operand);
        }

        public override void UnExecute()
        {
            this._calculator.Operation(this.Undo(this._operator), this._operand);
        }

        // Returns opposite operator for given operator
        private char Undo(char @operator)
        {
            switch (@operator)
            {
                case '+': return '-';
                case '-': return '+';
                case '*': return '/';
                case '/': return '*';
                default:
                    throw new ArgumentException("@operator");
            }
        }
    }
}
