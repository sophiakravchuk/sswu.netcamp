using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_12_KravchukSophia
{
    public class ActivityReport
    {
        private static readonly object _lock = new object();
        private static string resultFileName = "../../../report.txt";
        public static void WriteACustomerServed(Customer customer, int deskId, int time)
        {
            lock (_lock)
            {
                using StreamWriter file = new(resultFileName, append: true);
                string reportText = $"{customer.Name} with status {customer.Status} was served by pay desk number {deskId}, it took {time} seconds";
                file.WriteLineAsync(reportText);
                file.Flush();
            }

        }

        public static void WriteACustomerMoved(Customer customer, int oldDeskId, int newDeskId, int time)
        {
            lock (_lock)
            {
                using StreamWriter file = new(resultFileName, append: true);
                string reportText = $"{customer.Name} with status {customer.Status} was waiting for {time} seconds for pay " +
                    $"desk number {oldDeskId}, and was moved to desk number {newDeskId}";
                file.WriteLineAsync(reportText);
                file.Flush();
            }
        }
    }
}
