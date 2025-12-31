using UnityEngine;

public class RotateCell : Cell
{
	public SpinDir spinDir;

	public override void ExecuteStep()
	{
		TrySpinCell(x - 1, y);
		TrySpinCell(x + 1, y);
		TrySpinCell(x, y + 1);
		TrySpinCell(x, y - 1);
	}

	private void TrySpinCell(int x, int y)
	{
		if (x < 0 || x >= GridManager.width || y < 0 || y >= GridManager.height)
		{
			return;
		}
		Cell cell = GridManager.cellGrid[x, y];
		if (cell == null || cell is ImmobileCell)
		{
			return;
		}
		int num = (int)cell.direction;
		if (spinDir == SpinDir.Positive)
		{
			num++;
			if (num > 3)
			{
				num = 0;
			}
		}
		else
		{
			num--;
			if (num < 0)
			{
				num = 3;
			}
		}
		cell.SetDirection((Direction)num);
	}

	public void SetSpinDirection(SpinDir dir)
	{
		spinDir = dir;
		switch (dir)
		{
		case SpinDir.Positive:
			cellType = CellType.PositiveRotator;
			break;
		case SpinDir.Negative:
			cellType = CellType.NegativeRotator;
			break;
		}
		if (spinDir == SpinDir.Positive)
		{
			transform.localScale = new Vector3(-1f, 1f, 1f);
		}
	}
}
