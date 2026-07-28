namespace LGG.Infraestrutura.Library.Email
{
    public class EmailConteudo
    {
        public static string EmailConteudoCadastro(string endereco, string key)
{
    return $@"
<!DOCTYPE html>
<html lang=""pt-BR"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
</head>
<body style=""margin:0;padding:0;background-color:#131313;font-family:Arial,Helvetica,sans-serif;"">

<table role=""presentation"" cellpadding=""0"" cellspacing=""0"" border=""0"" width=""100%"" style=""background-color:#131313;padding:30px 0;"">
    <tr>
        <td align=""center"">

            <table role=""presentation"" cellpadding=""0"" cellspacing=""0"" border=""0"" width=""460"" style=""background-color:#262626;"">

                <tr>
                    <td align=""center"" style=""padding:20px;background:#131313;"">
                        <img
                            src=""https://www.image2url.com/r2/default/files/1785182321098-e3b971d4-5774-415e-9b5b-c8299f614cef.png""
                            alt=""LGG""
                            width=""180""
                            style=""display:block;border:0;outline:none;text-decoration:none;"">
                    </td>
                </tr>

                <tr>
                    <td style=""padding:35px;color:#FFFFFF;"">

                        <h1 style=""margin:0 0 20px 0;color:#44f5fe;font-size:28px;"">
                            Confirmação de Cadastro
                        </h1>

                        <hr style=""border:none;border-top:1px solid #555;margin:20px 0;"">

                        <p style=""font-size:16px;line-height:24px;margin:0 0 20px 0;"">
                            Bem-vindo à
                            <span style=""font-weight:bold;background-color:#7506f7;"">
                                &nbsp;LIGA GAMER
                            </span>,
                            estamos felizes em recebê-lo.
                        </p>

                        <p style=""font-size:16px;line-height:24px;margin:0 0 30px 0;"">
                            Para confirmar o seu cadastro, clique no botão abaixo:
                        </p>

                        <table role=""presentation"" align=""center"" cellpadding=""0"" cellspacing=""0"" border=""0"">
                            <tr>
                                <td bgcolor=""#44f5fe"" style=""border-radius:25px;"">
                                    <a href=""{endereco}Email/cadastro_confirmacao?key={key}""
                                       target=""_blank""
                                       style=""
                                            display:inline-block;
                                            padding:14px 35px;
                                            color:#000000;
                                            text-decoration:none;
                                            font-weight:bold;
                                            font-size:16px;"">
                                        Confirmar
                                    </a>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>

            </table>

        </td>
    </tr>
</table>
</br>
</br>
</br>
</body>
</html>";
}

    }
}
