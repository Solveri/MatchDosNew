using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public int width;
    public int height;
    public GameObject[] dots;
    public GameObject tilepreFab;
    private BackgroundTile[,] allTiles;
    public GameObject[,] arrayDot;
    
    // Start is called before the first frame update
    void Start()
    {
        allTiles = new BackgroundTile[width, height];
        arrayDot = new GameObject[width, height];
        SetUp();
    }
    
   private void SetUp(){
    for (int i = 0; i < width; i++)
    {
        for (int y = 0; y < height; y++)
        {
                Vector2 tempPos = new Vector2(i, y);
                GameObject tile =  Instantiate(tilepreFab,tempPos,Quaternion.identity);
                tile.name = $"{i},{y}";
                tile.transform.parent = this.transform;
                int dotToUse = Random.Range(0, dots.Length);
                GameObject dot = Instantiate(dots[dotToUse], tempPos, Quaternion.identity);
                dot.transform.parent = this.transform;
                dot.name = $"{i},{y}";
                arrayDot[i, y] = dot;
            }
    }
   }
}
