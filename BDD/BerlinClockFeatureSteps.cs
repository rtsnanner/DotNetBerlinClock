using System;
using TechTalk.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace BerlinClock
{
    [Binding]
    public class TheBerlinClockSteps
    {
        private ITimeConverter berlinClock = new TimeConverter();
        private String theTime;


        [When(@"the time is ""(.*)""")]
        public void WhenTheTimeIs(string time)
        {
            theTime = time;
        }

        [Then(@"the clock should look like")]
        public void ThenTheClockShouldLookLike(string theExpectedBerlinClockOutput)
        {
            Assert.AreEqual(berlinClock.convertTime(theTime), theExpectedBerlinClockOutput);
        }

        [Then(@"an error should be presented to user")]
        public void ThenAnErrorShouldBePresentedToUser()
        {
            Exception trownEx = null;
            try
            {
                berlinClock.convertTime(theTime);
            }
            catch (Exception ex)
            {
               trownEx = ex;                
            }

            Assert.IsInstanceOfType(trownEx, typeof(ArgumentException));
        }

    }
}
