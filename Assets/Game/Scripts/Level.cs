using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Game Data/Level")]
public class Level : ScriptableObject
{
    public int Rows => _rows;
    public int Columns => _columns;
    public RowData[] Layout => _layout;

    [SerializeField] int _rows;
    [SerializeField] int _columns;
    [SerializeField] RowData[] _layout;

    [Serializable]
    public class RowData
    {
        public int[] Bricks => _bricks;
        [SerializeField] int[] _bricks;
    }
}