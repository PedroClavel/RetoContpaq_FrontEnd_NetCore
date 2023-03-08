using Microsoft.AspNetCore.Mvc.RazorPages;
using Sesion.Business;

namespace RegistroDeAlumnosWebCore.Views
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public bool isUserValid = false;


        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGetValidateUser(string user, string password)
        {

            GetSesionService sesionService = new GetSesionService();

            isUserValid = sesionService.IsUserValid(user, password);
        }
    }
}