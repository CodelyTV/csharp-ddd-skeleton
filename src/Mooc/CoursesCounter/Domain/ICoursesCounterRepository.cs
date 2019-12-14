namespace CodelyTv.Mooc.CoursesCounter.Domain
{
    using System.Threading.Tasks;

    public interface ICoursesCounterRepository
    {
        Task Save(CoursesCounter counter);
        CoursesCounter Search();
    }
}