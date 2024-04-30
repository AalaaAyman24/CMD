using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_Project
{
    internal class Directory_Entry
    {
        public char[] name = new char[11]; // 11 byte public byte attribute 
        public byte attribute; // 1 byte
        public byte[] empty = new byte[12]; // 12 byte
        public int size; // 4 byte
        public int first_cluster; // 4 byte
        // Total 32 byte

        public Directory_Entry()  // Empty constructor
        {

        }
        public Directory_Entry(string n, byte attr, int sz, int fc)
        {
            attribute = attr;
            if (attribute == 0)  // File --> name + extension 
            {
                if (n.Length > 11)
                {
                    name = (n.Substring(7) + n.Substring(n.Length - 4)).ToCharArray();
                }
                else
                {
                    name = n.ToCharArray();
                }
            }
            else  //folder
            {
                name = n.Substring(0, Math.Min(11, n.Length)).ToCharArray();
            }

            size = sz;
            if (fc == 0)
            {
                first_cluster = Fat_Table.Get_Available_Block();
            }
            else
            {
                first_cluster = fc;  //Folder root
            }
        }

        public byte[] Convert_Directory_Entry()
        {
            byte[] data = new byte[32]; // Directory Entry is 32 byte 
            for (int i = 0; i < name.Length; i++)
            {
                data[i] = Convert.ToByte(name[i]);  //0:10
            }
            data[11] = attribute; //11 
            for (int i = 0; i < 12; i++)
            {
                data[i + 12] = empty[i]; //12:23

            }

            byte[] temp = new byte[4];  // int = 4 byte
            temp = BitConverter.GetBytes(size);
            for (int i = 0; i < 4; i++)
            {
                data[i + 24] = temp[i]; //24:27

            }

            temp = BitConverter.GetBytes(first_cluster);
            for (int i = 0; i < 4; i++)
            {
                data[i + 28] = temp[i];//27:31
            }

            return data;
        }

        public Directory_Entry Get_Directory_Entry(byte[] data)
        {
            Directory_Entry de = new Directory_Entry();
            for (int i = 0; i < 11; i++)
            {
                de.name[i] = Convert.ToChar(data[i]);
            }
            de.attribute = data[11];
            for (int i = 0; i < 12; i++)
            {
                de.empty[i] = data[i + 12];

            }
            byte[] temp = new byte[4];
            for (int i = 0; i < 4; i++)
            {
                temp[i] = data[i + 24];

            }
            de.size = BitConverter.ToInt32(temp, 0);
            for (int i = 0; i < 4; i++)
            {
                temp[i] = data[i + 28];
            }
            de.first_cluster = BitConverter.ToInt32(temp, 0);
            return de;
        }


    }


}
