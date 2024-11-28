using TechChallenge.Authentication.Model;

namespace TechChallenge.Authentication.Service
{
    public interface ICognitoService
    {
        Task<ResultadoDto> SignUp(UsuarioDto usuario);
        Task<ResultadoDto<TokenDto>> SignIn(string userName);
    }
}
