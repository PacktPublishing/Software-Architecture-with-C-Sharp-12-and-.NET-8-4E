using SmartSearch;
using System;

string[] fruits = new string[]{
            "Apples", "Apricots", "Avocados",
            "Bananas", "Boysenberries", "Blueberries", "Bing Cherry", "Blackberries",
            "Cherries", "Cantaloupe", "Crab apples", "Clementine", "Cucumbers",
            "Melons", "Pears", "Grapes", "Strawberries", 
        };

var sd = new SmartDictionary<string>(m => m, fruits);

bool finished = false;

while (!finished)
{

    Console.WriteLine("Enter your search:");
    var search = Console.ReadLine();

    Console.WriteLine();
    foreach (var fruit in sd.Search(search, 5))
        Console.WriteLine(fruit);
    Console.WriteLine("Finished? (y/n)");
    finished = (Console.ReadKey().KeyChar == 'y');
}
        