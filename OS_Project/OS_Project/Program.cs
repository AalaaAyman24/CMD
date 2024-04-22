using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace OS_Project
{
    internal class Program
    {
        public static Directory currentDirectory = new Directory();
        public static string path = "";

        public static void Main(string[] args)
        {
            Virtual_Disk.Initialize();

            Console.WriteLine("Welcome to the Command Line Interpreter! ");

            while (true)
            {

                Console.Write(path + "> ");
                string input = Console.ReadLine().ToLower().Trim();
                string[] commandParts = input.Split(' ');
                string command = commandParts.Length > 0 ? commandParts[0] : "";

                switch (command)
                {
                    case "help":
                        if (commandParts.Length > 1)
                        {
                            // Help
                            Command.DisplayCommandHelp(commandParts[1]);
                        }
                        else
                        {
                            // All commands
                            Command.DisplayAllCommandsHelp();
                        }
                        break;

                    case "cls":
                        // cls
                        Command.Cls();
                        break;

                    case "exit":
                    case "quit":
                        //Exit or quit
                         Command.Exit();
                        break;

                    case "md":
                        // Make directory
                        if (commandParts.Length == 2)
                        {
                            string directoryName = commandParts[1];
                            Command.Make_Directory(directoryName);
                        }
                        else
                        {
                            Console.WriteLine("Usage: md <directory_name>");
                        }
                        break;

                    case "rd":
                        // Remove directory
                        if (commandParts.Length == 2)
                        {
                            string directoryName = commandParts[1];
                            Command.Remove_Directory(directoryName);
                        }
                        else
                        {
                            Console.WriteLine("Usage: rd <directory_name>");
                        }
                        break;

                    case "dir":
                        // Display directory
                        Command.Display_Directory();
                        break;


                    default:
                        Console.WriteLine("Invalid command.");
                        break;
                }
            }
        }

    }
}
