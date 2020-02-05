using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkProject
{
    class GripData : Igrip
    {
        private void reheapDown(node[] heap, int loc, int last)
        {//check for children, if one is larger then swap
            int leftKey, rightKey, largestIndex;
            node hold;
            if (loc * 2 + 1 <= last)
            {
                leftKey = heap[loc * 2 + 1].importance;
                if (loc * 2 + 2 <= last)
                    rightKey = heap[loc * 2 + 2].importance;
                else
                    rightKey = leftKey - 1; // make so rightKey is always smaller than leftKey
                if (rightKey > leftKey)
                    largestIndex = 2 * loc + 2;
                else
                    largestIndex = 2 * loc + 1;
                if (heap[loc].importance < heap[largestIndex].importance)
                {
                    hold = heap[loc];
                    heap[loc] = heap[largestIndex];
                    heap[largestIndex] = hold;
                    reheapDown(heap, largestIndex, last);
                }
            }
        }
        private void deleteHeap(node[] heap, ref int last, ref string dataOut)
        {
            if (last >= 0)
            {
                dataOut = heap[0].Trait.ToString();
                heap[0] = heap[last];
                last--;
                reheapDown(heap, 0, last);
            }
        }
        //Iputter Interface implementation
        GripOptions Goptions;
        public GripOptions gripOptions { get; set; }
        public void setCharacteristic(params string[] gripName)
        {
            for (int a = 0; a < data.Length; a++) //data is the list of putters that fit, not data passed in
            {
                if (data[a].Contains(gripName[0]))
                {
                    for (int c = 0; c < 3; c++)
                    {
                        if (((MoistureManagement)c).ToString() == data[a].Split('\u00BB')[1])
                        {
                            Goptions.moisturemanagement = (MoistureManagement)c;
                        }
                    }
                    for(int c = 0; c <2; c++)
                    {
                        if (((AlignmentAid)c).ToString() == data[a].Split('\u00BB')[2])
                        {
                            Goptions.alignmentaid = (AlignmentAid)c;
                        }
                        if (((Feel)c).ToString() == data[a].Split('\u00BB')[3])
                        {
                            Goptions.feel = (Feel)c;
                        }
                    }
                    gripOptions = Goptions;
                }
            }
        }
        
        public GripData(node[] heap, int last)
        {
            int a = 0, l = last;
            deleteHeap(heap, ref l, ref GripSize);

            while (l >= 0)
            {
                gripCharacteristics[a] = "";
                deleteHeap(heap, ref l, ref gripCharacteristics[a]);
                a++;
            }
        }
        public string GripSize;
        public string[] gripFits;
        private string[] gripCharacteristics = new string[3];
        private string[] data; //holds the unsplit data for matching grips
        SaveData grip = new SaveData();

        public void GetGrip()
        {
            data = grip.accessData(gripCharacteristics);
            gripFits = new string[data.Length];
            for (int a = 0; a < data.Length; a++)
            {
                string[] temp = data[a].Split('\u00BB');
                gripFits[a] = temp[0];
            }
        }
        private bool InArray(string[] array, string TestCase)
        {//Checks that a string is not repeated in an array
            for (int a = 0; a < array.Length; a++)
            {
                if (array[a] != null && array[a].Equals(TestCase))
                    return true;
            }
            return false;
        }

        public string[] GripBrands()
        {
            string[] brands;
            string brand;
            int counter = 0;
            if (gripFits != null)
            {
                brands = new string[gripFits.Length];
                for (int a = 0; a < gripFits.Length; a++)
                {
                    brand = gripFits[a].Substring(0, gripFits[a].IndexOf(' '));
                    if (!InArray(brands, brand))
                    {
                        brands[counter] = brand;
                        counter++;
                    }
                }
                return brands;
            }
            return null;
        }
    }
}
