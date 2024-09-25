using Mysqlx.Crud;
using System.Diagnostics;
using System.Text;

namespace MyERP
{
    public class Invoice
    {
        public static void GenerateInvoice(SalesOrderHeader data)
        {

            var customer = Database.Instance.GetCustomerbyID(data.CustomerNumber);

            string htmlTemplate = File.ReadAllText("../../../Invoice/template.html");


            string CompanyName = "ENL SECURITY A/S";
            string InvoiceID = "Invoice ID";
            Enum OrderStatus  = data.Status;
            string CompanyAddress = "Struervej 55";
            string CompanyAddress2 = "9220 Aalborg Øst";
            string CompanyEmail = "ENL@Security.dk";
            string CompanyPhone = "+45 19901990";
            string CustomerName = customer.FirstName + " " + customer.LastName;
            string CustomerAddress = customer.FullAddress;
            string CustomerEmail = customer.Email;
            string CustomerPhone = customer.Phone;
            int CustomerID = customer.CustomerID;
            DateTime PurchaseDate = data.CreationDate;
            int OrderID = data.OrderNumber;
            //string InfoSalesLineNr = data.GetOrderLines;

            double PriceSubTotal = 0;
            foreach (var line in Database.instance.Lines)
            {
                if (line.OrderID == data.OrderNumber)
                {
                    PriceSubTotal += line.Price * line.Quantity;
                }

            }
            string PriceShipping = "49";
            string PriceDiscount = "0";
            double PriceTotal = PriceSubTotal * 1.25;
            double PriceTax =   PriceTotal - PriceSubTotal;


            int LineNo = 1;
            var stringBuilder = new StringBuilder();

            foreach (var line in Database.instance.Lines)
            {
                if (line.OrderID == data.OrderNumber)
                {
                    string SalesOrderLine =
                    ("<tr>" +
                    $"<th scope=\"row\">{LineNo:D1}</th>" + // No. Nummer
                    "<td>" +
                    "<div>" +
                    $"<h5 class=\"text-truncate font-size-14 mb-1\">{System.Net.WebUtility.HtmlEncode(line.Name)}</h5>" + // Vare navn
                    $"<p class=\"text-muted mb-0\">{System.Net.WebUtility.HtmlEncode(line.Description)}</p>" +// vare beskrivelse
                    "</div>" +
                    "</td>" +
                    $"<td>DKK {line.Price},-</td>" + // vare pris
                    $"<td>{line.Quantity}</td>" + // vare antal
                    $"<td>{line.Unit}</td>" +// vare enhed
                    $"<td class=\"text-end\">DKK {line.Price * line.Quantity}</td>" + // vare total
                    "</tr>");

                    stringBuilder.Append(SalesOrderLine);
                    LineNo++;
                }
            }

            string SalesOrderLines = stringBuilder.ToString();


            //string vareLinjer = "<tr><td>Produkt 1</td><td>100 DKK</td></tr>";

            string fakturaHtml = htmlTemplate
                .Replace("{{WebTitle}}", CompanyName)
                .Replace("{{InvoiceID}}", InvoiceID)
                .Replace("{{Status}}", OrderStatus.ToString())
                .Replace("{{CompanyName}}", CompanyName)
                .Replace("{{CompanyAddress}}", CompanyAddress)
                .Replace("{{CompanyEmail}}", CompanyEmail)
                .Replace("{{CompanyPhone}}", CompanyPhone)
                .Replace("{{CustomerName}}", CustomerName)
                .Replace("{{CustomerAddress}}", CustomerAddress)
                .Replace("{{CustomerEmail}}", CustomerEmail)
                .Replace("{{CustomerPhone}}", CustomerPhone)
                .Replace("{{CustomerID}}", CustomerID.ToString())
                .Replace("{{PurchaseDate}}", PurchaseDate.ToString())
                .Replace("{{OrderID}}", OrderID.ToString())
                .Replace("{{PriceSubTotal}}", PriceSubTotal.ToString())
                .Replace("{{PriceDiscount}}", PriceDiscount.ToString())
                .Replace("{{PriceShipping}}", PriceShipping)
                .Replace("{{PriceTax}}", PriceTax.ToString())
                .Replace("{{PriceTotal}}", PriceTotal.ToString())
                .Replace("{{SalesOrderLines}}", SalesOrderLines)
                .Replace("{{CompanyAddress2}}", CompanyAddress2);



            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../Invoice/invoice.html");
            if (File.Exists(filePath))
            {
                Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
            }
            else
            {
                Console.WriteLine("File not found: " + filePath);
            }


            File.WriteAllText(filePath, fakturaHtml);

            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = filePath,
                UseShellExecute = true
            };

