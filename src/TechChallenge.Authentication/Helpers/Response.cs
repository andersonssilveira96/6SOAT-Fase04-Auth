using Amazon.Lambda.APIGatewayEvents;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.Authentication.Model;

namespace TechChallenge.Authentication.Helpers
{

    public static class Response
    {       

        public static APIGatewayProxyResponse BadRequest(string mensagem)
        {
            var response = new APIGatewayProxyResponse
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Body = JsonConvert.SerializeObject(new ResultadoDto(false, mensagem)),
                Headers = new Dictionary<string, string> {
                { "Content-Type", "application/jsonn"  },
                { "X-Content-Type-Options", "nosniff" },
                { "Strict-Transport-Security", "max-age=1; includeSubDomains; preload" }
            }
            };

            return response;
        }

        public static APIGatewayProxyResponse Ok(object body)
        {
            var response = new APIGatewayProxyResponse
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = JsonConvert.SerializeObject(body),
                Headers = new Dictionary<string, string> {
                { "Content-Type", "application/jsonn"  },
                { "X-Content-Type-Options", "nosniff" },
                { "Strict-Transport-Security", "max-age=1; includeSubDomains; preload" }
            }
            };

            return response;
        }
    }
}
