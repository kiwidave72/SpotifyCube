using System;
using System.Collections.Generic;

namespace Spotify.Cube.Library
{
    public delegate void CubeAngleChangedEventHandler(object sender, CubeAngleEventArgs e);
    public delegate void CubeAclChangedEventHandler(object sender, CubeAclEventArgs e);

    public delegate void CubeGestureChangedEventHandler(object sender, CubeGestureEventArgs e);


    public class SmartCube
    {
        private double OldXRotation { get; set; }

        public event CubeAngleChangedEventHandler Changed;

        public event CubeAclChangedEventHandler AclChanged;

        public event CubeGestureChangedEventHandler GestureChanged;


        public List<double> historyAcl { get; set; }
        public bool recordingAcl { get; set; }

        
        public SmartCube(double currentX)
        {
            if(currentX <0 )
            {
                Console.WriteLine("default x was less than zero");
                OldXRotation = 0;
            }

            OldXRotation = currentX;

        }

        protected virtual void OnAngleChanged(CubeAngleEventArgs e)
        {
            if (Changed != null)
                Changed(this, e);
        }

        public void NewAngle(double x,double y,double z)
        {

            if (x > 1 || x < -1)
            {

                //did we start rotating for volumne
                //X: 0 Y: -5.19 Z: -2.94 accelY: 0.0100 accelX: 0.0000 accelZ: 0.1400
                if ((OldXRotation < 10 && x > 200) || (OldXRotation > 350 && x < 200))
                {
                    //the rotation rolled over a threshold
                    Console.WriteLine("over threshold");
                    return;
                }

                var change = x - OldXRotation;

                if (Math.Abs(change) < 5)
                {
                    //tolerance value
                    return;
                }

                // somewhere below we hit 360 (0 deg's) which then drops the volume down to spo

                //var absValue = Math.Abs(x - defaultValue);

                var percentage = (x/360)*100;

                OnGestureChange(new CubeGestureEventArgs("Volume", percentage));

                Console.WriteLine(string.Format("volume changed -> {0} deg's is {1}%", x, percentage));

                OldXRotation = x;
            }

            
        }

        public void NewAcl(double x, double y, double z)
        {
            // we started moving up for play/stop??
            if (z > 2 || z < -2)
            {
                OnGestureChange(new CubeGestureEventArgs("Play/Stop"));

                return;
            }

            if (x > 3.5 || x < -3.5 || y > 3.5 || y < -3.5)
            {
                OnGestureChange(new CubeGestureEventArgs("Shuffle"));

                return;
            }


        }

        private void OnAngleAclChanged(CubeAclEventArgs e)
        {
            if (AclChanged != null)
                AclChanged(this, e);
            
        }

        private void OnGestureChange(CubeGestureEventArgs e)
        {
            if (GestureChanged != null)
                GestureChanged(this, e);
        }

        public void InitialiseControllerVolume(float defaultVolume)
        {
            OnGestureChange(new CubeGestureEventArgs("Default Volume", defaultVolume));
        }
    }

    public class CubeGestureEventArgs : EventArgs
    {
        public string Gesture { get; private set; }

        public double Value { get; private set; }

        public CubeGestureEventArgs(string  gesture)
        {
            Gesture = gesture;
        }

        public CubeGestureEventArgs(string gesture, double value)
        {
            Gesture = gesture;
            Value = value;
        }
    }

    public class CubeAclEventArgs : EventArgs
    {

        public double Z_Angle { get; private set; }

        public CubeAclEventArgs(double x, double y, double z)
        {
            Z_Angle = z;
        }
    }

    public class CubeAngleEventArgs : EventArgs
    {

        public int X_Angle { get; private set; }
        public int Y_Angle { get; private set; }
        public int Z_Angle { get; private set; }

        public CubeAngleEventArgs(int x, int y, int z)
        {
            X_Angle = x;
            Y_Angle = y;
            Z_Angle = z;
        }

    }
}