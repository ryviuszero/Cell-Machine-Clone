public class MoverCell: Cell
{
    public override void ExecuteStep()
    {
        PushStack(direction, x, y);
    }
}