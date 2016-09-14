using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cube.Labrary
{
    public delegate void SerialMessageChangedEventHandler(object sender, SmartSerialPortEventArgs e);


    public class SmartSerialPortEventArgs : EventArgs
    {

        public string Message { get; set; }
    }

    public static class SmartSerialPort
    {
        private const bool _testMode = false;

        public static string PortName { get; set; }

        public static bool IsConnected { get; set; }

        public static bool IsSerialPortOpen { get; set; }

        public static SerialPort _serialPort { get; set; }


        public static Thread readThread = new Thread(Read);

        private static bool _continue;
        
        public static string readLine { get; set; }

        public static string ErrorMessage { get; set; }

        public static event SerialMessageChangedEventHandler Changed;

        public static void OnMessageChanged(SmartSerialPortEventArgs e)
        {
            if (Changed != null)
                Changed(null, e);
        }
 
        public static bool Init()
        {

            PortName = "COM22";

            _continue = true;

            ConnectPort();

            if (IsConnected)
            {
                readThread.Start();

                return true;
            }
            else
            {
                
                return false;
            }
        }
 
        public static void Read()
        {
            while (_continue)
            {
                try
                {
                    if (_serialPort !=null && _serialPort.IsOpen == false)
                    {
                        return;
                    }


                    string line = _serialPort.ReadLine();

                    var args = new SmartSerialPortEventArgs();

                    if (line.Length > 63)
                    {

                        args.Message = line;

                        OnMessageChanged(args);
                    }


                }
                catch (TimeoutException)
                {
                    _continue = false;
                    return;
                }
                catch (InvalidOperationException)
                {
                }

            }
        }
 
        private static void ConnectPort()
        {
                 try
                {
                    _serialPort = new SerialPort();
                    IsSerialPortOpen = _serialPort.IsOpen;


                    foreach (string s in SerialPort.GetPortNames())
                    {
                        Debug.WriteLine("   {0}", s);
                    }

                    _serialPort.PortName = PortName; //SerialPort.GetPortNames()[1];
                    _serialPort.BaudRate = 9600;
                    _serialPort.Parity = Parity.None;
                    _serialPort.DataBits = 8;
                    _serialPort.StopBits = StopBits.One;
                    _serialPort.Handshake = Handshake.None;
                    _serialPort.ReadTimeout = 500;
                    _serialPort.WriteTimeout = 500;

                    _serialPort.Open();

                }
                catch (IndexOutOfRangeException ex)
                {
                }
                catch (IOException ex)
                {
                    Console.WriteLine(ex.Message);

                    ErrorMessage =
                        "Unable to connect to bluetooth or serial connections, check the port number or that the device is working.";

                    IsConnected = false;

                    return;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                    IsConnected = false;

                    return;
                }

                IsConnected = true;
 
        }


        public static void Close()
        {
            _serialPort.Close();
        }
    }

}
