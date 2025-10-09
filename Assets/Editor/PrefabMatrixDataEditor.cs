#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PrefabMatrixData))]
public class PrefabGridDataEditor : Editor
{
    private PrefabMatrixData _matrixData;

    private void OnEnable()
    {
        _matrixData = (PrefabMatrixData)target;
        InitializeGrid();
    }

    private void InitializeGrid()
    {
        // Khởi tạo hoặc reset ma trận nếu kích thước thay đổi
        if (_matrixData.prefabGrid == null || _matrixData.prefabGrid.GetLength(0) != _matrixData.rows || _matrixData.prefabGrid.GetLength(1) != _matrixData.columns)
        {
            _matrixData.prefabGrid = new GameObject[_matrixData.rows, _matrixData.columns];
        }
    }

    public override void OnInspectorGUI()
    {
        // Cập nhật số hàng, cột và spacing
        EditorGUI.BeginChangeCheck();
        _matrixData.rows = EditorGUILayout.IntField("Rows", _matrixData.rows);
        _matrixData.columns = EditorGUILayout.IntField("Columns", _matrixData.columns);
        _matrixData.spacing = EditorGUILayout.FloatField("Spacing", _matrixData.spacing);
        if (EditorGUI.EndChangeCheck())
        {
            InitializeGrid();
            EditorUtility.SetDirty(_matrixData); // Mark as dirty
        }

        // Tính toán tổng chiều rộng của grid
        float totalWidth = _matrixData.columns * 20 + (_matrixData.columns - 1) * _matrixData.spacing;
        float padding = (EditorGUIUtility.currentViewWidth - totalWidth) / 2;

        // Hiển thị grid dưới dạng các ô kéo thả prefab
        EditorGUILayout.LabelField("Drag and Drop Prefabs into Grid:");
        for (int i = 0; i < _matrixData.rows; i++)
        {
            EditorGUILayout.BeginHorizontal();
            //GUILayout.Space(padding); // Thêm khoảng cách để căn giữa
            for (int j = 0; j < _matrixData.columns; j++)
            {
                _matrixData.prefabGrid[i, j] = (GameObject)EditorGUILayout.ObjectField(
                    _matrixData.prefabGrid[i, j],
                    typeof(GameObject),
                    false,
                    GUILayout.Width(35),
                    GUILayout.Height(35)
                );
                if (j < _matrixData.columns - 1)
                {
                    GUILayout.Space(_matrixData.spacing); // Thêm khoảng cách giữa các ô
                }
            }
            EditorGUILayout.EndHorizontal();
        }

        // Nút tạo prefab trong scene
        if (GUILayout.Button("Generate Grid in Scene"))
        {
            GenerateGridInScene();
        }
    }

    private void GenerateGridInScene()
    {
        // Xóa grid cũ nếu tồn tại
        // GameObject oldGrid = GameObject.Find("PrefabGrid");
        // if (oldGrid) DestroyImmediate(oldGrid);

        // Tạo GameObject cha
        GameObject gridParent = new GameObject("PrefabGrid");
        gridParent.transform.position = Vector3.zero;

        // Tính toán vị trí bắt đầu để căn giữa grid
        Vector3 startPos = CalculateGridStartPosition(_matrixData.rows, _matrixData.columns, _matrixData.spacing);

        // Duyệt qua ma trận và tạo prefab
        for (int i = 0; i < _matrixData.rows; i++)
        {
            for (int j = 0; j < _matrixData.columns; j++)
            {
                if (_matrixData.prefabGrid[i, j] != null)
                {
                    // Tính vị trí xếp đặt prefab
                    Vector3 position = startPos + new Vector3(j * _matrixData.spacing, -i * _matrixData.spacing, -i * _matrixData.spacing);
                    GameObject instance = PrefabUtility.InstantiatePrefab(_matrixData.prefabGrid[i, j]) as GameObject;
                    instance.transform.position = position;
                    instance.transform.parent = gridParent.transform;
                }
            }
        }
    }

    // Tính vị trí bắt đầu để grid được căn giữa
    private Vector3 CalculateGridStartPosition(int rows, int columns, float spacing)
    {
        float offsetX = (columns - 1) * spacing * 0.5f;
        float offsetZ = (rows - 1) * spacing * 0.5f;
        return new Vector3(-offsetX, offsetZ, offsetZ);
    }
}
#endif