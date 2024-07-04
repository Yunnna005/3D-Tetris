using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public BuildingSystem board { get; private set; }
    public TetrominoData data { get; private set; }
    public Vector3Int[] cells { get; private set; }
    public Vector3Int position { get; private set; }

    void Update()
    {
        print("I update");
        board.Clear();

        if (Input.GetKeyDown(KeyCode.A))
        {
            print("I want to move to the left");

            Move(Vector3Int.left);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            print("I want to move to the right");
            Move(Vector3Int.right);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Move(Vector3Int.down);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            Move(Vector3Int.up);
        }

        board.Set(this);
    }
    public void Initialize(BuildingSystem board, Vector3Int position, TetrominoData data)
    {
        this.board = board;
        this.position = position;
        this.data = data;

        if (this.cells == null)
        {
            this.cells = new Vector3Int[data.cells.Length];
        }

        for (int i = 0; i < data.cells.Length; i++)
        {
            this.cells[i] = data.cells[i];
        }
    }

    private bool Move(Vector3Int translation)
    {
        Vector3Int newPosition = this.position;
        newPosition.x += translation.x;
        newPosition.y += translation.y;
        newPosition.z = -1;

        bool valid = board.IsValidPosition(this, newPosition);

        if (valid) 
        { 
            this.position = newPosition;
        }

        return valid;
    }
}
