resource "aws_lambda_function" "techchallenge_lambda_auth" {
  function_name = "techchallenge-lambda-auth"
  filename      = "../TechChallenge.Authentication/auth_lambda.zip"
  handler       = "TechChallenge.Authentication::TechChallenge.Authentication.Function_LambdaAuth_Generated::LambdaAuth"
  runtime       = "dotnet8"
  role          = var.arn
  tags = {
    Name = "techchallenge-lambda"
  }
  timeout     = 30
  memory_size = 512
}

resource "aws_lambda_function" "techchallenge_lambda_signup" {
  function_name = "techchallenge-lambda-signup"
  filename      = "../TechChallenge.Authentication/auth_lambda.zip"
  handler       = "TechChallenge.Authentication::TechChallenge.Authentication.Function_LambdaSignUP_Generated::LambdaSignUP"
  runtime       = "dotnet8"
  role          = var.arn
  tags = {
    Name = "techchallenge-lambda"
  }
  timeout     = 30
  memory_size = 512
}