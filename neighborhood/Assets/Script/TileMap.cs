using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMap : MonoBehaviour
{
     public TileType[] tileTypes;
     int[,] tiles;

     private int mapSizeX = 10;
     private int mapSizeY = 10;

     private void Start()
     {
          tiles = new int[mapSizeX,mapSizeY];

          for (int x = 0; x < mapSizeX; x++) {
               for (int y = 0; y < mapSizeY; y++)
               {
                    tiles[x, y] = 0;
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

          GenerateMapVisual();
     }

     void GenerateMapVisual() {
          for (int x = 0; x < mapSizeX; x++) {
               for (int y = 0; y < mapSizeY; y++)
               {
                    TileType tt = tileTypes[tiles[x, y]];
                    Instantiate(tt.tileVisualPrefab, new Vector3(x, y, 0), Quaternion.identity);
               }
          }
          
     }
}