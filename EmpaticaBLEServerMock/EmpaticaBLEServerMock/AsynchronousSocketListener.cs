using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace EmpaticaBLEServerMock
{

    class AsynchronousSocketListener
    {
        private bool[] subscriptions = new bool[Enum.GetNames(typeof(E4Streams.STREAMS)).Length];

        // Thread signal.  
        public static ManualResetEvent allDone = new ManualResetEvent(false);

        Thread thread;
        Socket handler;

        bool stop = false;
        bool connected = false;

        private SensorValues sensorValues;


        public AsynchronousSocketListener(SensorValues sensorValues)
        {
            for (int i = 0; i < subscriptions.Length; ++i)
            {
                subscriptions[i] = false;
            }
            
            this.sensorValues = sensorValues;

            thread = new Thread(manageSubscriptions);
            thread.IsBackground = true;
            thread.Start();
        }

        public void StartListening()
        {
            // Establish the local endpoint for the socket.  
            // The DNS name of the computer  
            // running the listener is "host.contoso.com".  
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            //IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

            // Create a TCP/IP socket.  
            Socket listener = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            // Bind the socket to the local endpoint and listen for incoming connections.  
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(100);

                while (!stop)
                {
                    // Set the event to nonsignaled state.  
                    allDone.Reset();

                    // Start an asynchronous socket to listen for connections.  
                    Console.WriteLine("Waiting for a connection...");
                    listener.BeginAccept(
                        new AsyncCallback(AcceptCallback),
                        listener);

                    // Wait until a connection is made before continuing.  
                    allDone.WaitOne();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("\nPress ENTER to continue...");
            Console.Read();

        }

        public void AcceptCallback(IAsyncResult ar)
        {
            // Signal the main thread to continue.  
            allDone.Set();

            // Get the socket that handles the client request.  
            Socket listener = (Socket)ar.AsyncState;
            handler = listener.EndAccept(ar);

            // Create the state object.  
            StateObject state = new StateObject();
            state.workSocket = handler;
            handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                new AsyncCallback(ReadCallback), state);
        }

        public void ReadCallback(IAsyncResult ar)
        {
            String content = String.Empty;

            // Retrieve the state object and the handler socket  
            // from the asynchronous state object.  
            StateObject state = (StateObject)ar.AsyncState;
            //Socket handler = state.workSocket;

            // Read data from the client socket.   
            int bytesRead = handler.EndReceive(ar);

            if (bytesRead > 0)
            {
                // There  might be more data, so store the data received so far.  
                state.sb.Clear();
                state.sb.Append(Encoding.ASCII.GetString(
                    state.buffer, 0, bytesRead));

                // Check for end-of-file tag. If it is not there, read   
                // more data.  
                content = state.sb.ToString();

                if(content.Contains("\r\n"))
                {
                    content = content.Substring(0, content.Length - 2);
                }

                string messageType = content;
                string restOfMessage = "";

                if (content.IndexOf(' ') > -1)
                {
                    messageType = content.Substring(0, content.IndexOf(' '));
                    restOfMessage = content.Substring(content.IndexOf(' ') + 1);
                }

                /*
                 * [OPEN TCP CONNECTION]

  ==>  device_list
  <==  R device_list 2 | 9ff167 Empatica_E4 | 7a3166 Empatica_E4

  ==>  device_connect ffffff
  <==  R device_connect ERR the requested device is not available

  ==>  device_connect 9ff167
  <==  R device_connect OK

  ==>  device_subscribe bvp ON
  <==  R device_subscribe bvp OK

  <==  E4_Bvp 123345627891.123 3.212
  <==  E4_Bvp 123345627891.327 10.423
  <==  E4_Bvp 123345627891.472 12.665

  ==>  device_disconnect
  <==  R device_disconnect OK

[EOF]
Protocol Example (Manual BTLE)
[OPEN TCP CONNECTION]

  ==>  device_list
  <==  R device_list 0

  ==>  device_discover_list
  <==  R device_list 2 | 9ff167 Empatica_E4 allowed | 7a3166 Empatica_E4 not_allowed

  ==>  device_connect_btle 7a3166
  <==  R device_connect_btle ERR The device is not registered with the API key

  ==>  device_connect_btle 9ff167
  <==  R device_connect_btle OK

  ==>  device_list
  <==  R device_list 1 | 9ff167 Empatica_E4

  ==>  device_connect 9ff167
  <==  R device_connect OK

  ==>  device_subscribe acc ON
  <==  R device_subscribe acc OK

  <==  E4_Acc 123345627891.123 51 -2 -10
  <==  E4_Acc 123345627891.327 60 -8 -12
  <==  E4_Acc 123345627891.472 55 -16 -1

  ==>  device_disconnect
  <==  R device_disconnect OK
  */

                if (!connected && (!"device_list".Equals(messageType) && !"device_connect".Equals(messageType)))
                {
                    //R device_subscribe bat ERR You are not connected to any device
                    string deviceString = restOfMessage.Substring(0, 3);
                    Send(handler, "R " + messageType + " " + deviceString + " ERROR You are not connected to any device");
                }
                else
                { 
                    switch (messageType)
                    {
                        case "device_subscribe":
                            string device = restOfMessage.Substring(0, restOfMessage.IndexOf(' '));
                            string subscriptionType = restOfMessage.Substring(restOfMessage.IndexOf(' ') + 1);

                            int position = E4Streams.STREAMS_STRINGS.IndexOf(device.ToLower());

                            if (position != -1)
                            {
                                if ("on".Equals(subscriptionType.ToLower()))
                                {
                                    Send(handler, "R device_subscribe " + device.ToLower() + " OK\r\n");
                                    subscriptions[position] = true;
                                }
                                else if ("off".Equals(subscriptionType.ToLower()))
                                {
                                    subscriptions[position] = false;
                                    Send(handler, "R device_subscribe " + device.ToLower() + " OK\r\n");
                                }
                            }

                            break;
                        case "device_connect":
                            if ("CF3864".Equals(restOfMessage))
                            {
                                connected = true;
                                Send(handler, "R device_connect OK\r\n");
                            }
                            else
                            {
                                Send(handler, "R device not available\r\n");
                            }
                            break;
                        case "device_list":

                            Send(handler, "R device_list 1 | CF3864 Empatica_E4\r\n");

                            break;
                        case "device_discover_list":
                            break;
                        case "device_connect_btle":
                            break;
                        case "device_disconnect":
                            if(connected)
                            {
                                Send(handler, "R device_disconnect OK\r\n");

                            }
                            break;
                        case "pause":
                            break;
                        default:
                            Console.WriteLine("Message not recognized");
                            break;
                    }
                }


                /*if (content.IndexOf("<EOF>") > -1)
                {
                    // All the data has been read from the   
                    // client. Display it on the console.  
                    Console.WriteLine("Read {0} bytes from socket. \n Data : {1}",
                        content.Length, content);
                    // Echo the data back to the client.  
                    Send(handler, content);

                    //System.Threading.Thread.Sleep(5000);

                    //Send(handler, content);

                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }
                else
                {
                    // Not all data received. Get more.  
                    handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReadCallback), state);
                }*/

                handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                        new AsyncCallback(ReadCallback), state);
            }
        }

        private void Send(Socket handler, String data)
        {
            // Convert the string data to byte data using ASCII encoding.  
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            // Begin sending the data to the remote device.  
            handler.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), handler);
        }

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                //Socket handler = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.  
                int bytesSent = handler.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to client.", bytesSent);

                //handler.Shutdown(SocketShutdown.Both);
                //handler.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void manageSubscriptions()
        {
            string message = "";
            while (!stop)
            {
                for (int i = 0; i < E4Streams.STREAMS_STRINGS.Count; ++i)
                {
                    if (subscriptions[i])
                    {
                        // Calculate time as Linux time in seconds
                        DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                        TimeSpan diff = DateTime.Now.ToUniversalTime() - origin;
                        double total = diff.TotalSeconds;
                        String timeString = total.ToString();
                        switch (E4Streams.STREAMS_STRINGS[i])
                        {
                            case "acc": //3-axis acceleration
                                message = "E4_Acc " + timeString + " " + sensorValues.acc + " " + sensorValues.acc + " " + sensorValues.acc;
                                break;
                            case "bvp": //Blood Volume Pulse
                                message = "E4_Bvp " + timeString + " " + sensorValues.bvp;
                                break;
                            case "gsr": //Galvanic Skin Response
                                message = "E4_Gsr " + timeString + " " + sensorValues.gsr;
                                break;
                            case "ibi": //Interbeat Interval and Heartbeat
                                message = "E4_Ibi " + timeString + " " + sensorValues.ibi + "\r\nE4_Hr " + timeString + " " + sensorValues.hr;
                                break;
                            case "tmp": //Skin Temperature
                                message = "E4_Temperature " + timeString + " " + sensorValues.tmp;
                                break;
                            case "bat": //Device Battery
                                message = "E4_Battery " + timeString + " " + sensorValues.bat;
                                break;
                            case "tag":
                                message = "E4_Tag " + timeString + " " + sensorValues.tag;
                                break;
                        }
                        Send(handler, message + "\r\n");
                    }
                }

                Thread.Sleep(100);
            }
        }

        public void close()
        {

            allDone.Set();

            if (handler != null)
            {
                handler.Shutdown(SocketShutdown.Both);
                //handler.Close();
            }

            stop = true;
            thread = null;
        }
    }
}
