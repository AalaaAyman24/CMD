using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_Project
{
    internal class Command
    {
        public static void DisplayAllCommandsHelp()
        {
            Console.WriteLine("Available commands:\n");
            Console.WriteLine("cd             Displays the name of or changes the current directory.\n");
            Console.WriteLine("cls            Clear the console screen.\n");
            Console.WriteLine("dir            Displays a list of files and subdirectories in a directory.\n");
            Console.WriteLine("exit           Quits the CMD.EXE program (command interpreter).\n");
            Console.WriteLine("copy           Copies one or more files to another location.\n");
            Console.WriteLine("del            Deletes one or more files.\n");
            Console.WriteLine("help           Display help for all commands or a specific command\n");
            Console.WriteLine("md             Creates a directory.\n");
            Console.WriteLine("rd             Removes a directory.\n");
            Console.WriteLine("rename         Renames a file or files.\n");
            Console.WriteLine("type           Displays the contents of a text file.\n");
            Console.WriteLine("import         Import text file(s) from your computer.\n");
            Console.WriteLine("export         Export text file(s) to your computer.\n");
        }

        public static void DisplayCommandHelp(string specificCommand)
        {
            Dictionary<string, string> descriptions = new Dictionary<string, string> {
            { "cd", "Displays the name of or changes the current directory" },
            { "cls", "Clear the console screen" },
            { "dir", "Displays a list of files and subdirectories in a directory." },
            { "exit", "Quits the CMD.EXE program (command interpreter)." },
            { "copy", "Copies one or more files to another location." },
            { "del", "Deletes one or more files." },
            { "help", "Display help for all commands or a specific command" },
            { "md", "Creates a directory." },
            { "rd", "Removes a directory." },
            { "rename", "Renames a file or files." },
            { "type", "Displays the contents of a text file." },
            { "import", "Import text file(s) from your computer." },
            { "export", "Export text file(s) to your computer." },  };

            if (descriptions.ContainsKey(specificCommand))
            {
                Console.WriteLine($"{specificCommand}             {descriptions[specificCommand]}");
            }
            else
            {
                Console.WriteLine($"Command '{specificCommand}' is not supported by the help utility.");
            }
        }

        public static void Cls()
        {
            Console.Clear();
        }

        public static void Exit()
        {
            Environment.Exit(0);
        }

        public static void Make_Directory(string name)
        {
            int index = Program.currentDirectory.Search(name);
            if (index != -1)
            {
                Console.WriteLine("Error: Directory with the same name already exists.");
            }
            else
            {
                Directory_Entry newDir = new Directory_Entry(name,1,0,0);
                Program.currentDirectory.directoryTable.Add(newDir);
                //Directory newDir = new Directory(name, currentDirectory);
                Program.currentDirectory.Write_Directory();
                Console.WriteLine("Directory created successfully.");
            }
        }



        public static void Remove_Directory(string name)
        {
            int index = Program.currentDirectory.Search(name);
            if (index == -1)
            {
                Console.WriteLine("Error: Directory not found.");
            }
            else
            {
                Directory_Entry entry = Program.currentDirectory.directoryTable[index];
                if (entry.attribute == 1)
                {

                    Directory newDir = new Directory(name, 1, 0,entry.first_cluster, Program.currentDirectory);
                    newDir.Delete();
                    Program.currentDirectory.Write_Directory();
                    Console.WriteLine("Directory deleted successfully.");
                }
                else
                {
                    Console.WriteLine("Error: The specified name is not a directory.");
                }
            }
        }


        public static void Display_Directory()
        {
            Console.WriteLine("Directory Listing:");
            foreach (Directory_Entry entry in Program.currentDirectory.directoryTable)
            {
                Console.WriteLine(entry.name);
            }
        }

    }
}
