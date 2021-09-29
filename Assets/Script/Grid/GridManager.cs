using UnityEngine;

public class GridManager : MonoBehaviour
{
    #region Serialized Fields
    [SerializeField] GameObject tilePrefab;
    [SerializeField] GameObject clickBlocker;
    [SerializeField] float rate = 1f;
    #endregion

    #region Private Fields
    Tile[,] tiles;
    int vertical, horizontal, columns, rows;
    GameObject gridObject;
    bool isSimulating = false;
    #endregion

    #region MonoBehaviour Messages
    void Start()
    {
        //Dimensions
        vertical = (int)Camera.main.orthographicSize;
        horizontal = vertical * Screen.width / Screen.height;
        columns = horizontal * 2;
        rows = (vertical * 2) - 1;
        
        //Creating grid
        gridObject = new GameObject();
        gridObject.name = "Grid";

        //Populating grid with tiles
        tiles = new Tile[columns, rows];

        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                SpawnTile(i, j, Random.Range(0, 2));
            }
        }
    }
    #endregion

    #region Private Methods
    private void SpawnTile(int x, int y, int value)
    {
        Tile tile = Instantiate(tilePrefab, gridObject.transform).GetComponent<Tile>();
        tile.name = $"x:{x} y:{y}";
        tile.SetValue(value);
        tile.transform.position = new Vector3(x - (horizontal - 0.5f), y - (vertical - 0.5f));

        tiles[x, y] = tile;
    }

    private int NextCellState(int x, int y)
    {
        //Count active neighbours
        int activeNeighbours = 0;
        for (int i = x - 1; i <= x + 1; i++)
        {
            if (i < 0 || i >= columns) continue; //Edge tile

            for(int j = y - 1; j <= y + 1; j++)
            {
                if (j < 0 || j >= rows) continue; //Edge tile

                if (i == x && j == y) continue; //Same cell

                if (tiles[i,j].GetValue() == 1) activeNeighbours++;
            }
        }

        //Game of life rules
        if (tiles[x,y].GetValue() == 1)
        {
            if (activeNeighbours < 2 || activeNeighbours > 3) return 0;
            else return 1;
        }
        else
        {
            if (activeNeighbours == 3) return 1;
            else return 0;
        }
    }

    private void NextGridState() //Invoked
    {
        int[,] newState = new int[columns, rows];

        //Determine new state
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                newState[i, j] = NextCellState(i, j);
            }
        }

        //Se new state
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                tiles[i, j].SetValue(newState[i, j]);
            }
        }
    }
    #endregion

    #region Public Methods
    public void StartSimulation()
    {
        if (clickBlocker) clickBlocker.SetActive(true);
        InvokeRepeating("NextGridState", rate, rate);
    }

    public void StopSimulation()
    {
        CancelInvoke("NextGridState");
        if (clickBlocker) clickBlocker.SetActive(false);
    }

    public void ToggleSimulation()
    {
        isSimulating = !isSimulating;

        if (isSimulating) StartSimulation();
        else StopSimulation();
    }

    public void ClearGrid() //All tiles activated
    {
        if (!isSimulating)
        {
            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    tiles[i, j].SetValue(0);
                }
            }
        }
    }

    public void FillGrid() //All tiles deactivated
    {
        if (!isSimulating)
        {
            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    tiles[i, j].SetValue(1);
                }
            }
        }
    }

    public void RandomizeGrid() //Random tiles states
    {
        if (!isSimulating)
        {
            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    tiles[i, j].SetValue(Random.Range(0, 2));
                }
            }
        }  
    }
    #endregion
}
