using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MississippiMarbles.Classes
{ 
	//Player Class { int points, string name, array saved_dice}

	internal class Player
	{ 
		private String playerName;
		private int points = 0;
		private int[] savedDice = {};

		public Player(String playerName) { this.playerName = playerName; }

		public String getPlayerName { get { return playerName;}}
		public int getPoints { get { return points;}}
		public String getSavedDice() 
		{ 
			String values = "";
			foreach (var i in savedDice)
			{
				values = values + i.ToString();
			}
		 return values;
		}
		public void setPoints(int points) { this.points = points;}
		public void addToPot(int diceValue) {savedDice.Append(diceValue);}
	}
}
