using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildingSystem : MonoBehaviour
{
    public Piece activePiece { get; private set; }
    public static BuildingSystem current;
    public TetrominoData[] tetrominos;

    public Vector3Int spawnPosition = new Vector3Int(0, 18, -1);

    public Vector3Int boardSize = new Vector3Int(11,20, -1);

    int bound_left = -5;
    int bound_bottom = 0;
    int bound_right = 5;
    int bound_top = 19;

    public List<GameObject> currentPiece = new List<GameObject>();


    #region Unity methods

    private void Awake()
    {
        activePiece = GetComponentInChildren<Piece>();

        for (int i = 0; i < this.tetrominos.Length; i++)
        {
            this.tetrominos[i].Initialize();
        }
    }

    private void Start()
    {
        SpawnPiece();
    }
    #endregion

    #region Unitls



    public bool IsValidPosition(Piece piece, Vector3Int position)
    {
        for (int i = 0; i < piece.cells.Length; i++) 
        {
            Vector3Int gameObjectPos = piece.cells[i] + position;

            if (gameObjectPos.x > bound_right || gameObjectPos.x < bound_left)
            {
                return false;
            }

            if(gameObjectPos.y > bound_top || gameObjectPos.y < bound_bottom)
            {
                return false;
            }   
        }
        return true;
    }
    #endregion

    #region Building Placement

    public void SpawnPiece()
    {
        int randomIndex = Random.Range(0, tetrominos.Length);
        TetrominoData data = tetrominos[randomIndex];
        activePiece.isMoving = true;
        activePiece.Initialize(this, spawnPosition, data);

        Set(activePiece);

    }

    public void Set(Piece piece)
    {
        for (int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int gameObjectPos = piece.cells[i] + piece.position;
            GameObject obj = Instantiate(piece.data.prefab, gameObjectPos, Quaternion.identity);
            currentPiece.Add(obj);
            
        }
    }

    public void Clear()
    {

        foreach (GameObject obj in currentPiece)
        {
            Destroy(obj);
        }
        currentPiece.Clear();
    }

    public IEnumerator DelayedSpawnPiece()
    {
        yield return new WaitForSeconds(0.5f); // adjust the delay time as needed
        SpawnPiece();
    }
    #endregion
}