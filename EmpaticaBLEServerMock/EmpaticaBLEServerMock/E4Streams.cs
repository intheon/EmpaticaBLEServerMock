using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpaticaBLEServerMock
{
    class E4Streams
    {
        public enum STREAMS
        {
            acc, //3-axis acceleration
            bvp, //Blood Volume Pulse
            gsr, //Galvanic Skin Response
            ibi, //Interbeat Interval and Heartbeat
            tmp, //Skin Temperature
            bat, //Device Battery
            tag //Tag taken from the device (by pressing the button)}
        };

        public static List<string> STREAMS_STRINGS = new List<string>
        {
            "acc", //3-axis acceleration
            "bvp", //Blood Volume Pulse
            "gsr", //Galvanic Skin Response
            "ibi", //Interbeat Interval and Heartbeat
            "tmp", //Skin Temperature
            "bat", //Device Battery
            "tag" //Tag taken from the device (by pressing the button)}
        };

        public static List<double> STREAMS_FREQUENCIES = new List<double>
        {
            0.5, //3-axis acceleration
            0.5, //Blood Volume Pulse
            0.5, //Galvanic Skin Response
            0.5, //Interbeat Interval and Heartbeat
            0.5, //Skin Temperature
            0, //Device Battery
            0 //Tag taken from the device (by pressing the button)}
        };
    }
}
