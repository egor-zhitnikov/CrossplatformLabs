
using CrossplatformLab2;
using System.Numerics;
using System.Text;

class Program
{
    const string OuputPath = @"..\..\..\output.txt";
    static string[] tasklines;

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
            tasklines = File.ReadAllLines(path);
        }
        catch
        {
            FailMessage("Inccorect file path.");
            return;
        }
        CheckInput();       
    }

    public static void CheckInput()
    {
        //Проверка на буквы
        for (int i = 0; i < tasklines.Length; i++)
        {
            try
            {
                int a = int.Parse(tasklines[i]);
            }
            catch
            {
                FailMessage($"Input error in line {i} : {tasklines[i]}");
            }
        }
        if (int.Parse(tasklines[0]) != tasklines.Length - 1 || tasklines[1].Length!= int.Parse(tasklines[0]))
        {
            FailMessage("Inccorect N.");
            return;
        }
        char [,] path = FindPath(Solve(InitializeField()));
        DrawPath(path);
    }

    static void DrawPath(char[,]path) 
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < path.GetLength(0); i++)
        {
            for (int j = 0; j < path.GetLength(1); j++)
            {
                if (path[i, j] == '\0')
                {
                    Console.Write('.');
                    sb.Append(".");
                }
                else 
                {
                    Console.Write(path[i, j]);
                    sb.Append("#");
                }
                
            }
            Console.WriteLine();
            sb.Append("\n");
        }
        File.WriteAllText(OuputPath,sb.ToString());
    }

    static int [,] InitializeField() 
    {
        int[,] field = new int[int.Parse(tasklines[0]), int.Parse(tasklines[0])];
        for (int i = 1; i < tasklines.Length; i++)
        {
            string line = tasklines[i];
            for (int j = 0; j < line.Length; j++)
            {
                field[i - 1, j] = int.Parse(line[j].ToString());
            }
        }
        for (int i = 0; i < field.GetLength(0); i++)
        {
            for (int j = 0; j < field.GetLength(0); j++)
            {
                Console.Write(field[i, j].ToString());
            }
            Console.WriteLine();
        }
        Console.WriteLine();
        return field;
    }

    static int [,] Solve(int[,] field) 
    {
        int[,] result = new int [field.GetLength(0),field.GetLength(1)];
        List<Cell> coords = new List<Cell>();
        coords.Add(new Cell(field[result.GetLength(0)-1, result.GetLength(0) - 1], result.GetLength(0) - 1, result.GetLength(0) - 1));
        result[field.GetLength(0) - 1, field.GetLength(0) - 1] = field[field.GetLength(0) - 1, field.GetLength(0) - 1];
        while (coords.Count!=0) 
        {
            int count = coords.Count;
            int index = 0;
            for (int h=0;h<count;h++)
            {
                if (coords.Count == 0)
                    break;
                if (coords[index].X - 1 >= 0)
                {
                    if (result[coords[index].X - 1, coords[index].Y] > coords[index].Value + field[coords[index].X - 1, coords[index].Y] || result[coords[index].X - 1, coords[index].Y] == 0)
                    {
                        coords.Add(new Cell(field[coords[index].X - 1, coords[index].Y] + coords[index].Value, coords[index].X - 1, coords[index].Y));
                        result[coords[index].X - 1, coords[index].Y] = field[coords[index].X - 1, coords[index].Y] + coords[index].Value;
                    }
                    
                }
                if (coords[index].Y - 1 >= 0) 
                {
                    coords.Add(new Cell(field[coords[index].X, coords[index].Y - 1] + coords[index].Value, coords[index].X, coords[index].Y - 1));
                    result[coords[index].X, coords[index].Y - 1] = field[coords[index].X , coords[index].Y-1] + coords[index].Value;
                }
                
                coords.Remove(coords[index]);
                /*for (int f = 0; f < result.GetLength(0); f++)
                {
                    for (int j = 0; j < result.GetLength(1); j++)
                    {
                        Console.Write(result[f, j].ToString() + " \t");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();*/
            }
        }
        return result;
    }

    static char [,] FindPath(int[,] field) 
    {
        int fieldsize = field.GetLength(0) - 1;
        char[,] path = new char[fieldsize+1,fieldsize+1];
        Cell Finish = new Cell(0,0,0);
        Cell currentcell = new Cell(field[fieldsize, fieldsize], fieldsize, fieldsize);
        while (currentcell.X != Finish.X || currentcell.Y != Finish.Y) 
        {
            path[currentcell.X, currentcell.Y] = '#';
            Cell top=null;
            Cell left=null;
            if (currentcell.X - 1 >= 0) 
            {
                top = new Cell(field[currentcell.X - 1, currentcell.Y], currentcell.X - 1, currentcell.Y);
            }
            if (currentcell.Y - 1 >= 0)
            {
                left = new Cell(field[currentcell.X, currentcell.Y - 1], currentcell.X, currentcell.Y - 1);
            }
            currentcell = ChooseCell(top,left);
            
        }
        path[0, 0] = '#';
        return path;
    }

    static Cell ChooseCell(Cell top, Cell left) 
    {
        if (top == null)
            return left;
        else if (left == null)
            return top;
        else 
        {
            if (top.Value >= left.Value)
                return left;
            else
                return top;
        }
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
