using System.Text;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Api_core.Models;

namespace Api_core.Servicios
   
{
    public class Servicio_API : IServicios_API
    {
        private static string _usuario;
        private static string _clave;
        private static string _baseUrl;
        private static string _token;

        public Servicio_API()
        {

            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            _usuario = builder.GetSection("ApiSetting:usuario").Value;
            _clave = builder.GetSection("ApiSetting:clave").Value;
            _baseUrl = builder.GetSection("ApiSetting:baseUrl").Value;
        }

        //USAR REFERENCIAS 
        public async Task Autenticar()
        {

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);

            var credenciales = new Credencial() { usuario = _usuario, clave = _clave };

            var content = new StringContent(JsonConvert.SerializeObject(credenciales), Encoding.UTF8, "application/json");
            var response = await cliente.PostAsync("api/Validar", content);
            var json_respuesta = await response.Content.ReadAsStringAsync();

            var resultado = JsonConvert.DeserializeObject<ResultadoCredencial>(json_respuesta);
            _token = resultado.Token;
        }

        public async Task<List<Producto>> Lista()
        {
            List<Producto> lista = new List<Producto>();

            await Autenticar();


            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var response = await cliente.GetAsync("api/Producto/Lista");

            if (response.IsSuccessStatusCode)
            {

                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<ResultadoApi>(json_respuesta);
                lista = resultado.Lista;
            }

            return lista;
        }

        public async Task<Producto> Obtener(int idProducto)
        {
            Producto objeto = new Producto();

            await Autenticar();


            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var response = await cliente.GetAsync($"api/Producto/Obtener/{idProducto}");

            if (response.IsSuccessStatusCode)
            {

                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<ResultadoApi>(json_respuesta);
                objeto = resultado.Objeto;
            }

            return objeto;
        }

        public async Task<bool> Guardar(Producto objeto)
        {
            bool respuesta = false;

            await Autenticar();


            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            var content = new StringContent(JsonConvert.SerializeObject(objeto), Encoding.UTF8, "application/json");

            var response = await cliente.PostAsync("api/Producto/Guardar/", content);

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }

            return respuesta;
        }

        public async Task<bool> Editar(Producto objeto)
        {
            bool respuesta = false;

            await Autenticar();


            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            var content = new StringContent(JsonConvert.SerializeObject(objeto), Encoding.UTF8, "application/json");

            var response = await cliente.PutAsync("api/Producto/Editar/", content);

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }

            return respuesta;
        }

        public async Task<bool> Eliminar(int idProducto)
        {
            bool respuesta = false;

            await Autenticar();


            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);


            var response = await cliente.DeleteAsync($"api/Producto/Eliminar/{idProducto}");

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }

            return respuesta;
        }

        public Task<bool> Eliminar(Producto objeto)
        {
            throw new NotImplementedException();
        }
    }
}
