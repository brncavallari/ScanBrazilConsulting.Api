using Domain.Commands.v1.TimeOff.Create;
using Domain.Commands.v1.TimeOff.Reject;

namespace Domain.Templates.v1;
public static class EmailTemplate
{
    public static readonly string Subject = "Solicitação de Folga - Scan Brazil Consulting";
    public static readonly string SubjectRejected = "Solicitação de Folga - [Negado]";
    public static readonly string SubjectApproved = "Solicitação de Folga - [Aprovado]";
    public static string TimeOff(
        CreateTimeOffCommand createTimeOffCommand,
        string protocol)
    {
        return $@"<!DOCTYPE html>
                <html lang=""pt-BR"">
                <head>
                <meta charset=""UTF-8"">
                <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                <title>Solicitação de Folga</title>
                <style type=""text/css"">
                
                body, table, td, a {{ -webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; }}
                table, td {{ mso-table-lspace: 0pt; mso-table-rspace: 0pt; }}
                img {{ -ms-interpolation-mode: bicubic; border: 0; height: auto; line-height: 100%; outline: none; text-decoration: none; }}
                       .info-card {{ border-left: 4px solid #667eea; }}
                   </style>
                </head>

                <body style=""font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; line-height: 1.6; color: #333; margin: 0; padding: 0; background-color: #f6f9fc;"">
                   <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""background-color: #f6f9fc;"">
                       <tr>
                           <td align=""center"" style=""padding: 20px 0;"">
                               <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""600"" class=""email-container"" style=""max-width: 600px; margin: 0 auto; background: #ffffff; border-radius: 12px; overflow: hidden;"">
                   
                                   <tr>
                                       <td class=""header"" align=""center"" style=""background-color: #667eea; background: linear-gradient(135deg, #667eea 0%, #764ba2 100%); color: white; padding: 30px 20px;"">
                                           <h1 style=""margin: 0; font-size: 28px; font-weight: 600; color: white;"">📋 Solicitação de Folga</h1>
                                       </td>
                                   </tr>
                   
                                   <tr>
                                       <td class=""content"" style=""padding: 40px 30px;"">
                                           <p>Prezado(a) Administrador(a),</p>
                   
                                           <p>Você recebeu uma nova solicitação de folga que requer sua aprovação.</p>
                                           <p><b> Protocolo: {protocol} </b></p>

                                           <div class=""info-card"" style=""background: #f8f9fa; border-left: 4px solid #667eea; padding: 20px; border-radius: 4px; margin: 25px 0;"">
                                               <h3 style=""margin-top: 0; color: #333;"">📝 Detalhes da Solicitação</h3>
                       
                                               <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""margin: 20px 0;"">
                                                   <tr>
                                                       <td style=""width: 50%; padding-right: 7.5px;"">
                                                           <div class=""detail-item"" style=""background: #e9efff; padding: 15px; border-radius: 6px;"">
                                                               <strong style=""color: #555;"">👤 Solicitante:</strong><br>
                                                                   {createTimeOffCommand.UserEmail}
                                                           </div>
                                                       </td>
                                                       <td style=""width: 50%; padding-left: 7.5px;"">
                                                           <div class=""detail-item"" style=""background: #e9efff; padding: 15px; border-radius: 6px;"">
                                                               <strong style=""color: #555;"">📅 Data da Solicitação:</strong><br>
                                                              {DateTime.Now}
                                                           </div>
                                                       </td>
                                                   </tr>
                                                   <tr>
                                                       <td colspan=""2"" height=""15""></td> 
                                                   </tr>
                                                   <tr>
                                                       <td style=""width: 50%; padding-right: 7.5px;"">
                                                           <div class=""detail-item"" style=""background: #e9efff; padding: 15px; border-radius: 6px;"">
                                                               <strong style=""color: #555;"">🗓️ Período Solicitado:</strong><br>
                                                               {createTimeOffCommand.StartDate:dd/MM/yyyy} a {createTimeOffCommand.EndDate:dd/MM/yyyy}
                                                           </div>
                                                       </td>
                                                       <td style=""width: 50%; padding-left: 7.5px;"">
                                                           <div class=""detail-item"" style=""background: #e9efff; padding: 15px; border-radius: 6px;"">
                                                               <strong style=""color: #555;"">⏰ Horas Solicitadas:</strong><br>
                                                               {createTimeOffCommand.Hour}
                                                           </div>
                                                       </td>
                                                   </tr>
                                               </table>

                                               <div style=""margin-top: 15px; padding-top: 15px; border-top: 1px dashed #ccc;"">
                                                   <strong style=""color: #555;"">📄 Motivo:</strong><br>
                                                   {createTimeOffCommand.Remark}
                                               </div>
                                           </div>

                                           <div style=""text-align: center; margin: 30px 0;"">
                                               <p style=""color: #666; margin-bottom: 20px;"">
                                                   Clique no botão abaixo para acessar diretamente este protocolo no sistema:
                                               </p>
                       
                                               <!-- CTA BUTTON (FIXED: Estilos inlined e centralizado por tabela) -->
                                               <table border=""0"" cellpadding=""0"" cellspacing=""0"" align=""center"">
                                                   <tr>
                                                       <td align=""center"" style=""border-radius: 8px; background-color: #667eea;"">
                                                           <a href=""{$"http://localhost:5173/worktimer/approve/detail/{protocol}"}"" target=""_blank"" 
                                                               class=""cta-button""
                                                               style=""display: inline-block; 
                                                                       padding: 14px 32px;
                                                                       color: #ffffff; 
                                                                       text-decoration: none; 
                                                                       border-radius: 8px; 
                                                                       font-weight: 600; 
                                                                       font-size: 16px; 
                                                                       background-color: #667eea;
                                                                       background: linear-gradient(135deg, #667eea 0%, #764ba2 100%); 
                                                                       line-height: 1.2;
                                                                       border: 1px solid #764ba2;"">
                                                               🔍 Analisar Solicitação
                                                           </a>
                                                       </td>
                                                   </tr>
                                               </table>
                                           </div>
                                       </td>
                                   </tr>
                   
                                   <tr>
                                       <td class=""footer"" style=""background: #f8f9fa; padding: 25px; text-align: center; color: #6c757d; font-size: 14px; border-top: 1px solid #e9ecef;"">
                                           <p style=""font-size: 12px; color: #adb5bd; margin-top: 15px;"">
                                               Este é um e-mail automático. Por favor, não responda diretamente a esta mensagem.<br>
                                           </p>
                                       </td>
                                   </tr>
                   
                               </table>
                           </td>
                       </tr>
                   </table>
                </body>
                </html>";
    }

    public static string Rejected(
        RejectTimeOffCommand rejectTimeOffCommand,
        string approver)
    {
        string primaryColor = "#EF4444"; // Red-500 (cor de rejeição mais forte)
        string secondaryColor = "#4B5563"; // Gray-600 (para texto geral)
        string bodyBgColor = "#F3F4F6"; // Cinza claro (Gray-100) para o fundo do email

        string htmlContent = $@"
            <!DOCTYPE html>
            <html lang=""pt-BR"">
            <head>
                <meta charset=""UTF-8"">
                <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                <title>Solicitação Hora Rejeitada - Protocolo {rejectTimeOffCommand.Protocol}</title>
                <style>
                    body, html {{
                        margin: 0;
                        padding: 0;
                        background-color: {bodyBgColor} !important;
                        -webkit-text-size-adjust: 100%;
                        -ms-text-size-adjust: 100%;
                        width: 100% !important;
                    }}
                    .ExternalClass {{width: 100%;}}

                    .container {{
                        max-width: 600px;
                        margin: 40px auto;
                        padding: 20px;
                        background-color: #ffffff;
                        border-radius: 8px;
                        box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
                        border-top: 5px solid {primaryColor};
                        box-sizing: border-box;
                    }}
                    .header {{
                        text-align: center;
                        padding-bottom: 20px;
                        border-bottom: 1px solid #E5E7EB;
                    }}
                    .header h1 {{
                        color: {primaryColor};
                        font-size: 26px;
                        margin: 0;
                        font-weight: bold;
                    }}
                    .content {{
                        padding: 20px 0;
                        color: {secondaryColor};
                        line-height: 1.6;
                        font-size: 14px;
                    }}
                    .content p {{
                        margin-bottom: 15px;
                    }}
                    .protocol-box {{
                        background-color: #FEE2E2;
                        color: #B91C1C;
                        padding: 15px;
                        border-radius: 6px;
                        text-align: center;
                        font-weight: 700;
                        font-size: 16px;
                        margin-bottom: 25px;
                    }}
                    .rejection-reason {{
                        margin-top: 20px;
                        padding: 15px;
                        border: 1px solid #FCA5A5; 
                        background-color: #FEF2F2; 
                        border-radius: 6px;
                    }}
                    .rejection-reason h4 {{
                        color: #7F1D1D;
                        margin-top: 0;
                        margin-bottom: 10px;
                        font-size: 14px;
                        text-transform: uppercase;
                        font-weight: 700;
                    }}
                    .rejection-reason p {{
                        color: #374151;
                        font-style: italic;
                        margin: 0;
                        white-space: pre-wrap;
                    }}
                    .footer {{
                        text-align: center;
                        padding-top: 20px;
                        border-top: 1px solid #E5E7EB;
                        font-size: 12px;
                        color: {secondaryColor};
                    }}
                </style>
            </head>
            <body style=""background-color: {bodyBgColor};"">
                <!-- Wrapper que garante o fundo cinza em clientes de email -->
                <table width=""100%"" border=""0"" cellpadding=""0"" cellspacing=""0"" style=""background-color: {bodyBgColor};"">
                    <tr>
                        <td align=""center"" style=""padding: 20px 0;"">
                            <div class=""container"">
                                <div class=""header"">
                                    <h1>Solicitação REJEITADA</h1>
                                </div>

                                <div class=""content"">
                                    <p>Prezado(a) Usuário(a),</p>
                                    
                                    <p>Informamos que sua solicitação de tempo livre foi <strong>rejeitada</strong> após análise do seu gestor.</p>
                                    
                                    <div class=""protocol-box"">
                                        Número do Protocolo: <span>{rejectTimeOffCommand.Protocol}</span>
                                    </div>

                                    <div class=""rejection-reason"">
                                        <h4>Descrição da Recusa (Justificativa do Gestor): {approver} </h4>
                                        <p>{rejectTimeOffCommand.Description}</p>
                                    </div>

                                    <p style=""margin-top: 20px;"">Para mais detalhes ou dúvidas sobre a decisão, entre em contato com seu gestor direto ou o departamento de RH.</p>
                                </div>

                                <div class=""footer"">
                                    <p>Este é um email automático, por favor, não responda.</p>
                                    <p>&copy; {DateTime.UtcNow.Year} [Scan Brazil Constulting]</p>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </body>
            </html>";

        return htmlContent;
    }

    public static string Approved(
           string protocol,
           string approver)
    {
        string primaryColor = "#10B981";
        string secondaryColor = "#4B5563";
        string bodyBgColor = "#F3F4F6";

        string htmlContent = $@"
            <!DOCTYPE html>
            <html lang=""pt-BR"">
            <head>
                <meta charset=""UTF-8"">
                <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                <title>Solicitação de Tempo Livre Aprovada - Protocolo {protocol}</title>
                <style>
                    body, html {{
                        margin: 0;
                        padding: 0;
                        background-color: {bodyBgColor} !important;
                        -webkit-text-size-adjust: 100%;
                        -ms-text-size-adjust: 100%;
                        width: 100% !important;
                    }}
                    .ExternalClass {{width: 100%;}}

                    .container {{
                        max-width: 600px;
                        margin: 40px auto;
                        padding: 20px;
                        background-color: #ffffff;
                        border-radius: 8px;
                        box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
                        border-top: 5px solid {primaryColor};
                        box-sizing: border-box;
                    }}
                    .header {{
                        text-align: center;
                        padding-bottom: 20px;
                        border-bottom: 1px solid #E5E7EB;
                    }}
                    .header h1 {{
                        color: {primaryColor};
                        font-size: 26px;
                        margin: 0;
                        font-weight: bold;
                    }}
                    .content {{
                        padding: 20px 0;
                        color: {secondaryColor};
                        line-height: 1.6;
                        font-size: 14px;
                    }}
                    .content p {{
                        margin-bottom: 15px;
                    }}
                    .protocol-box {{
                        background-color: #D1FAE5;
                        color: #065F46;
                        padding: 15px;
                        border-radius: 6px;
                        text-align: center;
                        font-weight: 700;
                        font-size: 16px;
                        margin-bottom: 25px;
                    }}
                    .approval-info {{
                        margin-top: 20px;
                        padding: 15px;
                        border: 1px solid #A7F3D0;
                        background-color: #F0FAF5;
                        border-radius: 6px;
                    }}
                    .approval-info h4 {{
                        color: #044F3C;
                        margin-top: 0;
                        margin-bottom: 10px;
                        font-size: 14px;
                        text-transform: uppercase;
                        font-weight: 700;
                    }}
                    .approval-info p {{
                        color: #374151;
                        margin: 0;
                    }}
                    .footer {{
                        text-align: center;
                        padding-top: 20px;
                        border-top: 1px solid #E5E7EB;
                        font-size: 12px;
                        color: {secondaryColor};
                    }}
                </style>
            </head>
            <body style=""background-color: {bodyBgColor};"">
                <!-- Wrapper que garante o fundo cinza em clientes de email -->
                <table width=""100%"" border=""0"" cellpadding=""0"" cellspacing=""0"" style=""background-color: {bodyBgColor};"">
                    <tr>
                        <td align=""center"" style=""padding: 20px 0;"">
                            <div class=""container"">
                                <div class=""header"">
                                    <h1>Solicitação APROVADA</h1>
                                </div>

                                <div class=""content"">
                                    <p>Prezado(a) Usuário(a),</p>
                                    
                                    <p>Temos o prazer de informar que sua solicitação de hora foi <strong>aprovada</strong>!</p>
                                    
                                    <div class=""protocol-box"">
                                        Número do Protocolo: <span>{protocol}</span>
                                    </div>

                                    <div class=""approval-info"">
                                        <h4>Detalhes da Aprovação:</h4>
                                        <p>Sua solicitação foi revisada e aprovada pelo gestor: <strong>{approver}</strong>.</p>
                                    </div>

                                    <p style=""margin-top: 20px;"">O período solicitado já foi debitado de seu banco de hora.</p>
                                </div>

                                <div class=""footer"">
                                    <p>Este é um email automático, por favor, não responda.</p>
                                    <p>&copy; {DateTime.UtcNow.Year} [Scan Brazil Constulting]</p>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </body>
            </html>";

        return htmlContent;
    }
}
