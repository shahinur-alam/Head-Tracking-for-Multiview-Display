using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace DF_FaceTracking.cs
{

    public partial class ViewerWindow : Form
    {
        int GlobalImageNumber;
        string ComboVal="BelleBall";

        public ViewerWindow()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            //ImageViewer();
            //richTextBox1.Text = filepath;
            //richTextBox1.BringToFront();
            label1.BringToFront();
            button1.BringToFront();

            //Make the label Transparent
            //label1.Parent = pictureBox1;
            //label1.BackColor = Color.Transparent;
        }

        public void ReceiveFaceData(float yaw, float pitch)
        {
            //string filepath = "F:/Teddy_parallel/Bottom camera/Before/DSC01214.jpg";
            float viewingAngle,degreePerImage;
            viewingAngle = 15 * 2;
            int totalImage = 65;
            degreePerImage = viewingAngle / totalImage;

            //richTextBox1.Text= yaw.ToString();
            

            /*
             * Generating Image number for horizontal view,
             * */
            int imageNumber=0;
            if (-15 <= yaw && yaw<=15) // Check wheather the yaw value is in the range.
            {
                imageNumber = (int)(yaw / degreePerImage);
                if (yaw==0)
                {
                    imageNumber = (totalImage/2); //Total Number of horizontal view divided by two.
                }
                else
                {
                    imageNumber = (totalImage/2) + imageNumber;
                }
                
                //ImageViewer(filepath);
                
            }
            else if(-15 > yaw) // Check wheather it is out of range in left side
            {
                imageNumber = 0;
            }
            else if (yaw > 15) // Check wheather it is out of range in Right side
            {
                imageNumber = totalImage-1;
            }
            else
            { }


            /*
             * Generating File File number for vertical one view,
             * */
            int VerticalImageNumber = 0;
            /*
            if (-2 <= pitch && pitch <= 2) // Check wheather the yaw value is in the range.
            {
                    VerticalImageNumber = 0; //Total Number of horizontal view divided by two.

            }
            else if (-2 > pitch) // Check wheather it is out of range in left side
            {
                VerticalImageNumber = 2;
            }
            else if (pitch > 2) // Check wheather it is out of range in Right side
            {
                VerticalImageNumber = 1;
            }
            */
            if(imageNumber!=GlobalImageNumber) // Check whether the state is idle or not
            {
                ImageViewer(GenerateFilePath(imageNumber, VerticalImageNumber));
            }
                
         
            
            //richTextBox1.Text = "Degree Per Image: " + degreePerImage + "Yaw: " + yaw + " Pitch:"+pitch+" Image Number: " + imageNumber + "Vertical Im Num: "+VerticalImageNumber+" Original :" + degreePerImage * yaw+" Path"+ GenerateFilePath(imageNumber,VerticalImageNumber);
            label1.Text = "Degree Per Image: " + degreePerImage + " Yaw: " + yaw + " Pitch:" + pitch + "Horizontal Image Number: " + imageNumber +" "+GlobalImageNumber+ " Vertical Image Number: " + VerticalImageNumber +" Image Path: " + GenerateFilePath(imageNumber, VerticalImageNumber);

        }


        public string GenerateFilePath(int imageNumber, int VerticalImageNumber)
        {
            int NormalizedImgNumber;
            string FolderBasePath = "C:\\StereoMultiviewImage\\"+ComboVal+"\\14\\";
            int ValueDiff = Math.Abs(imageNumber - GlobalImageNumber);
            GlobalImageNumber = imageNumber;
            if(ValueDiff>1)
            {
                NormalizedImgNumber = (GlobalImageNumber + imageNumber) / 2;
            }
            else
            {
                NormalizedImgNumber = GlobalImageNumber;
            }
            return FolderBasePath+ VerticalImageNumber+"-" + NormalizedImgNumber + ".jpg";
        }


        public void ImageViewer(string filepath)
        {
            try
            {
                //pictureBox1.ImageLocation = filepath;
                Image image = Image.FromFile(filepath);
                pictureBox1.Image = image;
                //pictureBox1.Height = Screen.PrimaryScreen.Bounds.Height; // Screeen Height
                //pictureBox1.Width = Screen.PrimaryScreen.Bounds.Width; // Screeen Height
                pictureBox1.Height = Screen.PrimaryScreen.Bounds.Height;
                pictureBox1.Width = Screen.PrimaryScreen.Bounds.Width;
                //Controls.Add(pictureBox1);
            }
             catch(System.Exception)
            {
                //string msg=MessageBox.Show("File Not Found\n Please Keep The Stero Image in C drive with folder name StereoMultiviewImage",'Cancel', MessageBoxButtons.OK,MessageBoxIcon.Hand);
                if (MessageBox.Show("File Not Found\n Please Keep The Stero Image in C drive with folder name StereoMultiviewImage", "Exit?", MessageBoxButtons.OK) == System.Windows.Forms.DialogResult.OK)
                {
                    Application.Exit();
                }
            }

        }

        private void ViewerWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                //richTextBox1.Text = e.KeyCode.ToString();
                //this.Close();
                Application.Exit();
            }
        }

        private void NUDViewingAngle_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void ViewerWindow_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void NUDViewingAngle_ValueChanged(object sender, EventArgs e)
        {
           // MessageBox.Show(NUDViewingAngle.Value.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //this.Close();
            Application.Exit();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboVal = comboBox1.Text;
        }
    }
}
