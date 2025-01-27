using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MississippiMarbles.Classes
{
	internal class Roll(int[] dice)
	{
		public int[] dice = dice;
		public int[]? selectedDice;
		public int pointsScored = 0;

		private int CalculateScore()
		{
			return 0;
		}
		private bool ToOpen()
		{
			/*A player rolls six dice and sets aside any scoring combinations totaling less than 700 points, 
             * then re-rolls the remaining dice. If multiple scoring options are available, the player must set aside at least one but may keep more. 
             * The player can continue rolling as long as they score. Once the total score reaches 700 or more, the player can either risk the entire score by continuing to roll or 
             * pass their turn to lock in the score and officially "open." 
             * After opening, the player does not need to qualify with 700 points in future turns. 
             * If the player fails to reach 700 points and rolls a non-scoring combination, their turn ends, and they must wait until their next turn to try again.*/
			return true;
		}
		private bool DiceFlood(int[] dice)
		{
			int floodCount = 0;
			foreach (int value in dice)
			{
				if (value == 2) floodCount++;
			}
			if (floodCount == 4) return true;
			else return false;
		}
		private bool MultipleValue(int[] dice, int target, int index, int count, int repNum)
		{
			// Base case: If we reach the end of the array, check if the count is exactly 3
			if (index >= dice.Length)
			{
				return count >= repNum;
			}
			// Increment count if the current element matches the target number
			if (dice[index] == target)
			{
				count++;
			}
			// Recurse to the next index
			return MultipleValue(dice, target, index + 1, count, repNum);
		}

		private int ScoreThreeOfAKind(int[] selectedDice, int targetNumber, int score)
		{
			if (MultipleValue(selectedDice, targetNumber, 0, 0, 3))
			{
				return pointsScored += score;
			}
			return 0;
		}

		private int Straight(int[] selectedDice)
		{
			// Define the straight sequence we are looking for
			int[] straight = { 1, 2, 3, 4, 5, 6 };

			// Get distinct numbers and sort them
			int[] sortedUnique = selectedDice.Distinct().OrderBy(x => x).ToArray();

			// Check if the straight sequence exists in the sorted unique numbers
			if (straight.All(sortedUnique.Contains)) pointsScored += 2000;

			return 0;
		}

	}
}
