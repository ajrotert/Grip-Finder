using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkProject
{
    class SaveData
    {
        private string[] GripDataArray =
        {
            "GolfPride New Decade Multi-Compound»Good»No»Harder",
            "GolfPride New Decade MCC Align»Good»Yes»Harder",
            "GolfPride MCC Plus4 Align»Good»Yes»Harder",
            "GolfPride MCC* Plus4»Good»No»Harder",
            "GolfPride CP2 Pro»Neutral»No»Softer",
            "GolfPride CP2 Wrap»Neutral»No»Softer",
            "GolfPride Tour Velvet»Neutral»No»Harder",
            "GolfPride Tour Wrap»Bad»No»Harder",
            "Winn Dri-Tac»Good»No»Softer",
            "Winn DriTac Wrap»Good»No»Softer",
        };
        public static string[] uniqueID =
        {
            "New Decade Multi",
            "New Decade MCC",
            "MCC Plus4 Align",
            "MCC* Plus4",
            "CP2 Pro",
            "CP2 Wrap",
            "Tour Velvet",
            "Tour Wrap",
            "Dri-Tac",
            "DriTac Wrap"
        };
        public static Image[] imageLoc =
        {
            /*"Grip-GP-MCC.png",
            "Grip-GP-MCC_Align.png",
            "Grip-GP-MCC_Align_Plus4.png",
            "Grip-GP-MCC_Plus4.png",
            "Grip-GP-CP2_Pro.png",
            "Grip-GP-CP2_Wrap.png",
            "Grip-GP-TourVelvet.png",
            "Grip-GP-TourWrap.png",
            "Grip-WN-DriTac.png",
            "Grip-WN-DriTac_Wrap.png"*/
            Images1.Grip_GP_MCC,
            Images1.Grip_GP_MCC_Align,
            Images1.Grip_GP_MCC_Align_Plus4,
            Images1.Grip_GP_MCC_Plus4,
            Images1.Grip_GP_CP2_Pro,
            Images1.Grip_GP_CP2_Wrap,
            Images1.Grip_GP_TourVelvet,
            Images1.Grip_GP_TourWrap,
            Images1.Grip_WN_DriTac,
            Images1.Grip_WN_DriTac_Wrap
    };

        public string[] accessData(params string[] data) //returns a string of matching information
        {
            List<string> dataList = new List<string>();
            for (int a = 0; a < GripDataArray.Length; a++)
            {
                if (GripDataArray[a].Contains(data[0]))
                {
                    dataList.Add(GripDataArray[a]);
                }
            }

            if (dataList.Count == 0) //insures that there will always be one item returned
            {
                if (GripDataArray.Length != 0)
                    dataList.Add(GripDataArray[0]);
                else
                    dataList.Add("empty list");
            }

            int[] tracking = new int[dataList.Count];
            int trackingCount = 0;
            for (int a = 1; a < data.Length; a++) //the number of parameters sent in
            {
                for (int b = 0; b < dataList.Count && dataList.Count >= 1; b++)
                {
                    if (!dataList[b].Contains(data[a]))
                    {
                        tracking[trackingCount] = b;
                        trackingCount++;
                    }
                }
                if (trackingCount <= dataList.Count - 1)
                {
                    int eliminated = 0;
                    for (int b = 0; b < trackingCount; b++)
                    {
                        dataList.RemoveAt(tracking[b] - eliminated);
                        eliminated++;
                    }
                    trackingCount = 0;
                }
                trackingCount = 0;
            }
            string[] array = dataList.ToArray();
            return array;

        }
    }
}
