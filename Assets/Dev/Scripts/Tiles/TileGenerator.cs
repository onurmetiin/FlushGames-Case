using UnityEditor;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    public GameObject tilePrefab; // Instantiate edilecek GameObject'in prefab�
    public int rows; // Olu�turulacak sat�r say�s�
    public int columns; // Olu�turulacak s�tun say�s�
    public float gap; // tile lar aras� bo�luk
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
                // Tile'� instantiate et
                GameObject tile = Instantiate(tilePrefab, new Vector3(row * gap, 0, column * gap), Quaternion.identity);
                // Tile'� TileGenerator objesine ba�la
                tile.transform.SetParent(transform); 
            }
        }
    }

}
