namespace MyNamespace 
{
    using System;
    using System.IO;
    enum TypeType { Intel, Recon, Analyze, Collect };
    enum StatusType { Rejected, Approved, Pending };

    class ReportAnalyzer
    {
        const string FILENAME = "reports.txt";
        static string[] LoadFile(string filename)
        {
            string[] lines = new string[];
            try
            {
                string[] lines = File.ReadAllLines(filename);
                Console.WriteLine("File loaded.");
                if (lines.Length == 0) { Console.WriteLine("File is empty."); }
                return string[] lines = new string[1];
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found.");
                return lines;
            }
        }

        static bool IsGoodReport(string report)
        {
            
        }

        static int CountGoodReportst(string[])
        {
            int count = 0;
            reports = LoadFile();
            for (int i = 0; i < CountGoodReportst().Length; i++)
            {
                if (IsGoodReport(reports[i])) { count += 1; }
            }
            return count;
        }

        string[] allReports = LoadFile();
        int goodReportsNumber = CountGoodReportst(allReports);
        static string[] UnitName = new string[goodReportsNumber];
        static string[] ReportType = new string[goodReportsNumber];
        static int[] = new int[goodReportsNumber];
        static double[] Score = new double[goodReportsNumber];
        static string[] Status = new string[goodReportsNumber];

        static void Main()
        {

        }
    }
}
