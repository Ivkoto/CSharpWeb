namespace BankSystem.Client.Core
{
    using BankSystem.Client.Interfaces;
    using BankSystem.Client.IO;
    using System;
    using System.Linq;
    using System.Reflection;

    public class CommandProcessor : ICommandProcesor
    {
        private const string CommanSuffix = "Command";

        private IAuthenticationManager authenticationManager;

        public CommandProcessor(IAuthenticationManager authenticationManager)
        {
            this.authenticationManager = authenticationManager;
        }

        public string ProcessCommand(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return OutputMessages.InvalidCommand;
            }

            var commandInput = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            var currentCommand = commandInput[0];
            var commandArgs = commandInput.Skip(1).ToArray();

            Type commandType = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(t => t.Name == currentCommand + CommanSuffix);

            var commandParams = new object[] {commandArgs, this.authenticationManager };

            var cmd = Activator.CreateInstance(commandType, commandParams);
        }
    }
}