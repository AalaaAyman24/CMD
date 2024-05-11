using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Threading.Tasks;

namespace OS_Project
{
    internal class File_Entry : Directory_Entry
    {
        public string content;
        public Directory parent;


        public File_Entry(string n, byte attr, int sz, int fc, string ct = "", Directory pt = null) : base(n, attr, sz, fc)
        {
            parent = pt;
            content = ct;
        }

        public void Write_File()
        {

            int contentLength = content.Length;
            int totalBlocks = (int)Math.Ceiling(content.Length / 1024.0);
            int fullBlocks = content.Length / 1024;
            int remainder = content.Length % 1024;


            int fc;
            if (first_cluster != 0)
                fc = first_cluster;
            else
            {
                fc = Fat_Table.Get_Available_Block();
                first_cluster = fc;
            }

            int lc = -1;

            for (int i = 0; i < totalBlocks; i++)
            {
                byte[] blockData = new byte[1024];

                if (i < fullBlocks)
                {
                   
                    for (int j = 0; j < 1024; j++)
                    {
                        blockData[j] = (byte)content[i * 1024 + j];
                    }
                }
                else
                {
                    
                    int start = 1024 * fullBlocks;
                    for (int j = 0; j < 1024; j++)
                    {
                        if (j < remainder)
                        {
                            blockData[j] = (byte)content[start + j];
                        }
                        else
                        {
                            blockData[j] = (byte)'#'; 
                        }
                    }
                }
                Virtual_Disk.Write_Block(blockData, fc);
                Fat_Table.Set_Value(-1, fc);
                if (lc != -1)
                {
                    Fat_Table.Set_Value(fc, lc);
                }
                lc = fc;
                fc = Fat_Table.Get_Available_Block();
                //Console.WriteLine(Fat_Table.Get_Value(5));
            }
        
        // Write the new fat table
        Fat_Table.Write_Fat_Table();
           
    }


        public void Read_File()
        {

            List<byte> data = new List<byte>();
            int fc = first_cluster;
            int nc = Fat_Table.Get_Value(fc);
            data.AddRange(Virtual_Disk.Read_Block(fc));

            while (nc != -1)
            {
                fc = nc;
                if (first_cluster != -1)
                {
                    data.AddRange(Virtual_Disk.Read_Block(fc));
                    nc = Fat_Table.Get_Value(fc);
                }
            }
            

            for (int i = 0; i < data.Count; i++)
            {
                if (data[i] == (byte)'#')
                {

                    break;
                }

                content += (char)data[i];

            }
        }

        public void Delete_File(string name)
        {
            if (first_cluster != 0)
            {
                int fc = first_cluster;
                int next = Fat_Table.Get_Value(fc);

                do
                {
                    Fat_Table.Set_Value(0, fc);
                    fc = next;
                    if (fc != -1)
                        next = Fat_Table.Get_Value(fc);

                } while (fc != -1);

                Fat_Table.Write_Fat_Table();
            }

            int y = parent.Search(name);
            parent.directoryTable.RemoveAt(y);
            parent.Write_Directory();


        }

    }
}