using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;
using System.Data;
using System.Drawing;
using Console = Colorful.Console;
using System.Globalization;
using Timer = System.Threading.Timer;
using System.Threading;
using System.IO;
using Tobii.Interaction;
using Tobii.Interaction.Framework;


namespace TobiiEyeTracker4CDataStream
{
	class Program
	{
		private const string Format = "{0}\t{1}\t{2}";

		static void Main(string[] args)
		{
			
			Console.WriteLine("Minsis's STREAM.0.0.1VER",Color.HotPink);

			List<char> chars = new List<char>()
					{
						'S','U','C','C','E','S','S',':',
						' ','T','h','e',' ','c','o','n','n','e','c','t','i','o','n',' ','t','o',' ','t','h','e',' ',
						'M','i','n','s','i','s',' ','E','Y','E','_','T','E','N',' ','h','a','s',' ','b','e','e','n',' ',
						'c','o','r','r','e','c','t','l','y',' ','s','e','t',' ','u','p'
					};
			Console.WriteWithGradient(chars, Color.Yellow, Color.Fuchsia, 14);

			Console.WriteLine("\n");
			Console.WriteLine("==========================================================================");
			for (int i = 0; i < 3; i++)
			{
				int DA = 244;
				int V = 212;
				int ID = 255;
				Console.WriteAscii("TOBII EYE GAZE DATA", Color.FromArgb(DA, V, ID));

				DA -= 18;
				V -= 36;
			}
			Console.WriteLine("==========================================================================");

			//▼▽▼▽▼▽▼▽▼▽▼▽▼▽▼▽▼▽▼▽▼▽▼▽▼▽▼▽▼▽▼▽▼▽▼▽▼▽▼▽
			//▼▽▼▽▼▽▼▽▼▽▼▽▼▽▼▽▼▽▼▽▼▽▼▽▼▽▼▽▼▽▼▽▼▽▼▽▼▽▼▽
			//▼▽▼▽▼▽▼▽▼▽▼▽▼▽▼▽▼▽▼▽▼▽▼▽▼▽▼▽▼▽▼▽▼▽▼▽▼▽▼▽

			var host = new Host();

			var gazePointDataStream = host.Streams.CreateGazePointDataStream();

			Console.WriteLine("YOUR EYE CONNNECTED THIS MACHINE");
			Console.WriteLine("\n");
			Console.WriteLine("{0}     {1}     {2}","X GAZE","Y GAZE", "TIME");

			Console.WriteLine("Drag your *txt file!!!");
			var file = Console.ReadLine();
			FileStream fs = new FileStream(@file, FileMode.Append);
			StreamWriter sw = new StreamWriter(fs);

			
			gazePointDataStream.GazePoint((x, y, ts) =>
			{
				string time = DateTime.Now.ToString("hh:mm:ss");
				string point = string.Format(Format,(int)x,(int)y,time);
				sw.WriteLine(point);
				Console.WriteLine(point);
			});


			Console.ReadKey();
			if(Console.ReadKey == ConsoleKey.Execute())
				host.DisableConnection();
			sw.Flush();
			sw.Close();
			fs.Close();
		}
	}
}
