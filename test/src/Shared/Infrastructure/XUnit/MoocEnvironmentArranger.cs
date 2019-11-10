namespace CodelyTv.Test.Shared.Infrastructure.XUnit
{
    using Arranger;
    using EntityFramework;
    using Mooc.Shared.Infrastructure.Persistence.EntityFramework;

    public class MoocEnvironmentArranger : IEnvironmentArranger
    {
        private readonly MoocContext _context;

        public MoocEnvironmentArranger(MoocContext context)
        {
            this._context = context;
        }

        public void Arrange()
        {
            new DatabaseCleaner().Invoke(_context);
        }

        public void Close()
        {
        }
    }
}