using MississippiMarbles.Classes;

bool preGame = true;
bool game = false;
int playerCount = 0;
int turn = 0; 
List<Player> players = new List<Player>();
void DisplayMenu()
{
    Console.WriteLine("\n=== Welcome to Mississippi Marbles ===");
    Console.WriteLine("1) Add Player");
    Console.WriteLine("2) Start Game");
    Console.WriteLine("3) View Rules");
    Console.WriteLine("4) Quit");
    Console.WriteLine("======================================");
}

while (preGame)
{
    DisplayMenu();
    Console.Write("Choice: ");
    string input = Console.ReadLine();

    try
    {
        int option = int.Parse(input);

        if (option < 1 || option > 4)
        {
            Console.WriteLine("Invalid choice. Please select a number between 1 and 4.\n");
        }
        else
        {
            switch (option)
            {
                case 1:
                    Console.Write("\nEnter player name (1–10 characters): ");
                    string nameInput = Console.ReadLine();

                    if(playerCount == 4)
                    {
                        Console.WriteLine("Looks like the max player count has been reached. Please start the game!\n");
                    }
                    else if (!string.IsNullOrWhiteSpace(nameInput) && nameInput.Length <= 10 && playerCount <= 3)
                    {
                        Player p = new Player(nameInput);
                        players.Add(p);
                        Console.WriteLine($"\nPlayer '{nameInput}' has been added!");
                        playerCount++;
                    }
                    else
                    {
                        Console.WriteLine("Invalid name. Name must be 1–10 characters.\n");
                    }
                    Console.WriteLine($"Current Players: {playerCount}");
                    break;

                case 2:
                    if (players.Count <= 0)
                    {
                        Console.WriteLine("\nYou need at least one player to start the game!\n");
                    }
                    else
                    {
                        Console.WriteLine("\nStarting the game...\n");
                        preGame = false;
                        game = true;
                    }
                    break;

                case 3:
                    Console.WriteLine("\n=== Game Rules ===");
                    Console.WriteLine("To Start: Each player rolls a single die. Highest roll starts. Play proceeds clockwise.");
                    Console.WriteLine("\n700 to Open: Player rolls six dice. Any scoring combination < 700 requires continuing.");
                    Console.WriteLine("Points must be accumulated in a single turn. If you fail to score, your turn ends.");
                    Console.WriteLine("\nSpecial Rolls:");
                    Console.WriteLine("- 4 twos: Lose all accumulated points (A Flood)");
                    Console.WriteLine("- Straight (1-6): 2000 points (Big Muddy)");
                    Console.WriteLine("- 6 of a kind: 6000 points (All the Marbles)");
                    Console.WriteLine("\nWinning Condition: First player to reach or surpass 11,000 points wins!");
                    Console.WriteLine("==================\n");
                    break;

                case 4:
                    Console.WriteLine("\nExiting the game. Goodbye!");
                    preGame = false;
                    break;
            }
        }
    }
    catch
    {
        Console.WriteLine("Invalid input. Please enter a number between 1 and 4.\n");
    }
}

while (game)
{
	Game g = new Game(players, turn);
	Console.WriteLine(players.ElementAt(g.winnerTurn).getPlayerName + " won the game!");
	game = false;
}

//Console.WriteLine("Please enter player name!");
//string name = Console.ReadLine();
//Player player = new Player(name);
//player.addToPot(6);
//player.addToPot(3);
//player.addToPot(4);
//player.addToPot(1);
//Console.WriteLine(player.getPlayerName
//+ "\n" + player.getPoints 
//+ "\n" + player.getSavedDice());
