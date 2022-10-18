class Program
{
    const string OuputPath = @"..\..\..\output.txt";

    public static void Main(string[] args)
    {
        using (StreamWriter sw = new StreamWriter(OuputPath, false))
        {
            sw.Write("");
        }
        Console.WriteLine("Write input data path:");
        string path = Console.ReadLine();
        try
        {
            string[] tasks = File.ReadAllLines(path);
            Success("Success.");
            foreach (var task in tasks)
                WriteInfoAboutTask(task);
        }
        catch
        {
            Fail("Invalid file path.");
        }
        Console.WriteLine("Output file:");
        string text = File.ReadAllText(OuputPath);
        Console.ForegroundColor=ConsoleColor.Yellow;
        Console.WriteLine(text);
    }

    public static void WriteInfoAboutTask(string task)
    {
        string[] info = task.Split();
        Console.WriteLine("|№\t|N\t|K\t|");
        try
        {
            Console.WriteLine($"|{info[0]}\t|{info[1]}\t|{info[2]}\t|");
            if (int.TryParse(info[1], out int N) && int.TryParse(info[2], out int K)) 
            {
                string result = CountTheNumberOfWays(N, K);
                using (StreamWriter sw = new StreamWriter(OuputPath, true)) 
                {
                    sw.WriteLine($"Task #{info[0]}|N: {info[1]}|K: {info[2]}|Result: {result}");
                }
                Success("Result: " + result);
            }
                
            else
            {
                Fail("Result: Invalid data");
                Fail("Correct data format: 1 8 8");
            }
        }
        catch 
        {
            Fail("Result: Invalid data");
            Fail("Correct data format: 1 8 8");
        }
    }

    public static string CountTheNumberOfWays(int n, int k)
    {
        if (k > n || k <= 0 || n <= 0) 
        {
            return "0";
        }
        else if(k == 2)
        {
            int result = Factorial(n) * Factorial(n) / Factorial(n - k) * Factorial(n - k);
            return result.ToString();
        }
        else 
        {
            int result = Factorial(n) * Factorial(n) / Factorial(n - k) * Factorial(n - k) / Factorial(k);
            return result.ToString();
        }
    }

    public static int Factorial(int n) 
    {
        if (n == 1 || n == 0)
            return 1;
        else
            return n * Factorial(n - 1);
    }

    public static void Success(string text) 
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(text);
        Console.ForegroundColor = ConsoleColor.White;
    }

    public static void Fail(string text) 
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(text);
        Console.ForegroundColor = ConsoleColor.White;
    }
}
