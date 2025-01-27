using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MississippiMarbles.Classes
{
	internal class Roll(Player playerTurn)
	{
		Player player = playerTurn;
		public int pointsToAdd;
		public List<int> dice = new List<int>();
		public List<int>? selectedDice;
		public int pointsScored = 0;
		public enum Concepts
		{
			ONE,
			FIVE,
			STRAIGHT,
			TOK1,
			TOK2,
			TOK3,
			TOK4,
			TOK5,
			TOK6,
			FROK,
			FVOK,
			SOK
		}

		public void TryOpen(int diceNum)
		{
			Random r = new Random();
			int roll;
			bool choosingDice = true;
			List<Concepts> possibilities = new List<Concepts>();
			for (int i = 0; i < diceNum; i++)
			{
				roll = r.Next(1, 7);
				dice.Add(roll);
			}
			for (int i = 0; i < dice.Count; i++)
			{
				if (i == 0) Console.WriteLine("Dice rolled: ");
				Console.Write(dice.ElementAt(i) + " ");
				if (i == dice.Count - 1) Console.WriteLine('\n');
			}
			if (DiceFlood(dice))
			{
				Console.WriteLine("Dice Flood! You've lost all your points!\n");
				player.setPoints(0);
				player.turn = false;
				return;
			}
			else
			{
				if (Straight(dice)) possibilities.Add(Concepts.STRAIGHT);
				for (int i = 0; i < dice.Count; i++)
				{
					if (dice.ElementAt(i) == 1) possibilities.Add(Concepts.ONE);
					if (dice.ElementAt(i) == 5) possibilities.Add(Concepts.FIVE);
                }
				if (MultipleValue(dice, 1, 0, 0, 3)) possibilities.Add(Concepts.TOK1);
				if (MultipleValue(dice, 2, 0, 0, 3)) possibilities.Add(Concepts.TOK2);
				if (MultipleValue(dice, 3, 0, 0, 3)) possibilities.Add(Concepts.TOK3);
				if (MultipleValue(dice, 4, 0, 0, 3)) possibilities.Add(Concepts.TOK4);
				if (MultipleValue(dice, 5, 0, 0, 3)) possibilities.Add(Concepts.TOK5);
				if (MultipleValue(dice, 6, 0, 0, 3)) possibilities.Add(Concepts.TOK6);
				for (int i = 1; i <= 6; i++)
				{
					if (MultipleValue(dice, i, 0, 0, 4)) possibilities.Add(Concepts.FROK);
				}
				for (int i = 1; i <= 6; i++)
				{
					if (MultipleValue(dice, i, 0, 0, 5)) possibilities.Add(Concepts.FVOK);
				}
				for (int i = 1; i <= 6; i++)
				{
					if (MultipleValue(dice, i, 0, 0, 6)) possibilities.Add(Concepts.SOK);
				}

				if (possibilities.Count <= 0)
				{
					Console.WriteLine("No options, no points gained for this turn!\n");
					player.pointsToAdd = 0;
					player.turn = false;
					return;
				}

				while (choosingDice)
				{
					for (int i = 0; i < possibilities.Count; i++)
					{
						if (possibilities.ElementAt(i) == Concepts.ONE) Console.WriteLine(i+1 + ") 1 one = 100 points");
                        else if (possibilities.ElementAt(i) == Concepts.FIVE) Console.WriteLine(i + 1 + ") 1 five = 50 points");
                        else if (possibilities.ElementAt(i) == Concepts.TOK1) Console.WriteLine(i+1 + ") 3 ones = 500 points");
						else if (possibilities.ElementAt(i) == Concepts.TOK2) Console.WriteLine(i+1 + ") 3 twos = 200 points");
						else if (possibilities.ElementAt(i) == Concepts.TOK3) Console.WriteLine(i + 1 + ") 3 threes = 300 points");
						else if (possibilities.ElementAt(i) == Concepts.TOK4) Console.WriteLine(i + 1 + ") 3 fours = 400 points");
						else if (possibilities.ElementAt(i) == Concepts.TOK5) Console.WriteLine(i + 1 + ") 3 fives = 500 points");
						else if (possibilities.ElementAt(i) == Concepts.TOK6) Console.WriteLine(i + 1 + ") 3 sixes = 600 points");
						else if (possibilities.ElementAt(i) == Concepts.FROK) Console.WriteLine(i + 1 + ") Four of a kind = 1000 points");
						else if (possibilities.ElementAt(i) == Concepts.FVOK) Console.WriteLine(i + 1 + ") Five of a kind = 3000 points");
						else if (possibilities.ElementAt(i) == Concepts.SOK) Console.WriteLine(i + 1 + ") Six of a kind = 6000 points");
						else if (possibilities.ElementAt(i) == Concepts.STRAIGHT) Console.WriteLine(i + 1 + ") Straight = 2000 points");
					}

					Console.Write("Choice: ");
					int option;
					string input = Console.ReadLine();
					try
					{
						option = int.Parse(input);
						if (option > possibilities.Count || option < 1)
						{
							Console.WriteLine("Input must be between 1 and " + possibilities.Count + "\n");
						}
						else
						{
							if (possibilities.ElementAt(option - 1) == Concepts.ONE)
							{
								player.diceNum -= 1;
								player.pointsToAdd += 100;
                                Console.WriteLine("1 chosen\n");
								choosingDice = false;
								return;
							}
							else if (possibilities.ElementAt(option - 1) == Concepts.FIVE)
							{
								player.diceNum -= 1;
								player.pointsToAdd += 50;
								Console.WriteLine("5 chosen\n");
								choosingDice = false;
								return;
							}
							else if (possibilities.ElementAt(option - 1) == Concepts.STRAIGHT)
							{
								player.diceNum -= 6;
								player.setPoints(2000);
								Console.WriteLine("Straight chosen\n");
								choosingDice = false;
								return;
							}
							else if (possibilities.ElementAt(option - 1) == Concepts.TOK1)
							{
								player.diceNum -= 3;
								player.pointsToAdd += 500;
								Console.WriteLine("3 ones chosen\n");
								choosingDice = false;
								return;
							}
							else if (possibilities.ElementAt(option - 1) == Concepts.TOK2)
							{
								player.diceNum -= 3;
								player.pointsToAdd += 200;
								Console.WriteLine("3 twos chosen\n");
								choosingDice = false;
								return;
							}
							else if (possibilities.ElementAt(option - 1) == Concepts.TOK3)
							{
								player.diceNum -= 3;
								player.pointsToAdd += 300;
								Console.WriteLine("3 threes chosen\n");
								choosingDice = false;
								return;
							}
							else if (possibilities.ElementAt(option - 1) == Concepts.TOK4)
							{
								player.diceNum -= 3;
								player.pointsToAdd += 400;
								Console.WriteLine("3 fours chosen\n");
								choosingDice = false;
								return;
							}
							else if (possibilities.ElementAt(option - 1) == Concepts.TOK5)
							{
								player.diceNum -= 3;
								player.pointsToAdd += 500;
								Console.WriteLine("3 fives chosen\n");
								choosingDice = false;
								return;
							}
							else if (possibilities.ElementAt(option - 1) == Concepts.TOK6)
							{
								player.diceNum -= 3;
								player.pointsToAdd += 600;
								Console.WriteLine("3 sixes chosen\n");
								choosingDice = false;
								return;
							}
							else if (possibilities.ElementAt(option - 1) == Concepts.FROK)
							{
								player.diceNum -= 4;
								player.pointsToAdd += 1000;
								Console.WriteLine("Four of a kind chosen\n");
								choosingDice = false;
								return;
							}
							else if (possibilities.ElementAt(option - 1) == Concepts.FVOK)
							{
								player.diceNum -= 5;
								player.pointsToAdd += 3000;
								Console.WriteLine("Five of a kind chosen\n");
								choosingDice = false;
								return;
							}
							else if (possibilities.ElementAt(option - 1) == Concepts.SOK)
							{
								player.diceNum -= 6;
								player.pointsToAdd += 6000;
								Console.WriteLine("Six of a kind chosen\n");
								choosingDice = false;
								return;
							}
						}
					}
					catch
					{
						Console.WriteLine("Input must be a number \n");
					}
                }
			}
		}

		public void DisplayDiceAgain()
		{
			for (int i = 0; i < dice.Count; i++)
			{
				if (i == 0) Console.WriteLine("Dice rolled: ");
				Console.Write(dice.ElementAt(i) + " ");
				if (i == dice.Count - 1) Console.WriteLine('\n');
			}
		}


        public void TryRoll(int diceNum)
		{
			Random r = new Random();
			int roll;
			for (int i = 0; i < diceNum; i++)
			{
				roll = r.Next(1, 6);
				dice.Append(roll);
			}
		}

		private int CalculateScore()
		{
			while (!DiceFlood(dice))
			{
				//To Open

				SmoothWater();
				RidinRapids();
				Straight(selectedDice);
				ScoreUnusedDice();
			}

			return 0;
		}

		public void SmoothWater()
		{
			if (selectedDice != null)
			{
				ScoreThreeOfAKind(selectedDice, 1, 500); //Three Ones
				ScoreThreeOfAKind(selectedDice, 2, 200); //Three Twos
				ScoreThreeOfAKind(selectedDice, 3, 300); //Three Threes
				ScoreThreeOfAKind(selectedDice, 4, 400); //Three Fours
				ScoreThreeOfAKind(selectedDice, 5, 500); //Three Fives
				ScoreThreeOfAKind(selectedDice, 6, 500); //Three Sixes
			}
		}
		public void RidinRapids()
		{
			if (selectedDice != null)
			{
				for (int target = 1; target <= 6; target++)
				{
					// Call MultipleValue for each target number

					bool threeRepeats = MultipleValue(selectedDice, target, 0, 0, 3);
					bool fourRepeats = MultipleValue(selectedDice, target, 0, 0, 4);
					bool fiveRepeats = MultipleValue(selectedDice, target, 0, 0, 5);
					bool sixRepeats = MultipleValue(selectedDice, target, 0, 0, 6);

					// Award points based on the highest repetition achieved
					if (sixRepeats) pointsScored += 6000;
					else if (fiveRepeats) pointsScored += 3000;
					else if (fourRepeats) pointsScored += 1000;
					else if (threeRepeats) pointsScored += 0;
				}
			}
		}
		private void ScoreUnusedDice()
		{
			if (selectedDice != null)
			{
				// Create a set of used dice based on scoring functions
				HashSet<int> usedDice = new HashSet<int>();

				// Include dice used in SmoothWater (Three of a Kind)
				for (int target = 1; target <= 6; target++)
				{
					if (MultipleValue(selectedDice, target, 0, 0, 3))
					{
						usedDice.Add(target);
					}
				}

				// Include dice used in RidinRapids (Four, Five, or Six of a Kind)
				for (int target = 1; target <= 6; target++)
				{
					if (MultipleValue(selectedDice, target, 0, 0, 4) ||
						MultipleValue(selectedDice, target, 0, 0, 5) ||
						MultipleValue(selectedDice, target, 0, 0, 6))
					{
						usedDice.Add(target);
					}
				}

				// Include dice used in Straight
				int[] straight = { 1, 2, 3, 4, 5, 6 };
				if (straight.All(selectedDice.Distinct().OrderBy(x => x).Contains))
				{
					foreach (int num in straight)
					{
						usedDice.Add(num);
					}
				}

				// Calculate points for unused dice
				foreach (int die in selectedDice)
				{
					if (!usedDice.Contains(die))
					{
						pointsScored += 100; // Award 100 points for each unused die
						Console.WriteLine($"Unused die {die}: +100 points.");
					}
				}
			}
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
		public bool DiceFlood(List<int> dice)
		{
			int floodCount = 0;
			foreach (int value in dice)
			{
				if (value == 2) floodCount++;
			}
			if (floodCount == 4) return true;
			else return false;
		}
		private bool MultipleValue(List<int> dice, int target, int index, int count, int repNum)
		{
			// Base case: If we reach the end of the array, check if the count is equal to or over repetitive num
			if (index >= dice.Count)
			{
				return count == repNum;
			}
			// Increment count if the current element matches the target number
			if (dice[index] == target)
			{
				count++;
			}
			// Recurse to the next index
			return MultipleValue(dice, target, index + 1, count, repNum);
		}
		private int ScoreThreeOfAKind(List<int> selectedDice, int targetNumber, int score)
		{
			if (MultipleValue(selectedDice, targetNumber, 0, 0, 3))
			{
				return pointsScored += score;
			}
			return 0;
		}
		private bool Straight(List<int> selectedDice)
		{
			// Define the straight sequence we are looking for
			int[] straight = { 1, 2, 3, 4, 5, 6 };

			// Get distinct numbers and sort them
			int[] sortedUnique = selectedDice.Distinct().OrderBy(x => x).ToArray();

			// Check if the straight sequence exists in the sorted unique numbers
			if (straight.All(sortedUnique.Contains)) return true;
			else return false;
		}

	}
}
