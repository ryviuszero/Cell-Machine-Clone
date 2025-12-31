using UnityEditor;
using UnityEngine;

public abstract class Cell : MonoBehaviour
{
    public int x;
    public int y;
    public float rot;
    public Direction direction;
    public int oldX;
    public int oldY;
    public float oldRot;
    public int initialX;
    public int initialY;
    public float initialRot;
    public Direction initialDir;
    public CellType cellType;
    public bool active = true;
    public bool isGenerated = false;
    protected bool onGrid;

	public void setXY(int newX, int newY)
	{
        if (onGrid)
        {
            GridManager.cellGrid[x, y] = null;
        }
        else
        {
            onGrid = true;
        }
		x = newX;
		y = newY;
		GridManager.cellGrid[x, y] = this;
	}

    public virtual void SetDirection(Direction dir)
    {
        direction = dir;
        rot = 0f;
        switch (dir)
        {
            case Direction.Up:
                rot = 90f;
                break;
            case Direction.Right:
                rot = 0;
                break;
            case Direction.Down:
                rot = 270f;
                break;
            case Direction.Left:
                rot = 180f;
                break;
        }
        
    }

    public void Deactivate()
    {
        active = false;
        gameObject.SetActive(false);
        GridManager.cellGrid[x, y] = null;
    }

    public void ResetCell()
    {
        if(!active)
        {
            active = true;
            gameObject.SetActive(true);
        }
        x = initialX;
        y = initialY;
        transform.position = new Vector3(x, y, 0);
        SetDirection(initialDir);
        rot = initialRot;
        transform.rotation = Quaternion.Euler(0f, 0f, rot);
        GridManager.cellGrid[x, y] = this;
    }


    public abstract void ExecuteStep();

    protected bool PushStack(Direction dir, int posX, int posY)
    {
        if (GridManager.cellGrid[posX, posY] == null)
		{
			return true;
		}
        int tmpX = posX;
        int tmpY = posY;
        if (this is ImmobileCell )
		{
			return false;
		}

        if (GridManager.cellGrid[posX, posY] is SlideCell slideCell)
		{
			if (slideCell.orientation == Orientation.Vertical && (direction == Direction.Right || direction == Direction.Left))
			{
				return false;
			}
			if (slideCell.orientation == Orientation.Horizontal && (direction == Direction.Up || direction == Direction.Down))
			{
				return false;
			}
		}

        while(true)
        {
            switch (dir)
            {
            case Direction.Right:
                posX++;
                break;
            case Direction.Up:
                posY++;
                break;
            case Direction.Left:
                posX--;
                break;
            case Direction.Down:
                posY--;
                break;
            }
            if (posX < 0 || posX >= GridManager.width || posY < 0 || posY >= GridManager.height)
            {
                return false;
            }
            if (GridManager.cellGrid[posX, posY] == null)
            {
                break;
            }
            if (GridManager.cellGrid[posX, posY] is SlideCell slideCell2)
			{
				if (slideCell2.orientation == Orientation.Vertical && (direction == Direction.Right || direction == Direction.Left))
				{
					return false;
				}
				if (slideCell2.orientation == Orientation.Horizontal && (direction == Direction.Up || direction == Direction.Down))
				{
					return false;
				}
			}
			if (GridManager.cellGrid[posX, posY] is ImmobileCell)
			{
				return false;
			}
        }
        do
        {
            int newX = posX;
            int newY = posY;
            switch (direction)
			{
			case Direction.Right:
				posX--;
				break;
			case Direction.Up:
				posY--;
				break;
			case Direction.Left:
				posX++;
				break;
			case Direction.Down:
				posY++;
				break;
			}
            GridManager.cellGrid[posX, posY].setXY(newX, newY);
        }while (posX != tmpX || posY != tmpY);
        AudioManager.instance.Play("Move");

        return true;
    }

    public void SetCurAsInitial()
	{
		initialRot = rot;
		initialX = x;
		initialY = y;
		initialDir = direction;
	}

}
