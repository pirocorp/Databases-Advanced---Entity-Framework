namespace P07_Animals.Animals
{
    using System;

    public abstract class Animal : ISoundProducible
    {
        private string name;
        private int age;
        private string gender;

        protected Animal(string name, int age, string gender)
        {
            this.Name = name;
            this.Age = age;
            this.Gender = gender;
        }

        public string Name
        {
            get => this.name;
            private set => this.name = value;
        }

        public int Age
        {
            get => this.age;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Invalid input!");
                }

                this.age = value;
            }
        }

        public string Gender
        {
            get => this.gender;
            private set => this.gender = value;
        }

        public abstract string ProduceSound();

        public override string ToString()
        {
            return $"{this.Name} {this.Age} {this.Gender}";
        }
    }
}