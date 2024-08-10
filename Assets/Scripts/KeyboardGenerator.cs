using UnityEngine;

public class KeyboardGenerator : MonoBehaviour
{
    public GameObject hexKeyPrefab;  // Assign your hexagon prefab here
    public int rows = 15;             // Number of rows
    public int cols = 25;             // Number of columns
    public float hexSize = 1.0f;     // Size of each hexagon

    void Start()
    {
        GenerateHexagonalGrid();
    }

    void GenerateHexagonalGrid()
    {
        float xOffset = hexSize * Mathf.Sqrt(3f);  // Horizontal distance between hexagons
        float yOffset = hexSize * 1.5f;            // Vertical distance between hexagons

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                // Calculate the position of each hexagon
                float xPos = col * xOffset;
                float yPos = row * yOffset;

                // Offset every other row by half the horizontal distance
                if (row % 2 == 1)
                {
                    xPos += xOffset / 2f;
                }

                // Instantiate the hexagon at the calculated position
                GameObject hexKey = Instantiate(hexKeyPrefab, new Vector3(xPos, yPos, 0), Quaternion.Euler(0f, 0f, 30f));
                hexKey.transform.parent = this.transform;

                // Optionally, name the hexagon for easier identification
                hexKey.name = "HexKey_" + row + "_" + col;

                // Add a script to handle input on each hexagon (e.g., detecting mouse clicks)
                hexKey.AddComponent<HexKeyHandler>();
            }
        }

        transform.rotation = Quaternion.Euler(0f, 0f, 15f);
    }
}
