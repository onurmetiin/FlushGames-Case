using UnityEditor;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    public GameObject tilePrefab; // Instantiate edilecek GameObject'in prefabý
    public int rows; // Oluþturulacak satýr sayýsý
    public int columns; // Oluþturulacak sütun sayýsý
    public float gap; // tile lar arasý boþluk
    private void Start()
    {
        GenerateTiles();
    }

    public void GenerateTiles()
    {
        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                // Tile'ý instantiate et
                GameObject tile = Instantiate(tilePrefab, new Vector3(row * gap, 0, column * gap), Quaternion.identity);
                // Tile'ý TileGenerator objesine baðla
                tile.transform.SetParent(transform); 
            }
        }
    }

}
