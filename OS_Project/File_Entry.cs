using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_Project
{
    internal class File_Entry : Directory_Entry
    {
        public List<Directory_Entry> directoryTable = new List<Directory_Entry>();
        public string content;
        public Directory parent;


        public File_Entry(string n, byte attr, int fc, int sz, string ct = "", Directory pt = null) : base(n, attr, fc, sz)
        {
            parent = pt;
            content = ct;
        }

        public void Write_File()
        {

            double totalBlocks = Math.Ceiling(content.Length / 1024.0);
            int fc = first_cluster;
            int lc = -1;


            byte[] blockData = new byte[1024];
            for (int i = 0; i < totalBlocks; i++)
            {
                for (int j = i * 1024, k = 0; k < 1024; j++, k++)
                {
                    if (j < content.Length)
                    {
                        blockData[k] = (byte)content[j];
                    }
                    else
                    {
                        blockData[k] = (byte)'#';
                    }
                }
            }

            Virtual_Disk.Write_Block(blockData, fc);
            Fat_Table.Set_Value(-1, fc);
            if (lc != -1)
            {
                Fat_Table.Set_Value(fc, lc);
            }

            lc = fc;  // Remove 
            fc = Fat_Table.Get_Available_Block(); // Remove

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
                content += (char)data[i];

            }

        }

        public int Search(string n)
        {
            string s;
            for (int i = 0; i < directoryTable.Count; i++)
            {
                s = new string(directoryTable[i].name).TrimEnd('\0');
                if (s == n.TrimEnd('\0'))
                {
                    return i;
                }
            }
            return -1;
        }


        public void Delete_File(string name)
        {
            if (first_cluster != 0)
            {
                int fc = first_cluster;
                int next = Fat_Table.Get_Value(fc);

                do
                {
                    Fat_Table.Set_Value(fc, 0);
                    fc = next;
                    if (fc != -1)
                        next = Fat_Table.Get_Value(fc);

                } while (next != -1);

                Fat_Table.Write_Fat_Table();
            }
            int y = Search(name);
            Program.currentDirectory.directoryTable.RemoveAt(y);
            parent.Write_Directory();

        }

    }
}