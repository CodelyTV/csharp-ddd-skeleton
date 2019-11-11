namespace CodelyTv.Tests.Mooc.Shared.XUnit
{
    using CodelyTv.Mooc.Shared.Infrastructure.Persistence.EntityFramework;
    using Test.Shared.Infrastructure.Arranger;
    using Test.Shared.Infrastructure.EntityFramework;

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