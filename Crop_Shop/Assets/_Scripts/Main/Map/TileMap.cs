using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TileMap : MonoBehaviour {

    public GameObject player;
    public TileType[] tileType;

    private enum TileCategory
    {
        ground,
        dirt,
        seedbox,
        toolbox,
        trash,
        truck,
        phone
    }

    int[,] tiles;
    Node[,] graph;

    private int mapSizeX = 7;
    private int mapSizeY = 10;

    void Awake()
    {
        player.GetComponent<PlayerMovement>().tileX = (int)player.transform.position.x;
        player.GetComponent<PlayerMovement>().tileY = (int)player.transform.position.y;
        player.GetComponent<PlayerMovement>().map = this;

        PopulateMap();

        GeneratePathFindingGraph();
        //Spawn the visual map
        GenerateMap();
    }

    private void PopulateMap()
    {
        //Define map tiles
        tiles = new int[mapSizeX, mapSizeY];

        //Initialize map tiles to be ground
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                tiles[x, y] = (int)TileCategory.ground;
            }
        }

        //Create dirt locations
        for (int i = 0; i < 6; i++)
        {
            tiles[0, i + 3] = (int)TileCategory.dirt;
            tiles[2, i + 3] = (int)TileCategory.dirt;
            tiles[4, i + 3] = (int)TileCategory.dirt;
            tiles[6, i + 3] = (int)TileCategory.dirt;
        }

        //Create toolbox locations
        tiles[0, 2] = (int)TileCategory.toolbox;
        tiles[6, 9] = (int)TileCategory.toolbox;

        //Create seedbox locations
        tiles[6, 2] = (int)TileCategory.seedbox;
        tiles[0, 9] = (int)TileCategory.seedbox;

        //Create trash locations
        tiles[0, 0] = (int)TileCategory.trash;
        tiles[6, 0] = (int)TileCategory.trash;

        //Create truck locations
        for (int i = 0; i < 2; i++)
        {
            tiles[2, i] = (int)TileCategory.truck;
            tiles[3, i] = (int)TileCategory.truck;
            //tiles[4, i] = (int)TileCategory.truck;
        }

        //Create phone locations
        tiles[5, 0] = (int)TileCategory.phone;
    }

    public float CostToEnterTile(int sourceX, int sourceY, int targetX, int targetY)
    {
        TileType tt = tileType[ tiles[targetX, targetY] ];

        if (CanEnterTile(targetX, targetY) == false)
        {
            return 999f;
        }
        float cost = tt.movmentCost;
        return cost;
    }

    bool CanEnterTile(int x, int y)
    {
        TileType tt = tileType[ tiles[x, y] ];
        return tt.isWalkable;
    }

    void GeneratePathFindingGraph()
    {
        graph = new Node[mapSizeX, mapSizeY];

        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                graph[x, y] = new Node();
                graph[x, y].x = x;
                graph[x, y].y = y;
            }
        }

        //Calculate Node neighbours
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                //4 way connected map
                if (x > 0)
                {
                    graph[x, y].neighbours.Add(graph[x - 1, y]);
                }
                if (x < mapSizeX - 1)
                {
                    graph[x, y].neighbours.Add(graph[x + 1, y]);
                }
                if (y > 0)
                {
                    graph[x, y].neighbours.Add(graph[x, y - 1]);
                }
                if (y < mapSizeY - 1)
                {
                    graph[x, y].neighbours.Add(graph[x, y + 1]);
                }
            }
        }
    }

    void GenerateMap()
    {
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                TileType tt = tileType[tiles[x, y]];
                Vector3 ttPos = new Vector3(x, y, 0);
                GameObject click = (GameObject)Instantiate(tt.tileVisualPrefab, ttPos, Quaternion.identity);

                //Give every tile a position
                ClickTile ct = click.GetComponent<ClickTile>();
                ct.tileX = x;
                ct.tileY = y;
                ct.map = this;
            }
        }
    }

    public Vector3 TileCoordToWorldCoord(int x, int y)
    {
        return new Vector3(x, y, 0);
    }

    public void MovePlayer(int x, int y)
    {
        if (player.transform.position.x == x && player.transform.position.y == y)
        {
            ClickTile.click = false;
            return;
        }

        player.GetComponent<PlayerMovement>().currentPath = null;

        Dictionary<Node, float> distance = new Dictionary<Node, float>();
        Dictionary<Node, Node> previous = new Dictionary<Node, Node>();
        List<Node> unvisited = new List<Node>();

        Node source = graph[player.GetComponent<PlayerMovement>().tileX, player.GetComponent<PlayerMovement>().tileY];
        Node target = graph[x, y];

        distance[source] = 0;
        previous[source] = null;

        //Initialise everything to have Infinity distance
        foreach (Node v in graph)
        {
            if (v != source)
            {
                distance[v] = Mathf.Infinity;
                previous[v] = null;
            }
            unvisited.Add(v);
        }

        while (unvisited.Count > 0)
        {
            //'u' is going to be the unvisted node with the smallest distance
            Node u = null;
            foreach (Node possible in unvisited)
            {
                if (u == null || distance[possible] < distance[u])
                {
                    u = possible;
                }
            }

            if (u == target)
            {
                break;
            }

            unvisited.Remove(u);

            foreach (Node v in u.neighbours)
            {
                //float alt = distance[u] + u.DistanceTo(v);
                float alt = distance[u] + CostToEnterTile(u.x, u.y, v.x, v.y);
                if (alt < distance[v])
                {
                    distance[v] = alt;
                    previous[v] = u;
                }
            }
        }

        if (previous[target] == null)
        {
            //No route between our target and our source.
            return;
        }

        List<Node> currentPath = new List<Node>();
        Node current = target;

        //Step through previous sequence chain and add it to our path.
        while (current != null)
        {
            currentPath.Add(current);
            current  = previous[current];
        }

        currentPath.Reverse();

        player.GetComponent<PlayerMovement>().currentPath = currentPath;
    }
}
