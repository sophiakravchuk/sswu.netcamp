using System.Globalization;

namespace Homework_6_KravchukSophia
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AllUsersData allUsersData = new AllUsersData();

            FileReader.ReadFileIntoSystem("../../../QuartersDocs/IQuarter.txt", allUsersData);
            FileReader.ReadFileIntoSystem("../../../QuartersDocs/IIQuarter.txt", allUsersData);
            FileReader.ReadFileIntoSystem("../../../QuartersDocs/IIIQuarter.txt", allUsersData);
            FileReader.ReadFileIntoSystem("../../../QuartersDocs/IVQuarter.txt", allUsersData);

            Console.WriteLine("All data collected: \n " + allUsersData.ToString());


            string date = DateTime.Now.ToString("yy'-'MM'-'dd'-'HH'-'mm'-'ss");

            SystemReport.GetReportForAllUsersAsync(allUsersData, 1.68, $"../../../report{date}.txt");
            Console.WriteLine($"\nReport is written to file \"report{date}.txt\"");
            Console.WriteLine("____________________________________________________________\n");

            Console.WriteLine("Apartment with zero electricity usage:");
            Console.WriteLine("\n" + SystemReport.GetApartmentWithZeroElectricityUsage(allUsersData));
            Console.WriteLine("____________________________________________________________\n");

            Console.WriteLine("The biggest debdtor: ");
            Console.WriteLine("\n" + SystemReport.GetTheBiggestDebtor(allUsersData));
            Console.WriteLine("____________________________________________________________\n");

            Console.WriteLine("\n" + SystemReport.DaysTillLastMeasurement(allUsersData));
            Console.WriteLine("____________________________________________________________\n");

            Console.WriteLine(SystemReport.GetReportForUser(allUsersData, "Barnes", 1.68));

        }
    }
}