using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

public class Chunk
{
    public int height;
    public int width;
    public Chunk(int x, int y)
    {
        this.height = y;
        this.width = x;
    }
}

public class MapGenerator : MonoBehaviour
{
    public Tilemap obstacleTilemap;
    public Tilemap mapTilemap;

    public Tile flyingObstacle;
    public Tile groundObstacle;
    public Tile ceiling;
    public Tile floor;
    public Tile ground;
    //public Tile background;

    public Rigidbody2D player;

    public int ObstacleOffset = 10;
    public int ObstacleVariety = 5;

    public GameObject gameControllerObject;

    System.Random rand = new System.Random();

    private int mapHeight = 20;
    private int mapWidth;

    private enum obstacleEnum { ground, flyingUpper, flyingLower };
    private int tunnelHeight = 5;


    // Start is called before the first frame update
    void Start()
    {
        //obstacleTilemap = GetComponent<Tilemap>();
        //mapTilemap = GetComponent<Tilemap>();
        //obstacle = GetComponent<Tile>();
        //ceiling = GetComponent<Tile>();
        //floor = GetComponent<Tile>();
        //ground = GetComponent<Tile>();
        //player = GetComponent<Rigidbody2D>();
        mapWidth = updateMapWidth();

        int seed = rand.Next();
        obstacleTilemap.ClearAllTiles();
        mapTilemap.ClearAllTiles();
        UnityEngine.Random.InitState(seed);


        GenerateMap();
        GenerateObstacles();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("x = " + player.position.x + "; y = " + player.position.y + "; ");
        //Debug.Log("chunk nr " + OnWhichChunk(player) + "; ");
       if(player.position.x > mapWidth - 2 * PlayerController.speed)
        {
            Debug.Log("Clearing obstacle tiles!");
            obstacleTilemap.ClearAllTiles();
            gameControllerObject.GetComponent<GameController>().Transition(player);
            ObstacleOffset = Convert.ToInt32(PlayerController.speed);
            mapWidth = updateMapWidth();
            Debug.Log("Generating map!");
            //GenerateMap();
            Debug.Log("Done.");
            GenerateObstacles();
        }

    }

    void GenerateMap()
    {
        int middle = mapHeight / 2;
        for (int i = 0; i < mapHeight; i++)
        {
            if (i == middle - tunnelHeight)
            {
                for (int j = -15; j < mapWidth * 6; j++)
                {
                    mapTilemap.SetTile(new Vector3Int(j, tunnelHeight, 0), ceiling);
                }
            }
            else if (i == middle + 1)
            {
                for (int j = -15; j < mapWidth * 6; j++)
                {
                    mapTilemap.SetTile(new Vector3Int(j, -1, 0), floor);
                }
            }
            else if (i < middle - tunnelHeight || i > middle + 1)
            {
                for (int j = -15; j < mapWidth * 6; j++)
                {
                    mapTilemap.SetTile(new Vector3Int(j, middle - i, 0), ground);
                }
            }
        }
    }

    void GenerateObstacles()
    {
        int i = ObstacleOffset;
        int obstaclePosition;
        obstacleEnum obstacleLayer;
        while (i < mapWidth)
        {
            obstacleLayer = obstacleEnum.ground;
            obstaclePosition = (rand.Next(0, ObstacleVariety))-(ObstacleVariety/2);
            obstacleLayer += rand.Next(0, 10) % 3;
            if (obstaclePosition + i <= mapWidth-3*PlayerController.speed && obstaclePosition + i >= PlayerController.speed)
            {
                switch (obstacleLayer)
                {
                    case obstacleEnum.ground:
                        {
                            obstacleTilemap.SetTile(new Vector3Int(obstaclePosition + i, 0, 0), groundObstacle);
                            break;
                        }
                    case obstacleEnum.flyingUpper:
                        {
                            obstacleTilemap.SetTile(new Vector3Int(obstaclePosition + i, tunnelHeight - 2, 0), flyingObstacle);
                            break;
                        }
                    case obstacleEnum.flyingLower:
                        {
                            obstacleTilemap.SetTile(new Vector3Int(obstaclePosition + i, tunnelHeight - 4, 0), flyingObstacle);
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
            i += ObstacleOffset;
        }
    }

    private int updateMapWidth()
    {
       return 20 * Convert.ToInt32(PlayerController.speed);
    }
}