using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
				if (floodCount >= 4) return true;
			}
			return false;
		}
		private bool SmoothWater(int[] dice, int target, int index, int count) 
		{
            // Base case: If we reach the end of the array, check if the count is exactly 3
            if (index >= dice.Length)
            {
                return count >= 3;
            }
            // Increment count if the current element matches the target number
            if (dice[index] == target)
            {
                count++;
            }
            // Recurse to the next index
            return SmoothWater(dice, target, index + 1, count);
        }

		private int ThreeOnes(int[] selectedDice) 
		{
			if (SmoothWater(selectedDice, 1, 0, 0)) return pointsScored += 500;
			return 0;
		}
        private int ThreeTwos(int[] selectedDice)
        {
            if (SmoothWater(selectedDice, 2, 0, 0)) return pointsScored += 200;
            return 0;
        }
        private int ThreeThrees(int[] selectedDice)
        {
            if (SmoothWater(selectedDice, 3, 0, 0)) return pointsScored += 300;
            return 0;
        }
        private int ThreeFours(int[] selectedDice)
        {
            if (SmoothWater(selectedDice, 4, 0, 0)) return pointsScored += 400;
            return 0;
        }
        private int ThreeFives(int[] selectedDice)
        {
            if (SmoothWater(selectedDice, 5, 0, 0)) return pointsScored += 500;
            return 0;
        }
        private int ThreeSixes(int[] selectedDice)
        {
            if (SmoothWater(selectedDice, 6, 0, 0)) return pointsScored += 600;
            return 0;
        }
        private int RidinRapids(int[] dice) 
        {
            var counts = new Dictionary<int, int>();

            foreach (int num in dice)
            {
                if (!counts.ContainsKey(num))
                {
                    counts[num] = 0;
                }

                counts[num]++;

                if (counts[num] >= 4)
                {
                    return pointsScored += 1000;
                }
            }
            return 0;
        }
    }
}
