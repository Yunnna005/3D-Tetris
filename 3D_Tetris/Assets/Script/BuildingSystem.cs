using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildingSystem : MonoBehaviour
{
    public static BuildingSystem current;
    public GridLayout gridLayout;
    private Grid grid;
    [SerializeField] private Tilemap MainTilemap;

    Vector3 topPoint = new Vector3(0, 19.5f, 0);
    public GameObject I_shape;
    public GameObject O_shape;
    public GameObject L_shape;
    public GameObject J_shape;
    public GameObject Z_shape;
    public GameObject S_shape;
    public GameObject T_shape;

    #region Unity methods

    private void Awake(){
        current = this;
        grid = gridLayout.gameObject.GetComponent<Grid>();
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Q)){
            InitializeWithObject(O_shape);
        }else if(Input.GetKeyDown(KeyCode.W)){
            InitializeWithObject(J_shape);
        }else if(Input.GetKeyDown(KeyCode.E)){
            InitializeWithObject(S_shape);
        }else if(Input.GetKeyDown(KeyCode.R)){
            InitializeWithObject(Z_shape);
        }else if(Input.GetKeyDown(KeyCode.T)){
            InitializeWithObject(T_shape);
        }else if(Input.GetKeyDown(KeyCode.Y)){
            InitializeWithObject(I_shape);
        }
    }
    #endregion

    #region Unitls

    // public static Vector3 GetMouseWorldPosition(){
        
    //     Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //     print(ray);
    //     if(Physics.Raycast(ray, out RaycastHit hit)){
    //         return hit.point;
    //     }else{
    //         return Vector3.zero;
    //     }
    // }

    public Vector3 SnapCoordinateToGrid(Vector3 position){
        Vector3Int cellPos = gridLayout.WorldToCell(position);
        position = grid.GetCellCenterWorld(cellPos);
        return position;
    }
    #endregion

    #region Building Placement

    public void InitializeWithObject(GameObject prefab){
        Vector3 position = SnapCoordinateToGrid(topPoint);
        GameObject obj = Instantiate(prefab, position, Quaternion.identity);
    } 
    #endregion

}