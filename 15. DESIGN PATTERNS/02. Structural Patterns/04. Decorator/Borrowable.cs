namespace _04._Decorator
{
    using System;
    using System.Collections.Generic;

    public class Borrowable : Decorator
    {
        protected readonly List<string> _borrowers;

        public Borrowable(LibraryItem libraryItem) 
            : base(libraryItem)
        {
            this._borrowers = new List<string>();
        }

        public void BorrowItem(string name)
        {
            this._borrowers.Add(name);
            this.libraryItem.NumCopies--;
        }

        public void ReturnItem(string name)
        {
            this._borrowers.Remove(name);
            this.libraryItem.NumCopies++;
        }

        public override void Display()
        {
            base.Display();

            foreach (var borrower in this._borrowers)
            {
                Console.WriteLine(" borrower: " + borrower);
            }
        }
    }
}
