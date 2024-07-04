using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    // Rotation Matrix for 90 degrees (clockwise) rotation
    public static readonly float cos = Mathf.Cos(Mathf.PI / 2f);
    public static readonly float sin = Mathf.Sin(Mathf.PI / 2f);
    public static readonly float[] RotationMatrix = new float[] { cos, sin, -sin, cos };

    // Tetromino Cell Data
    public static readonly Dictionary<Tetromino, Vector3Int[]> Cells = new Dictionary<Tetromino, Vector3Int[]>()
    {
        { Tetromino.I, new Vector3Int[] { new Vector3Int(-1, 1,0), new Vector3Int( 0, 1,0), new Vector3Int( 1, 1,0), new Vector3Int( 2, 1,0) } },
        { Tetromino.J, new Vector3Int[] { new Vector3Int(-1, 1,0), new Vector3Int(-1, 0,0), new Vector3Int( 0, 0,0), new Vector3Int( 1, 0,0) } },
        { Tetromino.L, new Vector3Int[] { new Vector3Int( 1, 1,0), new Vector3Int(-1, 0,0), new Vector3Int( 0, 0,0), new Vector3Int( 1, 0,0) } },
        { Tetromino.O, new Vector3Int[] { new Vector3Int( 0, 1,0), new Vector3Int( 1, 1,0), new Vector3Int( 0, 0,0), new Vector3Int( 1, 0,0) } },
        { Tetromino.S, new Vector3Int[] { new Vector3Int( 0, 1,0), new Vector3Int( 1, 1,0), new Vector3Int(-1, 0,0), new Vector3Int( 0, 0,0) } },
        { Tetromino.T, new Vector3Int[] { new Vector3Int( 0, 1,0), new Vector3Int(-1, 0,0), new Vector3Int( 0, 0,0), new Vector3Int( 1, 0,0) } },
        { Tetromino.Z, new Vector3Int[] { new Vector3Int(-1, 1,0), new Vector3Int( 0, 1,0), new Vector3Int( 0, 0,0), new Vector3Int( 1, 0,0) } },
    };

    // Wall Kick Data for Tetromino I
    private static readonly Vector3Int[,] WallKicksI = new Vector3Int[,] {
        { new Vector3Int(0, 0,0), new Vector3Int(-2, 0,0), new Vector3Int( 1, 0,0), new Vector3Int(-2,-1,0), new Vector3Int( 1, 2,0) },
        { new Vector3Int(0, 0,0), new Vector3Int( 2, 0,0), new Vector3Int(-1, 0,0), new Vector3Int( 2, 1,0), new Vector3Int(-1,-2,0) },
        { new Vector3Int(0, 0,0), new Vector3Int(-1, 0,0), new Vector3Int( 2, 0,0), new Vector3Int(-1, 2,0), new Vector3Int( 2,-1,0) },
        { new Vector3Int(0, 0,0), new Vector3Int( 1, 0,0), new Vector3Int(-2, 0,0), new Vector3Int( 1,-2,0), new Vector3Int(-2, 1,0) },
        { new Vector3Int(0, 0,0), new Vector3Int( 2, 0,0), new Vector3Int(-1, 0,0), new Vector3Int( 2, 1,0), new Vector3Int(-1,-2,0) },
        { new Vector3Int(0, 0,0), new Vector3Int(-2, 0,0), new Vector3Int( 1, 0,0), new Vector3Int(-2,-1,0), new Vector3Int( 1, 2,0) },
        { new Vector3Int(0, 0,0), new Vector3Int( 1, 0,0), new Vector3Int(-2, 0,0), new Vector3Int( 1,-2,0), new Vector3Int(-2, 1,0) },
        { new Vector3Int(0, 0,0), new Vector3Int(-1, 0,0), new Vector3Int( 2, 0,0), new Vector3Int(-1, 2,0), new Vector3Int( 2,-1,0) },
    };

    // Wall Kick Data for Tetrominoes J, L, O, S, T, Z
    private static readonly Vector3Int[,] WallKicksJLOSTZ = new Vector3Int[,] {
        { new Vector3Int(0, 0,0), new Vector3Int(-1, 0,0), new Vector3Int(-1, 1,0), new Vector3Int(0,-2,0), new Vector3Int(-1,-2,0) },
        { new Vector3Int(0, 0,0), new Vector3Int( 1, 0,0), new Vector3Int( 1,-1,0), new Vector3Int(0, 2,0), new Vector3Int( 1, 2,0) },
        { new Vector3Int(0, 0,0), new Vector3Int( 1, 0,0), new Vector3Int( 1,-1,0), new Vector3Int(0, 2,0), new Vector3Int( 1, 2,0) },
        { new Vector3Int(0, 0,0), new Vector3Int(-1, 0,0), new Vector3Int(-1, 1,0), new Vector3Int(0,-2,0), new Vector3Int(-1,-2,0) },
        { new Vector3Int(0, 0,0), new Vector3Int( 1, 0,0), new Vector3Int( 1, 1,0), new Vector3Int(0,-2,0), new Vector3Int( 1,-2,0) },
        { new Vector3Int(0, 0,0), new Vector3Int(-1, 0,0), new Vector3Int(-1,-1,0), new Vector3Int(0, 2,0), new Vector3Int(-1, 2,0) },
        { new Vector3Int(0, 0,0), new Vector3Int(-1, 0,0), new Vector3Int(-1,-1,0), new Vector3Int(0, 2,0), new Vector3Int(-1, 2,0) },
        { new Vector3Int(0, 0,0), new Vector3Int( 1, 0,0), new Vector3Int( 1, 1,0), new Vector3Int(0,-2,0), new Vector3Int( 1,-2,0) },
    };

    // Dictionary for Wall Kick Data
    public static readonly Dictionary<Tetromino, Vector3Int[,]> WallKicks = new Dictionary<Tetromino, Vector3Int[,]>()
    {
        { Tetromino.I, WallKicksI },
        { Tetromino.J, WallKicksJLOSTZ },
        { Tetromino.L, WallKicksJLOSTZ },
        { Tetromino.O, WallKicksJLOSTZ },
        { Tetromino.S, WallKicksJLOSTZ },
        { Tetromino.T, WallKicksJLOSTZ },
        { Tetromino.Z, WallKicksJLOSTZ },
    };

}
