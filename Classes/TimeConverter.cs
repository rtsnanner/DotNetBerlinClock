using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

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
            //initializes clock parts
            char TopLight;
            char[] FirstRow = new String('O', 4).ToArray();
            char[] SecondRow = new String('O', 4).ToArray();
            char[] ThirdRow = new String('O', 11).ToArray();
            char[] FourthRow = new String('O', 4).ToArray();

            //validates input
            if (!Regex.IsMatch(aTime, "[0-2][0-9]:[0-5][0-9]:[0-5][0-9]"))
                throw new ArgumentException("invalid time");

            //split time string into integer parts
            var splitedTime = aTime.Split(':').Select(x => int.Parse(x)).ToArray();

            //perform another validation in hour locus
            if(splitedTime[0]>24)
                throw new ArgumentException("invalid time");

            //Top light is on when seconds value is even
            TopLight = splitedTime[2] % 2 == 0 ? 'Y' : 'O';

            //Fills rows
            FillFirstRow(FirstRow, splitedTime[0]);
            FillSecondRow(SecondRow, splitedTime[0]);
            FillThirdRow(ThirdRow, splitedTime[1]);
            FillFourthRow(FourthRow, splitedTime[1]);

            return FormatToBerlinClock(TopLight, FirstRow, SecondRow, ThirdRow, FourthRow);
        }

        /// <summary>
        /// Fills the first row of the clock applying 'R' for the positions that must be on depending on the hours.            
        /// </summary>
        /// <param name="row">The row to be filled</param>
        /// <param name="hours">The amount of hours in time</param>
        private void FillFirstRow(char[] row, int hours)
        {
            for (int i = 0; i < hours / 5; i++)
            {
                row[i] = 'R';
            }
        }

        /// <summary>
        /// Fills a row using 'R' representing places that are turned on.            
        /// </summary>
        /// <param name="row">The row to be filled</param>
        /// <param name="hours"></param>
        private void FillSecondRow(char[] row, int hours)
        {
            //The rule for second row is using mod operator in hours
            for (int i = 0; i < hours % 5; i++)
            {
                row[i] = 'R';
            }
        }

        /// <summary>
        /// Fills a row applying 'R' or 'Y' for the positions that must be on depending on the minutes.            
        /// </summary>
        /// <param name="row">The row to be filled</param>
        /// <param name="minutes">The amount of minutes in time</param>
        private void FillThirdRow(char[] row, int minutes)
        {
            for (int i = 0; i < minutes / 5; i++)
            {
                row[i] = (i + 1) % 3 == 0 ? 'R' : 'Y';
            }
        }

        /// <summary>
        /// Fills a row applying 'Y' for the positions that must be on depending on the minutes.            
        /// </summary>
        /// <param name="row">The row to be filled</param>
        /// <param name="minutes">The amount of minutes in time</param>
        private void FillFourthRow(char[] row, int minutes)
        {
            //same as second row. using mod.
            for (int i = 0; i < minutes % 5; i++)
            {
                row[i] = 'Y';
            }
        }

        /// <summary>
        /// Creates a string representation for a berlin clock time based on values of top light and its rows values
        /// </summary>
        private string FormatToBerlinClock(char TopLight, char[] FirstRow, char[] SecondRow, char[] ThirdRow, char[] FourthRow)
        {
            var sb = new StringBuilder();

            sb.AppendLine(TopLight.ToString());
            sb.AppendLine(string.Join("", FirstRow));
            sb.AppendLine(string.Join("", SecondRow));
            sb.AppendLine(string.Join("", ThirdRow));
            sb.Append(string.Join("", FourthRow));

            return sb.ToString();
        }
    }

}
