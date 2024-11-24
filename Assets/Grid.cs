using System;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
#region Parameters
    public static bool isInitialized{get; private set;} = false;
    public static int cellsize{get;private set;} = 2;
    public static int horizontals{get;private set;} = 100;
    public static int verticals{get;private set;} = 100;
    private static bool[,] grids;

    private static bool[,] tempGrids;
#endregion

#region Methods
    /// <summary>
    /// 初始化Grid
    /// </summary>
    /// <param name="_size">细胞大小</param>
    /// <param name="_horizontals">细胞横向数量</param>
    /// <param name="_verticals">细胞纵向数量</param>
    public static void Initialize(int _size = 2, int _horizontals = 100, int _verticals = 100)
    {
        grids = new bool[_horizontals, _verticals];
        tempGrids = new bool[_horizontals, _verticals];
        cellsize = _size;
        horizontals = _horizontals;
        verticals = _verticals;

        Debug.Log(string.Format("Grid initialized:{0} {1}", _horizontals, _verticals));
        isInitialized = true;
    }

    /// <summary>
    /// 读取指定位置的细胞状态
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public static bool ReadCell(int x, int y)
    {
        if(isInitialized == false)
        {
            Debug.LogWarning("Grid is not initialized");
            return false;
        }
        if(x >=0 && x < horizontals && y >= 0 && y < verticals)
        {
            return grids[x, y];
        }
        Debug.LogWarning("尝试读取超出边界的格子");
        return false;
    }

    /// <summary>
    /// 设置指定位置的细胞状态
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="value"></param>
    public static void SetCell(int x, int y, bool value)
    {
        if(isInitialized == false)
        {
            Debug.LogWarning("Grid is not initialized");
            return;
        }
        if(x >=0 && x < horizontals && y >= 0 && y < verticals)
        {
            grids[x, y] = value;
        }
    }

    /// <summary>
    /// 更新指定位置的细胞状态
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public static void UpdateCell(int x, int y)
    {
        if(isInitialized == false)
        {
            Debug.LogWarning("Grid is not initialized");
            return;
        }
        int flag = 0;
        for(int i = -1; i <= 1 ; i++)
        {
            for(int j = -1; j <= 1; j++)
            {
                if(i==0 && j == 0)
                {
                    continue;
                }
                else if(x+i >=0 && x+i < horizontals && y+j >= 0 && y+j < verticals)
                {
                    if(tempGrids[x+i, y+j])
                    {
                        flag++;
                    }
                }
            }
        }

        if(flag == 3)
        {
            grids[x, y] = true;
        }
        else if(flag == 2)
        {
            grids[x, y] = tempGrids[x, y];
        }
        else
        {
            grids[x, y] = false;
        }
    }

    /// <summary>
    /// 更新所有细胞状态
    /// </summary>
    public static void UpdateAllCells()
    {
        if(isInitialized == false)
        {
            Debug.LogWarning("Grid is not initialized");
            return;
        }
        for(int i = 0; i < horizontals; i++)
        {
            for(int j = 0; j < verticals; j++)
            {
                tempGrids[i, j] = grids[i, j];
            }
        }

        for(int i = 0; i < horizontals; i++)
        {
            for(int j = 0; j < verticals; j++)
            {
                UpdateCell(i, j);
            }
        }
    }
#endregion
}