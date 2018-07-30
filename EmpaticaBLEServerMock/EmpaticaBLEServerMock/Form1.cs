using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmpaticaBLEServerMock
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void accNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            accTrackBar.Value = (int)accNumericUpDown.Value;
        }

        private void accTrackBar_Scroll(object sender, EventArgs e)
        {
            accNumericUpDown.Value = accTrackBar.Value;
        }

        private void bvpNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            bvpTrackBar.Value = (int)bvpNumericUpDown.Value;
        }

        private void bvpTrackBar_Scroll(object sender, EventArgs e)
        {
            bvpNumericUpDown.Value = bvpTrackBar.Value;
        }

        private void gsrNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            gsrTrackBar.Value = (int)gsrNumericUpDown.Value;
        }

        private void gsrTrackBar_Scroll(object sender, EventArgs e)
        {
            gsrNumericUpDown.Value = gsrTrackBar.Value;
        }

        private void tmpNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            tmpTrackBar.Value = (int)tmpNumericUpDown.Value;
        }

        private void tmpTrackBar_Scroll(object sender, EventArgs e)
        {
            tmpNumericUpDown.Value = tmpTrackBar.Value;
        }

        private void ibiNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            ibiTrackBar.Value = (int)ibiNumericUpDown.Value;
        }

        private void ibiTrackBar_Scroll(object sender, EventArgs e)
        {
            ibiNumericUpDown.Value = ibiTrackBar.Value;
        }

        private void hrNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            hrTrackBar.Value = (int)hrNumericUpDown.Value;
        }

        private void hrTrackBar_Scroll(object sender, EventArgs e)
        {
            hrNumericUpDown.Value = hrTrackBar.Value;
        }

        private void batNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            batTrackBar.Value = (int)batNumericUpDown.Value;
        }

        private void batTrackBar_Scroll(object sender, EventArgs e)
        {
            batNumericUpDown.Value = batTrackBar.Value;
        }

        private void tagNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            tagTrackBar.Value = (int)tagNumericUpDown.Value;
        }

        private void tagTrackBar_Scroll(object sender, EventArgs e)
        {
            tagNumericUpDown.Value = tagTrackBar.Value;
        }
    }
}
