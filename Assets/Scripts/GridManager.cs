using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GridManager : MonoBehaviour
{

    public static GridManager instance;
    public GameObject emptyCell;
    public GameObject moverCell;
    public GameObject immobileCell;
    public GameObject enemyCell;

    private List<Cell> cells;
    public static Cell[,] cellGrid;
    public static EmptyCell[,] emptyCells;
    private List<Cell> generatedCells;
    public List<EnemyCell> enemies;
    private int enemiesKilled;
    private int totoalEnemies;
    public static int width = 12;
    public static int height = 8;

    float animTime = 0.2f;
    private float animElapsed;
    public static bool isAnimating;
    public static bool isPlayMode;
    public bool startedSim;

    private bool levelCompleted;

    

    private void Awake()
    {
        instance = this;
        cells = new List<Cell>();
        generatedCells = new List<Cell>();
        cellGrid = new Cell[width, height];
        enemies = new List<EnemyCell>();
        PositionCamera();
        BuildLevel();

    }

    private void BuildLevel()
    {
        (new Action[1]{
            MoveIntro
        })[GameData.level].Invoke();
    }

    private void MoveIntro()
    {
        SetGridSize(10, 7);
		SetBuildArea(1, 1, 4, 5);
		BuildBorder();
		SpawnEnemy(7, 2);                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    
		SpawnCell(CellType.RightMover, 2, 4);
		// UIEvents.instance.SetTutorialText("Drag cells in the build area. Press play to run the simulation. Destroy the enemy cells to win.");
       
    }

    public void PositionCamera()
    {
        float x = (float)width * 0.5f - 0.5f;
        float y = (float)height * 0.5f - 0.5f;
        float orthographicSize = (float)height * 0.5f + 2f;
        Camera.main.transform.position = new Vector3(x, y, -10f);
        Camera.main.orthographicSize = orthographicSize;
    }

    void Update()
    {
        AnimateCells();
    }

    private void AnimateCells()
    {
        if(!isAnimating)
        {
            return;
        }
        animElapsed += Time.deltaTime;
        float num = animElapsed / animTime;
        if (num >= 1f)
        {
            num = 1f;
            isAnimating = false;
        }
        foreach (Cell cell in cells)
        {
            cell.transform.position = Vector3.Lerp(new Vector3(cell.oldX, cell.oldY, 0f), new Vector3(cell.x, cell.y, 0f), num);
            cell.transform.rotation = Quaternion.Slerp(Quaternion.Euler(0f, 0f, cell.oldRot), Quaternion.Euler(0f, 0f, cell.rot), num);
        }
        if( num == 1f )
        {
            CheckEnemies();
        }
        if( num == 1f && isPlayMode)
        {
            ExecuteGlobalStep();
        }
    }

    private void CheckEnemies()
    {
        if (levelCompleted)
        {
            return;
        }
        foreach (EnemyCell enemy in enemies)
        {
            if (enemy.alive && cellGrid[enemy.x, enemy.y] != null)
            {
                enemy.Kill();
                enemiesKilled++;
                cellGrid[enemy.x, enemy.y].Deactivate();
            }
        }
        if (enemiesKilled == totoalEnemies)
        {
            levelCompleted = true;
            WinLevel();
        }
    }

    private void WinLevel()
    {
        return;   
    }

    private void ExecuteGlobalStep()
    {
        if (DragManager.instance.InDrag())
		{
			return;
		}
        startedSim = true;
        cells.Sort((Cell a, Cell b) => a.cellType.CompareTo(b.cellType));
        foreach (Cell cell in cells)
		{
			cell.oldX = cell.x;
			cell.oldY = cell.y;
			cell.oldRot = cell.rot;
		}
       foreach (Cell cell in cells)
        {
            cell.ExecuteStep();
        }
        isAnimating = true;
        animElapsed = 0f;
    }

    public void StepSim()
	{
		if (!isAnimating)
		{
			ExecuteGlobalStep();
		}
	}

	public void PlaySim()
	{
		isPlayMode = true;
		if (isPlayMode && !isAnimating)
		{
			ExecuteGlobalStep();
		}
	}

	public void PauseSim()
	{
		isPlayMode = false;
	}

    public void ResetSim()
    {
        enemiesKilled = 0;
        isPlayMode = false;
        isAnimating = false;
        for(int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                cellGrid[i, j] = null;
            }
        }
        foreach (Cell cell in cells)
        {
            cell.ResetCell();
        }
        foreach (EnemyCell enemy in enemies)
        {
            enemy.Reanimate();
        }
        
    }
    private void BuildBorder()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if(i == 0 || j == 0 || i == width - 1 || j == height - 1)
                {
                    SpawnCell(CellType.Immobile, i, j);
                }
            }
        }
    }

    private void BuildGrid()
    {
       emptyCells = new EmptyCell[width, height];
       for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                EmptyCell cell = Instantiate(emptyCell, new Vector3(i, j, 0.1f), Quaternion.identity).GetComponent<EmptyCell>();
                cell.transform.SetParent(transform);
                emptyCells[i, j] = cell;
            }
        }
    }

    private void SetBuildArea(int x, int y, int width, int height)
	{
		for (int i = x; i < x + width; i++)
		{
			for (int j = y; j < y + height; j++)
			{
				emptyCells[i, j].SetPlaceable(placeable: true);
			}
		}
	}

    private void BuildImmobileRect(int x, int y, int width, int height)
	{
		for (int i = x; i < x + width; i++)
		{
			for (int j = y; j < y + height; j++)
			{
				SpawnCell(CellType.Immobile, i, j);
			}
		}
	}

    private void SpawnCell(CellType cellType, int x, int y)
    {
        Cell cell = null;
        switch (cellType)
        {
            case CellType.RightMover:
                cell = Instantiate(moverCell, new Vector3(x, y, 0), Quaternion.identity).GetComponent<MoverCell>();
                cell.SetDirection(Direction.Right);
                break;
            case CellType.Immobile:
                cell = Instantiate(immobileCell, new Vector3(x, y, 0), Quaternion.identity).GetComponent<ImmobileCell>();
                break;
        }
        cell.transform.SetParent(transform);
        cell.setXY(x, y);
        cell.cellType = cellType;
        cell.oldRot = cell.rot;
		cell.oldX = cell.x;
		cell.oldY = cell.y;
		cell.transform.position = new Vector3(cell.x, cell.y);
		cell.transform.rotation = Quaternion.Euler(0f, 0f, cell.rot);
		cell.SetCurAsInitial();
        cells.Add(cell);
        if(startedSim)
        {
            generatedCells.Add(cell);
        }
    }

    private void SpawnEnemy(int x, int y)
    {
        EnemyCell enemy = Instantiate(enemyCell, new Vector3(x, y, 0), Quaternion.identity).GetComponent<EnemyCell>();
        enemy.transform.SetParent(transform);
        enemies.Add(enemy);
        enemy.setXY(x, y);
        totoalEnemies++;
    }

    private void Playground()
    {
        SetGridSize(40, 40);
    }

    private void SetGridSize(int w, int h)
    {
        width = w;
        height = h;
        cellGrid = new Cell[width, height];
        BuildGrid();
    }



}
   