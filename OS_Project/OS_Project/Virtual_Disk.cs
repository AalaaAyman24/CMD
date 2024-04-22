using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;

namespace OS_Project
{
    internal class Virtual_Disk
    {
        public const string FileName = "Disk.txt";
        public const int BlockSize = 1024;
        public const int TotalBlocks = 1024 * 1024; //1MB virtual disk
        private const byte SuperBlock = (byte)'0';
        private const byte FatTable = (byte)'*';
        private const byte FreeBlock = (byte)'#';

        public static void Initialize()
        {

            Directory Root = new Directory("root",1, 0, 5, null);
            Program.currentDirectory = Root;
            Program.path = new string(Root.name);
            if (!File.Exists(FileName))
            {
                using (FileStream disk = new FileStream(FileName, FileMode.Create, FileAccess.ReadWrite))
                {
                    // Write super block
                    for (int i = 0; i < BlockSize; i++)
                    {
                        disk.WriteByte(SuperBlock);
                    }
                    // Write fat table
                   
                    for (int j = 0; j < BlockSize * 4; j++)
                    {
                        disk.WriteByte(FatTable);
                    }

                    // Write free blocks
                    for (int i = 0; i < (BlockSize - 5) * BlockSize; i++)
                    {
                      
                        disk.WriteByte(FreeBlock);
                        
                    }
                }

                // initialize fat table
                Fat_Table.Initialize();

                Root.Write_Directory();
                // write fat table
                Fat_Table.Write_Fat_Table();
            }
            else
            {
                // if file exists, read fat table
                Fat_Table.Read_Fat_Table();
                Root.Read_Directory();
            }
        }

        public static void Write_Block(byte[] data, int index)
        {
            using (FileStream disk = new FileStream(FileName, FileMode.Open, FileAccess.ReadWrite))
            {
                disk.Write(data, 0, data.Length);  // data.length = 1024
            }
        }

        public static byte[] Read_Block(int index)
        {
            byte[] data = new byte[BlockSize];
            using (FileStream disk = new FileStream(FileName, FileMode.Open, FileAccess.ReadWrite))
            {
                disk.Read(data, 0, data.Length);  // data.length = 1024
            }
            return data;
        }
    }
}
