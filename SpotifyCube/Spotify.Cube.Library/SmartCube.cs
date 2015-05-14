using System;
using System.Collections.Generic;

namespace Spotify.Cube.Library
{

    public delegate void CubeAngleChangedEventHandler(object sender, CubeAngleEventArgs e);
    public delegate void CubeAclChangedEventHandler(object sender, CubeAclEventArgs e);

    public delegate void CubeGestureChangedEventHandler(object sender, CubeGestureEventArgs e);


    public class SmartCube
    {

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

        public void NewAngle(int x,int y,int z)
        {
            //did we start rotating for volumne
            //qW: 13491.0000 qX: 500.0000 qY: 694.0000 qZ: -9257.0000 accelY: 0.0800 accelX: -0.0600 accelZ: 0.1700
            
            
            OnAngleChanged(new CubeAngleEventArgs(x,y,z) );
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

        public CubeGestureEventArgs(string  gesture)
        {
            Gesture = gesture;
            
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