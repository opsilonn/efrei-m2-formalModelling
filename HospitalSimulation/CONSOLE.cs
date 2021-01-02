using System;


namespace HospitalSimulation
{
	public static class CONSOLE
	{
		/// <summary>
		/// The program waits for the user to press any key
		/// </summary>
		public static void WaitForInput()
		{
			Console.WriteLine("\n\n\n       Press any key to continue...");
			Console.ReadKey();
		}



		/// <summary> Displays a message in a given color </summary>
		/// <param name="color"> color of the message </param>
		/// <param name="message"> message to be displayed </param>
		public static void Write(ConsoleColor color, string message)
		{
			Console.ForegroundColor = color;
			Console.Write(message);
			Console.ResetColor();
		}


		/// <summary> Displays a message in a given color, then adds a backslash </summary>
		/// <param name="color"> color of the message </param>
		/// <param name="message"> message to be displayed </param>
		public static void WriteLine(ConsoleColor color, string message)
		{
			Console.ForegroundColor = color;
			Console.WriteLine(message);
			Console.ResetColor();
		}
	}
}