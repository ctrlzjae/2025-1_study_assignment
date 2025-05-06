using System;
using System.Linq;


namespace statistics
{
    class Program
    {
        static void Main(string[] args)
        {
            string[,] data = {
                {"StdNum", "Name", "Math", "Science", "English"},
                {"1001", "Alice", "85", "90", "78"},
                {"1002", "Bob", "92", "88", "84"},
                {"1003", "Charlie", "79", "85", "88"},
                {"1004", "David", "94", "76", "92"},
                {"1005", "Eve", "72", "95", "89"}
            };
            // You can convert string to double by
            // double.Parse(str)

            int stdCount = data.GetLength(0) - 1;
            // ---------- TODO ----------
            int[] math = new int[stdCount];
            int[] science = new int[stdCount];
            int[] english = new int[stdCount];
            int[] total = new int[stdCount];
            string[] names = new string[stdCount];

            for (int i = 1; i <= stdCount; i++)
            {
                names[i - 1] = data[i, 1];
                math[i - 1] = int.Parse(data[i, 2]);
                science[i - 1] = int.Parse(data[i, 3]);
                english[i - 1] = int.Parse(data[i, 4]);
                total[i - 1] = math[i - 1] + science[i - 1] + english[i - 1];
            }


            Console.WriteLine("Average Scores: ");
            Console.WriteLine($"Math: {math.Average():0.00}");
            Console.WriteLine($"Science: {science.Average():0.00}");
            Console.WriteLine($"English: {english.Average():0.00}");

            Console.WriteLine("\nMax and min Scores: ");
            Console.WriteLine($"Math: ({math.Max()}, {math.Min()})");
            Console.WriteLine($"Science: ({science.Max()}, {science.Min()})");
            Console.WriteLine($"English: ({english.Max()}, {english.Min()})");


            var totalsWithNames = names.Zip(total, (name, score) => new { name, score }).ToList();
            var ranked = totalsWithNames
                .OrderByDescending(x => x.score)
                .Select((x, idx) => new { x.name, Rank = idx + 1 })
                .ToDictionary(x => x.name, x => x.Rank);


            Console.WriteLine("\nStudents rank by total scores:");
            foreach (var name in names)
            {
                string suffix = GetRankSuffix(ranked[name]);
                Console.WriteLine($"{name}: {ranked[name]}{suffix}");
            }
        }


        static string GetRankSuffix(int rank)
        {
            if (rank % 100 >= 11 && rank % 100 <= 13)
                return "th";
            switch (rank % 10)
            {
                case 1: return "st";
                case 2: return "nd";
                case 3: return "rd";
                default: return "th";
            }
        }
        // --------------------
    }
}

/* example output

Average Scores: 
Math: 84.40
Science: 86.80
English: 86.20

Max and min Scores: 
Math: (94, 72)
Science: (95, 76)
English: (92, 78)

Students rank by total scores:
Alice: 2nd
Bob: 5th
Charlie: 1st
David: 4th
Eve: 3rd

*/
