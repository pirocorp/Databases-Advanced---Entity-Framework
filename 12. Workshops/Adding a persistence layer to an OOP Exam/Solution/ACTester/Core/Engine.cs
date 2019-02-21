namespace ACTester.Core
{
    using System;
    using Interfaces;

    public class Engine
    {
        public Engine(IActionManager actionManager, IUserInterface userInterface)
        {
            this.ActionManager = actionManager;
            this.UserInterface = userInterface;
        }

        public IActionManager ActionManager { get; private set; }

        public IUserInterface UserInterface { get; private set; }

        public void Run()
        {
            while (true)
            {
                var line = this.UserInterface.ReadLine();
                if (string.IsNullOrEmpty(line))
                {
                    break;
                }

                line = line.Trim();
                try
                {
                    var command = new Command(line);
                    var commandResult = this.ActionManager.ExecuteCommand(command);
                    this.UserInterface.WriteLine(commandResult);
                }
                catch (Exception ex)
                {
                    this.UserInterface.WriteLine(ex.Message);
                }
            }
        }
    }
}
