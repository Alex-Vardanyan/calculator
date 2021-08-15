using System;
using System.Collections.Generic;

namespace Calculator
{
	class Program
	{
		static void Main(string[] args)
		{

			Calculator();
		}

		static bool InputCheck(string expression) //in case of Input has unralated characters in it
		{
			string allowedCharacters = "0123456789*/+-().";
			for (int i = 0; i < expression.Length; i++)
			{
				if (allowedCharacters.Contains(expression[i]) == false)
				{
					return false;
				}
			}
			return true;
		}

		static bool BracketsCheck(string expression)
		{
			int numOfBrackets = 0;
			for (int i = 0; i < expression.Length; i++) //check for the balanced brackets
			{

				if (expression[i] == '(')
				{
					numOfBrackets += 1;
				}
				else if (expression[i] == ')')
				{
					numOfBrackets -= 1;
					if (numOfBrackets < 0) //in case of closing brackets appear before opening ones, for example ))) ... (((
					{
						break;
					}
				}
			}

			if (numOfBrackets != 0) //unbalaced brackets
			{
				return false;
			}
			return true;
		}

		static int Calculator()
		{
			Console.WriteLine("Welcome To Calculator!!!"); //just some intro
			Console.Write("Please Input Some Expression And Press Enter. You can use ");
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine(" *  /  +  -  (  ) and even spaces!");
			Console.ResetColor();
			Console.Write("For Example (7690+402)*30/(-5): ");

			string expression = Console.ReadLine(); // get the math expression from user

			List<string> elements = new List<string>();
			string operators = "+-/*()";
			int j = 0;
			int maxi;
			string checkAnother = "";

			expression = expression.Replace(" ", ""); //remove all unnecessary spaces

			//Console.WriteLine(expression); //to view the clear expression (without spaces)

			if (InputCheck(expression) == false)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Please input some other expression, use numbers, +, -, *, /, and ()");
				Console.ResetColor();

				while (checkAnother != "n" && checkAnother != "y") // try again?
				{
					Console.ForegroundColor = ConsoleColor.DarkYellow;
					Console.Write("Do you want to input another expression? [y/n]: ");
					checkAnother = Console.ReadLine();
					Console.ResetColor();
					if (checkAnother != "n" && checkAnother != "y")
					{
						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine("please input 'y' for yes or 'n' for no!");
						Console.ResetColor();
					}
				}
				if (checkAnother == "y")
				{
					Calculator();
				}
				Console.ReadLine();


				return -1;
			}

			if (BracketsCheck(expression) == false)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Please check if the brackets are balanced!");
				Console.ResetColor();

				while (checkAnother != "n" && checkAnother != "y") // try again?
				{
					Console.ForegroundColor = ConsoleColor.DarkYellow;
					Console.Write("Do you want to input another expression? [y/n]: ");
					checkAnother = Console.ReadLine();
					Console.ResetColor();
					if (checkAnother != "n" && checkAnother != "y")
					{
						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine("Please input 'y' for yes or 'n' for no!");
						Console.ResetColor();
					}
				}
				if (checkAnother == "y")
				{
					Calculator();
				}
				Console.ReadLine();

				return -1;
			}

			for (int i = 0; i < expression.Length; i++) //after checking if everything is correct, it's time to parse the input
			{
				if (operators.Contains(expression[i]))
				{
					if (expression.Substring(j, i - j) != "")
					{
						elements.Add(expression.Substring(j, i - j));
					}
					elements.Add(expression[i].ToString());
					j = i + 1;
				}

				else if (i == expression.Length - 1)
				{
					elements.Add(expression.Substring(j, i - j + 1));
				}

			}
			maxi = elements.Count;

			/*foreach (string item in elements)				//to view all items in elements, just in case!
			{
				Console.WriteLine(item);
			}*/

