using TECHCOOL.UI;

namespace MyERP.CompanyView
{
    public class CompanyViewScreen : Screen
    {
        public Company companyDisplay { get; set; }

        public CompanyViewScreen(Company c)
        {
            company = c;
            ExitOnEscape();
        }

        public override string Title { get; set; } = "Virksomheds Visning";
        public Company company { get; private set; }

        protected override void Draw()
        {
            int space = 54;
            WindowHelper.Spacer('┌', '─', space, '┐');
            Console.WriteLine("│{0,-53} │", "Tryk Esc for at forlade siden");
            WindowHelper.Spacer('└', '─', space, '┘');

            WindowHelper.Spacer('┌', '─', space, '┐');
            Console.WriteLine("│{0,-15} │ {1,-35} │", "Virksomheds ID", company.CompanyID);
            WindowHelper.Spacer('└', '─', space, '┘');

            WindowHelper.Spacer('┌', '─', space, '┐');
            Console.WriteLine("│{0,-15} │ {1,-35} │", "Navn", WindowHelper.Truncate(company.CompanyName, 35));
            Console.WriteLine("│{0,-15} │ {1,-35} │", "Address", WindowHelper.Truncate((company.Street + company.HouseNumber), 35));
            Console.WriteLine("│{0,-15} │ {1,-35} │", "Postnummer", company.ZipCode);
            Console.WriteLine("│{0,-15} │ {1,-35} │", "By", WindowHelper.Truncate(company.City, 35));
            Console.WriteLine("│{0,-15} │ {1,-35} │", "Land", WindowHelper.Truncate(company.Country, 35));
            Console.WriteLine("│{0,-15} │ {1,-35} │", "Currency", company.Currency);
            WindowHelper.Spacer('└', '─', space, '┘');
        }
    }
}
