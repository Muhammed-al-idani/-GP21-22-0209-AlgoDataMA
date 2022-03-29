using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor.Experimental.GraphView;

public class TileMap : MonoBehaviour
{
     public GameObject selectedUnit;
     public TileType[] tileTypes;

    

     int[,] tiles; 
     Node[,] graph;

      

     int mapSizeX = 10;
      int mapSizeY = 10;
     private void Start()
     {
          selectedUnit.GetComponent<Unit>().tileX = (int)selectedUnit.transform.position.x;
          selectedUnit.GetComponent<Unit>().tileY = (int)selectedUnit.transform.position.y;
          selectedUnit.GetComponent<Unit>().map=this;
          GenerateMapData();
          GeneratePathfindingGraph();
          GenerateMapVisual();
     }

     void GenerateMapData()
     {
          tiles = new int[mapSizeX, mapSizeY];
          int x, y;
          
          for ( x = 0; x < mapSizeX; x++) {
               for ( y = 0; y < mapSizeY; y++)
               {
                    tiles[x, y] = 0;
               }
          }

          for ( x = 3; x <= 5; x++) {
               for ( y = 0; y < 4; y++)
               {
                    tiles[x, y] = 1;
               }
               
          }

          tiles[4, 4] = 2;
          tiles[5, 4] = 2;
          tiles[6, 4] = 2;
          tiles[7, 4] = 2;
          tiles[8, 4] = 2;
          
          tiles[4, 5] = 2;
          tiles[4, 6] = 2;
          tiles[8, 5] = 2;
          tiles[8, 6] = 2;
          
     }

     public class Node
     {
          public List<Node> neighbours;
          public int x;
          public int y;

          public Node()
          {
               neighbours = new List<Node>();
          }

          public float DistanceTo(Node n) {
               if (n == null) {
                    Debug.LogError("?");
                    
               }
               return Vector2.Distance(new Vector2(x, y), new Vector2(n.x, n.y));
               
          }
     }

    

     void GeneratePathfindingGraph() {
          
          graph = new Node[mapSizeX, mapSizeY];

          for (int x = 0; x < mapSizeX; x++) {
               for (int y = 0; y < mapSizeY; y++) {
                    graph[x, y] = new Node();
                    graph[x, y].x = x;
                    graph[x, y].y = y;
               }
          }

          for (int x = 0; x < mapSizeX; x++) {
                         for (int y = 0; y < mapSizeY; y++) {
                              
                              if(x>0)
                                   graph[x,y].neighbours.Add(graph[x-1,y]);
                              if(x < mapSizeX-1)
                                   graph[x,y].neighbours.Add(graph[x+1,y]);
                              if(y > 0 )
                                   graph[x,y].neighbours.Add(graph[x,y-1]);
                              if(y < mapSizeY-1)
                                   graph[x,y].neighbours.Add(graph[x,y+1]);
                         }
          }
     }
     
     void GenerateMapVisual()
     {
          for (int x = 0; x < mapSizeX; x++) {
               for (int y = 0; y < mapSizeY; y++)
               {
                    TileType tt = tileTypes[tiles[x, y]];
                    GameObject go = (GameObject)Instantiate(tt.tileVisualPrefab, new Vector3(x, y, 0), Quaternion.identity);
                   
                    ClickableTile ct = go.GetComponent<ClickableTile>();
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

     public void GeneratePathTo(int x, int y) {
          selectedUnit.GetComponent<Unit>().currentPath = null;
          
          /* selectedUnit.GetComponent<Unit>().tileX = x;
           selectedUnit.GetComponent<Unit>().tileY = y;
           selectedUnit.transform.position = TileCoordToWorldCoord(x,y);
           */
         
         Dictionary<Node,float > dist = new Dictionary<Node, float>();
         Dictionary<Node, Node> prev = new Dictionary<Node, Node>();

         List<Node> unvisited = new List<Node>();
          
          Node source = graph[selectedUnit.GetComponent<Unit>().tileX, selectedUnit.GetComponent<Unit>().tileY];
          Node target = graph[x,y];

         dist[source] = 0;
         prev[source] = null;
         foreach (Node v in graph) {
              if (v != source)
              {
                   dist[v] = Mathf.Infinity;
                   prev[v] = null;
              }
              unvisited.Add(v);

         }

         while (unvisited.Count > 0)
         {
              Node u = null;
              foreach (Node possibleU in unvisited) {
                   if (u==null || dist[possibleU]<dist[u]) {
                        u = possibleU;
                   }
              }
              if (u == target)
              {
                   break;
              }
              unvisited.Remove(u);

              foreach (Node v in u.neighbours)
              {
                   float alt = dist[u] + u.DistanceTo(v);
                   if (alt < dist[v]) {
                        dist[v] = alt;
                        prev[v] = u;
                         
                   }
              }
         }

         if (prev[target]==null) {
              
              return;
         }

         List<Node>currentPath = new List<Node>();
         Node curr = target;

         while (curr != null) {
              currentPath.Add(curr);
              curr = prev[curr];
         }

         currentPath.Reverse();
         selectedUnit.GetComponent<Unit>().currentPath = currentPath;
     }
}
