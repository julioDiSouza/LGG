namespace EncurtadorLink.Shared.Library.Classes
{
    public static class EmailBody
    {
        public static string EmailResetStringBody(string endereco, string email, string emailToken)
        {
            return 
$@" 
<html>
<head>
</head>
<body style=""margin:0; font-family: Arial, Helvetica, sans-serif;"">
<div style=""width:400px; height: auto; margin: 0 auto; margin-top: 30px; padding:30px; background-color: #f7f7f7; border: 1px solid #dddddd;"">
<div>
<h1>Atualizar Senha</h1>
<hr>
<p>Você está recebendo este e-mail porque solicitou atualizar sua senha no site encurtador de links <strong>l4u</strong>.</p>
<p>Por favor pressione o botão abaixo para definir sua nova senha:</p>
<a href=""{endereco}/reset?code={emailToken}"" target=""_blank"" style=""background:#0d6efd;padding:10px;border:none; 
color:white;border-radius:20px;display:block;margin:0 auto;width:50%;text-align:center;text-decoration:none"">Atualizar Senha</a>
<br>
<p>
Esta opção estará disponível apenas por <b>15 minutos</b>.
</p>
<p>
Considerações, 
</p>
<p>
Equipe l4u.
</p>
</div>
</div>
<div style=""width:460px;height: 5px;margin: 0 auto; border: 1px solid #dddddd; margin-top: -2px"">
<div style=""width:153px; height: 5px;background-color: #2C87C5;float:left"">
</div>
<div style=""width:153px; height: 5px;background-color: #F2B505; float:left"">
</div>
<div style=""width:154px; height: 5px;background-color: #32A34F; float:left"">
</div>
</div>
</body>
</html> ";

        }

        public static string EmailConfirmStringBody(string endereco, string email, string emailToken)
        {
            return
$@" 
<html>
<head>
</head>
<body style=""margin:0; font-family: Arial, Helvetica, sans-serif;"">
<div style=""width:400px; height: auto; margin: 0 auto; margin-top: 30px; padding:30px; background-color: #f7f7f7; border: 1px solid #dddddd;"">
<div>
<h1>Confirmar Cadastro</h1>
<hr>
<p>Obrigado por utilizar o site encurtador de links <strong>l4u</strong>.</p>
<p>Por favor pressione o botão abaixo para confirmar seu cadastro:</p>
<a href=""{endereco}/confirm?code={emailToken}"" target=""_blank"" style=""background:#0d6efd;padding:10px;border:none; 
color:white;border-radius:20px;display:block;margin:0 auto;width:50%;text-align:center;text-decoration:none"">Confirmar</a>
<br>
<p>
Considerações, 
</p>
<p>
Equipe l4u.
</p>
</div>
</div>
<div style=""width:460px;height: 5px;margin: 0 auto; border: 1px solid #dddddd; margin-top: -2px"">
<div style=""width:153px; height: 5px;background-color: #2C87C5;float:left"">
</div>
<div style=""width:153px; height: 5px;background-color: #F2B505; float:left"">
</div>
<div style=""width:154px; height: 5px;background-color: #32A34F; float:left"">
</div>
</div>
</body>
</html> ";

        }
    }
}
