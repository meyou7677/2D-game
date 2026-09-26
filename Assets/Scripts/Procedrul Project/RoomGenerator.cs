using NUnit.Framework;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;


public class RoomGenerator : MonoBehaviour
{
    public enum Tile { empty, floor, wall};
    [Header("Area")]
    public int AreaWidth;
    public int AreaHeight;
    [Header("Randomisation")]
    public int roomAttempt;
    public Vector2Int RoomWidthRange;
    public Vector2Int RoomHeightRange;
    public bool allowoverlap;
    public int extracorridors;
    [Header("TileMap")]
    public Tilemap tilemap;
    public RuleTile brush;
    private Tile[,] grid;
    private List<RectInt> rooms = new List<RectInt>();
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        roomAttempt = PlayerPrefs.GetInt("roomattempt", roomAttempt);
        AreaWidth = PlayerPrefs.GetInt("areawidth", AreaWidth);
        AreaHeight = PlayerPrefs.GetInt("areaheight", AreaHeight);
    }

    public List<RectInt> getrooms()
    {
        return rooms;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0, 0.2f);
        Gizmos.DrawCube(transform.position + new Vector3(AreaWidth/2, AreaHeight/2), new Vector3(AreaWidth, AreaHeight, 0));
    }

    public void generate()
    {
        grid = new Tile[AreaWidth, AreaHeight];
        rooms.Clear();
        for(int x = 0;x < AreaWidth; x++)
        {
            for(int y = 0;y < AreaHeight; y++)
            {
                grid[x, y] = Tile.empty;
            }
        }
        createroom();
        connectrooms();
        paint();
    }

    private void createroom()
    {
        for(int i = 0; i < roomAttempt; i++)
        {
            int w = UnityEngine.Random.Range(RoomWidthRange.x, RoomWidthRange.y) + 2;
            int h = UnityEngine.Random.Range(RoomHeightRange.x, RoomHeightRange.y) + 2;
            int x = UnityEngine.Random.Range(1, AreaWidth - w);
            int y = UnityEngine.Random.Range(1, AreaHeight - h);
            RectInt newroom = new RectInt(x, y, w, h);
            if (!allowoverlap)
            {
                bool overlaps = false;
                foreach(var r in rooms)
                {
                    overlaps = newroom.Overlaps(r);
                    if (overlaps)
                    {
                        break;
                    }
                }
                if (overlaps)
                {
                    continue;
                }
            }
            rooms.Add(newroom);
            paintroom(newroom);
        }
    }

    private void paintroom(RectInt room)
    {
        for (int x = room.x; x < room.x + room.width; x++)
        {
            for (int y = room.y; y < room.y + room.height; y++)
            {
                grid[x, y] = Tile.floor;
            }
        }
    }

    private void paint()
    {
        tilemap.ClearAllTiles();
        for(int x = 0; x < AreaWidth; x++)
        {
            for( int y = 0; y < AreaHeight; y++)
            {
                if (grid[x, y] != Tile.empty)
                {
                    tilemap.SetTile(new Vector3Int(x, y, 0), brush);
                }
                
            }
        }
    }

    private void connectrooms()
    {
        
        List<int> done = new List<int>();
        done.Add(0);
        List<int> remaining = new List<int>();
        for(int i = 1; i < rooms.Count; i++)
        {
            remaining.Add(i);
        }
        List<Vector2Int> centres = new List<Vector2Int>();
        for(int i = 0; i < rooms.Count; i++)
        {
            Vector2Int roomscentre = new Vector2Int();
            roomscentre.x = rooms[i].x + rooms[i].width / 2;
            roomscentre.y = rooms[i].y + rooms[i].height / 2;
            centres.Add(roomscentre);
        }
        List<(int a, int b)> pairs = new List<(int a, int b)>();
        int Connection1 = UnityEngine.Random.Range(1, rooms.Count);
        int Connection2 = UnityEngine.Random.Range(1, rooms.Count);
        for(int i = 0; i < extracorridors; i++)
        {
            Connection1 = UnityEngine.Random.Range(1, rooms.Count);
            Connection2 = UnityEngine.Random.Range(1, rooms.Count);
            pairs.Add((Connection1, Connection2));
        }
        
        while (remaining.Count > 0)
        {
            float bestdistance = float.MaxValue;
            int besta = -1;
            int bestb = -1;
            foreach(int d in done)
            {
                foreach(int r in remaining)
                {
                    Vector2Int roomacentre = centres[d];
                    Vector2Int roombcentre = centres[r];
                    float distance = Vector2Int.Distance(roomacentre, roombcentre);
                    if(distance < bestdistance)
                    {
                        bestdistance = distance;
                        besta = d;
                        bestb = r;
                    }
                    
                }

            }
            pairs.Add((besta, bestb));
            remaining.Remove(bestb);
            done.Add(bestb);
        }
        foreach(var i in pairs)
        {
            carvecorridor(centres[i.a], centres[i.b]);
        }
    }

    private void carvecorridor(Vector2Int rooma, Vector2Int roomb)
    {
        int startx = Mathf.Min(rooma.x, roomb.x);
        int finishx = Mathf.Max(rooma.x, roomb.x);
        int starty = Mathf.Min(rooma.y, roomb.y);
        int finishy = Mathf.Max(rooma.y, roomb.y);
        for (int i = startx; i <= finishx; i++)
        {
            for(int x = -2; x <= 2; x++)
            {
                for (int y = -2; y <= 2; y++)
                {
                    grid[i + x, roomb.y + y] = Tile.floor;
                }
            }
        }
        int corridorx = startx;
        if(rooma.x == finishx)
        {
            corridorx = finishx;
        }
        
        for (int i = starty; i <= finishy; i++)
        {
            for (int x = -2; x <= 2; x++)
            {
                for (int y = -2; y <= 2; y++)
                {
                    grid[corridorx + x, i + y] = Tile.floor;
                }
            }
        }
    }

    public Vector2 ChooseTile(RectInt room, bool[,] isoccupied)
    {
        Vector2 position = new Vector2();
        int repeat = 30;
        do
        {
            int x = UnityEngine.Random.Range(room.x + 1, room.x + room.width - 1);
            int y = UnityEngine.Random.Range(room.y + 1, room.y + room.height - 1);
            position = new Vector2(x + 0.5f, y + 0.5f);
            repeat--;
        } while (isoccupied[(int)position.x, (int)position.y] && repeat > 0);
        if(repeat == 0)
        {
            Debug.LogError("ChooseTile failed to find unoccupied tile");
        }
        return position;
    }
}
