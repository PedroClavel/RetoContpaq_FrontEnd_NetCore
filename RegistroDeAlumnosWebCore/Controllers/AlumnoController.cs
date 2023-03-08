using Alumnos.Business;
using Generic.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sesion.Business;

namespace RegistroDeAlumnosWebCore.Controllers
{
    public class AlumnoController : Controller
    {
        public IActionResult ListaAlumnos()
        {
            var listaAlumno = new List<Alumno>();
            try
            {
                var alumnoService = new GetAlumnosservice();

                listaAlumno = alumnoService.GetListAlumnos();
            }
            catch (Exception)
            {

                throw;
            }

            return View(listaAlumno);
        }

        [HttpGet]
        public string GetAlumno(int idAlumno)
        {
            JObject resultadoJson = new JObject();
            var resultadoDTO = new ResultadoDTO();

            try
            {
                var alumnoService = new GetAlumnosservice();

                resultadoDTO = alumnoService.GetAlumno(idAlumno);

                resultadoJson.Add("isCompleted", resultadoDTO.isCompleted);
                resultadoJson.Add("message", resultadoDTO.messageError);
                resultadoJson.Add("alumno", JsonConvert.SerializeObject(resultadoDTO.alumno));
            }
            catch (Exception error)
            {
                resultadoJson.Add("isUserValid", false);
                resultadoJson.Add("message", error.Message);
            }

            return resultadoJson.ToString(Newtonsoft.Json.Formatting.None);
        }

        [HttpPost]
        public string InsertAlumno(string nombres, string apellidoP, string apellidoM, int edad, int grado, string grupo, int? telefono)
        {
            JObject resultadoJson = new JObject();
            var resultadoDTO = new ResultadoDTO();

            try
            {
                var alumnoService = new AlumnoCRUDService();

                resultadoDTO = alumnoService.InsertAlumno(nombres, apellidoP, apellidoM, edad, grado, grupo, telefono);

                resultadoJson.Add("isValid", resultadoDTO.isCompleted);
                resultadoJson.Add("message", resultadoDTO.messageError);
            }
            catch (Exception error)
            {
                resultadoJson.Add("isValid", false);
                resultadoJson.Add("message", error.Message);
            }

            return resultadoJson.ToString(Newtonsoft.Json.Formatting.None);
        }

        [HttpPost]
        public string EditAlumno(int idAlumno, string nombres, string apellidoP, string apellidoM, int edad, int grado, string grupo, int? telefono)
        {
            JObject resultadoJson = new JObject();
            var resultadoDTO = new ResultadoDTO();

            try
            {
                var alumnoService = new AlumnoCRUDService();

                resultadoDTO = alumnoService.EditAlumno(idAlumno, nombres, apellidoP, apellidoM, edad, grado, grupo, telefono);

                resultadoJson.Add("isValid", resultadoDTO.isCompleted);
                resultadoJson.Add("message", resultadoDTO.messageError);
            }
            catch (Exception error)
            {
                resultadoJson.Add("isValid", false);
                resultadoJson.Add("message", error.Message);
            }

            return resultadoJson.ToString(Newtonsoft.Json.Formatting.None);
        }

        [HttpPost]
        public string DeleteAlumno(int idAlumno)
        {
            JObject resultadoJson = new JObject();
            var resultadoDTO = new ResultadoDTO();

            try
            {
                var alumnoService = new AlumnoCRUDService();

                resultadoDTO = alumnoService.DeleteAlumno(idAlumno);

                resultadoJson.Add("isValid", resultadoDTO.isCompleted);
                resultadoJson.Add("message", resultadoDTO.messageError);
            }
            catch (Exception error)
            {
                resultadoJson.Add("isValid", false);
                resultadoJson.Add("message", error.Message);
            }

            return resultadoJson.ToString(Newtonsoft.Json.Formatting.None);
        }

        [HttpPost]
        public string ValidateAlumno(string nombres, string apellidoP, string apellidoM, int edad, int grado, string grupo, int? telefono)
        {
            JObject resultadoJson = new JObject();

            try
            {
                var alumnoService = new AlumnoCRUDService();

                alumnoService.ValidateAlumno(nombres, apellidoP, apellidoM, edad, grado, grupo, telefono);

                resultadoJson.Add("isValid", true);
                resultadoJson.Add("message", string.Empty);
            }
            catch (Exception error)
            {
                resultadoJson.Add("isValid", false);
                resultadoJson.Add("message", error.Message);
            }

            return resultadoJson.ToString(Newtonsoft.Json.Formatting.None);
        }
    }
}
