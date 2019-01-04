namespace P05_StorageBox
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Box<T>
    {
        private T[] data;
        private int count;

        public Box()
        {
            this.data = new T[4];
            this.count = 0;
        }

        public int Count => this.count;

        public void Add(T item)
        {
            if (this.count == this.data.Length)
            {
                var oldData = this.data;
                this.data = new T[this.count * 2];
                oldData.CopyTo(this.data, 0);
            }

            this.data[this.count] = item;
            this.count++;
        }

        public T Remove()
        {
            var lastIndex = this.count - 1;
            var lastElement = this.data[lastIndex];
            this.data[lastIndex] = default(T);
            this.count--;
            return lastElement;
        }

        public override string ToString()
        {
            var result = string.Join(", ", this.data);
            return result;
        }
    }
}