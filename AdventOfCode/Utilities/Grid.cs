using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Utilities
{
  public class Grid<T>
  {
    public int Width
    {
      get;
    }

    public int Height
    {
      get;
    }

    public Grid( int width, int height )
    {
      Width = width;
      Height = height;
    }

    public Grid(T[][] arrays)
    {
      
    }

    public Grid(List<List<T>> listOfLists)
    {

    }

    #region

    public List<T> GetRow( int rowIndex )
    {
      return null;
    }

    public List<T> GetColumn(int columnIndex)
    {
      return null;
    }



    public override string ToString()
    {
      return base.ToString();
    }

    #endregion
  }
}
