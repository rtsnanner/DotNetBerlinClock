using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BerlinClock
{
    public class TimeConverter : ITimeConverter
    {
        /// <summary>
        /// Given a time in format 'HH:mm:ss' it returns a Berlin Clock formatted time where each row represent a piece of clock
        /// with value 'O' for light off and 'R' or 'Y' for light turned on
        /// </summary>
        /// <param name="aTime"></param>
        /// <returns></returns>
        public string convertTime(string aTime)
        {
            return new MyBerlinClock(aTime).ToString();
        }

        /// <summary>
        /// This class separates all pieces of berlin clock where reach line is represented by a variable
        /// </summary>
        private class MyBerlinClock
        {
            private char TopLight;
            private char[] FirstRow;
            private char[] SecondRow;
            private char[] ThirdRow;
            private char[] FourthRow;

            /// <summary>
            /// Receives a string in format "HH:mm:ss" and parses it to a int array.
            /// Then uses it to fill each row of the clock
            /// </summary>
            /// <param name="aTime">The time in format "HH:mm:ss"</param>
            public MyBerlinClock(string aTime)
            {
                //split time string into integer parts
                var splitedTime = aTime.Split(':').Select(x => int.Parse(x)).ToArray();
                
                //Top light is on when seconds value is odd
                TopLight = splitedTime[2] % 2 == 0 ? 'Y' : 'O';

                FillFirstRow(splitedTime[0]);
                FillSecondRow(splitedTime[0]);
                FillThirdRow(splitedTime[1]);
                FillFourthRow(splitedTime[1]);
            }

            /// <summary>
            /// Fills the first row of the clock initializing FirstRow array with value 'O' 
            /// then applying 'R' for the positions that must be on depending on the hours.            
            /// </summary>
            /// <param name="hours">The amount of hours in time</param>
            private void FillFirstRow(int hours)
            {
                FirstRow = new String('O', 4).ToArray();

                for (int i = 0; i < hours / 5; i++)
                {
                    FirstRow[i] = 'R';
                }
            }

            /// <summary>
            /// Initializes SecondRow with 'O' in all positions then fills it using 'R' representing places that are turned on.            
            /// </summary>
            /// <param name="hours"></param>
            private void FillSecondRow(int hours)
            {
                SecondRow = new String('O', 4).ToArray();

                //The rule for second row is using mod operator in hours
                for (int i = 0; i < hours % 5; i++)
                {
                    SecondRow[i] = 'R';
                }
            }

            /// <summary>
            /// Fills the third row of the clock initializing ThirdRow array with value 'O' 
            /// then applying 'R' or 'Y' for the positions that must be on depending on the minutes.            
            /// </summary>
            /// <param name="minutes">The amount of minutes in time</param>
            private void FillThirdRow(int minutes)
            {
                ThirdRow = new String('O', 11).ToArray();

                for (int i = 0; i < minutes / 5; i++)
                {
                    ThirdRow[i] = (i+1) % 3 == 0 ? 'R' : 'Y';
                }
            }

            /// <summary>
            /// Fills the fourth row of the clock initializing FourthRow array with value 'O' 
            /// then applying 'Y' for the positions that must be on depending on the minutes.            
            /// </summary>
            /// <param name="minutes">The amount of minutes in time</param>
            private void FillFourthRow(int minutes)
            {
                FourthRow = new String('O', 4).ToArray();

                //same as second row. using mod.
                for (int i = 0; i < minutes % 5; i++)
                {
                    FourthRow[i] = 'Y';
                }
            }

            /// <summary>
            /// The to string method is overriden in order to actually represent the time in berlin clock for the expected format
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                var sb = new StringBuilder();

                sb.AppendLine(TopLight.ToString());
                sb.AppendLine(string.Join("",FirstRow));
                sb.AppendLine(string.Join("", SecondRow));
                sb.AppendLine(string.Join("", ThirdRow));
                sb.Append(string.Join("", FourthRow));

                return sb.ToString();

            }
        }
    }
}
