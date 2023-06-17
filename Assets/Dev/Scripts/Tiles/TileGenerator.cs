using UnityEditor;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    public GameObject tilePrefab;   // Instantiate edilecek GameObject'in prefabý
    
    [Range(1,10)]
    public int rows;                // Oluþturulacak satýr sayýsý (1-10 arasý - li deðer alamaz)

    [Range(1, 10)]
    public int columns;             // Oluþturulacak sütun sayýsý (1-10 arasý - li deðer alamaz)

    public float tileSize;          // Oluþturulacak tile'larýn kenar uzunluðu

    public float gap = 2.1f;               // tile lar arasý boþluk


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Vector3 GridLeftBottom = (transform.position - (new Vector3(rows * tileSize + (rows-1) * gap * .5f, 0, columns * tileSize + (columns - 1) * gap * .5f)));

        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                //Vector3 tileCenter = GridLeftBottom + new Vector3(row * tileSize + tileSize * gap * .5f, 0 ,columns * tileSize * gap + tileSize * gap * .5f);
                Gizmos.DrawWireCube(new Vector3(row * gap,0, column * gap), new Vector3(2, .5f, 2));
            }
        }
    }

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
