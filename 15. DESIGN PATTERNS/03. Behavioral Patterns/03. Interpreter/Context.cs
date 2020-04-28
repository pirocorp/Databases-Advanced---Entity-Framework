namespace _03._Interpreter
{
    /// <summary>
    /// The 'Context' class
    /// </summary>
    public class Context
    {
        private string _input;
        private int _output;

        public Context(string input)
        {
            this._input = input;
        }

        public string Input
        {
            get => this._input;
            set => this._input = value;
        }

        public int Output
        {
            get => this._output;
            set => this._output = value;
        }
    }
}
