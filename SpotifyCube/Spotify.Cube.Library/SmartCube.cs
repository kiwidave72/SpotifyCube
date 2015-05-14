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


        protected virtual void OnAngleChanged(CubeAngleEventArgs e)
        {
            if (Changed != null)
                Changed(this, e);
        }

        public void NewAngle(double x,double y,double z)
        {
            //did we start rotating for volumne
            //X: 0 Y: -5.19 Z: -2.94 accelY: 0.0100 accelX: 0.0000 accelZ: 0.1400
            if ((OldXRotation < 10 && x > 350) || (OldXRotation >350 && x < 10))
            {
                //the rotation rolled over a threshold
                return;
            }

            if (x > OldXRotation + 10)
            {
                x = OldXRotation + 10;
            }
            else if (x < OldXRotation - 10)
            {
                x = OldXRotation - 10;
            }

            var change = x - OldXRotation;

            if (change < 5)
            {
                //tolerance value
                return;
            }

            OnGestureChange(new CubeGestureEventArgs("Volume", change));
            //OnAngleChanged(new CubeAngleEventArgs(x,y,z) );
            OldXRotation = x;
        }

        public void NewAcl(double x, double y, double z)
        {
            // we started moving up for play/stop??
            if (z > 1 || z < -1)
            {
                if (recordingAcl )
                {
                    //historyAcl.Add(z);
                
                    OnAngleAclChanged(new CubeAclEventArgs(x, y, z));
                }
                else
                {
                    //historyAcl = new List<double>();
                
                    recordingAcl = true;

                    OnGestureChange(new CubeGestureEventArgs("Play/Stop"));

                }

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