module "lambda" {
  source = "./modules/lambda"
  arn    = data.aws_iam_role.name.arn
}

module "apiGateway" {
  source               = "./modules/apiGateway"
  arn_lambda_auth      = module.lambda.lambda_arn_auth
  arn_lambda_signup    = module.lambda.lambda_arn_signup
  environment          = var.environment
  nome_lambda_auth     = module.lambda.nome_lambda_auth
  nome_lambda_signup   = module.lambda.nome_lambda_signup
}


