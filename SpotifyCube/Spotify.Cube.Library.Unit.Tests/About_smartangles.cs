using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;
using Assert = Xunit.Assert;

namespace Spotify.Cube.Library.Unit.Tests
{
    public class About_smartangles
    {
         

        [Theory]
        [InlineData(0, 360, 0, 100, 180 , 180, 50, 0)]
        [InlineData(0, 360, 0, 100, 360 , 180, 50, 50)]

        public void for_complex_locked_inputs_we_get_the_right_output(double minInputValue, double maxInputValue, double minOutputValue, double maxOutputValue, double inputValue, double lockedInputValue, double lockedOutputValue, double expectedOutputValue)
        {
            var angle = new SmartAngle(minInputValue, maxInputValue, minOutputValue, maxOutputValue);
            
            angle.LockWith(lockedInputValue,lockedOutputValue);

            angle.ChangeWith(inputValue);

            Assert.True(angle.OutputValue == expectedOutputValue,string.Format("Expected {0} but got {1}" , expectedOutputValue,angle.OutputValue));
        }


        [Theory]
        [InlineData(0, 360, 0, 100, 0, 0, 0, 0)]
        [InlineData(0, 360, 0, 100, 360, 0, 0, 100)]
        [InlineData(0, 360, 0, 100, 3.6, 0, 0, 1)]
        [InlineData(0, 360, 0, 100, 180, 0, 0, 50)]
        public void for_an_simple_input_we_get_the_right_output(double minInputValue, double maxInputValue, double minOutputValue, double maxOutputValue, double inputValue, double lockedInputValue, double lockedOutputValue, double expectedOutputValue)
        {
            var angle = new SmartAngle(minInputValue, maxInputValue, minOutputValue, maxOutputValue);

            angle.LockWith(0, 0);

            angle.ChangeWith(inputValue);

            Assert.True(angle.OutputValue == expectedOutputValue, string.Format("Expected {0} but got {1}", expectedOutputValue, angle.OutputValue));
        }

    }
}
