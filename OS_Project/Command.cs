using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;

namespace OS_Project
{
    internal class Command
    {
        public static void DisplayAllCommandsHelp()  //help
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



        public static void Cls() //cls
        {
            Console.Clear();
        }



        public static void Exit() //exit
        {
            Environment.Exit(0);
        }



        public static void Make_Directory(string name)  //md
        {
            int index = Program.currentDirectory.Search(name);
            if (index != -1)
            {
                Console.WriteLine("Error: Directory with the same name already exists.");
            }
            else
            {
                Directory_Entry newDir = new Directory_Entry(name, 1, 0, 0);
                Program.currentDirectory.directoryTable.Add(newDir);
                Program.currentDirectory.Write_Directory();

                if (Program.currentDirectory.parent != null)
                {
                    Program.currentDirectory.parent.Update_Content(Program.currentDirectory.Get_Directory_Entry());
                }

                Console.WriteLine("Directory created successfully.");
            }
        }



        public static void Remove_Directory(string name)  // rd
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

                    Directory newDir = new Directory(name, 1, 0, entry.first_cluster, Program.currentDirectory);
                    newDir.Delete_Directory(name);
                    Program.currentDirectory.Write_Directory();
                    Console.WriteLine("Directory deleted successfully.");
                }
                else
                {
                    Console.WriteLine("Error: The specified name is not a directory.");
                }
            }
        }



        public static void Display_Directory()  //dir
        {
            Console.WriteLine("Directory Listing:");
            foreach (Directory_Entry entry in Program.currentDirectory.directoryTable)
            {
                Console.WriteLine(entry.name);
            }
        }



        public static void Rename(string oldName, string newName)  //rename
        {
            int index_oldName = Program.currentDirectory.Search(oldName);
            int index_newName = Program.currentDirectory.Search(newName);
            if (index_oldName != -1)
            {
                if (index_newName == -1)
                {
                    Directory_Entry entry = Program.currentDirectory.directoryTable[index_oldName];

                    // Update the name 
                    entry.name = newName.ToCharArray();
                    Program.currentDirectory.Write_Directory();
                    Console.WriteLine($"Directory '{oldName}' renamed to '{newName}' successfully.");

                }
                else
                {
                    Console.WriteLine($"Error: '{newName}' is the same as the old name.");
                }
            }
            else
            {
                Console.WriteLine("Error: The specified name is not a file.");
            }
        }




        public static void Type(string name) //type
        {
            int index = Program.currentDirectory.Search(name);
            if (index != -1)
            {
                int fc = Program.currentDirectory.directoryTable[index].first_cluster;
                int sz = Program.currentDirectory.directoryTable[index].size;
                File_Entry f = new File_Entry(name, 0, sz, fc, "", Program.currentDirectory);
                f.Read_File();

                Console.WriteLine(f.content);
            }
            else
            {
                Console.WriteLine("Error: The specified name is not a file.");
            }

        }




        public static void Delete_File(string name) //del
        {
            
            int index = Program.currentDirectory.Search(name);
            if (index != -1)
            {
                int fc = Program.currentDirectory.directoryTable[index].first_cluster;
                int sz = Program.currentDirectory.directoryTable[index].size;
                File_Entry f = new File_Entry(name, 0, sz, fc, "", Program.currentDirectory);
                f.Delete_File(name);
                Console.WriteLine("File deleted successfully.");
            }

            else
            {
                Console.WriteLine("Error: The specified name is not a file.");
            }
         

        }



        public static void Change_Directory(string name)  //cd
        {
            if (name == "..")
            {
                if (Program.currentDirectory.parent != null)
                {
                    Program.currentDirectory = Program.currentDirectory.parent;
                    int index_slash = Program.path.LastIndexOf("\\");
                    Program.path = Program.path.Substring(0, index_slash);
                    Console.WriteLine($"Changed to parent directory: {Program.currentDirectory}");
                }
                else
                {
                    Console.WriteLine("Already in the root directory.");
                }
            }
            else
            {
                int index = Program.currentDirectory.Search(name);
                if (index != -1)
                {
                    Directory_Entry entry = Program.currentDirectory.directoryTable[index];
                    Directory newDir = new Directory(name, 1, 0, entry.first_cluster, Program.currentDirectory);
                    Program.currentDirectory = newDir;
                    Program.path += "\\" + name;
                    Program.currentDirectory.Read_Directory();

                }
                else
                {
                    Console.WriteLine($"Error: Directory '{name}' not found.");
                }
            }
        }

        


        public static void Import(string path)  //import 
        {
            Console.WriteLine(path);
            if (System.IO.File.Exists(path))
            {

                string[] pathParts = path.Split('\\'); // Split the path(extract name & size)
                string fileName = pathParts[pathParts.Length - 1];
                string fileContent = System.IO.File.ReadAllText(path);
                int size = fileContent.Length;
                int index = Program.currentDirectory.Search(fileName);
                int fc = 0;

                if (index == -1)
                {
                    File_Entry f = new File_Entry(fileName, 0, size, fc, fileContent, Program.currentDirectory);
                    f.Write_File();
                    Directory_Entry d = new Directory_Entry(fileName, 0, size, f.first_cluster);
                    Program.currentDirectory.directoryTable.Add(d);
                    Program.currentDirectory.Write_Directory();

                    if (Program.currentDirectory.parent != null)
                    {
                        Program.currentDirectory.parent.Update_Content(Program.currentDirectory.Get_Directory_Entry());
                    }
                }
                else
                {
                    Console.WriteLine("Error: File with the same name already exists.");
                }
            }
            else
            {
                Console.WriteLine("Error: The specified name is not a file.");
            }

        }


        public static void  Export(string name, string dest)  //export 
        {
            Console.WriteLine(name);
            Console.WriteLine(dest);

            int index = Program.currentDirectory.Search(name);
            if(index != -1) 
            {

                if (System.IO.Directory.Exists(dest))
                {
                    int fc = Program.currentDirectory.directoryTable[index].first_cluster;
                    int sz = Program.currentDirectory.directoryTable[index].size;
                    File_Entry f = new File_Entry(name, 0, sz, fc, "", Program.currentDirectory);
                    f.Read_File();
                    using (StreamWriter sw = new StreamWriter(dest + '\\' + name))
                    {
                        sw.WriteLine(f.content);
                    }
                }
                else
                {
                    Console.WriteLine("File not found at the specified path.");
                }

            }
            else
            {
                Console.WriteLine("Error: The specified name is not a file.");
            }
        }


        public static void Copy (string src, string dest)  //copy
        {
            string[] pathParts = dest.Split('\\');
            int index_src = Program.currentDirectory.Search(src);
            int index_dest = Program.currentDirectory.Search(pathParts[0]);
            if (index_src == -1)
            {
                Console.WriteLine($"Error: Source file '{src}' not found.");
                return;
            }

            if (index_dest == -1)
            {
                Console.WriteLine($"Error: Destination directory '{dest}' not found.");
                return;
            }
            if (index_src != -1)
            {
                int fc = Program.currentDirectory.directoryTable[index_dest].first_cluster;
                Directory d = new Directory(dest, 1, 0, fc, Program.currentDirectory);
                d.Read_Directory();
                Directory_Entry de = Program.currentDirectory.directoryTable[index_src];
                int indx3 = d.Search(src);
                if (indx3 != -1)
                {
                    Console.WriteLine($"Error: File '{src}' already exists in '{dest}'.");
                    return;
                }
                d.directoryTable.Add(de);
                d.Write_Directory();
                if (d.parent != null)
                {
                    d.parent.Update_Content(d.Get_Directory_Entry());
                }
            }
            else
            {
                Console.WriteLine("Error: Destination directory not found.");
            }
        }

    }
}




