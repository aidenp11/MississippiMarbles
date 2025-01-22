using MississippiMarbles.Classes;

Console.WriteLine("Please enter your name!");
String name = Console.ReadLine();
Player player = new Player(name);
player.addToPot(4);
String dice = player.getSavedDice();
player.addToPot(3);
player.addToPot(2);
player.addToPot(5);
player.addToPot(6);
Console.WriteLine(player.getPlayerName + 
                    "\n" + player.getPoints + 
                    "\n" + player.getSavedDice() + 
                    "\n" + name);