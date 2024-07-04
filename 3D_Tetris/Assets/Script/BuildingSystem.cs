using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildingSystem : MonoBehaviour
{
    public Piece activePiece { get; private set; }
    public static BuildingSystem current;
    public GridLayout gridLayout;
    private Grid grid;
    [SerializeField] private Tilemap MainTilemap;
    public TetrominoData[] tetrominos;

    Vector3Int spawnPosition = new Vector3Int(0, 18, -1);

    public Vector3Int boardSize = new Vector3Int(11,20, -1);
    public Bounds bounds
    {
        get
        {
            Vector3 position = new Vector3(-boardSize.x/2,boardSize.y/2, -1);
            return new Bounds(position, this.boardSize);
        }
    }

    List<GameObject> currentPiece = new List<GameObject>();
    #region Unity methods

    private void Awake()
    {
        current = this;
        grid = gridLayout.gameObject.GetComponent<Grid>();

        this.activePiece = GetComponentInChildren<Piece>();

        for (int i = 0; i < this.tetrominos.Length; i++)
        {
            this.tetrominos[i].Initialize();
        }
    }

    private void Start()
    {
        SpawnPiece();
        print("Bound: "+bounds);
    }

    void Update()
    {
        print("I'm updating main");
    }
    #endregion

    #region Unitls

    public Vector3 SnapCoordinateToGrid(Vector3 position)
    {
        Vector3Int cellPos = gridLayout.WorldToCell(position);
        position = grid.GetCellCenterWorld(cellPos);
        return position;
    }

    public void SpawnPiece()
    {
        int randomIndex = Random.Range(0, tetrominos.Length);
        TetrominoData data = tetrominos[randomIndex];
       

        this.activePiece.Initialize(this, spawnPosition, data);

        Set(activePiece);
    }

    public void Set(Piece piece)
    {
        for (int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int gameObjectPos = piece.cells[i] + piece.position;
            GameObject obj = Instantiate(piece.data.prefab, gameObjectPos, Quaternion.identity);
            currentPiece.Add(obj);
            print("Instantiate");
        }
        
    }

    public void Clear()
    {
        print("I am in Clear method");
        for (int i = 0; i < currentPiece.Count; i++)
        {
            Destroy(currentPiece[i]);
            print("Clear");
        }
    }

    public bool IsValidPosition(Piece piece, Vector3Int position)
    {
        print("I am in validPosition method");
        Bounds bounds = this.bounds;

        for (int i = 0; i < piece.cells.Length; i++) 
        {
            Vector3Int gameObjectPos = piece.cells[i] + position;
            print(gameObjectPos.ToString());

            if (bounds.Contains(gameObjectPos))
            {
                return false;
            }

            //if (MainTilemap.HasTile(gameObjectPos))
            //{
            //    return false;
            //}
        }
        return true;
    }
    #endregion

    #region Building Placement

    public void InitializeWithObject(GameObject prefab)
    {
        Vector3 position = SnapCoordinateToGrid(spawnPosition);
        GameObject obj = Instantiate(prefab, position, Quaternion.identity);
    }

    #endregion


}