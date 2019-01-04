namespace P04_RandomList
{
    using System;
    using System.Collections.Generic;

    public class RandomList : List<string>
    {
        private readonly Random rnd;

        public RandomList()
            :base()
        {
            this.rnd = new Random();
        }

        public string RandomString()
        {
            var index = this.rnd.Next(0, this.Count - 1);
            var result = this[index];
            this.RemoveAt(index);
            return result;
        }
    }
}