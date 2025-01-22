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

		Player(String playerName) { this.playerName = playerName; }

		public String getPlayerName { get { return playerName;}}
		public int getPoints { get { return points;}}
		public int[] getSavedDice { get { return savedDice;} }
		public void setPoints(int points) { this.points = points;}
		public void addToPot(int diceValue) {savedDice.Append(diceValue);}
	}
}
