using UnityEngine;

public class EmptyCell : MonoBehaviour
{
	public bool canPlace;

	private SpriteRenderer sp;

	private void Awake()
	{
		sp = GetComponent<SpriteRenderer>();
	}

	public void SetPlaceable(bool placeable)
	{
		canPlace = placeable;
		if (canPlace)
		{
			sp.color = new Color(0.25f, 0.25f, 0.25f);
		}
	}
}