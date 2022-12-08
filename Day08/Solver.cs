using System.Collections.Generic;
using System.Linq;

namespace Day08;

public class Solver
{
    private List<List<int>> _gridListRows;

    private List<List<int>> _gridListCols;

    public Solver()
    {
        GetInputs();
    }

    public int Solve1()
    {
        var count = 0;
        
        for (var i = 0; i < _gridListRows.Count; i++)
        {
            for (var j = 0; j < _gridListRows.Count; j++)
            {
                if (IsVisible(i, j))
                {
                    count++;
                }
            }
        }
        
        return count;
    }
    
    public int Solve2()
    {
        var scenicScores = new List<int>();
        
        for (var i = 1; i < _gridListRows.Count - 1; i++)
        {
            for (var j = 1; j < _gridListRows.Count - 1; j++)
            {
                var scenicScore = 
                    GetViewingDistanceLeft(i, j) 
                    * GetViewingDistanceRight(i, j) 
                    * GetViewingDistanceUp(i, j) 
                    * GetViewingDistanceDown(i, j);
                scenicScores.Add(scenicScore);
            }
        }
        
        return scenicScores.Max();
    }

    bool IsVisible(int i, int j)
    {
        if (i == 0 || j == 0 || i == _gridListRows.First().Count - 1 || j == _gridListRows.First().Count - 1)
        {
            return true;
        }
        
        return 
            IsVisibleToLeft(i, j) 
            || IsVisibleToRight(i, j)
            || IsVisibleUp(i, j)
            || IsVisibleDown(i, j);
    }

    bool IsVisibleToLeft(int rowIndex, int colIndex)
    {
        var height = _gridListRows[rowIndex][colIndex];
        var row = _gridListRows[rowIndex];

        var subList = row.Take(colIndex + 1).ToList();
        var tallTreeCount = subList.Count(x => x >= height);

        return tallTreeCount == 1;
    }

    int GetViewingDistance(List<int> sublist)
    {
        var distance = 0;
        
        for (var i = 1; i < sublist.Count; i++)
        {
            distance++;
            if (sublist[i] >= sublist[0]) break;
        }

        return distance;
    }
    
    int GetViewingDistanceLeft(int rowIndex, int colIndex)
    {
        var row = _gridListRows[rowIndex];

        var subList = row.Take(colIndex + 1).ToList();
        subList.Reverse();
        return GetViewingDistance(subList);
    }
    
    int GetViewingDistanceRight(int rowIndex, int colIndex)
    {
        var row = _gridListRows[rowIndex];

        var subList = row.Skip(colIndex).Take(int.MaxValue).ToList();
        return GetViewingDistance(subList);
    }
    
    int GetViewingDistanceUp(int rowIndex, int colIndex)
    {
        var col = _gridListCols[colIndex];

        var subList = col.Take(rowIndex + 1).ToList();
        subList.Reverse();
        return GetViewingDistance(subList);
    }
    
    int GetViewingDistanceDown(int rowIndex, int colIndex)
    {
        var col = _gridListCols[colIndex];

        var subList = col.Skip(rowIndex).Take(int.MaxValue).ToList();
        return GetViewingDistance(subList);
    }
    
    bool IsVisibleToRight(int rowIndex, int colIndex)
    {
        var height = _gridListRows[rowIndex][colIndex];
        var row = _gridListRows[rowIndex];

        var subList = row.Skip(colIndex).Take(int.MaxValue).ToList();
        var tallTreeCount = subList.Count(x => x >= height);

        return tallTreeCount == 1;
    }
    
    bool IsVisibleUp(int rowIndex, int colIndex)
    {
        var height = _gridListRows[rowIndex][colIndex];
        var col = _gridListCols[colIndex];

        var subList = col.Take(rowIndex + 1).ToList();
        var tallTreeCount = subList.Count(x => x >= height);

        return tallTreeCount == 1;
    }
    
    bool IsVisibleDown(int rowIndex, int colIndex)
    {
        var height = _gridListRows[rowIndex][colIndex];
        var col = _gridListCols[colIndex];

        var subList = col.Skip(rowIndex).Take(int.MaxValue).ToList();
        var tallTreeCount = subList.Count(x => x >= height);

        return tallTreeCount == 1;
    }
    
    void GetInputs()
    {
        var lines = System.IO.File.ReadAllLines("input.txt");

        _gridListRows = Enumerable.Range(1, lines.Length).Select(_ => Enumerable.Repeat(0, lines.Length).ToList()).ToList();
        _gridListCols = Enumerable.Range(1, lines.Length).Select(_ => Enumerable.Repeat(0, lines.Length).ToList()).ToList();

        for (var i = 0; i < lines.Length; i++)
        {
            for (var j = 0; j < lines.Length; j++)
            {
                _gridListRows[i][j] = (int)char.GetNumericValue(lines[i][j]);
                _gridListCols[j][i] = (int)char.GetNumericValue(lines[i][j]);
            }
        }
    }
}