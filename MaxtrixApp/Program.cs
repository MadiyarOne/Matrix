using System;

class Program
{
    const int MaxMatrixSize = 100;
    const int Filled = 1;
    const int Empthy = 0;

    static readonly int[] RowDirections = { -1, 1, 0, 0 };
    static readonly int[] ColDirections = { 0, 0, -1, 1 };

    static void Main()
    {
        Console.WriteLine("Enter the matrix as a string (e.g., '1,0,1;0,1,0'): ");
        var input = Console.ReadLine();

        var result = CalculateArea(input);
        Console.WriteLine("Number of areas formed by 1s: " + result);
    }

    static int CalculateArea(string input)
    {
        var matrix = ParseMatrix(input);
        var rowCount = matrix.Length;
        var colCount = matrix[0].Length;

        var visited = InitializeVisitedArray(rowCount, colCount);

        var areaCount = 0;

        for (var i = 0; i < rowCount; i++)
        {
            for (var j = 0; j < colCount; j++)
            {
                if (matrix[i][j] != Filled || visited[i][j]) continue;
                ExploreArea(matrix, visited, i, j);
                areaCount++;
            }
        }

        return areaCount;
    }

    static int[][] ParseMatrix(string input)
    {
        if (input is null)
        {
            throw new Exception("Input is null");
        }

        if (input.Contains('"'))
        {
            input = input.Replace("\"", "");
        }

        input = input.Trim();
        
        var rows = input.Split(';');
        var rowCount = rows.Length;

        var matrix = new int[rowCount][];
        for (var i = 0; i < rowCount; i++)
        {
            matrix[i] = Array.ConvertAll(rows[i].Split(','), int.Parse);
        }

        return matrix;
    }

    static bool[][] InitializeVisitedArray(int rowCount, int colCount)
    {
        var visited = new bool[rowCount][];
        for (var i = 0; i < rowCount; i++)
        {
            visited[i] = new bool[colCount];
        }
        return visited;
    }

    static void ExploreArea(int[][] matrix, bool[][] visited, int row, int col)
    {
        if (row < 0 || col < 0 || row >= matrix.Length || col >= matrix[0].Length || matrix[row][col] == Empthy || visited[row][col])
        {
            return;
        }

        visited[row][col] = true;

        for (var i = 0; i < 4; i++)
        {
            var newRow = row + RowDirections[i];
            var newCol = col + ColDirections[i];
            ExploreArea(matrix, visited, newRow, newCol);
        }
    }
}
