using MississippiMarbles.Classes;

bool preGame = true;
bool game = false;
int turn = 0; 
List<Player> players = new List<Player>();
while (preGame)
{
	Console.WriteLine("1) Add Player\n2) Start Game\n3) View Rules\n4) Quit");
	Console.Write("Choice: ");

	int option;
	string input = Console.ReadLine();
	try
	{
		option = int.Parse(input);
		if (option > 4 || option < 1)
		{
			Console.WriteLine("Input must be 1, 2, 3 or 4\n");
		}
		else
		{
			switch (option)
			{
				case 1:
					Console.Write("Enter player name: ");
					string nameInput = Console.ReadLine();
					if (nameInput != null && nameInput != "" && nameInput.Count() <= 10)
					{
						Player p = new Player(nameInput);
						players.Add(p);
						Console.WriteLine(nameInput + " added\n");
					}
					else
					{
						Console.WriteLine("Name must be between 1 and 10 characters\nPlayer not added\n");
					}
					break;
				case 2:
					if (players.Count <= 0)
					{
						Console.WriteLine("You must have at least one player to begin the game\n");
					}
					else
					{
						preGame = false;
						game = true;
						Console.WriteLine('\n');
					}
					break;
				case 3:
					Console.WriteLine("To Start: Each player rolls a single die. Highest roll starts. Play proceeds to the left. (clockwise)\r\n\r\n700 to Open: Player rolls six dice. Any scoring combination thrown that is less than 700 may be set aside and player rolls remaining dice. (If player has more than one scoring possibility he is only required to keep one, but may keep any number he chooses. Example: 2 ones and a 5 are rolled; he may keep the ones and put back the 5.)\r\n\r\nPlayer can continue to roll remaining dice (setting aside scorers) as long as he continues to score. When he reaches 700 or more he may elect to continue, risking the entire amount, or pass turn to next player and be officially opened. Once a player reaches 700 or more and elects to pass the roll to next player he does not have to qualify on subsequent turns. If a player fails to get 700 points to open before throwing a non-scoring roll he must wait until his next turn to try again.\r\n\r\nAfter Opening: After a player officially opened he can stop after any scoring roll on subsequent turns and take the score he has earned. Play then passes to next player. If he should at any time throw a non-scoring roll he would lose only the number of points accumulated in that turn, but not any he has acquired previously.\r\n\r\nException: If at any time a player should throw 4 twos on one roll he loses all points he has acquired in the game and must start over on his next turn. This means reopening with 700 points.\r\n\r\nWinning: First to reach or surpass 11,000 points wins.\r\n\r\nAll scoring combinations must be thrown on one roll. Example: To get 500 points for 3 ones you cannot throw 2 ones, set them aside and throw another one on your next roll. This would constitute a total of 300 points rolled.\r\n\r\nA straight occurs when a one, two, three, four, five, and six are thrown on one roll.\r\n\r\nIf all six dice have been used as scorers in one turn, the player may elect to continue his turn with a fresh roll of all dice. If a player has not officially opened with 700 points he must continue his turn.\r\n\r\nSCORING\r\n700 to open (Makin' Mud)\r\n11,000 points (The Delta) (Win the Game)\r\n1 counts 100 points\r\n3 of a kind (Smooth Water)\r\n3 ones = 500 points\r\n3 twos = 200 points\r\n3 threes = 300 points\r\n3 fours = 400 points\r\n3 fives = 500 points\r\n3 sixes = 600 points\r\n4 of a kind = 1000 points (Ridin' the Rapids)\r\nStraight = 2000 points (Big Muddy)\r\n5 of a kind = 3000 points (Channel Cat)\r\n6 of a kind = 6000 points (All the Marbles)\r\n4 twos = Lose Total Accumulated Score (A Flood)\n");
					break;
				case 4:
					preGame = false;
					break;
			}
		}
	}
	catch
	{
		Console.WriteLine("Input must be 1, 2, 3 or 4\n");
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
