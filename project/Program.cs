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

        static void ScoreStats(double[] scores, ref int count)
        {
            double max = 0;
            double min = 100;
            double sum = 0;
            for (int i=0; i < count; i++)
            {
                double score = scores[i]; 
                sum += score;
                if (score < min) { min = score; }
                if (score > max) { max = score; }
            }
            Console.WriteLine("=== Report Statistics ===");
            Console.WriteLine($"Total reports: {count}");
            Console.WriteLine($"Minimun score: {min}");
            Console.WriteLine($"Maximun score: {max}");
            Console.WriteLine($"Average score: {sum / scores.Length}");
            Console.WriteLine("");
        }
        static void ReportsByStatus(StatusType[] status, ref int count)
        {
            int pending = 0;
            int approved = 0;
            int rejected = 0;
            for (int i = 0; i < count; i++)
            {
                if (status[i] == StatusType.Pending) { pending++; }
                if (status[i] == StatusType.Approved) { approved++; }
                if (status[i] == StatusType.Rejected) { rejected++; }
            }
            Console.WriteLine("=== Reports by Status ===");
            Console.WriteLine($"Pending: {pending}");
            Console.WriteLine($"Approved: {approved}");
            Console.WriteLine($"Rejected: {rejected}");
            Console.WriteLine();
        }
        static void ReportsByType(TypeType[] status, ref int count)
        {
            int collect = 0;
            int analyze = 0;
            int intel = 0;
            int recon = 0;
            for (int i = 0; i < count; i++)
            {
                if (status[i] == TypeType.Collect) { collect++; }
                if (status[i] == TypeType.Analyze) { analyze++; }
                if (status[i] == TypeType.Intel) { intel++; }
                if (status[i] == TypeType.Recon) { recon++; }
            }
            Console.WriteLine("=== Reports by Type ===");
            Console.WriteLine($"Collect: {collect}");
            Console.WriteLine($"Analyze: {analyze}");
            Console.WriteLine($"Intel: {intel}");
            Console.WriteLine($"Recon: {recon}");
            Console.WriteLine();
        }


        static void Main()
        {
            int count =FilterGoodOnes();
            Console.WriteLine("=== All Valid Report ===");
            for (int i = 0; i < count; i++)
            {                
                Console.WriteLine($"Unit Name: {UnitName[i]}");
                Console.WriteLine($"Report Type: {ReportType[i]}");
                Console.WriteLine($"Priority: {Priority[i]}");
                Console.WriteLine($"Score: {Score[i]}");
                Console.WriteLine($"Status: {Status[i]}");
                Console.WriteLine();
            }
            ScoreStats(Score, ref count);
            ReportsByStatus(Status, ref count);
            ReportsByType(ReportType, ref count);

        }
    }
}



