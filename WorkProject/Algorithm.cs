using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkProject
{
    class Algorithm : Igrip
    {
        public Algorithm(bool[] data, double lfinger, double lhand, int[] userImportance)
        {
            _data = data;
            _userImportance = userImportance;
            this.lfinger = lfinger;
            this.lhand = lhand;
        }
        private bool[] _data;
        private int[] _userImportance;
        private double lfinger;
        private double lhand;

        private GripSize[,] sizechart = {
            { GripSize.Oversize, GripSize.Oversize, GripSize.Oversize, GripSize.Oversize, GripSize.Oversize},
            { GripSize.Midsize, GripSize.Midsize, GripSize.Midsize, GripSize.Midsize, GripSize.Oversize },
            { GripSize.Standard, GripSize.Standard, GripSize.Midsize, GripSize.Midsize, GripSize.Midsize },
            { GripSize.Standard, GripSize.Standard, GripSize.Standard, GripSize.Standard, GripSize.Standard },
            { GripSize.Standard, GripSize.Standard, GripSize.Standard, GripSize.Standard, GripSize.Standard },
            { GripSize.Ladies, GripSize.Ladies, GripSize.Ladies, GripSize.Ladies, GripSize.Standard },
            { GripSize.Ladies, GripSize.Ladies, GripSize.Ladies, GripSize.Ladies, GripSize.Ladies},
            { GripSize.Junior, GripSize.Junior, GripSize.Junior, GripSize.Junior, GripSize.Ladies}
        };
        private double[] sizeh = { 2.5, 3.0, 3.5, 4 };
        private double[] sizev = { 9.0, 8.5, 8, 7.5, 7, 6.5, 6 };

        int a, b;

        public static int[] importanceLevel = { 3,4,2,5 }; //This needs to change with the different orders
        private int[] relativeImportance = new int[importanceLevel.Length];
        public const int HEAP_SIZE = 4;
        private int last = -1;
        private node[] heap = new node[HEAP_SIZE];
        public GripData grip;

        private node _size;
        private node _feel;
        private node _moisture;
        private node _alignment;

        //Iputter Interface implementation
        GripOptions Goptions;
        public GripOptions gripOptions { get; set; }
        public void setCharacteristic(params string[] data)
        {
            Goptions.feel = (Feel)_feel.Trait;
            Goptions.moisturemanagement = (MoistureManagement)_moisture.Trait;
            Goptions.alignmentaid = (AlignmentAid)_alignment.Trait;
            gripOptions = Goptions;
        }

        private void FindArrVal()
        {
            for (int a = 0; a < relativeImportance.Length; a++)
            {
               relativeImportance[a] = importanceLevel[a] * _userImportance[a];
            }
        }

        public double Matching()
        {
            double total = (_feel.importance + _moisture.importance + _alignment.importance);
            double matchingC = 0;
            if (grip.gripOptions.feel == gripOptions.feel)
            {
                matchingC += _feel.importance;
            }
            if (grip.gripOptions.alignmentaid == gripOptions.alignmentaid)
            {
               matchingC += _alignment.importance;
            }
            if (grip.gripOptions.moisturemanagement == gripOptions.moisturemanagement)
            {
               matchingC += _moisture.importance;
            }

            return (matchingC / total) * 100;
        }
        private void reheapUp(node[] heap, int child)
        {
           if (child != 0)
           {
                int parent = (child - 1) / 2;
                if (heap[parent].importance < heap[child].importance)
                {
                    node temp = heap[parent];
                    heap[parent] = heap[child];
                    heap[child] = temp;
                    reheapUp(heap, parent);
                }
            }
        }

        private void insertHeap(node[] heap, ref int last, node data)
        {
            if (last != HEAP_SIZE - 1)
            {
                last++;
                heap[last] = data;
                reheapUp(heap, last);
            }
        }


        public string[] FindGrip()
        {
            FindArrVal();
            //Moisture Management 2X2 - 4
            if (_data[0] && _data[1])
            {
                _moisture.Trait = MoistureManagement.Good;
                _moisture.importance = Math.Max(relativeImportance[0], relativeImportance[1]);
                insertHeap(heap, ref last, _moisture);
            }
            else if(!_data[0] && _data[1])
            {
                _moisture.Trait = MoistureManagement.Neutral;
                _moisture.importance = Math.Max(relativeImportance[0], relativeImportance[1]);
                insertHeap(heap, ref last, _moisture);
            }
            else if(_data[0] && !_data[a])
            {
                _moisture.Trait = MoistureManagement.Good;
                _moisture.importance = Math.Max(relativeImportance[0], relativeImportance[1]);
                insertHeap(heap, ref last, _moisture);
            }
            else
            {
                _moisture.Trait = MoistureManagement.Bad;
                _moisture.importance = Math.Max(relativeImportance[0], relativeImportance[1]);
                insertHeap(heap, ref last, _moisture);
            }

            //Alignment
            if(_data[2])
            {
                _alignment.Trait = AlignmentAid.Yes;
                _alignment.importance = relativeImportance[2];
                insertHeap(heap, ref last, _alignment);
            }
            else
            {
                _alignment.Trait = AlignmentAid.No;
                _alignment.importance = relativeImportance[2];
                insertHeap(heap, ref last, _alignment);
            }

            //Feel
            if(_data[3])
            {
                _feel.Trait = Feel.Harder;
                _feel.importance = relativeImportance[3];
                insertHeap(heap, ref last, _feel);
            }
            else
            {
                _feel.Trait = Feel.Softer;
                _feel.importance = relativeImportance[3];
                insertHeap(heap, ref last, _feel);
            }

            //Size
            for (a = 0; a< 4 && lfinger> sizeh[a]; a++) { }
            for (b = 0; b< 7 && lhand <= sizev[b]; b++) { }
            _size.importance = 27;
            _size.Trait = sizechart[b, a];
            insertHeap(heap, ref last, _size);
            
             grip = new GripData(heap, last);
             grip.GetGrip();
             return grip.gripFits; //string of grips
        }
    }
}