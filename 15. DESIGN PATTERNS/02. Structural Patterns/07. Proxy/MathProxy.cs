namespace _07._Proxy
{
    /// <summary>
    /// The 'Proxy Object' class
    /// </summary>
    public class MathProxy : IMath
    {
        private readonly Math _math;

        public MathProxy()
        {
            this._math = new Math();
        }

        public double Add(double x, double y)
        {
            return this._math.Add(x, y);
        }
        public double Sub(double x, double y)
        {
            return this._math.Sub(x, y);
        }
        public double Mul(double x, double y)
        {
            return this._math.Mul(x, y);
        }
        public double Div(double x, double y)
        {
            return this._math.Div(x, y);
        }
    }
}
