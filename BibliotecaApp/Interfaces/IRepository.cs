namespace BibliotecaApp.Interfaces;

public interface IRepository<T>
{
    void Agregar(T entidad);

    List<T> ObtenerTodos();

    T? BuscarPorId(int id);

    void Eliminar(int id);
}