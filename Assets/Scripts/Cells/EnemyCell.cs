using UnityEngine;

public class EnemyCell : MonoBehaviour
{
    public int x;
    public int y;
    public bool alive = true;

    public  GameObject particleExplosion;

    public void setXY(int newX, int newY)
    {
        x = newX;
        y = newY;
        transform.position = new Vector3(x, y, 0);
    }

    public void Kill()
    {
        gameObject.SetActive(false);
        alive = false;
    }

    public void Reanimate()
    {
        gameObject.SetActive(true);
        alive = true;
    }
}
