using System;
using System.Collections.Generic;

namespace HashTables
{
    class Program
    {
        static void Main(string[] args)
        {
            var size = 1000;
            var table = new HashTableOpen(size);
            //var table = new HashTableChains(size);
            var count = 10;
            Console.WriteLine("Generating...");

            var users = new List<UserData>();
            for (var i = 0; i < count; i++)
                users.Add(UserData.RandomInstance());
            Console.WriteLine("Pushing...");

            var keys = new List<int>();
            for (var i = 0; i < count; i++)
                keys.Add(table.Add(users[i]));
            Console.WriteLine($"Done, press enter to print out all the {count} objects");
            Console.ReadLine();

            var cnt = 0;
            for (var i = 0; i < count; i++)
            {
                var e = table.Search(users[i].Id);
                cnt += e.Id == users[i].Id && users[i].RegDate == e.RegDate ? 1 : 0;
                Console.BackgroundColor = e.Id == users[i].Id && users[i].RegDate == e.RegDate
                    ? ConsoleColor.Green
                    : ConsoleColor.Red;
                Console.WriteLine($"{e.Id} - {e.RegDate} \t\t\t\t| {users[i].Id} - {users[i].RegDate}");
            }

            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine($"{cnt}/{count} ({cnt * 100.0 / count}%)");
            Console.WriteLine($"The largest cluster:{table.GetLargestCluster()}");
            Console.ReadLine();
        }
    }
}
