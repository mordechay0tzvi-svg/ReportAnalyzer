namespace MyNamespace 
{
    using System;
    using System.IO;
    enum TypeType { Intel, Recon, Analyze, Collect };
    enum StatusType { Rejected, Approved, Pending };

    class ReportAnalyzer
    {
        const string FILENAME = "reports.txt";

        static string[] UnitName = new string[100];
        static TypeType[] ReportType = new TypeType[100];
        static int[] Priority = new int[100];
        static double[] Score = new double[100];
        static StatusType[] Status = new StatusType[100];

        static string[] LoadFile()
        {
            string[] lines = new string[0];
            try
            {
                lines = File.ReadAllLines(FILENAME);
                Console.WriteLine("File loaded.");
                if (lines.Length == 0) 
                {
                    Console.WriteLine("File is empty.");
                    return lines;
                }
                return lines;
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found.");
                return lines;
            }
        }

        static bool IsGoodReport(string report, ref int count)
        {
            string[] shapedReport = report.Split(',');
            if (shapedReport.Length != 5) { return false; }
            string r0 = shapedReport[0].Trim();
            string r1 = shapedReport[1].Trim();
            r1 = char.ToUpper(r1[0]) + r1.Substring(1).ToLower();
            string r2 = shapedReport[2].Trim();
            string r3 = shapedReport[3].Trim();
            string r4 = shapedReport[4].Trim();
            r4 = char.ToUpper(r4[0]) + r4.Substring(1).ToLower();
            if (!Enum.TryParse<TypeType>(r1, out TypeType type))  { return false; }
            else if (!int.TryParse(r2, out int priority))  { return false; }
            else if (priority < 0 || priority > 5) { return false; }
            else if (!double.TryParse(r3, out double score))  { return false; }
            else if (score < 0 || score > 100) { return false; }
            else if (!Enum.TryParse<StatusType>(r4, out StatusType status))  { return false; }

            else
            {
                UnitName[count] = r0;
                ReportType[count] = type;
                Priority[count] = priority; 
                Score[count] = score;
                Status[count] = status;
                count++;
                return true;
            }
        }

        static int FilterGoodOnes()
        {
            string[] reports = LoadFile();
            int count = 0;
            for (int i = 0; i < reports.Length; i++) 
                { IsGoodReport(reports[i], ref count); }
            Console.WriteLine($"Valid records: {count}");
            Console.WriteLine($"Invalid records: {reports.Length - count}");
            return count;
        }
        static void Main()
        {
            int count =FilterGoodOnes();
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine($"Unit Name: {UnitName[i]}");
                Console.WriteLine($"Report Type: {ReportType[i]}");
                Console.WriteLine($"Priority: {Priority[i]}");
                Console.WriteLine($"Score: {Score[i]}");
                Console.WriteLine($"Status: {Status[i]}");
                Console.WriteLine("");
            }
        }
    }
}

