using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    public class JournalManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private JournalRepository _journalRepository;
        private string _connectionString;

        public JournalManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _journalRepository = new JournalRepository(connectionString);
            _connectionString = connectionString;
        }
       
        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Journal Entry Menu");
            Console.WriteLine("1) Add a journal entry.");
            Console.WriteLine("2) List Journal Entries.");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Journal journal = new Journal();

                    Console.WriteLine("Please enter a title.");
                    string title = Console.ReadLine();
                    journal.Title = title;
                    
                    Console.WriteLine("Please add text content.");
                    string content = Console.ReadLine();
                    journal.Content = content;

                    _journalRepository.Insert(journal);
                    return this;

                case "2":
                    List();
                    return this;

                default:
                    Console.WriteLine("Invalid Selection");
                    return this;





            }
            
        }
        private void List()
        {
            List<Journal> journals = _journalRepository.GetAll();
            foreach (Journal journal in journals)
            {
                Console.WriteLine(journal.ToString());
            }
        }
    }
}



                    

            
