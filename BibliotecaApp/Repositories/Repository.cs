using BibliotecaApp.Interfaces;

namespace BibliotecaApp.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly List<T> _datos = new();

    public void Agregar(T entidad)
    {
        _datos.Add(entidad);
    }

    public List<T> ObtenerTodos()
    {
        return _datos;
    }

    public T? BuscarPorId(int id)
    {
        var propiedadId = typeof(T).GetProperty("Id");

        return _datos.FirstOrDefault(x =>
            propiedadId != null &&
            (int)(propiedadId.GetValue(x) ?? 0) == id);
    }

    public void Eliminar(int id)
    {
        var entidad = BuscarPorId(id);

        if (entidad != null)
        {
            _datos.Remove(entidad);
        }
    }
}