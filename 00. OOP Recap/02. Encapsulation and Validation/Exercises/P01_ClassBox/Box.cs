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
            this.x = x;
            this.y = y;
            this.z = z;
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
    }
}