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

    public Tile obstacle;
    public Tile ceiling;
    public Tile floor;
    public Tile ground;

    public Rigidbody2D player;

    System.Random rand = new System.Random();

    int[] obstaclesPosition = new int[10];

    float currentPositionY;


    Chunk[] chunk = new Chunk[6];
    int chunkHeight = 50;
    int chunkWidth = 50;


    bool[] isChunkGenerated = new bool[6];
    // Start is called before the first frame update
    void Start()
    {
        int seed = rand.Next();
        obstacleTilemap.ClearAllTiles();
        mapTilemap.ClearAllTiles();
        UnityEngine.Random.InitState(seed);



        for(int i=0; i<6; i++)
        {
            chunk[i] = new Chunk(chunkWidth * i, chunkHeight);
            //Debug.Log(chunk[i].width);
        }

        GenerateMap();
        GenerateChunk(chunk[0]);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("x = " + player.position.x + "; y = " + player.position.y + "; ");
        Debug.Log("chunk nr " + OnWhichChunk(player) + "; ");
        switch (OnWhichChunk(player))
        {
            case 0:
                {
                    if (!isChunkGenerated[1])
                    {
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
                        isChunkGenerated[3] = false;
                        GenerateFirstAndLastChunk();

                        isChunkGenerated[5] = true;
                        isChunkGenerated[0] = true;
                    }
                    break;
                }
            case 5:
                {
                    isChunkGenerated[0] = false;
                    if(player.position.x > 275)
                    {
                        player.position = new Vector3(25, player.position.y);
                    }
                    break;
                }
            default:
                {
                    break;
                }
        }
        //PRZEJSCIE Z 5 NA 0
        /*if (player.position.x > 20)
        {
            currentPositionY = player.position.y;
            tilemap.ClearAllTiles();
            GenerateMap();
            player.position = new Vector2(0, currentPositionY);
        }*/
    }

    void GenerateFirstAndLastChunk()
        {
            int offset = 10;
            int i = 0;
            while (i < chunkWidth)
            {
                int pos = rand.Next(0, offset);
                obstacleTilemap.SetTile(new Vector3Int(pos + i, 0, 0), obstacle);
                obstacleTilemap.SetTile(new Vector3Int(pos + i + 250, 0, 0), obstacle);
                i += offset;
            }
        }



    void GenerateMap()
    {
        int srodek = chunkHeight / 2;
        for(int i=0; i<chunkHeight; i++)
        {
            if(i == srodek - 5)
            {
                for(int j=-8; j<chunkWidth*6; j++)
                {
                    mapTilemap.SetTile(new Vector3Int(j, 5,0),ceiling);
                }
            }
            else if (i == srodek + 1)
            {
                for(int j=-8; j<chunkWidth*6; j++)
                {
                    mapTilemap.SetTile(new Vector3Int(j, -1, 0), floor);
                }
            }
            else if(i<srodek-5 || i > srodek + 1)
            {
                for (int j = -8; j < chunkWidth * 6; j++)
                {
                    mapTilemap.SetTile(new Vector3Int(j, srodek-i, 0),ground);
                }
            }
            //Debug.Log("i = " + i);
        }
    }

    void GenerateChunk(Chunk chunk)
    {
        int offset = 15;
        int i = 0;
        while(i<chunkWidth)
        {
            int pos = rand.Next(0, offset);
            obstacleTilemap.SetTile(new Vector3Int( chunk.width + pos + i, 0, 0), obstacle);

            i+= offset;
        }
    }

    int OnWhichChunk(Rigidbody2D rb)
    {
        return Convert.ToInt32(rb.position.x) / chunkHeight;
    }
}