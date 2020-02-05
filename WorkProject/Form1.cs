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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            StartUp();
        }

        private System.Windows.Forms.PictureBox[] pictureBoxArray;
        private PictureBox pictureBox2 = new PictureBox(), pictureBox3 = new PictureBox();
        private Button RestartButton = new Button();
        private System.Windows.Forms.TextBox textBox1 = new TextBox();
        private System.Windows.Forms.PictureBox pictureBox1 = new PictureBox();
        private System.Windows.Forms.Label TopLabel = new Label();
        private System.Windows.Forms.TextBox textBox2 = new TextBox();
        private System.Windows.Forms.Label HandLengthLabel = new Label();
        private System.Windows.Forms.Label label2 = new Label();
        private System.Windows.Forms.Button button1 = new Button();
        private System.Windows.Forms.Button FindGrip = new Button();

        public const int Number_Of_Options = 4;
        private Algorithm fitter;
        double lhand, lfinger;
        private string[] results;
        private int Starting_Location_X = 50;
        private int Starting_Location_Y = 41;
        //public static string FileLocation = "C:\\Users\\DSU\\Desktop\\WorkProject\\WorkProject\\Images\\";
        private bool called = false;

        public static bool[] data = new bool[Number_Of_Options];
        public static int[] userImportance = { 5, 5, 5, 5 };
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                try
                {
                    lfinger = Convert.ToDouble(textBox2.Text);
                    lhand = Convert.ToDouble(textBox1.Text);

                    Options option = new Options();
                    if (!called)
                    {
                        option.Show();
                        called = true;
                    }
                    FindGrip.Visible = true;
                }
                catch
                {
                    textBox1.Text = "";
                    textBox2.Text = "";
                }
            }
        }

        private void FindGrip_Click(object sender, EventArgs e)
        {
            Console.WriteLine(userImportance[0 ] + " " + userImportance[1] + " " + userImportance[2] + " " + userImportance[3]);
            fitter = new Algorithm(data, lfinger, lhand, userImportance);
            results = fitter.FindGrip();
            fitter.setCharacteristic();
            HideAllFront();
            FillData();
        }
        private void DisplayImages()
        {
            int a;
            for (a = 0; a < results.Length; a++)
            {
                pictureBoxArray[a] = new PictureBox();
                pictureBoxArray[a].Location = new System.Drawing.Point(Starting_Location_X + 160 * a, Starting_Location_Y);
                pictureBoxArray[a].Name = "pictureBox2" + a;
                pictureBoxArray[a].Size = new System.Drawing.Size(151, 301);
                pictureBoxArray[a].TabIndex = 8;
                pictureBoxArray[a].TabStop = false;
                for (int b = 0; b < SaveData.uniqueID.Length; b++)
                {
                    if (results[a].Contains(SaveData.uniqueID[b]))
                    {
                        //pictureBoxArray[a].Image = Image.FromFile(FileLocation + SaveData.imageLoc[b]);
                        pictureBoxArray[a].Image = SaveData.imageLoc[b];
                        break;
                    }
                }
                Controls.Add(pictureBoxArray[a]);
            }
            AddLabel(Starting_Location_X + 160 * a, Starting_Location_Y);
            AddButton(Starting_Location_X + 160 * a, Starting_Location_Y + 223);
        }
        private void FillData()
        {
            pictureBoxArray = new PictureBox[results.Length];
            if(results.Length > 2)
            {
                string[] brands = fitter.grip.GripBrands();
                ImageBoxSet();
            }
            else
            {
                DisplayImages();
            }
        }
        private void ImageBoxSet()
        {
            pictureBox2.Location = new System.Drawing.Point(60, 41);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new System.Drawing.Size(270, 301);
            pictureBox2.TabIndex = 8;
            pictureBox2.TabStop = false;
            try
            {
                //pictureBox2.Image = Image.FromFile(FileLocation + "Grip-GolfPrideLogo.png");
                pictureBox2.Image = Images1.Grip_GolfPrideLogo;
            }
            catch
            {
                //have a text file in documents that specifies location for images
            }
            pictureBox2.Click += new System.EventHandler(pictureBox2_Click);

            pictureBox3.Location = new System.Drawing.Point(354, 41);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new System.Drawing.Size(270, 301);
            pictureBox3.TabIndex = 9;
            pictureBox3.TabStop = false;
            //pictureBox3.Image = Image.FromFile(FileLocation + "Grip-WinnLogo.png");
            pictureBox3.Image = Images1.Grip_WinnLogo;
            pictureBox3.Click += new System.EventHandler(pictureBox3_Click);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox3);
        }
        private void AddButton(int x, int y)
        {
            RestartButton.BackColor = System.Drawing.Color.Transparent;
            RestartButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            RestartButton.ForeColor = System.Drawing.Color.White;
            RestartButton.Location = new System.Drawing.Point(x+75, y);
            RestartButton.Name = "RestartButton";
            RestartButton.Size = new System.Drawing.Size(163, 78);
            RestartButton.TabIndex = 2;
            RestartButton.Text = "Restart";
            RestartButton.UseVisualStyleBackColor = false;
            RestartButton.Click += new System.EventHandler(restart_Click);
            Controls.Add(RestartButton);
        }
        private void AddLabel(int x, int y)
        {
            Label newLabel = new Label();
            newLabel.AutoSize = true;
            newLabel.BackColor = System.Drawing.Color.Transparent;
            newLabel.Font = new System.Drawing.Font("Britannic Bold", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            newLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            newLabel.Location = new System.Drawing.Point(x, y);
            newLabel.Name = "newLabel";
            newLabel.Size = new System.Drawing.Size(231, 53);
            newLabel.TabIndex = 2;
            newLabel.Text = "Grip Size:\n" + fitter.grip.GripSize;
            newLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            Controls.Add(newLabel);
        }
        
        private void HideAllFront()
        {
            button1.Visible = false;
            FindGrip.Visible = false;
            label2.Visible = false;
            HandLengthLabel.Visible = false;
            TopLabel.Visible = false;
            textBox1.Visible = false;
            textBox2.Visible = false;
            pictureBox1.Visible = false;
        }
        private void restart_Click(object sender, EventArgs e)
        {
            if (called)
            {
                Controls.Clear();
                StartUp();
                called = false;
                //RestartButton.Click -= restart_Click;
                //Form1 fn = new Form1();
                //fn.Show();
                //Hide();
                //Problem with this is that the program does not terminate
            }
        }

        private void StartUp()
        {
            //pictureBox4.Image = Image.FromFile(FileLocation + "LoadingLogo.png");
            pictureBox4.Image = Images1.LoadingLogo6;

            textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            textBox1.Location = new System.Drawing.Point(50, 154);
            textBox1.MinimumSize = new System.Drawing.Size(234, 53);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new System.Drawing.Size(234, 53);
            textBox1.TabIndex = 0;
            textBox1.Visible = true;
            textBox1.Text = "";

            pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom)));
            //pictureBox1.Image = Image.FromFile(FileLocation + "HandDisplay.png");
            pictureBox1.Image = Images1.HandDisplay3;
            pictureBox1.Location = new System.Drawing.Point(488, 41);
            //pictureBox1.MinimumSize = new System.Drawing.Size(300, 301);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new System.Drawing.Size(450, 451);
            //pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            pictureBox1.Visible = true;

            TopLabel.AutoSize = true;
            TopLabel.BackColor = System.Drawing.Color.Transparent;
            TopLabel.Font = new System.Drawing.Font("Britannic Bold", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            TopLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            TopLabel.Location = new System.Drawing.Point(51, 41);
            TopLabel.MinimumSize = new System.Drawing.Size(231, 53);
            TopLabel.Name = "TopLabel";
            TopLabel.Size = new System.Drawing.Size(231, 53);
            TopLabel.TabIndex = 2;
            TopLabel.Text = "Hand Size";
            TopLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            TopLabel.Visible = true;

            textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            textBox2.Location = new System.Drawing.Point(50, 243);
            textBox2.MinimumSize = new System.Drawing.Size(234, 53);
            textBox2.Multiline = true;
            textBox2.Name = "textBox2";
            textBox2.Size = new System.Drawing.Size(234, 53);
            textBox2.TabIndex = 3;
            textBox2.Visible = true;
            textBox2.Text = "";

            HandLengthLabel.AutoSize = true;
            HandLengthLabel.BackColor = System.Drawing.Color.Transparent;
            HandLengthLabel.Font = new System.Drawing.Font("Britannic Bold", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            HandLengthLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            HandLengthLabel.Location = new System.Drawing.Point(75, 120);
            HandLengthLabel.MinimumSize = new System.Drawing.Size(176, 32);
            HandLengthLabel.Name = "HandLengthLabel";
            HandLengthLabel.Size = new System.Drawing.Size(176, 32);
            HandLengthLabel.TabIndex = 4;
            HandLengthLabel.Text = "Hand Length";
            HandLengthLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            HandLengthLabel.Visible = true;

            label2.AutoSize = true;
            label2.BackColor = System.Drawing.Color.Transparent;
            label2.Font = new System.Drawing.Font("Britannic Bold", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            label2.Location = new System.Drawing.Point(65, 210);
            label2.MinimumSize = new System.Drawing.Size(204, 32);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(204, 32);
            label2.TabIndex = 5;
            label2.Text = "Longest Finger";
            label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            label2.Visible = true;

            button1.Location = new System.Drawing.Point(93, 332);
            button1.MinimumSize = new System.Drawing.Size(132, 41);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(132, 41);
            button1.TabIndex = 6;
            button1.Text = "Select Options";
            button1.UseVisualStyleBackColor = true;
            button1.Click += new System.EventHandler(button1_Click);
            button1.Visible = true;

            FindGrip.BackColor = System.Drawing.SystemColors.ActiveCaption;
            FindGrip.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            FindGrip.Location = new System.Drawing.Point(Starting_Location_X, 332);
            FindGrip.Name = "FindGrip";
            FindGrip.Size = new System.Drawing.Size(234, 53);
            FindGrip.TabIndex = 7;
            FindGrip.Text = "FIND GRIP";
            FindGrip.UseVisualStyleBackColor = false;
            FindGrip.Visible = false;
            FindGrip.Click += new System.EventHandler(FindGrip_Click);

            Controls.Add(FindGrip);
            Controls.Add(button1);
            Controls.Add(label2);
            Controls.Add(HandLengthLabel);
            Controls.Add(textBox2);
            Controls.Add(TopLabel);
            Controls.Add(pictureBox1);
            Controls.Add(textBox1);
            Controls.Add(pictureBox4);

            int[] temp = { 5, 5, 5, 5 };
            userImportance = temp;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Controls.Remove(pictureBox2);
            Controls.Remove(pictureBox3);
            List<string> removeBrands = new List<string>(results);
            for(int a = 0; a<removeBrands.Count(); a++)
            {
                if (!removeBrands[a].Contains("GolfPride"))
                {
                    removeBrands.RemoveAt(a);
                    a--;
                }
            }
            results = removeBrands.ToArray();
            DisplayImages();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Controls.Remove(pictureBox2);
            Controls.Remove(pictureBox3);
            List<string> removeBrands = new List<string>(results);
            for (int a = 0; a < removeBrands.Count(); a++)
            {
                if (!removeBrands[a].Contains("Winn"))
                {
                    removeBrands.RemoveAt(a);
                    a--;
                }
            }
            results = removeBrands.ToArray();
            DisplayImages();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
