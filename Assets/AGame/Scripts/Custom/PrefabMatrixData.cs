using UnityEngine;

[CreateAssetMenu(fileName = "PrefabGridData", menuName = "Tools/Prefab Grid Data")]
public class PrefabMatrixData : ScriptableObject
{
    public int rows = 3;         // Số hàng
    public int columns = 3;      // Số cột
    public float spacing = 1f;   // Khoảng cách giữa các prefab
    public GameObject[,] prefabGrid; // Ma trận chứa prefab
}