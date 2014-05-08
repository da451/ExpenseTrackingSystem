namespace DAL.Repository
{
    public interface IUoW
    {
        void BeginTransaction();

        void Commit();

        void RollBack();
    }
}