            Process.Start(psi);
        }
    }
}


//StringBuilder htmlContent = new StringBuilder();

//string filePath = "invoice.html";
//File.WriteAllText(filePath, htmlContent.ToString());

//ProcessStartInfo psi = new ProcessStartInfo
//{
//    FileName = filePath,
//    UseShellExecute = true
//};

//Process.Start(psi);


//            htmlContent.AppendLine("<!DOCTYPE html>");
//            htmlContent.AppendLine("<html lang=\"en\">");
//            htmlContent.AppendLine("<head>");
//            htmlContent.AppendLine("<meta charset=\"utf-8\">");
//            htmlContent.AppendLine("<title>Faktura - ENL SECURITY A/S</title>");
//            htmlContent.AppendLine("<meta name=\"viewport\" content=\"width=device-width, initial-scale=1\">");
//            htmlContent.AppendLine("<link href=\"https://cdn.jsdelivr.net/npm/bootstrap@5.0.1/dist/css/bootstrap.min.css\" rel=\"stylesheet\">");
//            htmlContent.AppendLine("<style type=\"text/css\">");
//            htmlContent.AppendLine("@media print {");
//            htmlContent.AppendLine("@page { ");
//            htmlContent.AppendLine("margin: none;");
//            htmlContent.AppendLine("} ");
//            htmlContent.AppendLine("body {");
//            htmlContent.AppendLine("margin: none;");
//            htmlContent.AppendLine("} ");
//            htmlContent.AppendLine(" }");
//            htmlContent.AppendLine("body { margin-top: 20px; background-color: #eee; }");
//            htmlContent.AppendLine(".card { box-shadow: 0 20px 27px 0 rgb(0 0 0 / 5%); }");
//            htmlContent.AppendLine("</style>");
//            htmlContent.AppendLine("</head>");
//            htmlContent.AppendLine("<body>");
//            htmlContent.AppendLine("<link rel=\"stylesheet\" href=\"https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.13.1/css/all.min.css\" ");
//            htmlContent.AppendLine("integrity=\"sha256-2XFplPlrFClt0bIdPgpz8H7ojnk10H69xRqd9+uTShA=\" crossorigin=\"anonymous\" />");
//            htmlContent.AppendLine("<div class=\"container\">");
//            htmlContent.AppendLine("<div class=\"row\">");
//            htmlContent.AppendLine("<div class=\"col-lg-12\">");
//            htmlContent.AppendLine("<div class=\"card\">");
//            htmlContent.AppendLine("<div class=\"card-body\">");
//            htmlContent.AppendLine("<div class=\"invoice-title\">");
//            // BETALT STATUS ? 
//            htmlContent.AppendLine($"<h4 class=\"float-end font-size-15\">Faktura #DS0204 <span class=\"badge bg-success font-size-12 ms-2\">{data.Status}</span></h4>");
//            htmlContent.AppendLine("<div class=\"mb-4\">");
//            htmlContent.AppendLine("<h2 class=\"mb-1 text-muted\">ENL SECURITY</h2>");
//            htmlContent.AppendLine("</div>");
//            htmlContent.AppendLine("<div class=\"text-muted\">");
//            // AFSENDER OPLYSNINGER
//            htmlContent.AppendLine("<p class=\"mb-1\">Struervej 55, 9220 Aalborg, Danmark </p>");
//            htmlContent.AppendLine("<p class=\"mb-1\">ENL@SECURITY.DK, +45 85 515 515</p>");
//            htmlContent.AppendLine("</div>");
//            htmlContent.AppendLine("</div>");
//            htmlContent.AppendLine("<hr class=\"my-4\">");
//            htmlContent.AppendLine("<div class=\"row\">");
//            htmlContent.AppendLine("<div class=\"col-sm-6\">");
//            htmlContent.AppendLine("<div class=\"text-muted\">");
//            // DEBITOR OPLYSNINGER

