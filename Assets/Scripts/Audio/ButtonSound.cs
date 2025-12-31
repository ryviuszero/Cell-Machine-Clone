using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerEnterHandler
{
    public Sound hoverSound;
    public Sound pressSound;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(delegate
        {
            if (pressSound != null)
            {
                AudioManager.instance.Play(pressSound);
            }
        });
    }

    public void OnPointerDown(PointerEventData eventData)
	{
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (hoverSound != null)
		{
			AudioManager.instance.Play(hoverSound);
		}
	}
    
}