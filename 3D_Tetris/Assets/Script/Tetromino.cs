using UnityEngine.Tilemaps;
using UnityEngine;

public enum Tetromino{
    I,
    O,
    T,
    J,
    L,
    S,
    Z
}

[System.Serializable]
public class TetrominoData
{
    public Tetromino tetromino;
    public GameObject prefab;
    public Vector3Int[] cells { get; private set; }

    public void Initialize()
    {
        this.cells = Data.Cells[this.tetromino];
    }

}