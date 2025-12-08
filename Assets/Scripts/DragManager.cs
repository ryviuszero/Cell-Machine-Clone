using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class DragManager : MonoBehaviour
{
    public static DragManager instance;
    private Cell draggedCell;
    private Vector3 offset;
    private bool isDragging;
    private Camera cam;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if(!GridManager.instance.startedSim)
        {
            if(Input.GetMouseButtonDown(0))
            {
                BeginDrag();
            }
            if(Input.GetMouseButtonUp(0) && isDragging)
            {
                EndDrag();
            }
            if (isDragging)
            {
                OnDrag();
            }
        }
    }

    private void BeginDrag()
    {
        Vector3 vector = cam.ScreenToWorldPoint(Input.mousePosition);
        int num = Mathf.RoundToInt(vector.x);
        int num2 = Mathf.RoundToInt(vector.y);
        if(CanDrag(num, num2))
        {
            Cell cell = GridManager.cellGrid[num, num2];
            if (cell != null)
            {
                draggedCell = cell;
                draggedCell.transform.position = new Vector3(draggedCell.transform.position.x, draggedCell.transform.position.y, -0.2f);
                offset = vector - cell.transform.position;
                isDragging = true;
            }
        }
        
    }

    private void OnDrag()
    {
        Vector3 vector = cam.ScreenToWorldPoint(Input.mousePosition);
        draggedCell.transform.position = vector - offset;
    }

    private void EndDrag()
    {
        isDragging = false;
        int num = Mathf.RoundToInt(draggedCell.transform.position.x);
        int num2 = Mathf.RoundToInt(draggedCell.transform.position.y);
        if(ValidDropCell(num, num2))
        {
           draggedCell.setXY(num, num2);
           draggedCell.transform.position = new Vector3(num, num2, 0);
           draggedCell.SetCurAsInitial();
        }
        draggedCell.transform.position = new Vector3(draggedCell.x, draggedCell.y, 0);
    }

    private bool ValidDropCell(int x, int y)
    {
        if(x >= 0 && x < GridManager.width && y >= 0 && y < GridManager.height && GridManager.cellGrid[x, y] == null)
        {
            return GridManager.emptyCells[x, y].canPlace;
        }
        return false;
    }

    private bool CanDrag(int x, int y)
    {
        if(x >= 0 &&  x < GridManager.width && y >= 0 && y < GridManager.height)
        {
            return GridManager.emptyCells[x, y].canPlace;
        }
        return false;
    }

    public bool InDrag()
    {
        return isDragging;
    }
}
