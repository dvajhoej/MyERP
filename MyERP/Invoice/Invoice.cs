using Mysqlx.Crud;
using System.Diagnostics;
using System.Text;

namespace MyERP
{
    public class Invoice
    {
        public int InvoiceID { get; set; }
        public int SalesOrderHeadID { get; set; }
        public DateTime InvoiceDate { get; set; }

        public Invoice(int invoiceID, int salesOrderHeadID, DateTime invoiceDate)
        {
            InvoiceID = invoiceID;
            SalesOrderHeadID = salesOrderHeadID;
            InvoiceDate = invoiceDate;
        }
        public Invoice()
        {
            
        }

        public static void GenerateInvoice(SalesOrderHeader data, Invoice invoice)
        {

            var customer = Database.Instance.GetCustomerbyID(data.CustomerNumber);

            string htmlTemplate = File.ReadAllText("../../../Invoice/template.html");

            if (customer != null)
            {
                string CompanyName = "LNE SECURITY A/S";
                string InvoiceID = invoice.InvoiceID.ToString();
                Enum OrderStatus = data.Status;
                string CompanyAddress = "Struervej 55";
                string CompanyAddress2 = "9220 Aalborg Øst";
                string CompanyEmail = "LNE@Security.dk";
                string CompanyPhone = "+45 19901990";
                string CustomerName = customer.FirstName + " " + customer.LastName;
                string CustomerAddress = customer.FullAddress;
                string CustomerEmail = customer.Email;
                string CustomerPhone = customer.Phone;
                int CustomerID = customer.CustomerID;
                DateTime InvoiceDate = invoice.InvoiceDate.Date;
                DateTime DueDate = invoice.InvoiceDate.Date.AddDays(30);
                int OrderID = data.OrderNumber;

                double PriceSubTotal = 0;
                foreach (var line in Database.Instance.SalesOrderLines)
                {
                    if (line.SalesOrderHeadID == data.OrderNumber)
                    {
                        PriceSubTotal += line.Price * line.Quantity;
                    }

                }
                double PriceShipping = 49;
                string PriceDiscount = "0";
                double PriceTotal = PriceShipping + (PriceSubTotal * 1.25);
                double PriceTax = PriceTotal - PriceSubTotal;


                int LineNo = 1;
                var stringBuilder = new StringBuilder();

                foreach (var line in Database.Instance.SalesOrderLines)
                {
                    if (line.SalesOrderHeadID == data.OrderNumber)
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
                    .Replace("{{InvoiceDate}}", InvoiceDate.ToShortDateString())
                    .Replace("{{OrderID}}", OrderID.ToString())
                    .Replace("{{PriceSubTotal}}", PriceSubTotal.ToString())
                    .Replace("{{PriceDiscount}}", PriceDiscount.ToString())
                    .Replace("{{PriceShipping}}", PriceShipping.ToString())
                    .Replace("{{PriceTax}}", PriceTax.ToString())
                    .Replace("{{PriceTotal}}", PriceTotal.ToString())
                    .Replace("{{SalesOrderLines}}", SalesOrderLines)
                    .Replace("{{DueDate}}", DueDate.ToShortDateString())
                    .Replace("{{CompanyAddress2}}", CompanyAddress2);
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"../../../Invoice/invoices/invoice-{invoice.InvoiceID}-{invoice.InvoiceDate:d}.html");
                if (File.Exists(filePath))
                {
                    Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
                }
                else
                {
                    Console.WriteLine("File not found: " + filePath);
                    CreateInvoiceFile(filePath);
                }


                File.WriteAllText(filePath, fakturaHtml);

                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = filePath,
                    UseShellExecute = true
                };

                Process.Start(psi);
            }
            else
            {
                throw new Exception("Kunde existere ikke");
            }
        }
            



           
        static void CreateInvoiceFile(string filePath)
        {
            try
            {
                // Ensure the directory exists
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));

                // Create a simple HTML file
                string htmlContent = "";
                File.WriteAllText(filePath, htmlContent);

                Console.WriteLine("File created: " + filePath);
                Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while creating the file: " + ex.Message);
            }
        }
    }

}