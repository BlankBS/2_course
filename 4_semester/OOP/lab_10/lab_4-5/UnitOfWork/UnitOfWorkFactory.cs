namespace lab_4_5.UnitOfWork
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        public IUnitOfWork Create() => new UnitOfWork();
    }
}
