using Domain.Commands.v1.TimeOff.Create;

namespace Domain.Constants.v1;
public static class EmailConstant
{
    public static readonly string Subject = "Solicitação de Folga - Scan Brazil Consulting";
    public static string TimeOffTemplate(
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
                                                           <a href=""{"http://localhost:5173/worktimer"}"" target=""_blank"" 
                                                               class=""cta-button""
                                                               style=""display: inline-block; 
                                                                       padding: 14px 32px;
                                                                       color: #ffffff; 
                                                                       text-decoration: none; 
                                                                       border-radius: 8px; 
                                                                       font-weight: 600; 
                                                                       font-size: 16px; 
                                                                       background-color: #667eea; /* Fallback Solid Color */
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
}
