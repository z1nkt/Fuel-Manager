namespace Fuel.Manager.Server.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IList<T> GetAll();
        T GetById(int id);
        void Create (T entity);
        void Update (T entity);
        void Delete (T entity);

    }
}
