class Program
{
    const string OuputPath = @"..\..\..\output.txt";
    static char[,] completemap;
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
            string[] lines = File.ReadAllLines(path);
            InitializeArray(lines);
            SuccessMessage("Success.");
        }
        catch
        {
            FailMessage("Invalid file path or input data.");
        }
    }

    public static void InitializeArray(string[] lines) 
    {
        if (lines == null)
            FailMessage("file is empty");
        if (int.TryParse(lines[0], out int N) && int.TryParse(lines[0], out int line1) && int.TryParse(lines[0], out int line2) && int.TryParse(lines[0], out int line3))
        {
            int[,] map = new int[N, N];
            completemap = new char[N, N];
            completemap[0, 0] = '#';
            completemap[N-1, N-1] = '#';
            List<char> array = new List<char>();
            for (int i = 1; i < lines.Length; i++)
            {
                foreach (var item in lines[i])
                {
                    array.Add(item);
                }
            }
            int counter = 0;
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    int.TryParse(array[counter].ToString(),out int res);
                    map[i, j] = res;
                    counter++;
                }
            }
            Solve(map,N-1,N-1);
            string result = string.Empty;
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (completemap[i, j] == '\0')
                        result+=".";
                    else
                        result+="#";
                }
                result += "\n";
            }
            SuccessMessage(result);
        }
        else 
        {
            Console.WriteLine("Wrong data");
        }
        
    }

    public static int Solve(int[,] arr,int n,int m) 
    {
        while (n >= 0 && m >= 0) 
        {
            if (n == 0 && m == 0)
                return 0;
            else if (n - 1 < 0)
            {
                completemap[n, m - 1] = '#';
                return Solve(arr, n, m - 1);
            }
            else if (m - 1 < 0)
            {
                completemap[n - 1, m] = '#';
                return Solve(arr, n - 1, m);
            }
            else if (arr[n - 1, m] < arr[n, m - 1])
            {
                completemap[n-1,m]= '#';
                return Solve(arr, n - 1, m);
            } 
            else 
            {
                completemap[n, m-1] = '#';
                return Solve(arr, n, m - 1);
            }
        }
        return 0;            
    }

    public static void SuccessMessage(string text)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(text);
        Console.ForegroundColor = ConsoleColor.White;
    }

    public static void FailMessage(string text)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(text);
        Console.ForegroundColor = ConsoleColor.White;
    }
}
