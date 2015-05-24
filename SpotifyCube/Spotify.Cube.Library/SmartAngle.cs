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

            double value = normalizedInputValue * pctOfMaxRange;

            var outputValue = Math.Round(value * MaxOutputValue);

            var normalizedOutputValue = NormalizeOutput(outputValue);

            OutputValue = normalizedOutputValue;
                
 
        }

        private double NormalizeInput(double inputvalue)
        {
 
            NormalizeInputValue = inputvalue - LockedInputValue;

            return NormalizeInputValue;
         }

        private double NormalizeOutput(double outputvalue)
        {

          
            NormalizeOutputValue = LockedOutputValue + outputvalue;

            if (NormalizeOutputValue < 0)
                NormalizeOutputValue = 0;

            if(NormalizeOutputValue > MaxOutputValue)
                NormalizeOutputValue = MaxOutputValue;

            
            return  NormalizeOutputValue;
 
        }
    }


    
}
