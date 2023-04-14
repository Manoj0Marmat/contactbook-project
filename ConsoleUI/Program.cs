using ContactBookLibrary;
using System;
using System.Collections.Generic;

namespace ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ContactController controller = new ContactController();
            List<Contact> contacts = new List<Contact>();
            controller.Get(ref contacts);
            foreach (Contact contact in contacts)
            {
                Console.WriteLine(contact);
            }
        }
    }
}
