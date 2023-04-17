using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Mail;
using SendGrid;

namespace CleanArchitecture.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailServiceController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ISendGridClient _sendGridClient;

        public MailServiceController(IConfiguration configuration, ISendGridClient sendGridClient)
        {
            _configuration = configuration;
            _sendGridClient = sendGridClient;
        }

        [HttpPost("Send mail")]
        public async Task<IActionResult> SendMail()
        {
            //var apiKey = "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";
            var apiKey = _configuration["SendGridAPI"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("rdt2923@gmail.com", "Mail Send Example");
            var subject = "Sending with SendGrid is Fun";
            var to = new EmailAddress("rdt2922@gmail.com");
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, htmlContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
            return Ok(response);
        }

        [HttpPost("sendmail/Attachement")]
        public async Task<IActionResult> SendingMail(IFormFile ImageFile, string ToEmail)
        {
            try
            {
                string html = @"
<!DOCTYPE html>
<html>
<head>
	<meta charset='UTF-8'>
	<title>Order Detail</title>
	<style type='text/css'>
		body {
			font-family: Arial, sans-serif;
			font-size: 14px;
			margin: 0;
			padding: 0;
		}
		h1 {
			font-size: 24px;
			font-weight: bold;
			margin-top: 20px;
			margin-bottom: 20px;
			text-align: center;
		}
		table {
			border-collapse: collapse;
			margin: 0 auto;
			width: 100%;
		}
		th, td {
			border: 1px solid #ccc;
			padding: 10px;
			text-align: center;
		}
		th {
			background-color: #f0f0f0;
			font-weight: bold;
		}
		#left{
            font-weight: bold;
			float: left;
        }

	</style>
</head>
<body>
<div style='background-color : #bababa'>
        <h1>RDT-Ecommerce</h1>
        <h1>RDT-Ecommerce</h1>

    </div>

	<h1  id='left'>RDT-Ecommerce</h1>
	<h1>Order Detail</h1>
	<table>
		<thead>
			<tr>
				<th>Product Name</th>
				<th>Quantity</th>
				<th>Price</th>
				<th>Total</th>
			</tr>
		</thead>
		<tbody>
			<tr>
				<td>Product 1</td>
				<td>2</td>
				<td>₹10.00</td>
				<td>₹20.00</td>
			</tr>
			<tr>
				<td>Product 2</td>
				<td>1</td>
				<td>₹25.00</td>
				<td>₹25.00</td>
			</tr>
			<tr>
				<td>Product 3</td>
				<td>3</td>
				<td>₹15.00</td>
				<td>₹45.00</td>
			</tr>

			<tr>
				<td colspan='3' style='text-align:right'>Total:</td>
				<td>₹96.30</td>
			</tr>
		</tbody>
	</table>
</body>
</html>

";

                //    var apiKey = _configuration.GetSection("SendGridEmailSettings")
                //.GetValue<string>("APIKey");
                //    var client = new SendGridClient(apiKey);

                string fromEmail = _configuration.GetSection("SendGridEmailSettings")
            .GetValue<string>("FromEmail");

                string fromName = _configuration.GetSection("SendGridEmailSettings")
                .GetValue<string>("FromName");

                var msg = new SendGridMessage()
                {
                    From = new EmailAddress(fromEmail, fromName),
                    Subject = "File Attachement Email",
                    HtmlContent = html,
                    PlainTextContent = "Check Attached File",
                };

                await msg.AddAttachmentAsync(
                    ImageFile.FileName,
                    ImageFile.OpenReadStream(),
                    ImageFile.ContentType,
                    "attachment"
                );
                msg.AddTo(ToEmail);

                var response = await _sendGridClient.SendEmailAsync(msg);
                string message = response.IsSuccessStatusCode ? "Email Send Successfully" :
                "Email Sending Failed";
                return Ok(response);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}