# Battleships

This is a command-line application that provides a solution to the Battleships problem defined [here](https://medium.com/guestline-labs/hints-for-our-interview-process-and-code-test-ae647325f400)

## Getting Started

### Prerequisites

Download and install the [.NET Core SDK 5](<https://dotnet.microsoft.com/download/dotnet/5.0>) on your computer.

### Installing

To build the application go to the folder of the solution an execute.

```powershell
dotnet build
```

### Running the application

To execute the solution go to the solution folder and execute

```powershell
dotnet run --project .\Battleships.Console\Battleships.Console.csproj
```

## Running the tests

To run the test, go to the folder Battleships.Tests inside the solution and execute

```powershell
cd .\Battleships.Tests\ 
dotnet test
```

## Notes

### Solution notes

The solution proposed is composed by 3 projects (tests, console, domain).

* Battleships.Tests: Contains the unit test of the app

* Battleships.Console: Contains the console interface of the solution

* Battleships.Domain: Contains the application logic

### Battleships.Domain

#### Design

The root of our domain is the Game class. This class is composed of a GameConfiguration (that determines the size of the grid and the number of ships) and a list of ships. These ships are positioned in several cells

[![Domain Relationship](https://mermaid.ink/img/eyJjb2RlIjoiY2xhc3NEaWFncmFtXG4gICAgR2FtZSBcIjFcIiAtLSogXCIxXCIgR2FtZUNvbmZpZ3VyYXRpb25cbiAgICBHYW1lIFwiMVwiIC0tKiBcIipcIiBTaGlwXG4gICAgR2FtZSAtLT4gQ2VsbFxuICAgIFNoaXAgLS0-IENlbGxcbiAgICAgICAgICAgICIsIm1lcm1haWQiOnsidGhlbWUiOiJkZWZhdWx0In0sInVwZGF0ZUVkaXRvciI6ZmFsc2V9)](https://mermaid-js.github.io/mermaid-live-editor/#/edit/eyJjb2RlIjoiY2xhc3NEaWFncmFtXG4gICAgR2FtZSBcIjFcIiAtLSogXCIxXCIgR2FtZUNvbmZpZ3VyYXRpb25cbiAgICBHYW1lIFwiMVwiIC0tKiBcIipcIiBTaGlwXG4gICAgR2FtZSAtLT4gQ2VsbFxuICAgIFNoaXAgLS0-IENlbGxcbiAgICAgICAgICAgICIsIm1lcm1haWQiOnsidGhlbWUiOiJkZWZhdWx0In0sInVwZGF0ZUVkaXRvciI6ZmFsc2V9)

The more interesting part of the domain is the random positioning of the ships. What the Game does is:

1. Roll a dice to decide how we want to position the ship "horizontal"/"vertical"
1. Get the cells that in an empty board can be used as a start position of the ship in the direction selected, removing the cells that will put the ship in collision with previous positioned ships
1. Roll a dice to decide which of the valid cells we will use to position the ship

If I didn't miss something this algorithm works with the proposed problem (10*10 board with 1 battleship and 2 frigates) but is not prepared to deal with more complex scenarios (scenarios in which you can get to a situation with impossible solutions)

### Implementation details

In this solution, I tried to balance the different principles that are outlined in the definition of the problem, and the time constraints,  but in the end as all the balance exercise is very subjective.

So, for example

* I only created unit tests for two public methods. I tried to focus on the methods that I thought were the more error-prone. The calculation of the "shadow" of the ship, and the calculation of the valid board to position a ship, but all the public methods has to have their own tests

* I defined the method GetValidCells in the Game class as public when it has to be private, I did it because I wanted to test it but actually what was correct is to test the method Initialize (is the public method that provokes the calls to the GetValidCells) but as the initialize depends on the Random class I'd have to wrap this class so I will be able to mock in the tests, and I find that this extra work although is the correct thing represents more code, more time-consuming and little extra value.

I tried to make the code as simple and self-explanatory as possible but avoiding comments. To do this I created several functions that actually his only goal is to make the code more readable, and I only added one comment that mainly is to explain the concept of the shadow of a ship because this concept is a bit "alien" to the problem but for me was useful.

As a final note, this application uses some features of c#9, this is the reason that it needs .net 5.0, (not only to show-off and because is cool ;-) ) but because it allows me to write more concise code. These are the [top level statement](https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-9#top-level-statements) and the [pattern matching enhancements](https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-9#pattern-matching-enhancements) and you can see these features in the console project.
