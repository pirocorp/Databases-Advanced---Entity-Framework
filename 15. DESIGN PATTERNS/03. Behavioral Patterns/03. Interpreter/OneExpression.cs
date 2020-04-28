﻿namespace _03._Interpreter
{
    /// <summary>
    /// A 'TerminalExpression' class
    /// <remarks>
    /// One checks for I, II, III, IV, V, VI, VII, VIII, IX
    /// </remarks>
    /// </summary>
    public class OneExpression : Expression
    {
        public override string One() { return "I"; }

        public override string Four() { return "IV"; }

        public override string Five() { return "V"; }

        public override string Nine() { return "IX"; }

        public override int Multiplier() { return 1; }
    }
}
