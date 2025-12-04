using UnityEngine;
using UnityEngine.UI;


public class UIEvent : MonoBehaviour
{
    public Sprite playSprite;
    public Sprite pauseSprite;
    public Image playBtnImage;
    public GameObject resetButton;
    public static UIEvent instance;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StepButtonPressed();
        }
        if(Input.GetKeyDown(KeyCode.Return))
        {
            PlayButtonPressed();
        }
    }

    public void PlayButtonPressed()
    {
        if(GridManager.isPlayMode)
        {
            GridManager.instance.PlaySim();
            playBtnImage.sprite = playSprite;
        }else
        {
            GridManager.instance.PlaySim();
            playBtnImage.sprite = pauseSprite;
            resetButton.SetActive(true);
        }
    }

    public void StepButtonPressed()
    {
        resetButton.SetActive(true);
        if(GridManager.isPlayMode)
        {
            GridManager.instance.PauseSim();
        }
        GridManager.instance.StepSim();
    }

    public void ResetButtonPressed()
    {
        GridManager.instance.ResetSim();
        resetButton.SetActive(false);
        playBtnImage.sprite = playSprite;
    }

    public void PauseButtonPressed()
    {
        GridManager.instance.PauseSim();
    }
}
