using System.Collections.Generic;
using System.Linq;

namespace Day08;

// If the ugly code is in another file then it's clean, right?  Right?...
public static class Helper
{
    public static bool IsVisibleToLeft(int rowIndex, int colIndex, List<List<int>> gridListRows)
    {
        var height = gridListRows[rowIndex][colIndex];
        var row = gridListRows[rowIndex];

        var subList = row.Take(colIndex + 1).ToList();
        var tallTreeCount = subList.Count(x => x >= height);

        return tallTreeCount == 1;
    }
    
    public static bool IsVisibleToRight(int rowIndex, int colIndex, List<List<int>> gridListRows)
    {
        var height = gridListRows[rowIndex][colIndex];
        var row = gridListRows[rowIndex];

        var subList = row.Skip(colIndex).Take(int.MaxValue).ToList();
        var tallTreeCount = subList.Count(x => x >= height);

        return tallTreeCount == 1;
    }
    
    public static bool IsVisibleUp(int rowIndex, int colIndex, List<List<int>> gridListCols)
    {
        var height = gridListCols[colIndex][rowIndex];
        var col = gridListCols[colIndex];

        var subList = col.Take(rowIndex + 1).ToList();
        var tallTreeCount = subList.Count(x => x >= height);

        return tallTreeCount == 1;
    }
    
    public static bool IsVisibleDown(int rowIndex, int colIndex, List<List<int>> gridListCols)
    {
        var height = gridListCols[colIndex][rowIndex];
        var col = gridListCols[colIndex];

        var subList = col.Skip(rowIndex).Take(int.MaxValue).ToList();
        var tallTreeCount = subList.Count(x => x >= height);

        return tallTreeCount == 1;
    }
    
    public static int GetViewingDistance(List<int> sublist)
    {
        var distance = 0;
        
        for (var i = 1; i < sublist.Count; i++)
        {
            distance++;
            if (sublist[i] >= sublist[0]) break;
        }

        return distance;
    }
    
    public static int GetViewingDistanceLeft(int rowIndex, int colIndex, List<List<int>> gridListRows)
    {
        var row = gridListRows[rowIndex];

        var subList = row.Take(colIndex + 1).ToList();
        subList.Reverse();
        return GetViewingDistance(subList);
    }
    
    public static int GetViewingDistanceRight(int rowIndex, int colIndex, List<List<int>> gridListRows)
    {
        var row = gridListRows[rowIndex];

        var subList = row.Skip(colIndex).Take(int.MaxValue).ToList();
        return GetViewingDistance(subList);
    }
    
    public static int GetViewingDistanceUp(int rowIndex, int colIndex, List<List<int>> gridListCols)
    {
        var col = gridListCols[colIndex];

        var subList = col.Take(rowIndex + 1).ToList();
        subList.Reverse();
        return GetViewingDistance(subList);
    }
    
    public static int GetViewingDistanceDown(int rowIndex, int colIndex, List<List<int>> gridListCols)
    {
        var col = gridListCols[colIndex];

        var subList = col.Skip(rowIndex).Take(int.MaxValue).ToList();
        return GetViewingDistance(subList);
    }
}