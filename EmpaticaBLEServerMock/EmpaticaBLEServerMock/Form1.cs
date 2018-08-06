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
        private const double SECONDS_IN_MINUTE = 60.0;
        private AsynchronousSocketListener listener = null;
        SensorValues sensorValues = new SensorValues();

        public Form1()
        {
            InitializeComponent();

            listener = new AsynchronousSocketListener(sensorValues);
            listener.StartListening();

            //listenerThread = new Thread(listener.StartListening);
            //listenerThread.Start();

            sensorValues.accX = (double)accXNumericUpDown.Value;
            sensorValues.accY = (double)accYNumericUpDown.Value;
            sensorValues.accZ = (double)accZNumericUpDown.Value;

            sensorValues.bat = (double)batNumericUpDown.Value;

            sensorValues.bvp = (double)bvpNumericUpDown.Value;

            sensorValues.gsr = (double)gsrNumericUpDown.Value;

            sensorValues.hr = (double)hrNumericUpDown.Value;
            sensorValues.ibi = SECONDS_IN_MINUTE / sensorValues.hr;

            sensorValues.tmp = (double)tmpNumericUpDown.Value;
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            listener.close();
            listener = null;
            Application.Exit();
        }

        private void accXNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            accXTrackBar.Value = (int)accXNumericUpDown.Value;
            sensorValues.accX = (double)accXNumericUpDown.Value;
        }

        private void accXTrackBar_Scroll(object sender, EventArgs e)
        {
            accXNumericUpDown.Value = accXTrackBar.Value;
            sensorValues.accX = (double)accXNumericUpDown.Value;
        }

        private void accYNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            accYTrackBar.Value = (int)accYNumericUpDown.Value;
            sensorValues.accY = (double)accYNumericUpDown.Value;
        }

        private void accYTrackBar_Scroll(object sender, EventArgs e)
        {
            accYNumericUpDown.Value = accYTrackBar.Value;
            sensorValues.accY = (double)accYNumericUpDown.Value;
        }

        private void accZNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            accZTrackBar.Value = (int)accZNumericUpDown.Value;
            sensorValues.accZ = (double)accZNumericUpDown.Value;
        }

        private void accZTrackBar_Scroll(object sender, EventArgs e)
        {
            accZNumericUpDown.Value = accZTrackBar.Value;
            sensorValues.accZ = (double)accZNumericUpDown.Value;
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


        private void hrNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            hrTrackBar.Value = (int)hrNumericUpDown.Value;
            sensorValues.hr = (double)hrNumericUpDown.Value;

            sensorValues.ibi = SECONDS_IN_MINUTE / sensorValues.hr;
        }

        private void hrTrackBar_Scroll(object sender, EventArgs e)
        {
            hrNumericUpDown.Value = hrTrackBar.Value;
            sensorValues.hr = (double)hrNumericUpDown.Value;

            sensorValues.ibi = SECONDS_IN_MINUTE / sensorValues.hr;
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

        private void tagButton_Click(object sender, EventArgs e)
        {
            listener.SendTagMessage();
        }

        
    }
}
