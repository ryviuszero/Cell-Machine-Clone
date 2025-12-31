using System;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{

    public static GridManager instance;
    public GameObject emptyCell;
    public GameObject moverCell;
    public GameObject immobileCell;
    public GameObject rotCell;
    public GameObject pushCell;
    public GameObject genCell;
    public GameObject slideCell;
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
        ResetSim();
        PositionCamera();
        BuildLevel();

    }

	private void BuildLevel()
	{
		(new Action[17]
		{
			MoverIntro, PushIntro, RotatorIntro, GeneratorIntro1, SliderIntro, GeneratorIntro, AroundCorner1, PushDuplicate, RotateTimer, DoubleGenerator,
			RotateLine, RotateGeneratorHorizontal, MoverShooter, Explosion, GradualMover, RotateGeneratorHard, DoubleCornerExplosion
		})[GameData.level]();
	}

    private void MoverIntro()
    {
        SetGridSize(10, 7);
		SetBuildArea(1, 1, 4, 5);
		BuildBorder();
		SpawnEnemy(7, 2);                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    
		SpawnCell(CellType.RightMover, 2, 4);
		UIEvents.instance.SetTutorialText("Drag cells in the build area. Press play to run the simulation. Destroy the enemy cells to win.");
       
    }

    private void PushIntro()
    {
        SetGridSize(12, 9);
		SetBuildArea(1, 1, 5, 7);
		BuildBorder();
		SpawnEnemy(7, 6);
		SpawnEnemy(8, 6);
		SpawnEnemy(9, 6);
		SpawnCell(CellType.RightMover, 2, 4);
		SpawnCell(CellType.Push, 1, 1);
		SpawnCell(CellType.Push, 2, 2);
		UIEvents.instance.SetTutorialText("Most cells can be pushed by others");
    }

    private void RotatorIntro()
	{
		SetGridSize(10, 7);
		SetBuildArea(1, 1, 4, 5);
		BuildBorder();
		SpawnEnemy(7, 3);
		SpawnCell(CellType.UpMover, 4, 4);
		SpawnCell(CellType.NegativeRotator, 2, 2);
		UIEvents.instance.SetTutorialText("The rotator cell spins cells next to it");
	}

	private void GeneratorIntro1()
	{
		SetGridSize(12, 5);
		SetBuildArea(1, 1, 4, 4);
		BuildBorder();
		SpawnEnemy(10, 2);
		SpawnEnemy(8, 2);
		SpawnEnemy(9, 2);
		SpawnCell(CellType.RightGenerator, 2, 1);
		SpawnCell(CellType.Push, 3, 2);
		UIEvents.instance.SetTutorialText("The generator cell duplicates the cell behind it");
	}

	private void GeneratorIntro()
	{
		SetGridSize(10, 7);
		SetBuildArea(1, 1, 4, 5);
		BuildBorder();
		SpawnEnemy(7, 2);
		SpawnEnemy(7, 4);
		SpawnCell(CellType.RightMover, 2, 1);
		SpawnCell(CellType.DownGenerator, 4, 3);
	}

	private void SliderIntro()
	{
		SetGridSize(15, 10);
		SetBuildArea(1, 6, 10, 3);
		BuildBorder();
		SpawnEnemy(13, 2);
		SpawnEnemy(12, 2);
		SpawnCell(CellType.RightMover, 2, 2);
		SpawnCell(CellType.VSlide, 3, 2);
		SpawnCell(CellType.DownMover, 5, 8);
		SpawnCell(CellType.DownMover, 8, 8);
		SpawnCell(CellType.Push, 7, 7);
		SpawnCell(CellType.Push, 1, 8);
		UIEvents.instance.SetTutorialText("Some cells can only move in one direction");
	}

	private void AroundCorner1()
	{
		SetGridSize(14, 13);
		SetBuildArea(1, 6, 5, 6);
		BuildBorder();
		BuildImmobileRect(6, 5, 7, 7);
		SpawnEnemy(11, 2);
		SpawnCell(CellType.RightMover, 2, 8);
		SpawnCell(CellType.DownMover, 3, 7);
		SpawnCell(CellType.DownMover, 5, 8);
		SpawnCell(CellType.VSlide, 1, 10);
		SpawnCell(CellType.Push, 2, 9);
		SpawnCell(CellType.Push, 3, 9);
	}

	private void MoverShooter()
	{
		SetGridSize(20, 14);
		SetBuildArea(1, 8, 6, 5);
		BuildBorder();
		for (int i = 10; i < 18; i++)
		{
			SpawnEnemy(i, 3);
		}
		SpawnCell(CellType.RightMover, 2, 9);
		SpawnCell(CellType.RightMover, 5, 12);
		SpawnCell(CellType.HSlide, 3, 11);
		SpawnCell(CellType.DownMover, 3, 12);
		SpawnCell(CellType.RightGenerator, 4, 8);
		SpawnCell(CellType.RightGenerator, 2, 5);
		SpawnCell(CellType.HSlide, 1, 5);
	}

	private void Explosion()
	{
		SetGridSize(15, 13);
		SetBuildArea(1, 1, 5, 4);
		BuildBorder();
		BuildImmobileRect(1, 5, 7, 7);
		for (int i = 8; i < 14; i++)
		{
			for (int j = 7; j < 12; j++)
			{
				SpawnEnemy(i, j);
			}
		}
		SpawnCell(CellType.Immobile, 2, 2);
		SpawnCell(CellType.Push, 3, 4);
		SpawnCell(CellType.RightGenerator, 1, 4);
		SpawnCell(CellType.RightGenerator, 1, 3);
		SpawnCell(CellType.UpGenerator, 4, 4);
	}

	private void DoubleCornerExplosion()
	{
		SetGridSize(20, 16);
		SetBuildArea(1, 10, 5, 5);
		BuildBorder();
		BuildImmobileRect(1, 5, 10, 5);
		for (int i = 1; i < 8; i++)
		{
			for (int j = 1; j < 5; j++)
			{
				SpawnEnemy(i, j);
			}
		}
		SpawnCell(CellType.RightGenerator, 1, 14);
		SpawnCell(CellType.RightGenerator, 2, 14);
		SpawnCell(CellType.DownGenerator, 3, 14);
		SpawnCell(CellType.LeftGenerator, 5, 14);
	}

	private void RotateTimer()
	{
		SetGridSize(20, 12);
		SetBuildArea(1, 8, 18, 3);
		BuildBorder();
		SpawnEnemy(9, 2);
		SpawnCell(CellType.RightMover, 1, 6);
		SpawnCell(CellType.Push, 2, 6);
		for (int i = 3; i < 8; i++)
		{
			SpawnCell(CellType.NegativeRotator, i, 6);
		}
		SpawnCell(CellType.DownMover, 5, 10);
		SpawnCell(CellType.DownMover, 7, 10);
		SpawnCell(CellType.NegativeRotator, 3, 9);
		SpawnCell(CellType.HSlide, 3, 10);
	}

	private void RotateGeneratorHard()
	{
		SetGridSize(12, 9);
		SetBuildArea(1, 1, 8, 4);
		BuildBorder();
		SpawnEnemy(10, 2);
		SpawnEnemy(3, 6);
		SpawnCell(CellType.RightGenerator, 1, 2);
		SpawnCell(CellType.Push, 2, 4);
		SpawnCell(CellType.Push, 4, 4);
		SpawnCell(CellType.PositiveRotator, 3, 3);
		SpawnCell(CellType.LeftMover, 5, 3);
		SpawnCell(CellType.HSlide, 5, 4);
	}

	private void DoubleGenerator()
	{
		SetGridSize(15, 7);
		SetBuildArea(5, 1, 5, 5);
		BuildBorder();
		SpawnEnemy(12, 3);
		SpawnEnemy(3, 3);
		SpawnCell(CellType.RightGenerator, 7, 4);
		SpawnCell(CellType.LeftGenerator, 8, 1);
		SpawnCell(CellType.VSlide, 5, 3);
		SpawnCell(CellType.Immobile, 9, 2);
	}

	private void RotateLine()
	{
		SetGridSize(15, 14);
		SetBuildArea(1, 1, 5, 12);
		BuildBorder();
		SpawnEnemy(11, 10);
		SpawnEnemy(11, 8);
		SpawnEnemy(11, 9);
		SpawnEnemy(11, 7);
		SpawnCell(CellType.UpMover, 2, 3);
		SpawnCell(CellType.UpMover, 4, 12);
		SpawnCell(CellType.UpMover, 2, 8);
		SpawnCell(CellType.UpMover, 4, 3);
		SpawnCell(CellType.UpMover, 5, 4);
		SpawnCell(CellType.NegativeRotator, 2, 1);
		SpawnCell(CellType.VSlide, 5, 3);
		SpawnCell(CellType.Immobile, 2, 10);
		SpawnCell(CellType.Immobile, 4, 6);
	}

	private void RotateGeneratorHorizontal()
	{
		SetGridSize(17, 9);
		SetBuildArea(7, 1, 3, 7);
		BuildBorder();
		for (int i = 1; i < 16; i++)
		{
			if (i < 6 || i > 10)
			{
				SpawnEnemy(i, 4);
			}
		}
		SpawnCell(CellType.RightGenerator, 7, 4);
		SpawnCell(CellType.NegativeRotator, 9, 5);
		SpawnCell(CellType.Push, 7, 1);
		SpawnCell(CellType.Push, 8, 7);
		SpawnCell(CellType.Immobile, 9, 2);
		SpawnCell(CellType.Immobile, 9, 3);
	}

	private void PushDuplicate()
	{
		SetGridSize(17, 13);
		SetBuildArea(7, 1, 9, 4);
		BuildBorder();
		SpawnEnemy(2, 2);
		SpawnEnemy(3, 2);
		SpawnEnemy(2, 4);
		SpawnEnemy(8, 10);
		SpawnCell(CellType.UpGenerator, 7, 2);
		SpawnCell(CellType.LeftMover, 9, 2);
		SpawnCell(CellType.Push, 12, 1);
		SpawnCell(CellType.Push, 14, 2);
		SpawnCell(CellType.Push, 8, 1);
		SpawnCell(CellType.Push, 7, 4);
		SpawnCell(CellType.Push, 10, 1);
		SpawnCell(CellType.Push, 13, 3);
		SpawnCell(CellType.Push, 10, 2);
	}

	private void GradualMover()
	{
		SetGridSize(23, 14);
		SetBuildArea(1, 6, 21, 2);
		BuildBorder();
		for (int i = 8; i < 20; i++)
		{
			for (int j = 9; j < 10; j++)
			{
				SpawnEnemy(i, j);
			}
		}
		for (int k = 8; k < 20; k++)
		{
			for (int l = 4; l < 5; l++)
			{
				SpawnEnemy(k, l);
			}
		}
		SpawnCell(CellType.UpGenerator, 9, 7);
		SpawnCell(CellType.DownGenerator, 17, 6);
		SpawnCell(CellType.RightMover, 3, 6);
		SpawnCell(CellType.RightMover, 3, 7);
		SpawnCell(CellType.Push, 13, 6);
		SpawnCell(CellType.Push, 15, 7);
		SpawnCell(CellType.HSlide, 20, 6);
		SpawnCell(CellType.HSlide, 20, 7);
		SpawnCell(CellType.HSlide, 21, 6);
		SpawnCell(CellType.HSlide, 21, 7);
		SpawnCell(CellType.NegativeRotator, 2, 7);
		SpawnCell(CellType.NegativeRotator, 4, 7);
	}


    public void PositionCamera()
    {
        float x = (float)width * 0.5f - 0.5f;
        float y = (float)height * 0.5f - 0.5f;
        float orthographicSize = (float)height * 0.5f + 7f;
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
        PlayerPrefs.SetInt("lev" + GameData.level, 1);
        EventManager.TriggerEvent("LevelComplete");  
    }

    private void ExecuteGlobalStep()
    {
        if (DragManager.instance.InDrag())
		{
			return;
		}
        startedSim = true;
        cells.Sort((Cell a, Cell b) => a.cellType.CompareTo(b.cellType));
        int cellLenght = cells.Count;
        for(int k = 0; k < cellLenght; k++)
		{
			cells[k].oldX = cells[k].x;
			cells[k].oldY = cells[k].y;
			cells[k].oldRot = cells[k].rot;
		}
       for(int k = 0; k < cellLenght; k++)
        {
            if(cells[k].active)
            {
                cells[k].ExecuteStep();
            }
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
        levelCompleted = false;
        startedSim = false;
        for(int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                cellGrid[i, j] = null;
            }
        }
        int cellLenght = cells.Count;
        for(int k = 0; k < cellLenght; k++)
        {
            if( cells[k].isGenerated )
            {
                cells[k].Deactivate();
                Destroy(cells[k].gameObject);
                cells.RemoveAt(k);
                cellLenght--;
                k--;
            }
            else
            {
                cells[k].ResetCell();            
            }
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

    public void SpawnCell(CellType cellType, int x, int y, bool isGenerated = false)
    {
        Cell cell = null;
        switch (cellType)
        {
            case CellType.RightMover:
                cell = Instantiate(moverCell, new Vector3(x, y, 0), Quaternion.identity).GetComponent<MoverCell>();
                cell.SetDirection(Direction.Right);
                break;
            case CellType.LeftMover:
                cell = Instantiate(moverCell, new Vector3(x, y, 0), Quaternion.identity).GetComponent<MoverCell>();
                cell.SetDirection(Direction.Left);
                break;
            case CellType.UpMover:
                cell = Instantiate(moverCell, new Vector3(x, y, 0), Quaternion.identity).GetComponent<MoverCell>();
                cell.SetDirection(Direction.Up);
                break;
            case CellType.DownMover:
                cell = Instantiate(moverCell, new Vector3(x, y, 0), Quaternion.identity).GetComponent<MoverCell>();
                cell.SetDirection(Direction.Down);
                break;
            case CellType.Immobile:
                cell = Instantiate(immobileCell, new Vector3(x, y, 0), Quaternion.identity).GetComponent<ImmobileCell>();
                break;
            case CellType.Push:
                cell = Instantiate(pushCell, new Vector3(x, y, 0), Quaternion.identity).GetComponent<PushCell>();
                break;
            case CellType.RightGenerator:
                cell = Instantiate(genCell, new Vector3(x, y, 0), Quaternion.identity).GetComponent<GeneratorCell>();
                cell.SetDirection(Direction.Right);
                break;
            case CellType.LeftGenerator:
                cell = Instantiate(genCell, new Vector3(x, y, 0), Quaternion.identity).GetComponent<GeneratorCell>();
                cell.SetDirection(Direction.Left);
                break;
            case CellType.UpGenerator:
                cell = Instantiate(genCell, new Vector3(x, y, 0), Quaternion.identity).GetComponent<GeneratorCell>();
                cell.SetDirection(Direction.Up);
                break;
            case CellType.DownGenerator:
                cell = Instantiate(genCell, new Vector3(x, y, 0), Quaternion.identity).GetComponent<GeneratorCell>();
                cell.SetDirection(Direction.Down);
                break;
            case CellType.PositiveRotator:
                cell = Instantiate(rotCell, new Vector3(x, y, 0), Quaternion.identity).GetComponent<RotateCell>();
                (cell as RotateCell).SetSpinDirection(SpinDir.Positive);
                break;
            case CellType.NegativeRotator:
                cell = Instantiate(rotCell, new Vector3(x, y, 0), Quaternion.identity).GetComponent<RotateCell>();
                (cell as RotateCell).SetSpinDirection(SpinDir.Negative);
                break;
            case CellType.HSlide:
                cell = Instantiate(slideCell, new Vector3(x, y, 0), Quaternion.identity).GetComponent<SlideCell>();
			    cell.SetDirection(Direction.Right);
                break;
            case CellType.VSlide:
                cell = Instantiate(slideCell, new Vector3(x, y, 0), Quaternion.identity).GetComponent<SlideCell>();
                cell.SetDirection(Direction.Up);
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
        cell.isGenerated = isGenerated;
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
   