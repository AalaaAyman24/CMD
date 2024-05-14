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
        internal static object current;

        public static object DirectoryCurrent { get; internal set; }

        public static void Main(string[] args)
        {
            Virtual_Disk.Initialize();



            // Console.WriteLine("Welcome to the Command Line Interpreter! ");

            while (true)
            {

                Console.Write(path + "\\> ");
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


                    case "rename":
                        // Rename file(s)
                        if (commandParts.Length == 3)
                        {
                            Command.Rename(commandParts[1], commandParts[2]);
                        }
                        else
                        {
                            Console.WriteLine("Usage: rename <oldName> <newName>");
                        }
                        break;


                    case "type":
                        // Display the content
                        if (commandParts.Length == 2)
                        {
                            Command.Type(commandParts[1]);
                        }
                        else
                        {
                            Console.WriteLine("Usage: type <fileName>");
                        }
                        break;


                    case "cd":
                        // Changes the current directory
                        if (commandParts.Length == 1 )
                        {
                            Console.WriteLine(currentDirectory + "\n");  
                        }
                        else if (commandParts.Length == 2)
                        {
                            Command.Change_Directory(commandParts[1]);
                        }
                        break;



                    case "copy":
                        // Copies one or more files to another location
                        if (commandParts.Length == 3)
                        {
                            string src = commandParts[1];
                            string dest = commandParts[2];

                            Command.Copy(src, dest);
                            //Console.WriteLine($"Copied '{src}' to '{dest}'.");
                        }
                        else
                        {
                            Console.WriteLine("Usage: copy <source_file> <destination_file>");
                        }
                        break;

                    case "del":
                        //Deletes one or more files.
                        if (commandParts.Length == 2)
                        {
                            string fileName = commandParts[1];
                            Command.Delete_File(fileName);
                        }
                        else
                        {
                            Console.WriteLine("Usage: del <file_name>");
                        }
                        break;


                    case "import":
                        //Import text file(s)
                        if (commandParts.Length == 2)
                        {
                            Command.Import(commandParts[1]);
                        }
                        else
                        {
                            Console.WriteLine("Usage: type <path>");
                        }
                        break;

                    
                       case "export":
                        //Export text file(s)
                       if (commandParts.Length == 3)
                       {
                            Command.Export(commandParts[1], commandParts[2]);
                       }
                       else
                       {
                           Console.WriteLine("Usage: export <source> <destination>");
                       }
                       break;
                   

                    default:
                        
                        Console.WriteLine("Invalid command.");
                        break;
                }
            }
        }

    }
}
