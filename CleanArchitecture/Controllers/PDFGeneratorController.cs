using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PdfSharpCore;
using PdfSharpCore.Pdf;
using System.Drawing.Imaging;
using System;
using System.Drawing.Printing;
using TheArtOfDev.HtmlRenderer.PdfSharp;
using Stripe;
using System.Runtime.Intrinsics.Arm;

namespace CleanArchitecture.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PDFGeneratorController : ControllerBase
    {
        [HttpGet("Download/pdf")]
        public async Task<ActionResult> GenratePdf(int OrderId)
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

            var document = new PdfDocument();
            string Content = html;
            PdfGenerator.AddPdfPages(document, Content, PageSize.A4);
            byte[] response = null;
            using (MemoryStream ms = new MemoryStream())
            {
                document.Save(ms);
                response = ms.ToArray();
            }

            string fileName = "Innvoice.pdf";
            return File(response, "application/pdf", fileName);
        }

        [HttpGet]
        public IActionResult CreatePDF()
        {
            string html = "<html><head><style>.my-class { color: red; } #my-id { font-weight: bold; }</style></head><body><h1 id='my-id'>Welcome to my website</h1><p class='my-class'>This text is red.</p></body></html>";

            var document = new PdfDocument();
            string Content = html;
            PdfGenerator.AddPdfPages(document, Content, PageSize.A4);
            byte[] response = null;
            using (MemoryStream ms = new MemoryStream())
            {
                document.Save(ms);
                response = ms.ToArray();
            }

            string fileName = "Innvoice.pdf";
            return File(response, "application/pdf", fileName);
        }
    }
}