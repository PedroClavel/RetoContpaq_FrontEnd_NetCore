using Generic.Data.Entities;
using Newtonsoft.Json;
using RestSharp;

namespace Alumnos.Business
{
    public class AlumnoCRUDService
    {
        public ResultadoDTO InsertAlumno(string nombres, string apellidoP, string apellidoM, int edad, int grado, string grupo, int? telefono)
        {
            var response = ExecuteApiRest($"https://localhost:7140/api/Alumno/InsertAlumno/{nombres}/{apellidoP}/{apellidoM}/{edad}/{grado}/{grupo}/{telefono}", Method.Post);

            var resultadoDTO = JsonConvert.DeserializeObject<ResultadoDTO>(response);

            if (resultadoDTO == null)
                throw new Exception("No se pudo convertir la respuesta a tipo ResultadoDTO.");

            return resultadoDTO;
        }

        public ResultadoDTO EditAlumno(int idAlumno, string nombres, string apellidoP, string apellidoM, int edad, int grado, string grupo, int? telefono)
        {
            var response = ExecuteApiRest($"https://localhost:7140/api/Alumno/EditAlumno/{idAlumno}/{nombres}/{apellidoP}/{apellidoM}/{edad}/{grado}/{grupo}/{telefono}", Method.Post);

            var resultadoDTO = JsonConvert.DeserializeObject<ResultadoDTO>(response);

            if (resultadoDTO == null)
                throw new Exception("No se pudo convertir la respuesta a tipo ResultadoDTO.");

            return resultadoDTO;
        }

        public ResultadoDTO DeleteAlumno(int idAlumno)
        {
            var response = ExecuteApiRest($"https://localhost:7140/api/Alumno/DeleteAlumno/{idAlumno}/", Method.Post);

            var resultadoDTO = JsonConvert.DeserializeObject<ResultadoDTO>(response);

            if (resultadoDTO == null)
                throw new Exception("No se pudo convertir la respuesta a tipo ResultadoDTO.");

            return resultadoDTO;
        }

        public void ValidateAlumno(string nombres, string apellidoP, string apellidoM, int edad, int grado, string grupo, int? telefono)
        {
            var response = ExecuteApiRest($"https://localhost:7140/api/Alumno/ValidateAlumno/{nombres}/{apellidoP}/{apellidoM}/{edad}/{grado}/{grupo}/{telefono}", Method.Post);

            if (!string.IsNullOrWhiteSpace(response))
                throw new Exception(response);
        }

        private string ExecuteApiRest(string path, Method method)
        {
            var options = new RestClientOptions()
            {
                MaxTimeout = 5000000,
            };
            var client = new RestClient(options);
            var request = new RestRequest(path, method);
            var body = @"";
            request.AddParameter("text/plain", body, ParameterType.RequestBody);

            RestResponse response = client.Execute(request);

            if (string.IsNullOrWhiteSpace(response.Content))
                throw new Exception("No se obtubo respuesta del servidor.");

            return response.Content;
        }
    }
}
