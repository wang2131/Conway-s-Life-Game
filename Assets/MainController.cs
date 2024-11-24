using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    public int x;
    public int y;

    public int cellsize;

    public GridsViewer gridsViewer;
    private void Awake() {
        Grid.Initialize(cellsize, x, y);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GridsViewer.CanAct = !GridsViewer.CanAct;
        }
        if(Input.GetMouseButton(0))
        {
            Vector2 mousePos = Input.mousePosition;
            int x = (int)(mousePos.x/Grid.cellsize);
            int y = (int)(mousePos.y/Grid.cellsize);
            if(x >= 0 && x < Grid.horizontals && y >= 0 && y < Grid.verticals)
            {
                Grid.SetCell(x, y, true);
                gridsViewer.images[x, y].color = Color.white;
            }
        }
    }
}
