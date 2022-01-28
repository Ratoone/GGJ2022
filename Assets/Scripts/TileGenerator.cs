using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    public int size = 128;
    public GameObject referenceTile;
    
    private GameObject[,] tiles;

    // Start is called before the first frame update
    void Start()
    {
        tiles = new GameObject[size, size];
        for (int i = 0; i < size; i++) {
            for (int j = 0; j < size; j++) {
                GameObject newTile = Instantiate(referenceTile, new Vector3((float)i, 1, (float)j), Quaternion.identity) as GameObject;
                newTile.transform.localScale = Vector3.one;
                // newTile.selectColor()
                newTile.GetComponent<Renderer>().material.SetColor("_Color", Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f));
                tiles[i,j] = newTile;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
