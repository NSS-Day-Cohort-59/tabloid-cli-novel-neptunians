using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabloidCLI.UserInterfaceManagers
{
    internal class JournalManager : IUserInterfaceManager
    {
        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Journal Entry Menu");
            Console.WriteLine("1) Add a journal entry.");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.WriteLine("Please enter a title.");
                    string title = Console.ReadLine();
                    Console.WriteLine("Please add text content.");
                    string content = Console.ReadLine();
                    return this;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;





            }
        }
    }
}
