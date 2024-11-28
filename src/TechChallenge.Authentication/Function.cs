using Amazon.Extensions.NETCore.Setup;
using Amazon.Lambda.Annotations;
using Amazon.Lambda.Annotations.APIGateway;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using TechChallenge.Authentication.Helpers;
using TechChallenge.Authentication.Model;
using TechChallenge.Authentication.Service;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace TechChallenge.Authentication;

public class Function
{

    /// <summary>
    /// A simple function that takes a string and does a ToUpper
    /// </summary>
    /// <param name="input">The event for the Lambda function handler to process.</param>
    /// <param name="context">The ILambdaContext that provides methods for logging and describing the Lambda environment.</param>
    /// <returns></returns>
    [LambdaFunction]
    [RestApi(LambdaHttpMethod.Post, "/auth")]
    public async Task<APIGatewayProxyResponse> LambdaAuth(APIGatewayProxyRequest request,
                                                  ILambdaContext context,
                                                  [FromServices] ICognitoService cognitoService,
                                                  [FromServices] IOptions<OptionsDto> awsOptions)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(awsOptions);

            var usuario = ObterUsuario(request, awsOptions.Value, ehCadastro: false);
            var usuarioEhPadrao = usuario.CPF.Equals(awsOptions.Value.UserDefault);
            if (usuarioEhPadrao)
            {
                var resultadoCadastroUsuario = await cognitoService.SignUp(usuario);
                if (!resultadoCadastroUsuario.Sucesso)
                {
                    return Response.BadRequest(resultadoCadastroUsuario.Mensagem);
                }
            }

            var resultadoLogin = await cognitoService.SignIn(usuario.CPF);
            if (!resultadoLogin.Sucesso)
            {
                return Response.BadRequest(resultadoLogin.Mensagem);
            }

            var tokenResult = resultadoLogin.Value!;
            return !string.IsNullOrEmpty(tokenResult.AccessToken) ? Response.Ok(tokenResult) : Response.BadRequest("Não possui token");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    [LambdaFunction]
    [RestApi(LambdaHttpMethod.Post, "/signup")]
    public async Task<APIGatewayProxyResponse> LambdaSignUP(APIGatewayProxyRequest request,
                                                  ILambdaContext context,
                                                  [FromServices] ICognitoService cognitoService,
                                                  [FromServices] IOptions<OptionsDto> awsOptions)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(awsOptions);

            var usuario = ObterUsuario(request, awsOptions.Value, ehCadastro: true);
            
            var resultadoCadastroUsuario = await cognitoService.SignUp(usuario);
            if (!resultadoCadastroUsuario.Sucesso)
            {
                return Response.BadRequest(resultadoCadastroUsuario.Mensagem);
            }

            var resultadoLogin = await cognitoService.SignIn(usuario.CPF);
            if (!resultadoLogin.Sucesso)
            {
                return Response.BadRequest(resultadoLogin.Mensagem);
            }

            var tokenResult = resultadoLogin.Value;
            return !string.IsNullOrEmpty(tokenResult.AccessToken) ? Response.Ok(tokenResult) : Response.BadRequest("Não possui token");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    private UsuarioDto ObterUsuario(APIGatewayProxyRequest request, OptionsDto awsOptions, bool ehCadastro)
    {
        var usuario = JsonConvert.DeserializeObject<UsuarioDto>(request.Body) ?? new UsuarioDto();
        ArgumentNullException.ThrowIfNull(usuario);

        if (!string.IsNullOrEmpty(usuario.CPF) && !ehCadastro)
            return new UsuarioDto(awsOptions.UserDefault, awsOptions.EmailDefault, awsOptions.UserDefault);

        string cpf = usuario.CPF ?? string.Empty;
        string email = usuario.Email ?? string.Empty;
        string nome = usuario.Nome ?? string.Empty;
        var user = new UsuarioDto(cpf, email, nome);
        return user;
    }
}
