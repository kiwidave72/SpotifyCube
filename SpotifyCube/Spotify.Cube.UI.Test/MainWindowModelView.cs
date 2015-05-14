using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Cube.Labrary;
using Spotify.Cube.Library;

namespace Cube.Test
{
    public class MainWindowModelView : INotifyPropertyChanged
    {
        public bool IsInitialized=false;

        private int _X_Angle;
        
        private int _Y_Angle;
        
        private int _Z_Angle;
        
        private int _Last_Z_Angle;
        
        private int _Last_X_Angle;
        
        private int _Last_Y_Angle;


        private double _Z_Acel;


        private string _serialMessage;
        private string _gesture;
        private float _defaultVolume;
        private float _volume;

        public SmartCube model { get; set; }
 
        public event PropertyChangedEventHandler PropertyChanged;
         
        public MainWindowModelView ()
        {

            initVolumeSetting = true;


            if (SmartSerialPort.Init() == false)
            {
                return;
            }
            
            IsInitialized = true;

            SmartSerialPort.Changed += SmartSerialPort_Changed; ;

        }

        private void InitSmartCube(double currentX)
        {
            DefaultVolume =(float) (currentX/360)*100;

            model = new SmartCube(currentX);

            model.Changed += cube_Changed;

            model.AclChanged += model_AclChanged;

            model.GestureChanged += model_GestureChanged;

            model.InitialiseControllerVolume(DefaultVolume);
        }


        void model_GestureChanged(object sender, CubeGestureEventArgs e)
        {
            Gesture = e.Gesture;

        }

        public string Gesture {
            get { return _gesture; }
            set
            {
                _gesture = value;
                OnPropertyChanged("Gesture");
            }
        }

        void model_AclChanged(object sender, CubeAclEventArgs e)
        {
            Z_Acel = e.Z_Angle;

        }

        public double Z_Acel {
            get { return _Z_Acel; }
            set
            {
                _Z_Acel = value;
                OnPropertyChanged("Z_Acel");
                
            }
        }

        void SmartSerialPort_Changed(object sender, SmartSerialPortEventArgs e)
        {

            SerialMessage = e.Message;
            try
            {
                var splitArray = e.Message.Split(' ');


                if (initVolumeSetting)
                {
                    InitSmartCube(Convert.ToDouble(splitArray[1])); 
                    
                    

                    initVolumeSetting = false;
                    
                    return;
                }

                if (model != null)
                {
                    model.NewAcl(Convert.ToDouble(splitArray[splitArray.Count() - 5]), Convert.ToDouble(splitArray[splitArray.Count() - 3]), Convert.ToDouble(splitArray[splitArray.Count() - 1]));

                    model.NewAngle(Convert.ToDouble(splitArray[1]), 0, 0);
                    
                }

            }
            catch (System.FormatException )
            {
                
                Console.WriteLine("Bad data");
            }


        }

        public bool initVolumeSetting    { get; set; }

        public string SerialMessage 
        { 

            get { return _serialMessage; }
            set
            {
                _serialMessage = value;
                OnPropertyChanged("SerialMessage");
            }
        }


        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                //Console.WriteLine(propertyName);
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        void cube_Changed(object sender, CubeAngleEventArgs e)
        {
            X_Angle = e.X_Angle;
             
            Z_Angle = e.Z_Angle;
            
            Y_Angle = e.Y_Angle;

         }

        public float DefaultVolume
        {
            get { return _defaultVolume; }
            set
            {
                _defaultVolume = value;
                OnPropertyChanged("DefaultVolume");
            }
        }

        public float Volume
        {
            get { return _volume; }
            set
            {
                _volume = value;
                OnPropertyChanged("Volume");
            }
            
        }


        public int X_Angle
        {
            get
            {
                return _X_Angle;
                
            }

            set
            {
                _X_Angle = value;

                OnPropertyChanged("X_Angle");
            }
        }

        public int Y_Angle
        {
            get
            {
                return _Y_Angle;
                
            }

            set
            {
                _Y_Angle = value;

                OnPropertyChanged("Y_Angle");
            }
        }
        public int Z_Angle
        {
            get
            {
                return _Z_Angle;
                
            }

            set
            {
                _Z_Angle = value;

                OnPropertyChanged("Z_Angle");
            }
        }

        public int Last_X_Angle
        {
            get
            {
                return _Last_X_Angle;

            }

            set
            {
                _Last_X_Angle = value;

                OnPropertyChanged("Last_X_Angle");
            }
        }

        public int Last_Y_Angle
        {
            get
            {
                return _Last_Y_Angle;

            }

            set
            {
                _Last_Y_Angle = value;

                OnPropertyChanged("Last_Y_Angle");
            }
        }

        public int Last_Z_Angle
        {
            get
            {
                return _Last_Z_Angle;

            }

            set
            {
                _Last_Z_Angle = value;

                OnPropertyChanged("Last_Z_Angle");
            }
        }



    }
}
