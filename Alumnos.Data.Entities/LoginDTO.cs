namespace Generic.Data.Entities
{
    public class LoginDTO
    {
        public LoginDTO()
        {
            idLogin = 0;
            usuario = string.Empty;
            pass = string.Empty;
            fechaAlta = new DateTime();
            fechaModificacion = null;
        }

        public int idLogin { get; set; }

        public string usuario { get; set; } = null!;

        public string pass { get; set; } = null!;

        public DateTime fechaAlta { get; set; }

        public DateTime? fechaModificacion { get; set; }
    }
}
