output "lambda_arn_auth" {
  description = "ARN da lambda"
  value       = aws_lambda_function.techchallenge_lambda_auth.invoke_arn
}

output "lambda_arn_signup" {
  description = "ARN da lambda"
  value       = aws_lambda_function.techchallenge_lambda_signup.invoke_arn
}

output "nome_lambda_auth" {
  description = "Nome da Lambda Auth"
  value       = aws_lambda_function.techchallenge_lambda_auth.function_name
}

output "nome_lambda_signup" {
  description = "Nome da Lambda Signup"
  value       = aws_lambda_function.techchallenge_lambda_signup.function_name
}