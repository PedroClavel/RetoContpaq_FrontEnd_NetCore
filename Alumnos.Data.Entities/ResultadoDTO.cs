namespace Generic.Data.Entities
{
    public class ResultadoDTO
    {
        public ResultadoDTO()
        {
            this.isCompleted = false;
            this.messageError = string.Empty;
            alumno = new Alumno();
        }

        public bool isCompleted { get; set; }

        public string messageError { get; set; }

        public Alumno alumno { get; set; }
    }
}
