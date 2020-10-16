using System.Threading.Tasks;

namespace CodelyTv.Mooc.CoursesCounters.Domain
{
    public interface ICoursesCounterRepository
    {
        Task Save(CoursesCounter counter);
        Task<CoursesCounter> Search();
    }
}
