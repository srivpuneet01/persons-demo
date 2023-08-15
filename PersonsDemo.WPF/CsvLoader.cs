using PersonsDemo.WPF.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsDemo.WPF
{
    public static class CsvLoader
    {
        public static IEnumerable<Person> LoadPersons(string fileName)
        {
            using var streamReader = new StreamReader(fileName);
            string line;
            var persons = new List<Person>();
            while ((line = streamReader?.ReadLine()) != null)
            {
                string[] parts = line.Split(',');
                if (parts.Length == 6)
                {
                    Person person = new Person
                    {
                        Name = parts[0],
                        Country = parts[1],
                        Address = parts[2],
                        PostalZip = parts[3],
                        Email = parts[4],
                        Phone = parts[5],
                    };
                    persons.Add(person);
                }
                else
                {
                    Console.WriteLine($"Invalid line format: {line}");
                }
            }

            if (persons.Any())
            {
                // Remove header column
                persons.RemoveAt(0);
            }

            return persons;
        }
    }
}
