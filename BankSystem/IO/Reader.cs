namespace BankSystem.Client.IO
{
    using BankSystem.Client.Interfaces;
    using System;

    public class Reader : IReader
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}