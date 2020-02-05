using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkProject
{
    public enum GripSize
    {
        Junior,
        Ladies,
        Standard,
        Midsize,
        Oversize
    }
    public enum MoistureManagement
    {
        Good,
        Neutral,
        Bad
    }
    public enum Feel
    {
        Softer,
        Harder
    }
    public enum AlignmentAid
    {
        Yes,
        No
    }
    public struct GripOptions
    {
        public GripSize gripsize;
        public MoistureManagement moisturemanagement;
        public Feel feel;
        public AlignmentAid alignmentaid;
    };
    public struct node
    {
        public int importance;
        public object Trait; //used for heaps, stores the user data in levels of importance
    };

    interface Igrip
    {
        GripOptions gripOptions { get; set; }
        void setCharacteristic(params string[] data);
    }
}
