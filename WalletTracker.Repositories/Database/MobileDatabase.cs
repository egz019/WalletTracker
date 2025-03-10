namespace WalletTracker.Repositories.Database;

public class MobileDatabase : IMobileDatabase
{
    private readonly string _databaseName;
    protected readonly SQLiteAsyncConnection _dbAsyncConnection;
    protected readonly SQLiteConnection _dbConnection;

    public MobileDatabase(string databaseName)
    {
        _databaseName = databaseName;
        var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), _databaseName);
        _dbAsyncConnection = new SQLiteAsyncConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache);
        _dbConnection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache);
    }

    public Task<T> FirstOrDefaultAsync<T>(Expression<Func<T, bool>> expression) where T : class, IDataObjectBase, new()
        => _dbAsyncConnection.Table<T>().FirstOrDefaultAsync(expression);

    public Task<List<T>> ToListAsync<T>() where T : class, IDataObjectBase, new()
        => _dbAsyncConnection.Table<T>().ToListAsync();
   
    public Task<List<T>> ToListAsync<T>(Expression<Func<T, bool>> expression) where T : class, IDataObjectBase, new()
        => _dbAsyncConnection.Table<T>().Where(expression).ToListAsync();

    public async Task<int> InsertAsync<T>(T item) where T : class, IDataObjectBase, new()
        => await _dbAsyncConnection.InsertAsync(item);

    public async Task<int> InsertAll<T>(IEnumerable<T> list) where T : class, IDataObjectBase, new()
      => await _dbAsyncConnection.InsertAllAsync(list);

    public async Task<int> UpdateAsync<T>(T item) where T : IDataObjectBase
    {
        item.Modified = DateTime.Now;
        return await _dbAsyncConnection.UpdateAsync(item);
    }

    public Task<int> DeleteAllAsync<T>() where T : IDataObjectBase, new()
        => _dbAsyncConnection.DeleteAllAsync<T>();

    public Task<int> DeleteAsync<T>(int id) where T : IDataObjectBase, new()
        => _dbAsyncConnection.DeleteAsync<T>(id);
}
