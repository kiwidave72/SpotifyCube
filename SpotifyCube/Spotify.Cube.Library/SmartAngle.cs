using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Spotify.Cube.Library
{
    public class SmartAngle
    {
        public double MaxOutputValue { get; set; }

        public double MinOutputValue { get; set; }

        public double MaxInputValue { get; set; }

        public double MinInputValue{ get; set; }

        public double OutputValue { get; set; }

        public double LockedOutputValue { get; set; }

        public double LockedInputValue  { get; set; }

        public double NormalizeInputValue { get; set; }

        public double NormalizeOutputValue { get; set; }

        public SmartAngle(double minInputValue, double maxInputValue, double minOutputValue, double maxOutputValue)
        {
            LockWith(0, 0);

            MinInputValue = minInputValue;
            
            MaxInputValue = maxInputValue;

            MinOutputValue = minOutputValue;
            
            MaxOutputValue = maxOutputValue;
            
        }

        public void LockWith(double lockedInputValue, double lockedOutputValue)
        {
            LockedInputValue = lockedInputValue;

            LockedOutputValue = lockedOutputValue;

        }

        public void ChangeWith(double inputvalue)
        {

            var pctOfMaxRange = (double)1 / (double)MaxInputValue;

            var normalizedInputValue= NormalizeInput(inputvalue);

            if (normalizedInputValue != LockedInputValue)
            {
                double value = normalizedInputValue * pctOfMaxRange;

                var outputValue = Math.Round(value * MaxOutputValue);

                var normalizedOutputValue = NormalizeOutput(outputValue);

                OutputValue = normalizedOutputValue;
                
            }
            else
            {
                OutputValue = LockedOutputValue;
            }

        }

        private double NormalizeInput(double inputvalue)
        {
            if (inputvalue == LockedInputValue)
            {
                NormalizeInputValue = 0;
            }

            if (inputvalue > LockedInputValue)
            {
                NormalizeInputValue = inputvalue - LockedInputValue;   
                    
            }
            else
            {
                NormalizeInputValue = LockedInputValue - inputvalue;
            }

            return NormalizeInputValue;
        }

        private double NormalizeOutput(double outputvalue)
        {

            if (outputvalue == LockedOutputValue)
            {
                NormalizeOutputValue = 0;
            }

            if (outputvalue > LockedOutputValue)
            {
                NormalizeOutputValue  =outputvalue - LockedOutputValue;

            }
            else 
            {
                NormalizeOutputValue  = LockedOutputValue - outputvalue;
            }

            return NormalizeOutputValue;

        }
    }


    
}
