using UnityEngine;

public class GeneratorCell : Cell
{
	public override void ExecuteStep()
	{
		int num = x;
		int num2 = y;
		int num3 = x;
		int num4 = y;
		switch (direction)
		{
		case Direction.Right:
			num--;
			num3++;
			break;
		case Direction.Up:
			num2--;
			num4++;
			break;
		case Direction.Left:
			num++;
			num3--;
			break;
		case Direction.Down:
			num2++;
			num4--;
			break;
		}
		if (!(GridManager.cellGrid[num, num2] == null) && PushStack(direction, num3, num4))
		{
			GridManager.instance.SpawnCell(GridManager.cellGrid[num, num2].cellType, num3, num4, true);
			GridManager.cellGrid[num3, num4].oldX = x;
			GridManager.cellGrid[num3, num4].oldY = y;
			GridManager.cellGrid[num3, num4].transform.position = new Vector3(x, y);
		}
	}
}
