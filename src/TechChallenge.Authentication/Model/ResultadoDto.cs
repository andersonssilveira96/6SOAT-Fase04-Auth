namespace TechChallenge.Authentication.Model
{
    public class ResultadoDto
    {
        public ResultadoDto(bool sucesso, string mensagem = "")
        {           
            Sucesso = sucesso;
            Mensagem = mensagem;
        }

        public bool Sucesso { get; }
        public string Mensagem { get; }
        public bool Falhou => !Sucesso;

        public static ResultadoDto Falha(string mensagem)
        {
            return new ResultadoDto(false);
        }

        public static ResultadoDto<T> Falha<T>(string mensagem)
        {
            return new ResultadoDto<T>(default, false, mensagem);
        }      
      
        public static ResultadoDto Ok()
        {
            return new ResultadoDto(true);
        }

        public static ResultadoDto<T> Ok<T>(T value)
        {
            return new ResultadoDto<T>(value, true);
        }
    }
    public class ResultadoDto<T> : ResultadoDto
    {
        protected internal ResultadoDto(T value, bool sucesso, string mensagem = "")
            : base(sucesso, mensagem)
        {
            Value = value;
        }

        public T Value { get; set; }
    }
}
