using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public BuildingSystem board { get; private set; }
    public TetrominoData data { get; private set; }
    public Vector3Int[] cells { get; private set; }
    public Vector3Int position { get; private set; }
    public int rotationIndex {  get; private set; }

    public float stepDelay = 1f;
    public float lockDelay = 0.5f;

    private float stepTime;
    private float lockTime;

    public bool isMoving;
    #region Movement

    private void Start()
    {
        isMoving = true;
    }
    void Update()
    {
        if (isMoving) { 
        board.Clear();
        this.lockTime += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.A))
        {
            Move(Vector3Int.left);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            Move(Vector3Int.right);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            Move(Vector3Int.up);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            HardDrop();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Rotate(-1);
        } else if (Input.GetKeyDown(KeyCode.E))
        {
            Rotate(1);
        }

        if (Time.time >= stepTime)
        {
            Step();
        }
                board.Set(this);
        }

    }

    private void Step()
    {
        stepTime = Time.time + stepDelay;

        Move(Vector3Int.down);

        if (lockTime >= stepDelay) { 
            Lock();
        }
        
    }
    private void HardDrop()
    {
        while (Move(Vector3Int.down))
        {
            continue;
        }
        Lock();
    }

    private void Lock()
    {
        isMoving = false;
        print("Lock");
        //board.Set(this);
        //board.SpawnPiece();
        board.StartCoroutine(board.DelayedSpawnPiece());
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
            this.lockTime = 0f;
        }
        return valid;
    }
    #endregion

    public void Initialize(BuildingSystem board, Vector3Int position, TetrominoData data)
    {
        this.board = board;
        this.position = position;
        this.data = data;
        this.rotationIndex = 0;
        this.stepTime = Time.time + stepDelay;
        this.lockTime = 0f;

        if (this.cells == null)
        {
            this.cells = new Vector3Int[data.cells.Length];
        }

        for (int i = 0; i < data.cells.Length; i++)
        {
            this.cells[i] = data.cells[i];
        }
    }

    #region Rotation
    private void Rotate(int direction)
    {
        int originalRotation = this.rotationIndex;
        rotationIndex = Wrap(rotationIndex + direction, 0, 4);

        ApplyRotationMatrix(direction);

        if (!TestWallKicks(rotationIndex, direction))
        {
            rotationIndex = originalRotation;
            ApplyRotationMatrix(-direction);
        }
    }

    private void ApplyRotationMatrix(int direction)
    {
        for (int i = 0; i < this.cells.Length; i++)
        {
            Vector3 cell = cells[i];
            int x, y;
            switch (this.data.tetromino)
            {
                case Tetromino.I:
                case Tetromino.O:
                    cell.x -= 0.5f;
                    cell.y -= 0.5f;
                    x = Mathf.CeilToInt((cell.x * Data.RotationMatrix[0] * direction) + (cell.y * Data.RotationMatrix[1] * direction));
                    y = Mathf.CeilToInt((cell.x * Data.RotationMatrix[2] * direction) + (cell.y * Data.RotationMatrix[3] * direction));
                    break;

                default:
                    x = Mathf.RoundToInt((cell.x * Data.RotationMatrix[0] * direction) + (cell.y * Data.RotationMatrix[1] * direction));
                    y = Mathf.RoundToInt((cell.x * Data.RotationMatrix[2] * direction) + (cell.y * Data.RotationMatrix[3] * direction));
                    break;
            }

            this.cells[i] = new Vector3Int(x, y, 0);
        }
    }

    private bool TestWallKicks(int rotationIndex, int rotationDirection)
    {
        int wallKickIndex = GetWallKickIndex(rotationIndex, rotationDirection);

        for (int i = 0; i < this.data.wallKicks.GetLength(1); i++)
        {
            Vector3Int translation = this.data.wallKicks[wallKickIndex, i];

            if (Move(translation))
            {
                return true;
            }
        }

        return false;
    }

    private int GetWallKickIndex(int rotationIndex, int rotationDirection) 
    {
        int wallKickIndex = rotationIndex * 2;

        if (rotationDirection < 0) {
            wallKickIndex--;
        }

        return Wrap(wallKickIndex, 0, this.data.wallKicks.GetLength(0));
    }
    #endregion
    private int Wrap(int input, int min, int max)
    {
        if (input < min)
        {
            return max - (min - input) % (max - min);
        }
        else
        {
            return min + (input - min) % (max - min);
        }
  
    }
}
