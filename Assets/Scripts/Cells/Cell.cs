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

    protected bool PushStack(Direction dir)
    {
        if (GridManager.cellGrid[x, y] == null)
		{
			return true;
		}
        int tmpX = x;
        int tmpY = y;
        if (this is ImmobileCell )
		{
			return false;
		}
        switch (dir)
        {
        case Direction.Right:
            x++;
            break;
        case Direction.Up:
            y++;
            break;
        case Direction.Left:
            x--;
            break;
        case Direction.Down:
            y--;
            break;
        }

        GridManager.cellGrid[tmpX, tmpY].setXY(x, y);

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