//            htmlContent.AppendLine("<h5 class=\"font-size-16 mb-3\">Debitor:</h5>");
//            htmlContent.AppendLine($"<h5 class=\"font-size-15 mb-2\">{customer.Fullname}</h5>"); // Kunde navn
//            htmlContent.AppendLine($"<h5 class=\"font-size-15 mb-2\">{customer.FullAddress}</h5>"); // Kunde addresse
//            htmlContent.AppendLine($"<h5 class=\"font-size-15 mb-2\">{customer.Email}</h5>"); // Kunde email
//            htmlContent.AppendLine($"<h5 class=\"font-size-15 mb-2\">{customer.Phone}</h5>"); // Kunde telefon

//            htmlContent.AppendLine("</div>");
//            htmlContent.AppendLine("</div>");
//            htmlContent.AppendLine("<div class=\"col-sm-6\">");
//            htmlContent.AppendLine("<div class=\"text-muted text-sm-end\">");
//            htmlContent.AppendLine("<div>");
//            htmlContent.AppendLine("<h5 class=\"font-size-15 mb-1\">Faktura nr:</h5>"); // faktura nr
//            htmlContent.AppendLine($"<p>#{data.OrderNumber}</p>");
//            htmlContent.AppendLine("</div>");
//            htmlContent.AppendLine("<div class=\"mt-4\">");
//            htmlContent.AppendLine("<h5 class=\"font-size-15 mb-1\">Købs dato:</h5>"); // købs dato
//            htmlContent.AppendLine($"<p>12 Oct, 2020</p>");
//            htmlContent.AppendLine("</div>");
//            htmlContent.AppendLine("<div class=\"mt-4\">");
//            htmlContent.AppendLine("<h5 class=\"font-size-15 mb-1\">Ordre nr:</h5>"); // ordre nr
//            htmlContent.AppendLine($"<p>#1123456</p>");
//            htmlContent.AppendLine("</div>");
//            htmlContent.AppendLine("</div>");
//            htmlContent.AppendLine("</div>");
//            htmlContent.AppendLine("</div>");
//            htmlContent.AppendLine("<div class=\"py-2\">");
//            htmlContent.AppendLine("<h5 class=\"font-size-15\">Ordre oversigt</h5>"); // ordre oversigt
//            htmlContent.AppendLine("<div class=\"table-responsive\">");
//            htmlContent.AppendLine("<table class=\"table align-middle table-nowrap table-centered mb-0\">");
//            htmlContent.AppendLine("<thead>");
//            htmlContent.AppendLine("<tr>");
//            htmlContent.AppendLine("<th style=\"width: 70px;\">No.</th>");
//            htmlContent.AppendLine("<th>Vare</th>");
//            htmlContent.AppendLine("<th>Pris</th>");
//            htmlContent.AppendLine("<th>Antal</th>");
//            htmlContent.AppendLine("<th>Enhed</th>");
//            htmlContent.AppendLine("<th class=\"text-end\" style=\"width: 120px;\">Total</th>");
//            htmlContent.AppendLine("</tr>");
//            htmlContent.AppendLine("</thead>");
//            htmlContent.AppendLine("<tbody>");

//            int i = 1;
//            foreach (var line in Database.instance.Lines)
//            {
//                if (line.OrderID == data.OrderNumber)
//                    {
//                    htmlContent.AppendLine("<tr>");
//                    htmlContent.AppendLine($"<th scope=\"row\">0{i:D1}</th>"); // No. Nummer
//                    htmlContent.AppendLine("<td>");
//                    htmlContent.AppendLine("<div>");
//                    htmlContent.AppendLine($"<h5 class=\"text-truncate font-size-14 mb-1\">{line.Name}</h5>");   // Vare navn
//                    htmlContent.AppendLine($"<p class=\"text-muted mb-0\">{line.Description}</p>"); // vare beskrivelse
//                    htmlContent.AppendLine("</div>");
//                    htmlContent.AppendLine("</td>");
//                    htmlContent.AppendLine($"<td>DKK {line.Price}</td>"); // vare pris
//                    htmlContent.AppendLine($"<td>{line.Quantity}</td>"); // vare antal
//                    htmlContent.AppendLine($"<td>{line.Unit}</td>"); // vare enhed

//                    htmlContent.AppendLine($"<td class=\"text-end\">DKK {line.Price * line.Quantity}</td>"); // vare total
//                    htmlContent.AppendLine("</tr>");
//                    i++;
//                }
//            }

//            //for (int i = 1; i < 2; i++)
//            //{

