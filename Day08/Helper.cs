using System.Collections.Generic;
using System.Linq;

namespace Day08;

// If the ugly code is in another file then it's clean, right?  Right?...
public static class Helper
{
    public static bool IsVisibleToLeft(int rowIndex, int colIndex, List<List<int>> gridListRows)
    {
        var subList = GetSublistLeft(rowIndex, colIndex, gridListRows);
        return IsVisible(subList);
    }

    public static bool IsVisibleToRight(int rowIndex, int colIndex, List<List<int>> gridListRows)
    {
        var subList = GetSubListRight(rowIndex, colIndex, gridListRows);
        return IsVisible(subList);
    }

    public static bool IsVisibleUp(int rowIndex, int colIndex, List<List<int>> gridListCols)
    {
        var subList = GetSubListUp(rowIndex, colIndex, gridListCols);
        return IsVisible(subList);
    }

    public static bool IsVisibleDown(int rowIndex, int colIndex, List<List<int>> gridListCols)
    {
        var subList = GetSubListDown(rowIndex, colIndex, gridListCols);
        return IsVisible(subList);
    }

    static bool IsVisible(List<int> subList)
    {
        var height = subList.First();
        return subList.Count(x => x >= height) == 1;
    }
    
    public static int GetViewingDistanceLeft(int rowIndex, int colIndex, List<List<int>> gridListRows)
    {
        var subList = GetSublistLeft(rowIndex, colIndex, gridListRows);
        return GetViewingDistance(subList);
    }
    
    public static int GetViewingDistanceRight(int rowIndex, int colIndex, List<List<int>> gridListRows)
    {
        var subList = GetSubListRight(rowIndex, colIndex, gridListRows);
        return GetViewingDistance(subList);
    }
    
    public static int GetViewingDistanceUp(int rowIndex, int colIndex, List<List<int>> gridListCols)
    {
        var subList = GetSubListUp(rowIndex, colIndex, gridListCols);
        return GetViewingDistance(subList);
    }
    
    public static int GetViewingDistanceDown(int rowIndex, int colIndex, List<List<int>> gridListCols)
    {
        var subList = GetSubListDown(rowIndex, colIndex, gridListCols);
        return GetViewingDistance(subList);
    }
    
    static int GetViewingDistance(List<int> subList)
    {
        var distance = 0;
        for (var i = 1; i < subList.Count; i++)
        {
            distance++;
            if (subList[i] >= subList[0]) break;
        }
        return distance;
    }
    
    static List<int> GetSublistLeft(int rowIndex, int colIndex, List<List<int>> gridListRows)
    {
        var row = gridListRows[rowIndex];

        var subList = row.Take(colIndex + 1).ToList();
        subList.Reverse();
        return subList;
    }
    
    static List<int> GetSubListRight(int rowIndex, int colIndex, List<List<int>> gridListRows)
    {
        var row = gridListRows[rowIndex];

        var subList = row.Skip(colIndex).Take(int.MaxValue).ToList();
        return subList;
    }
    
    static List<int> GetSubListUp(int rowIndex, int colIndex, List<List<int>> gridListCols)
    {
        var col = gridListCols[colIndex];

        var subList = col.Take(rowIndex + 1).ToList();
        subList.Reverse();
        return subList;
    }
    
    static List<int> GetSubListDown(int rowIndex, int colIndex, List<List<int>> gridListCols)
    {
        var col = gridListCols[colIndex];

        var subList = col.Skip(rowIndex).Take(int.MaxValue).ToList();
        return subList;
    }
}