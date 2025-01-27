﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MississippiMarbles.Classes
{
	internal class Game
	{
		public int winnerTurn;
		public Game(List<Player> players, int turn)
		{
			Player player = players.ElementAt(turn);
			player.diceNum = 6;
			player.turn = true;
			Roll roll;
			Console.WriteLine(player.getPlayerName + "'s turn\nScore: " + player.getPoints);
			while (player.turn)
			{
				if (player.diceNum == 0)
				{
					player.setPoints(player.getPoints + player.pointsToAdd);
					Console.WriteLine("Player score is now " + player.getPoints + '\n');
					player.turn = false;
				}
				Console.WriteLine(player.diceNum + " dice left\nTotal score to be added for this turn: " + player.pointsToAdd);
				Console.Write("1) Roll\n2) End Turn\n");
				Console.Write("Choice: ");
				int option;
				string input = Console.ReadLine();
				try
				{
					option = int.Parse(input);
					if (option > 2 || option < 1)
					{
						Console.WriteLine("Input must be 1 or 2\n");
					}
					else
					{
						switch (option)
						{
							case 1:
								roll = new Roll(player);
								roll.TryOpen(player.diceNum);
								break;
							case 2:
								if (player.getPoints == 0 && player.pointsToAdd < 700)
								{
									Console.WriteLine("You need 700 or more points to start!\n");
									player.turn = false;
								}
								else
								{
									player.setPoints(player.getPoints + player.pointsToAdd);
									Console.WriteLine("Player score is now " + player.getPoints + '\n');
									player.turn = false;
								}
								break;
						}
					}
				}
				catch
				{
					Console.WriteLine("Input must be 1 or 2\n");
				}
			}
			if (player.getPoints >= 11000)
			{
				winnerTurn = turn;
				return;
			}
			else if (players.Count == turn + 1)
			{
				turn = 0;
				new Game(players, turn);
			}
			else new Game(players, turn + 1);
		}
	}
}
