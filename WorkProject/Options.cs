using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorkProject
{
    public partial class Options : Form
    {
        public Options()
        {
            InitializeComponent();
        }
        private string[] LabelText =
        {
            "Do you play in wet \r\nor rainy conditions?",
            "Do your hands sweat\r\nwhen you play?",
            "Do you want alignment \r\nhelp?",
            "Do you prefer a \r\nfirmer or softer grip?"
        };
        private Image[] ImageLocations =
        {
            //"Grip-Wet.png", "Grip-Wet.png", "Grip-Alignment.png", "Grip-Feel.png"
            Images1.Grip_Wet, Images1.Grip_Wet, Images1.Grip_Alignment, Images1.Grip_Feel
        };
        private int counter;
        private void Options_Load(object sender, EventArgs e)
        {
            counter=0;
            ProgressBar.Value = 25;
            ProgressBar.ForeColor = Color.Tan;
            trackBar1.Value = 5;
        }

        private void YesButton_Click(object sender, EventArgs e)
        {
            UpdateWindow(true);
        }

        private void NoButton_Click(object sender, EventArgs e)
        {
            UpdateWindow(false);
        }
        public void UpdateWindow(bool val)
        {
            Form1.data[counter] = val;
            counter++;
            trackBar1.Value = 5;
            ProgressBar.Value = (counter + 1) * 20;
            if (counter == 4)
            {
                Hide();
            }
            else if(counter==3)
            {
                //pictureBox1.Image = Image.FromFile(Form1.FileLocation + ImageLocations[counter]);
                pictureBox1.Image = ImageLocations[counter];
                YesButton.Text = "Firmer";
                NoButton.Text = "Softer";
                OptionText.Text = LabelText[counter];
            }
            else
            {
                //pictureBox1.Image = Image.FromFile(Form1.FileLocation + ImageLocations[counter]);
                pictureBox1.Image = ImageLocations[counter];
                OptionText.Text = LabelText[counter];
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            Form1.userImportance[counter] = trackBar1.Value;
        }
    }
}