			for (int i = elements.Count - 1; i >= 0; i--) //first of all expressions in brackets
			{
				if (elements[i].Contains("(") == true)      //finding the first ( by reading the expression from right to left
				{
					int k = i;
					while (elements[k].Contains(")") == false) // finding the corresponding ")"
					{
						k++;
					}
					if (k - i == 3) //in case of (-n), where n is some number
					{
						elements[i + 1] = (-(float.Parse(elements[i + 2]))).ToString();
						for (int m = i + 2; m + 1 < elements.Count; m++)
						{
							elements[m] = elements[m + 1];
						}
					}

					else if (k - i >= 4)
					{
						for (int n = i + 1; n < k; n++)
						{
							if (elements[n] == "*")
							{
								float value = float.Parse(elements[n - 1]) * float.Parse(elements[n + 1]);
								elements[n - 1] = value.ToString();
								int m = n;
								while (m + 2 < elements.Count)
								{
									elements[m] = elements[m + 2];
									m += 1;
								}
								n -= 1;
								k -= 2;
								maxi -= 2;
							}

							if (elements[n] == "/")
							{
								if (float.Parse(elements[n + 1]) == 0f)
								{
									Console.ForegroundColor = ConsoleColor.Red;
									Console.WriteLine("You can't devide by zero. Please try another expression!");
									Console.ResetColor();

									while (checkAnother != "n" && checkAnother != "y") // try again?
									{
										Console.ForegroundColor = ConsoleColor.DarkYellow;
										Console.Write("Do you want to input another expression? [y/n]: ");
										checkAnother = Console.ReadLine();
										Console.ResetColor();
										if (checkAnother != "n" && checkAnother != "y")
										{
											Console.ForegroundColor = ConsoleColor.Red;
											Console.WriteLine("please input 'y' for yes or 'n' for no!");
											Console.ResetColor();
										}
									}
									if (checkAnother == "y")
									{
										Calculator();
									}
									Console.ReadLine();

									return -1;
								}
								float value = float.Parse(elements[n - 1]) / float.Parse(elements[n + 1]);
								elements[n - 1] = value.ToString();
								int m = n;
								while (m + 2 < elements.Count)
								{
									elements[m] = elements[m + 2];
									m += 1;
								}
								n -= 1;
								k -= 2;
								maxi -= 2;
							}

						}

						for (int n = i + 1; n < k; n++)
						{
							if (elements[n] == "+")
							{
								float value = float.Parse(elements[n - 1]) + float.Parse(elements[n + 1]);
								elements[n - 1] = value.ToString();
								int m = n;
								while (m + 2 < elements.Count)
								{
									elements[m] = elements[m + 2];
									m += 1;
								}
								n -= 1;
								k -= 2;
								maxi -= 2;
							}

							if (elements[n] == "-")
							{
								float value = float.Parse(elements[n - 1]) - float.Parse(elements[n + 1]);
								elements[n - 1] = value.ToString();
								int m = n;
								while (m + 2 < elements.Count)
								{
									elements[m] = elements[m + 2];
									m += 1;
								}
								n -= 1;
								k -= 2;
								maxi -= 2;
							}
						}

					}

					for (int m = i; m + 1 < elements.Count; m++) // removing all brackets
					{
						elements[m] = elements[m + 1];
					}
					for (int m = i + 1; m + 1 < elements.Count; m++)
					{
						elements[m] = elements[m + 1];
					}
					maxi -= 2;
				}
			}

			/*foreach (string item in elements)               //to view all items in elements, just in case!
			{
				Console.WriteLine(item);
			}*/

			//after dealing with all brackets
			for (int i = 0; i < maxi; i++)
			{
				if (elements[i] == "*")
				{
					float value = float.Parse(elements[i - 1]) * float.Parse(elements[i + 1]);
					elements[i - 1] = value.ToString();
					int n = i;
					while (n + 2 < elements.Count)
					{
						elements[n] = elements[n + 2];
						n += 1;
					}
					i -= 1;
					maxi -= 2;
				}



				if (elements[i] == "/")
				{
					if (float.Parse(elements[i + 1]) == 0f)
					{
						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine("You can't devide by zero. Please try another expression!");
						Console.ResetColor();

						while (checkAnother != "n" && checkAnother != "y") // try again?
						{
							Console.ForegroundColor = ConsoleColor.DarkYellow;
							Console.Write("Do you want to input another expression? [y/n]: ");
							checkAnother = Console.ReadLine();
							Console.ResetColor();
							if (checkAnother != "n" && checkAnother != "y")
							{
								Console.ForegroundColor = ConsoleColor.Red;
								Console.WriteLine("please input 'y' for yes or 'n' for no!");
								Console.ResetColor();
							}
						}
						if (checkAnother == "y")
						{
							Calculator();
						}
						Console.ReadLine();

						return -1;
					}
					float value = float.Parse(elements[i - 1]) / float.Parse(elements[i + 1]);
					elements[i - 1] = value.ToString();
					int n = i;
					while (n + 2 < elements.Count)
					{
						elements[n] = elements[n + 2];
						n += 1;
					}
					i -= 1;
					maxi -= 2;
				}
			}

			for (int i = 0; i < maxi; i++)
			{

				if (elements[i] == "+")
				{
					float value = float.Parse(elements[i - 1]) + float.Parse(elements[i + 1]);
					elements[i - 1] = value.ToString();
					int n = i;
					while (n + 2 < elements.Count)
					{
						elements[n] = elements[n + 2];
						n += 1;
					}
					i -= 1;
					maxi -= 2;
				}

				if (elements[i] == "-")
				{
					float value = float.Parse(elements[i - 1]) - float.Parse(elements[i + 1]);
					elements[i - 1] = value.ToString();
					int n = i;
					while (n + 2 < elements.Count)
					{
						elements[n] = elements[n + 2];
						n += 1;
					}
					i -= 1;
					maxi -= 2;
				}
			}


			Console.ForegroundColor = ConsoleColor.Green; // answer time
			Console.WriteLine("The answer is: {0}", elements[0]);
			Console.ResetColor();


			while (checkAnother != "n" && checkAnother != "y") // try again?
			{
				Console.ForegroundColor = ConsoleColor.DarkYellow;
				Console.Write("Do you want to input another expression? [y/n]: ");
				checkAnother = Console.ReadLine();
				Console.ResetColor();
				if (checkAnother != "n" && checkAnother != "y")
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("please input 'y' for yes or 'n' for no!");
					Console.ResetColor();
				}
			}
			if (checkAnother == "y")
			{
				Calculator();
			}
			Console.ReadLine();
			return 0;
		}
	}
}
