namespace kursovayaApi.Models.Abstractions
{
    public interface ICommonService<T,K>
    {
        bool Create(T model);
        bool Update(K id, T model);
        bool Delete(K id);
        Task<T> Get(K id);
        Task<IEnumerable<T>> GetAll();
    }
}