//            //    htmlContent.AppendLine("<tr>");
//            //    htmlContent.AppendLine($"<th scope=\"row\">0{i}</th>"); // No. Nummer
//            //    htmlContent.AppendLine("<td>");
//            //    htmlContent.AppendLine("<div>");
//            //    htmlContent.AppendLine("<h5 class=\"text-truncate font-size-14 mb-1\">Black Strap A012</h5>");   // Vare navn
//            //    htmlContent.AppendLine("<p class=\"text-muted mb-0\">Watch, Black</p>"); // vare beskrivelse
//            //    htmlContent.AppendLine("</div>");
//            //    htmlContent.AppendLine("</td>");
//            //    htmlContent.AppendLine("<td>$245.50</td>"); // vare pris
//            //    htmlContent.AppendLine($"<td>{i}</td>"); // vare antal
//            //    htmlContent.AppendLine("<td class=\"text-end\">$245.50</td>"); // vare total
//            //    htmlContent.AppendLine("</tr>");
//            //}

//            double ii = 0;
//            foreach (var line in Database.instance.Lines)
//            {
//                if (line.OrderID == data.OrderNumber)
//                {
//                    ii += line.Price * line.Quantity;
//                }

//            }

//            double iii = ii * 1.25;

//            htmlContent.AppendLine("<th scope=\"row\" colspan=\"4\" class=\"text-end\">Sub Total</th>"); // total uden moms
//            htmlContent.AppendLine($"<td class=\"text-end\">{ii}</td>");
//            htmlContent.AppendLine("</tr>");
//            htmlContent.AppendLine("<tr>");
//            htmlContent.AppendLine("<th scope=\"row\" colspan=\"4\" class=\"border-0 text-end\">Rabat :</th>"); // rabat
//            htmlContent.AppendLine("<td class=\"border-0 text-end\">- DKK 0.00</td>");
//            htmlContent.AppendLine("</tr>");
//            htmlContent.AppendLine("<tr>");
//            htmlContent.AppendLine("<th scope=\"row\" colspan=\"4\" class=\"border-0 text-end\">Shipping Charge :</th>"); // fragt pris
//            htmlContent.AppendLine("<td class=\"border-0 text-end\">DKK 49.00</td>");
//            htmlContent.AppendLine("</tr>");
//            htmlContent.AppendLine("<tr>");
//            htmlContent.AppendLine("<th scope=\"row\" colspan=\"4\" class=\"border-0 text-end\">Tax</th>"); // moms udgør
//            htmlContent.AppendLine($"<td class=\"border-0 text-end\">{iii-ii}</td>");
//            htmlContent.AppendLine("</tr>");
//            htmlContent.AppendLine("<tr>");
//            htmlContent.AppendLine("<th scope=\"row\" colspan=\"4\" class=\"border-0 text-end\">Total</th>"); // ordre total
//            htmlContent.AppendLine($"<td class=\"border-0 text-end\"><h4 class=\"m-0 fw-semibold\">{iii}</h4></td>");
//            htmlContent.AppendLine("</tr>");
//            htmlContent.AppendLine("</tbody>");
//            htmlContent.AppendLine("</table>");
//            htmlContent.AppendLine("</div>");
//            htmlContent.AppendLine("<div class=\"d-print-none mt-4\">");
//            htmlContent.AppendLine("<div class=\"float-end\">");
//            htmlContent.AppendLine("<a href=\"javascript:window.print()\" class=\"btn btn-success me-1\"><i class=\"fa fa-print\"></i></a>");
//            htmlContent.AppendLine("</div>");
//            htmlContent.AppendLine("</div>");
//            htmlContent.AppendLine("</div>");
//            htmlContent.AppendLine("</div>");
//            htmlContent.AppendLine("</div>");
//            htmlContent.AppendLine("</div>");
//            htmlContent.AppendLine("</div>");
//            htmlContent.AppendLine("<script src=\"https://code.jquery.com/jquery-1.10.2.min.js\"></script>");
//            htmlContent.AppendLine("<script src=\"https://cdn.jsdelivr.net/npm/bootstrap@5.0.1/dist/js/bootstrap.bundle.min.js\"></script>");
//            htmlContent.AppendLine("<script type=\"text/javascript\">");
//            htmlContent.AppendLine("</script>");
//            htmlContent.AppendLine("</body>");
//            htmlContent.AppendLine("</html>");