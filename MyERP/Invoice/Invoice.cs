using Mysqlx.Crud;
using System.Diagnostics;
using System.Text;

namespace MyERP
{
    // Define a class Invoice to represent an invoice
    public class Invoice
    {
        // Public properties to store the invoice details
        public int InvoiceID { get; set; }
        public int SalesOrderHeadID { get; set; }
        public DateTime InvoiceDate { get; set; }

        // Constructor to initialize the invoice details
        public Invoice(int invoiceID, int salesOrderHeadID, DateTime invoiceDate)
        {
            InvoiceID = invoiceID;
            SalesOrderHeadID = salesOrderHeadID;
            InvoiceDate = invoiceDate;
        }

        // Default constructor
        public Invoice()
        {
        }

        // Static method to generate an invoice
        public static void GenerateInvoice(SalesOrderHeader data, Invoice invoice)
        {
            // Get the customer details
            var customer = Database.Instance.GetCustomerbyID(data.CustomerNumber);

            // Check if the customer exists
            if (customer != null)
            {
                // Read the HTML template for the invoice
                string htmlTemplate = File.ReadAllText("../../../Invoice/template.html");

                // Define the variables to replace in the HTML template
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

                // Calculate the prices
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

                // Create a StringBuilder to build the HTML for the sales order lines
                var stringBuilder = new StringBuilder();

                // Iterate through the sales order lines
                int LineNo = 1;
                foreach (var line in Database.Instance.SalesOrderLines)
                {
                    if (line.SalesOrderHeadID == data.OrderNumber)
                    {
                        // Create the HTML for the sales order line
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

                        // Append the HTML to the StringBuilder
                        stringBuilder.Append(SalesOrderLine);
                        LineNo++;
                    }
                }

                // Get the HTML for the sales order lines
                string SalesOrderLines = stringBuilder.ToString();

                // Replace the variables in the HTML template
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

                // Define the file path for the invoice
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"../../../Invoice/invoices/invoice-{invoice.InvoiceID}-{invoice.InvoiceDate:d}.html");

                // Check if the file exists
                if (File.Exists(filePath))
                {
                    // Open the file
                    Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
                }
                else
                {
                    // Create the file
                    CreateInvoiceFile(filePath);
                }

                // Write the HTML to the file
                File.WriteAllText(filePath, fakturaHtml);

                // Open the file
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = filePath,
                    UseShellExecute = true
                };

                Process.Start(psi);
            }
            else
            {
                // Throw an exception if the customer does not exist
                throw new Exception("Kunde existere ikke");
            }
        }

        // Static method to create an invoice file
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