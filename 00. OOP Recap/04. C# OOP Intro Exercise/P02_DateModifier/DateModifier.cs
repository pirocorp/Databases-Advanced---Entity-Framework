namespace P02_DateModifier
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class DateModifier
    {
        private DateTime firstDate;
        private DateTime secondDate;

        public DateModifier()
        {
        }

        public int DayDifference(string firstDateString, string secondDateString)
        {
            this.firstDate = this.ParseDate(firstDateString);
            this.secondDate = this.ParseDate(secondDateString);
            var result = (int)this.firstDate.Subtract(this.secondDate).TotalDays;
            return Math.Abs(result);
        }

        private DateTime ParseDate(string firstDateString)
        {
            var firstDateTokens = firstDateString.Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries);
            var year = int.Parse(firstDateTokens[0]);
            var month = int.Parse(firstDateTokens[1]);
            var day = int.Parse(firstDateTokens[2]);
            var result = new DateTime(year, month, day);
            return result;
        }
    }
}