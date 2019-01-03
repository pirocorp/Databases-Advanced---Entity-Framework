namespace P01_ClassBox
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Box
    {
        private decimal x;
        private decimal y;
        private decimal z;

        public Box(decimal x, decimal y, decimal z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public decimal X
        {
            get => this.x;
            private set
            {
                this.ValidatePositiveNumber(nameof(this.X), value);
                this.x = value;
            }
        }

        public decimal Y
        {
            get => this.y;
            private set
            {
                this.ValidatePositiveNumber(nameof(this.Y), value);
                this.y = value;
            }
        }

        public decimal Z
        {
            get => this.z;
            private set
            {
                this.ValidatePositiveNumber(nameof(this.Z), value);
                this.z = value;
            }
        }

        public decimal Volume()
        {
            var result = this.x * this.y * this.z;
            return result;
        }
        
        public decimal LateralSurfaceArea()
        {
            var result = 2 * this.x * this.y + 2 * this.z * this.y;
            return result;
        }

        public decimal SurfaceArea()
        {
            var result = this.LateralSurfaceArea() + 2 * this.x * this.z;
            return result;
        }

        private void ValidatePositiveNumber(string nameOfParameter, decimal number)
        {
            if (number <= 0)
            {
                throw new ArgumentException($"{nameOfParameter} cannot be zero or negative.");
            }
        }
    }
}