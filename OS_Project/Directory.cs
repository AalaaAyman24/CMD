using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_Project
{
    internal class Directory : Directory_Entry
    {
        public List<Directory_Entry> directoryTable = new List<Directory_Entry>();
        public Directory parent;

 
        public Directory() : base()
        {
            
        }

        public Directory(string n,byte bt, int sz, int fc, Directory pt) : base(n, bt, sz, fc)
        {

            parent = pt;
        }

        public void Write_Directory()
        {
            byte[] directory_table = new byte[32 * directoryTable.Count];
            byte[] directory_entry = new byte[32];


            for (int i = 0; i < directoryTable.Count; i++)
            {
                directory_entry = directoryTable[i].Convert_Directory_Entry();
                for (int j = i * 32; j < (i + 1) * 32; j++)
                {
                    directory_table[j] = directory_entry[j % 32];
                }
            }

            // int totalBytes = 32 * directoryTable.Count;
            int totalBlocks = (int)Math.Ceiling(directory_table.Length / 1024.0);
            int fullBlocks = directory_table.Length / 1024;
            int remainder = directory_table.Length % 1024;

            int fc = first_cluster;
            int lc = -1;

            for (int i = 0; i < totalBlocks; i++)
            {
                byte[] blockData = new byte[1024];
                if (i < fullBlocks)
                {
                    for (int j = 0; j < 1024; j++)
                    {
                        blockData[j] = directory_table[i * 1024 + j];
                    }
                }
                else
                {
                    int indx = 1024 * fullBlocks;
                    for (int j = 0; j < 1024; j++)
                    {
                        if (j < remainder)
                        {
                            blockData[j] = directory_table[indx + j];
                        }
                        else
                        {
                            blockData[j] = (byte)'#';
                        }

                    }
                }
                Virtual_Disk.Write_Block(blockData, fc);
                Fat_Table.Set_Value(-1, fc);
                if(lc != -1)
                {
                    Fat_Table.Set_Value(fc, lc);
                }
                lc = fc;
                fc = Fat_Table.Get_Available_Block();
                Console.WriteLine(Fat_Table.Get_Value(5));

            }

            // Write the new fat table
            Fat_Table.Write_Fat_Table();
        }

        public void Read_Directory()
        {
            Console.WriteLine(Fat_Table.Get_Value(5));
            List<byte> data = new List<byte>();
            List<Directory_Entry> directory_table = new List<Directory_Entry>();
            int fc = first_cluster;
            int nc = Fat_Table.Get_Value(fc);
            data.AddRange(Virtual_Disk.Read_Block(fc));

            while(nc != -1)
            {
                fc = nc;
                if (first_cluster != -1)
                {
                    data.AddRange(Virtual_Disk.Read_Block(fc));
                    nc = Fat_Table.Get_Value(fc);
                }
            }

            bool flag = false;
            for (int i = 0; i < data.Count / 32; i++)
            {
                byte[] temp = new byte[32];
                for (int j = 0; j < 32; j++)
                {
                    if (data[i * 32 + j] == (byte)'#')
                    {
                        flag = true;
                        break;
                    }
                    temp[j] = data[i * 32 + j];             
                }
                if(flag)
                {
                    break;
                }
                directory_table.Add(Get_Directory_Entry(temp));
            }
            directoryTable = directory_table;
        }
        
        public int Search (string n)
        {
            string s;
            for (int i = 0; i < directoryTable.Count; i++) 
            {
                s = new string(directoryTable[i].name).TrimEnd('\0');
                if(s == n.TrimEnd('\0'))
                {
                    return i;
                }
            }
            return -1;
        }
        
        public void Delete()
        {
            // Not complete
        }



    }
}
