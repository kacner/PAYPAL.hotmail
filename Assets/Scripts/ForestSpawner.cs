using UnityEngine;

public class ForestSpawner : MonoBehaviour
{
    [Header("Grid Settings")]
    public int rows = 5;
    public int columns = 5;
    public float cellSize = 1f;
    public float rowOverlap = 0.2f;

    [Header("Sprites")]
    public GameObject[] ForestTiles;
    public GameObject lastRowPrefab;

    void Start()
    {
        SpawnGrid();
    }

    void SpawnGrid()
    {
        if (ForestTiles == null || lastRowPrefab == null)
        {
            Debug.LogError("Prefabs or ForestTiles are not assigned!");
            return;
        }

        Vector2 startPosition = (Vector2)transform.position - new Vector2((columns - 1) * cellSize / 2, (rows - 1) * cellSize / 2);

        for (int row = 0; row < rows; row++)
        {
            int orderInLayer = -5 - row;  // Calculate order in layer for the current row

            for (int col = 0; col < columns; col++)
            {
                // Calculate spawn position with row overlap
                Vector2 spawnPosition = startPosition + new Vector2(col * cellSize, row * cellSize - row * rowOverlap);

                int spawnType = Random.Range(0, ForestTiles.Length);

                // Use lastRowPrefab for the first row, otherwise use the default Prefab
                GameObject prefabToUse = (row == 0) ? lastRowPrefab : ForestTiles[spawnType];

                GameObject square = Instantiate(prefabToUse, spawnPosition, Quaternion.identity, transform);
                square.transform.localScale = Vector3.one * cellSize;

                // Set order in layer for the SpriteRenderer
                square.GetComponent<SpriteRenderer>().sortingOrder = orderInLayer;
            }
        }
    }
}