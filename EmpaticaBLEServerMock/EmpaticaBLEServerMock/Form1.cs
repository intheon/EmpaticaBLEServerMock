using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Threading;


namespace EmpaticaBLEServerMock
{
    public partial class Form1 : Form
    {

        private AsynchronousSocketListener listener = null;
        SensorValues sensorValues = new SensorValues();
        Thread listenerThread;

        public Form1()
        {
            InitializeComponent();

            listener = new AsynchronousSocketListener(sensorValues);

            listenerThread = new Thread(listener.StartListening);
            listenerThread.Start();
            ;
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            listener.close();
            listener = null;
            Application.Exit();
        }

        private void accNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            accTrackBar.Value = (int)accNumericUpDown.Value;
            sensorValues.acc = (double)accNumericUpDown.Value;
        }

        private void accTrackBar_Scroll(object sender, EventArgs e)
        {
            accNumericUpDown.Value = accTrackBar.Value;
            sensorValues.acc = (double)accNumericUpDown.Value;
        }

        private void bvpNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            bvpTrackBar.Value = (int)bvpNumericUpDown.Value;
            sensorValues.bvp = (double)bvpNumericUpDown.Value;
        }

        private void bvpTrackBar_Scroll(object sender, EventArgs e)
        {
            bvpNumericUpDown.Value = bvpTrackBar.Value;
            sensorValues.bvp = (double)bvpNumericUpDown.Value;
        }

        private void gsrNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            gsrTrackBar.Value = (int)gsrNumericUpDown.Value;
            sensorValues.gsr = (double)gsrNumericUpDown.Value;
        }

        private void gsrTrackBar_Scroll(object sender, EventArgs e)
        {
            gsrNumericUpDown.Value = gsrTrackBar.Value;
            sensorValues.gsr = (double)gsrNumericUpDown.Value;
        }

        private void tmpNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            tmpTrackBar.Value = (int)tmpNumericUpDown.Value;
            sensorValues.tmp = (double)tmpNumericUpDown.Value;
        }

        private void tmpTrackBar_Scroll(object sender, EventArgs e)
        {
            tmpNumericUpDown.Value = tmpTrackBar.Value;
            sensorValues.tmp = (double)tmpNumericUpDown.Value;
        }

        private void ibiNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            ibiTrackBar.Value = (int)ibiNumericUpDown.Value;
            sensorValues.ibi = (double)ibiNumericUpDown.Value;
        }

        private void ibiTrackBar_Scroll(object sender, EventArgs e)
        {
            ibiNumericUpDown.Value = ibiTrackBar.Value;
            sensorValues.ibi = (double)ibiNumericUpDown.Value;
        }

        private void hrNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            hrTrackBar.Value = (int)hrNumericUpDown.Value;
            sensorValues.hr = (double)hrNumericUpDown.Value;
        }

        private void hrTrackBar_Scroll(object sender, EventArgs e)
        {
            hrNumericUpDown.Value = hrTrackBar.Value;
            sensorValues.hr = (double)hrNumericUpDown.Value;
        }

        private void batNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            batTrackBar.Value = (int)batNumericUpDown.Value;
            sensorValues.bat = (double)batNumericUpDown.Value;
        }

        private void batTrackBar_Scroll(object sender, EventArgs e)
        {
            batNumericUpDown.Value = batTrackBar.Value;
            sensorValues.bat = (double)batNumericUpDown.Value;
        }

        private void tagNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            tagTrackBar.Value = (int)tagNumericUpDown.Value;
            sensorValues.tag = (double)tagNumericUpDown.Value;
        }

        private void tagTrackBar_Scroll(object sender, EventArgs e)
        {
            tagNumericUpDown.Value = tagTrackBar.Value;
            sensorValues.tag = (double)tagNumericUpDown.Value;
        }
    }
}
