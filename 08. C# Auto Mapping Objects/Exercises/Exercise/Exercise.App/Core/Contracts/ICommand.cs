﻿namespace Exercise.App.Core.Contracts
{
    public interface ICommand
    {
        string Execute(string[] args);
    }
}