using Battleships.Domain;
using System;
using System.Text.RegularExpressions;

string line;
static string GetResult(SalvoResult salvo) => salvo switch
{
    SalvoResult.Hit => "hit",
    SalvoResult.Miss => "miss",
    SalvoResult.OutOfBoard => "Great!!! A new crater on the moon.",
    SalvoResult.Sunk => "Sunk!! :-(",
    SalvoResult.GameOver => "You win!!!",
    _ => "I don't know what's happening"
};

Console.WriteLine("Wellcome to Battleships");

var game = new Game(new GameConfiguration(10, 10, 1, 2));
game.Initialize();

Console.WriteLine("Waiting for shots. Write A9 to shot at column A row 9. Q to quit");

while ((line = Console.ReadLine()) != null && line != "Q")
{
    Console.WriteLine(  
        (new Regex(@"[A-J][0-9]").IsMatch(line)) ?
            GetResult(game.Shot(new Cell(line.Substring(0, 1), line[1..]))) :
            $"Sorry I didn't understand the cell from {line}");
}