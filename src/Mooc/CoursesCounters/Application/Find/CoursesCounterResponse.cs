using System;

namespace CodelyTv.Mooc.CoursesCounters.Application.Find
{
    public class CoursesCounterResponse
    {
        public int Total { get; }

        public CoursesCounterResponse(int total)
        {
            Total = total;
        }

        public override bool Equals(object obj)
        {
            if (this == obj) return true;

            var item = obj as CoursesCounterResponse;
            if (item == null) return false;

            return Total == item.Total;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Total);
        }
    }
}
