﻿namespace P01_SingleInheritance
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Dog : Animal
    {
        public void Bark()
        {
            Console.WriteLine("Barking...");
        }
    }
}