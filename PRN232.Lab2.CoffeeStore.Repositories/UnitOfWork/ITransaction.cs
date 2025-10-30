namespace PRN232.Lab2.CoffeeStore.Repositories.UnitOfWork
{
    public interface ITransaction : IAsyncDisposable
    {
        Task CommitAsync();
        Task RollbackAsync();
    }
}
