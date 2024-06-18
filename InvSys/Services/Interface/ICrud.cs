namespace InvSys.Services.Interface;
public interface ICrudService<T>
{
    List<T> GetAll();
    T GetById(int id);
    void Add(T entity);
    void Update(T entity);
    void Delete(int id);
}

