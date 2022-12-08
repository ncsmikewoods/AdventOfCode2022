using System;
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

        var blah = new List<string>();
        
        for (var i = 0; i < _gridListRows.Count; i++)
        {
            for (var j = 0; j < _gridListRows.Count; j++)
            {
                if (IsVisible(i, j))
                {
                    count++;
                    blah.Add($"{i},{j}");
                }
            }
        }
        
        return count;
    }
    
    public int Solve2()
    {
        return 0;
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