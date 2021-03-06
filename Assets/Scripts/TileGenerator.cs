using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TileGenerator : MonoBehaviour
{
    private GameObject[,] tiles;
    private Texture2D levelBitmap;

    public float scale;
    // public string levelName;
    public GameObject referenceTile;
    public GameObject wall;
    public GameObject endGoal;
    public GameObject player;
    public Vector2 playerPosition;
    public Vector2 goalPosition;
    public Color goalColor;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        string levelName = SceneManager.GetActiveScene().name;
        levelBitmap = Resources.Load("Levels/" + levelName) as Texture2D;

        tiles = new GameObject[levelBitmap.width, levelBitmap.height];
        for (int i = 0; i < levelBitmap.width; i++) {
            for (int j = 0; j < levelBitmap.height; j++) {
                GameObject newTile = Instantiate(referenceTile, scale * new Vector3((float)i, 0, (float)j), referenceTile.transform.rotation) as GameObject;
                newTile.transform.localScale *= scale;
                if (levelBitmap.GetPixel(i,j).a < 1f) {
                    newTile.GetComponent<Renderer>().enabled = false;
                    newTile.GetComponent<Tile>().setInstantColor(Color.black);
                }
                else {
                    newTile.GetComponent<Tile>().setInstantColor(levelBitmap.GetPixel(i,j));
                }
                tiles[i,j] = newTile;
            }
        }

        endGoal.transform.position = new Vector3(scale * goalPosition[0], 1, scale * goalPosition[1]);
        endGoal.transform.localScale *= scale;
        endGoal.GetComponent<Renderer>().material.SetColor("_EmissionColor", goalColor);

        player.transform.position = new Vector3(scale * playerPosition[0], 1, scale * playerPosition[1]);
        
        GameObject wall1 = Instantiate(wall, scale * new Vector3(levelBitmap.width / 2, 0, -1f), Quaternion.identity) as GameObject;
        wall1.transform.localScale = new Vector3(levelBitmap.width * scale, 100, 1);
        GameObject wall2 = Instantiate(wall, scale * new Vector3(-1f, 0, levelBitmap.height / 2), Quaternion.identity) as GameObject;
        wall2.transform.localScale = new Vector3(1, 100, levelBitmap.height * scale);
        GameObject wall3 = Instantiate(wall, scale * new Vector3(levelBitmap.width / 2, 0, levelBitmap.height), Quaternion.identity) as GameObject;
        wall3.transform.localScale = new Vector3(levelBitmap.width * scale, 100, 1);
        GameObject wall4 = Instantiate(wall, scale * new Vector3(levelBitmap.width, 0, levelBitmap.height / 2), Quaternion.identity) as GameObject;
        wall4.transform.localScale = new Vector3(1, 100, levelBitmap.height * scale);
    }
}
