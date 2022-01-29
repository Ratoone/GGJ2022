using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    private GameObject[,] tiles;
    private Texture2D levelBitmap;

    public int size = 128;
    public float scale;
    public string levelName;
    public GameObject referenceTile;

    // Start is called before the first frame update
    void Start()
    {
        levelBitmap = Resources.Load("Levels/" + levelName) as Texture2D;

        tiles = new GameObject[size, size];
        for (int i = 0; i < size; i++) {
            for (int j = 0; j < size; j++) {
                GameObject newTile = Instantiate(referenceTile, scale * new Vector3((float)i, 0, (float)j), referenceTile.transform.rotation) as GameObject;
                newTile.transform.localScale *= scale;
                // newTile.GetComponent<Renderer>().material.SetColor("_Color", Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f));
                newTile.GetComponent<Tile>().setInstantColor(levelBitmap.GetPixel(i,j));
                tiles[i,j] = newTile;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
