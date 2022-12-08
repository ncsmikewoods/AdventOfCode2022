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
                var scenicScore = GetScenicScore(i, j);
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
            Helper.IsVisibleToLeft(i, j, _gridListRows) 
            || Helper.IsVisibleToRight(i, j, _gridListRows)
            || Helper.IsVisibleUp(i, j, _gridListCols)
            || Helper.IsVisibleDown(i, j, _gridListCols);
    }

    int GetScenicScore(int i, int j)
    {
        return
            Helper.GetViewingDistanceLeft(i, j, _gridListRows)
            * Helper.GetViewingDistanceRight(i, j, _gridListRows)
            * Helper.GetViewingDistanceUp(i, j, _gridListCols)
            * Helper.GetViewingDistanceDown(i, j, _gridListCols);
    }
    
    void GetInputs()
    {
        var lines = System.IO.File.ReadAllLines("input.txt");

        // Create/initialize a row-wise and column-wise grid
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