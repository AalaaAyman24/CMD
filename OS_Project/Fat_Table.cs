using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace OS_Project
{
    internal class Fat_Table
    {
        private static int[] fatTable = new int[1024]; // Array to store fat table entries

        public static void Initialize()
        {
            // Initialize fat entries 

            fatTable[0] = -1; // super block

            for (int i = 1; i < 4; i++)
            {
                fatTable[i] = i + 1;
            }

            fatTable[4] = -1; //last fat table

            for (int i = 5; i < 1023; i++)
            {
                fatTable[i] = 0;  
            }
        }

        
        public static void Write_Fat_Table()
        {
            byte[] buffer = new byte[Virtual_Disk.BlockSize * 4];   // Convert from int to byte , fat table size = 1024 * 4
            Buffer.BlockCopy(fatTable, 0, buffer, 0,fatTable.Length );

            // Write the fat table(data) into the virtual disk file
            using (FileStream disk = new FileStream(Virtual_Disk.FileName, FileMode.Open, FileAccess.ReadWrite))
            {
                disk.Seek(1024, SeekOrigin.Begin); 
                disk.Write(buffer, 0, buffer.Length); // Write fat table bytes
            }
        }


        
        public static void Read_Fat_Table()
        {
            byte[] buffer = new byte[Virtual_Disk.BlockSize * 4];

            using (FileStream disk = new FileStream(Virtual_Disk.FileName, FileMode.Open,FileAccess.ReadWrite))
            {
                disk.Seek(1024, SeekOrigin.Begin);  // Seek past the super block 

                disk.Read(buffer, 0, buffer.Length);  // Read fat table bytes
            }
            Buffer.BlockCopy(buffer, 0, fatTable, 0, buffer.Length);


        }



        public static void Print_Fat_Table()
        {
            Console.WriteLine("Fat Table: ");
            for (int i = 0; i < fatTable.Length; i++)
            {
                Console.WriteLine($"Block {i}: {fatTable[i]}");
            }
        }


        public static int Get_Available_Block()
        {
            for (int i = 0; i < fatTable.Length; i++)
            {
                if (fatTable[i] == 0)
                {
                    return i; // Return index 
                }
            }
            return -1;  // If no empty blocks
        }


        public static int Get_Value(int index)
        {
            
            
                return fatTable[index];
       
        }


        public static void Set_Value(int value, int index)
        {
            
                fatTable[index] = value;
            
        }

        public static int Get_Number_Of_Free_Blocks()
        {
            int count = 0;
            foreach (int f in fatTable)
            {
                if (f == 0)
                {
                    count++;
                }
            }
            return count;
        }

        public static int Get_Free_Space()
        {
            return Get_Number_Of_Free_Blocks() * 1024;
        }

    }
}
