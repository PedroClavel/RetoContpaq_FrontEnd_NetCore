using Generic.Data.Entities;
using Newtonsoft.Json;
using RestSharp;
using System.Text.RegularExpressions;

namespace Alumnos.Business
{
    public class GetAlumnosservice
    {
        public List<Alumno> GetListAlumnos()
        {            
            var response = ExecuteApiRest($"https://localhost:7140/api/Alumno/GetListAlumnos", Method.Get);

            var resultadoDTO = JsonConvert.DeserializeObject<List<Alumno>>(response);

            if (resultadoDTO == null)
                throw new Exception("No se obtubo respuesta del servidor");

            return resultadoDTO;
        }

        public ResultadoDTO GetAlumno(int idAlumno)
        {            
            var response = ExecuteApiRest($"https://localhost:7140/api/Alumno/GetAlumno/{idAlumno}", Method.Get);

            var resultadoDTO = JsonConvert.DeserializeObject<ResultadoDTO>(response);

            if (resultadoDTO == null)
                throw new Exception("No se obtubo respuesta del servidor");

            return resultadoDTO;
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