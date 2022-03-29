using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameView : MonoBehaviour
{
   public BoardPiece BoardPiecePrefab;
   private List<BoardPiece> boardPieces = new List<BoardPiece>();
   
   
   public void updateView(State state)
   {
      foreach (var boardPiece in this.boardPieces)
      {
         Destroy(boardPiece.gameObject);
      }
      boardPieces.Clear();

      for (int x = 0; x < state.gridWidth; x++)
      {
         for (int y = 0; y < state.GridHeight; y++)
         {
            var boardPiece = Instantiate(BoardPiecePrefab,new Vector3(x,y),Quaternion.identity);
            boardPiece.sprite.color=Color.green;
            boardPieces.Add(boardPiece);
         }
      }
      var player = Instantiate(BoardPiecePrefab,new Vector3(state.player.x,state.player.y,-0.1f),Quaternion.identity);
      player.sprite.color=Color.blue;
      boardPieces.Add(player);
      var enemy = Instantiate(BoardPiecePrefab,new Vector3(state.enemy.x,state.enemy.y,-0.1f),Quaternion.identity);
      enemy.sprite.color=Color.red;
      boardPieces.Add(enemy);
      var goal = Instantiate(BoardPiecePrefab,new Vector3(state.goal.x,state.goal.y-0.05f),Quaternion.identity);
      goal.sprite.color=Color.yellow;
      boardPieces.Add(goal);
   }
}
