using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GridsViewer : MonoBehaviour
{
    public GameObject gridPrefab;
    public Image[,] images;

    public float RenderUpdateduration = 0.5f;
    private float lastUpdateTime = 0;

    public static bool CanAct = false;

    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(Grid.cellsize*Grid.horizontals, Grid.cellsize*Grid.verticals, false);
        images = new Image[Grid.horizontals,Grid.verticals];
        for (int i = 0; i < Grid.horizontals; i++)
        {
            for (int j = 0; j < Grid.verticals; j++)
            {
                GameObject go = Instantiate(gridPrefab,this.transform);
                go.transform.localPosition = new Vector3(i*Grid.cellsize,j*Grid.cellsize,0);
                go.transform.localScale = Vector2.one*Grid.cellsize;
                images[i,j] = go.GetComponent<Image>();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!CanAct)
        {
            return;
        }
        if(Time.time - lastUpdateTime > RenderUpdateduration)
        {
            Grid.UpdateAllCells();
            UpdateGrids();
            lastUpdateTime = Time.time;
        }
    }

    public void UpdateGrids()
    {
        for (int i = 0; i < Grid.horizontals; i++)
        {
            for (int j = 0; j < Grid.verticals; j++)
            {
                images[i,j].color = Grid.ReadCell(i,j)? Color.white : Color.black;
            }
        }
    }


}
