using Api_core.Models;

namespace Api_core.Servicios
{
    public interface IServicios_API
    {
        //Usamos Polimorfismo por interface por que esta basado por contratos,
        //que son los encargados de decir que puedo hacer o no y como debo de hacerlo.
        //En nuestro ejemplo vamos a crear un contrato que se llamara IServicios_API
        //y definiremos los comportamientos que queremos en nuestra interfaz para
        //luego implementarlo en nuestras clase hijo servicios_API
        Task<List<Producto>> Lista();

        //declaramos todos los metodos para la ejecusión de las API
        Task <Producto> Obtener (int idProducto);

        Task <bool> Eliminar(int idProducto);

        Task <bool> Guardar(Producto objeto);

        Task <bool> Editar(Producto objeto);

    }
}
