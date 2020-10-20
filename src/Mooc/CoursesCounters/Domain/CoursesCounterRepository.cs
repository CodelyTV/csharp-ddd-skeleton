using System.Threading.Tasks;

namespace CodelyTv.Mooc.CoursesCounters.Domain
{
    public interface CoursesCounterRepository
    {
        Task Save(CoursesCounter counter);
        Task<CoursesCounter> Search();
    }
}
