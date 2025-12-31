public class MoverCell: Cell
{
    public override void ExecuteStep()
    {
        PushStack(direction, x, y);
    }

    public override void SetDirection(Direction dir)
	{
		base.SetDirection(dir);
		switch (dir)
		{
		case Direction.Right:
			cellType = CellType.RightMover;
			break;
		case Direction.Up:
			cellType = CellType.UpMover;
			break;
		case Direction.Left:
			cellType = CellType.LeftMover;
			break;
		case Direction.Down:
			cellType = CellType.DownMover;
			break;
		}
	}
}