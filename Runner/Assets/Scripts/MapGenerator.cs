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
    public int ObstacleRangeMin = 5;
    public int ObstacleRangeMax = 10;

    public GameObject gameControllerObject;

    System.Random rand = new System.Random();

    Chunk[] chunk = new Chunk[6];
    int chunkHeight = 50;
    int chunkWidth = 50;

    private enum obstacleEnum { ground, flyingUpper, flyingLower };
    private int tunnelHeight = 5;
    bool[] isChunkGenerated = new bool[6];


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


        int seed = rand.Next();
        obstacleTilemap.ClearAllTiles();
        mapTilemap.ClearAllTiles();
        UnityEngine.Random.InitState(seed);


        
        for (int i = 0; i < 6; i++)
        {
            chunk[i] = new Chunk(chunkWidth * i, chunkHeight);
        }

        GenerateMap();
        GenerateChunk(chunk[0]);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("x = " + player.position.x + "; y = " + player.position.y + "; ");
        //Debug.Log("chunk nr " + OnWhichChunk(player) + "; ");
        switch (OnWhichChunk(player))
        {
            case 0:
                {
                    if (!isChunkGenerated[1])
                    {
                        ClearChunk(chunk[4]);
                        ClearChunk(chunk[5]);
                        isChunkGenerated[5] = false;
                        GenerateChunk(chunk[1]);
                        isChunkGenerated[1] = true;
                    }
                    break;
                }
            case 1:
                {
                    if (!isChunkGenerated[2])
                    {
                        isChunkGenerated[0] = false;
                        GenerateChunk(chunk[2]);
                        isChunkGenerated[2] = true;
                    }
                    break;
                }
            case 2:
                {
                    if (!isChunkGenerated[3])
                    {
                        ClearChunk(chunk[0]);
                        isChunkGenerated[1] = false;
                        GenerateChunk(chunk[3]);
                        isChunkGenerated[3] = true;
                    }
                    break;
                }
            case 3:
                {
                    if (!isChunkGenerated[4])
                    {
                        ClearChunk(chunk[1]);
                        isChunkGenerated[2] = false;
                        GenerateChunk(chunk[4]);
                        isChunkGenerated[4] = true;
                    }
                    break;
                }
            case 4:
                {
                    if (!isChunkGenerated[5] && !isChunkGenerated[0])
                    {
                        ClearChunk(chunk[2]);
                        isChunkGenerated[3] = false;
                        GenerateFirstAndLastChunk();

                        isChunkGenerated[5] = true;
                        isChunkGenerated[0] = true;
                    }
                    break;
                }
            case 5:
                {
                    ClearChunk(chunk[3]);
                    isChunkGenerated[4] = false;
                    isChunkGenerated[0] = false;
                    if (player.position.x > 275)
                    {
                        gameControllerObject.GetComponent<GameController>().Transition(player);
                    }
                    break;
                }
            default:
                {
                    break;
                }
        }
    }

    void GenerateFirstAndLastChunk()
    {
        int offset = 10;
        int i = 0;
        while (i < chunkWidth)
        {
            int pos = rand.Next(0, offset);
            obstacleTilemap.SetTile(new Vector3Int(pos + i, 0, 0), groundObstacle);
            obstacleTilemap.SetTile(new Vector3Int(pos + i + 250, 0, 0), groundObstacle);
            i += offset;
        }
    }


    void GenerateMap()
    {
        int middle = chunkHeight / 2;
        for (int i = 0; i < chunkHeight; i++)
        {
            if (i == middle - tunnelHeight)
            {
                for (int j = -15; j < chunkWidth * 6; j++)
                {
                    mapTilemap.SetTile(new Vector3Int(j, tunnelHeight, 0), ceiling);
                }
            }
            else if (i == middle + 1)
            {
                for (int j = -15; j < chunkWidth * 6; j++)
                {
                    mapTilemap.SetTile(new Vector3Int(j, -1, 0), floor);
                }
            }
            else if (i < middle - tunnelHeight || i > middle + 1)
            {
                for (int j = -15; j < chunkWidth * 6; j++)
                {
                    mapTilemap.SetTile(new Vector3Int(j, middle - i, 0), ground);
                }
            }
            //Debug.Log("i = " + i);
        }
    }

    void GenerateChunk(Chunk chunk)
    {
        int i = 0;
        int obstaclePosition;
        obstacleEnum obstacleLayer;
        while (i < chunkWidth)
        {
            obstacleLayer = obstacleEnum.ground;
            obstaclePosition = rand.Next(ObstacleRangeMin, ObstacleRangeMax);
            obstacleLayer += rand.Next(0, 10) % 3;
            Debug.Log("LAYER: " + obstacleLayer);
            if (obstaclePosition + i <= 50)
            {
                switch (obstacleLayer)
                {
                    case obstacleEnum.ground:
                        {
                            obstacleTilemap.SetTile(new Vector3Int(chunk.width + obstaclePosition + i, 0, 0), groundObstacle);
                            break;
                        }
                    case obstacleEnum.flyingUpper:
                        {
                            obstacleTilemap.SetTile(new Vector3Int(chunk.width + obstaclePosition + i, tunnelHeight - 2, 0), flyingObstacle);
                            break;
                        }
                    case obstacleEnum.flyingLower:
                        {
                            obstacleTilemap.SetTile(new Vector3Int(chunk.width + obstaclePosition + i, tunnelHeight - 4, 0), flyingObstacle);
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

    void ClearChunk(Chunk chunk)
    {
        for (int i = chunk.width; i < chunk.width + chunkWidth; i++)
        {
            obstacleTilemap.SetTile(new Vector3Int(i, 0, 0), null);
            obstacleTilemap.SetTile(new Vector3Int(i, tunnelHeight - 2, 0), null);
            obstacleTilemap.SetTile(new Vector3Int(i, tunnelHeight - 4, 0), null);
        }
    }

    int OnWhichChunk(Rigidbody2D rb)
    {
        return Convert.ToInt32(rb.position.x) / chunkWidth;
    }
}