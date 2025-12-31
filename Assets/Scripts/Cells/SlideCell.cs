public class SlideCell : Cell
{
	public Orientation orientation;

	public override void ExecuteStep()
    {}

	public override void SetDirection(Direction dir)
	{
		base.SetDirection(dir);
		if (dir == Direction.Up || dir == Direction.Down)
		{
			orientation = Orientation.Vertical;
		}
		else
		{
			orientation = Orientation.Horizontal;
		}
	}
}
