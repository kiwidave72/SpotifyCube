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

        public double LockedOuputValue { get; set; }

        public double LockedInputValue  { get; set; }

        public SmartAngle(double minInputValue, double maxInputValue, double minOutputValue, double maxOutputValue)
        {
            LockWith(0, 0);

            MinInputValue = minInputValue;
            
            MaxInputValue = maxInputValue;

            MinOutputValue = minOutputValue;
            
            MaxOutputValue = maxOutputValue;
            
        }

        public void LockWith(double volume, double cubeAngle)
        {
            LockedInputValue = volume;

            LockedOuputValue = cubeAngle;

        }

        public void ChangeWith(double inputvalue)
        {

            var pctOfMaxRange = (double)1 / (double)MaxInputValue;

            var normalizedInputValue= NormalizeInputValue(inputvalue);

            double value = normalizedInputValue * pctOfMaxRange;

            var outputValue = Math.Round(value * MaxOutputValue);

            var normalizedOutputValue = NormalizeOuputValue(outputValue);

            OutputValue = normalizedOutputValue;

        }

        private double NormalizeInputValue(double inputvalue)
        {
            return inputvalue;
        }

        private double NormalizeOuputValue(double outputvalue)
        {
            return outputvalue;
        }

    }


    
}
