using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Assets.Dev.Editors.Tiles
{
    public class TileEditor:EditorWindow
    {
        private GameObject boxPrefab;
        private int rows;
        private int columns;
        private float gap;


        [MenuItem("Grid Editor/Create New Grid")]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow(typeof(TileEditor));
        }

        private void OnGUI()
        {
            GUILayout.Label("Box Creator", EditorStyles.boldLabel);

            // Input fields for box size
            rows = EditorGUILayout.IntField("Rows", rows);
            columns = EditorGUILayout.IntField("Columns", columns);
            gap = EditorGUILayout.FloatField("Gap", gap);


            if (GUILayout.Button("Create New Grid"))
            {
                CreateGrid();
                Close();
            }
        }

        private void CreateGrid()
        {
            if (boxPrefab == null)
            {
                Debug.LogError("Tile prefab is not assigned!");
                return;
            }

            if (gap <= 2.1) gap = 2.1f;

            GameObject grid = new GameObject("Grid");
            
            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    // Tile'ı instantiate et
                    GameObject tile = Instantiate(boxPrefab, new Vector3(row * gap, 0, column * gap), Quaternion.identity,grid.transform);
                }
            }
        }

        private void OnEnable()
        {
            boxPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Dev/Prefabs/Tile.prefab");
        }
    }
}
