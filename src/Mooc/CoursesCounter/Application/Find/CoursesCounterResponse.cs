namespace CodelyTv.Mooc.CoursesCounter.Application.Find
{
    using System;

    public class CoursesCounterResponse
    {
        public int Total { get; private set; }

        public CoursesCounterResponse(int total)
        {
            Total = total;
        }

        public override bool Equals(object obj)
        {
            if (this == obj) return true;

            var item = obj as CoursesCounterResponse;
            if (item == null) return false;

            return this.Total == item.Total;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Total);
        }
    }
}