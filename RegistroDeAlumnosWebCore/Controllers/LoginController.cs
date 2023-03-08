using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Sesion.Business;

namespace RegistroDeAlumnosWebCore.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public string ValidateLogin(string usuario, string pass)
        {
            var isUserValid = false;
            JObject resultadoJson = new JObject();

            try
            {
                var sesionService = new GetSesionService();

                isUserValid = sesionService.IsUserValid(usuario, pass);

                resultadoJson.Add("isUserValid", isUserValid);
                resultadoJson.Add("message", string.Empty);
            }
            catch (Exception error)
            {
                resultadoJson.Add("isUserValid", false);
                resultadoJson.Add("message", error.Message);
            }

            return resultadoJson.ToString(Newtonsoft.Json.Formatting.None);
        }       
    }
}