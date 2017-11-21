namespace BankSystem.Client.IO
{
    using BankSystem.Client.Interfaces;
    using System;

    public class Writer : IWriter
    {
        public void WriteMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}