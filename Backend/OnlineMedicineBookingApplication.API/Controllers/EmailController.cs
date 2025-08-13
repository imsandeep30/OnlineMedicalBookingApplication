using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using OnlineMedicineBookingApplication.Application.Models;

[ApiController]
[Route("api/[controller]")]
public class EmailController : ControllerBase
{
    [HttpPost("send")]
    public async Task<IActionResult> SendEmail([FromBody] EmailRequest request)
    {
        var fromEmail = "medpickonline@gmail.com";      
        var appPassword = "ujod srfi vhjp ftde";       

        try
        {
            using var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromEmail, appPassword),
                EnableSsl = true,
            };

            using var mailMessage = new MailMessage
            {
                From = new MailAddress(fromEmail),
                Subject = request.Subject,
                Body = request.Body,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(request.To);

            await smtpClient.SendMailAsync(mailMessage);

            return Ok("Email sent successfully");
        }
        catch (System.Exception ex)
        {
            return StatusCode(500, "Error sending email: " + ex.Message);
        }
    }
}
