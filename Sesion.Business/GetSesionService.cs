using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using RestSharp;
using Generic.Data.Entities;

namespace Sesion.Business
{
    public class GetSesionService
    {
        public bool IsUserValid(string user, string password)
        {
            var options = new RestClientOptions()
            {
                MaxTimeout = 5000000,
            };
            var client = new RestClient(options);
            var request = new RestRequest($"https://localhost:7140/api/Sesion/IsUserValid/{user}/{password}", Method.Get);
            var body = @"";
            request.AddParameter("text/plain", body, ParameterType.RequestBody);

            RestResponse response = client.Execute(request);

            if (string.IsNullOrWhiteSpace(response.Content))
                throw new Exception("No se obtubo respuesta del servidor.");

            var resultadoDTO = JsonConvert.DeserializeObject<ResultadoDTO>(response.Content);

            if (resultadoDTO == null)
                throw new Exception("No se devolvio el formato correcto en la petición.");

            if (!string.IsNullOrWhiteSpace(resultadoDTO.messageError))
                throw new Exception(resultadoDTO.messageError);

            return resultadoDTO.isCompleted;
        }
    }
}