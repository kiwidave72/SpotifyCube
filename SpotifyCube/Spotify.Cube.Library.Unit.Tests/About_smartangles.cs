using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;
using Assert = Xunit.Assert;

namespace Spotify.Cube.Library.Unit.Tests
{
    public class About_smartangles
    {
         

        [Theory]
        [InlineData(0, 360, 0, 100, 0, 0)]
        [InlineData(0, 360, 0, 100, 360, 100)]
        [InlineData(0, 360, 0, 100, 3.6, 1)]
        [InlineData(0, 360, 0, 100, 180, 50)]
        public void for_an_input_we_get_the_right_output(double minInputValue, double maxInputValue, double minOutputValue, double maxOutputValue,double inputValue, double expectedOutputValue)
        {
            var angle = new SmartAngle(minInputValue, maxInputValue, minOutputValue, maxOutputValue);
            
            angle.LockWith(0,0);

            angle.ChangeWith(inputValue);

            Assert.True(angle.OutputValue == expectedOutputValue);
        }


    }
}
