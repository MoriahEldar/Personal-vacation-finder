using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BE
{
    [Serializable]
    public class HostingUnit
    {
        public long HostingUnitKey { get; set; }
        public string OwnerId { get; set; }
        public string HostingUnitName { get; set; }
        public string Description { get; set; }
        private int numRooms;
        public int NumRooms
        {
            get
            {
                return numRooms;
            }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Invalid number of rooms");
                numRooms = value;
            }
        }
        private int adultsBeds; // In each room
        public int AdultsBeds
        {
            get
            {
                return adultsBeds;
            }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Invalid number of adults beds");
                adultsBeds = value;
            }
        }
        private int childrenBeds; // In each room
        public int ChildrenBeds
        {
            get
            {
                return childrenBeds;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Invalid number of children beds");
                childrenBeds = value;
            }
        }
        private int cribs; // Total
        public int Cribs
        {
            get
            {
                return cribs;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Invalid number of cribs");
                cribs = value;
            }
        }
        public TzimmerArea Area { get; set; }
        public string Address { get; set; } 
        public TzimmerType Type { get; set; }
        public bool Pool { get; set; }
        public bool Garden { get; set; }
        public bool ChildrenAttractions { get; set; }
        [XmlIgnore]
        public bool[,] Diary { get; set; }
        public string TempMatrix
        {
            get
            {
                if (Diary == null)
                    return null;
                string result = "";
                if (Diary != null)
                {
                    int sizeA = Diary.GetLength(0);
                    int sizeB = Diary.GetLength(1);
                    result += "" + sizeA + "," + sizeB;
                    for (int i = 0; i < sizeA; i++)
                        for (int j = 0; j < sizeB; j++)
                            result += "," + Diary[i, j];
                }
                return result;
            }
            set
            {
                if (value != null && value.Length > 0)
                {
                    string[] values = value.Split(',');
                    int sizeA = int.Parse(values[0]);
                    int sizeB = int.Parse(values[1]);
                    Diary = new bool[sizeA, sizeB];
                    int index = 2;
                    for (int i = 0; i < sizeA; i++)
                        for (int j = 0; j < sizeB; j++)
                            Diary[i, j] = bool.Parse(values[index++]);
                }
            }
        }
        public override string ToString()
        {
            return base.ToString();
        }
        private bool this[DateTime i] // Indexer
        {
            get
            {
                return Diary[i.Month - 1, i.Day - 1]; // The array starts from 0 so we need to do minus 1
            }
            set
            {
                Diary[i.Month - 1, i.Day - 1] = value; // The array starts from 0 so we need to do minus 1
            }
        }
    }
}
