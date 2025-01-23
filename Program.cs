using MississippiMarbles.Classes;


Console.WriteLine("Please enter player name!");
string name = Console.ReadLine();
Player player = new Player(name);
player.addToPot(6);
player.addToPot(3);
player.addToPot(4);
player.addToPot(1);
Console.WriteLine(player.getPlayerName
+ "\n" + player.getPoints 
+ "\n" + player.getSavedDice());
