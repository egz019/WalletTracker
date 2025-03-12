using System.Linq.Expressions;
namespace WalletTracker.Repositories.Interfaces;

public interface IMobileDatabase
{
    Task<T> FirstOrDefaultAsync<T>(Expression<Func<T, bool>> expression) where T : class, IDataObjectBase, new();
    Task<List<T>> ToListAsync<T>() where T : class, IDataObjectBase, new();
    Task<List<T>> ToListAsync<T>(Expression<Func<T, bool>> expression) where T : class, IDataObjectBase, new();
    Task<int> InsertAsync<T>(T item) where T : class, IDataObjectBase, new();
    Task<int> DeleteAllAsync<T>() where T : IDataObjectBase, new();
    Task<int> DeleteAsync<T>(int id) where T : IDataObjectBase, new();
    Task<int> UpdateAsync<T>(T item) where T : IDataObjectBase;
    Task<int> InsertAllAsync<T>(IEnumerable<T> list) where T : class, IDataObjectBase, new();
    
    void InsertAll<T>(IEnumerable<T> list) where T : class, IDataObjectBase, new();
    Task<int> InsertOrUpdateAsync<T>(T item) where T : class, IDataObjectBase, new();
    T FirstOrDefault<T>(Expression<Func<T, bool>> expression) where T : class, IDataObjectBase, new();
    int Insert<T>(T item) where T : class, IDataObjectBase, new();
    int InsertOrUpdate<T>(T item) where T : class, IDataObjectBase, new();
}
