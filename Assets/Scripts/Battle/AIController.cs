using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
     public bool m_isActive;

     private List<Piece> m_pieces;
     private PiecesManager m_piecesManager;
     
     public void Initiate(PiecesManager piecesManager)
     {
          m_piecesManager = piecesManager;
          m_piecesManager.m_changePlayerEvent.AddListener(CheakMove);
          m_pieces = m_piecesManager.GetPieceListAI();
     }
     
     public bool ControllerAI()
     {
          if (m_isActive)
          {
               m_isActive = false;
               m_piecesManager.SetPlayerAI();
          }
          else
          {
               m_isActive = true;
               m_piecesManager.SetPlayerAI();
          }

          CheakMove();
          return m_isActive;
     }

     public void CheakMove()
     {
          if (m_piecesManager.GetIdPlayer() == 2)
          {
               int randomIndex = Random.Range(0, m_pieces.Count);

               for (int i = randomIndex; i < m_pieces.Count; i++)
               {
                    List<ChessboardSquare> chessboardSquare =  m_pieces[i].GetAcceptSquareAi();

                    if (chessboardSquare.Count > 0)
                    {
                         ChessboardSquare target = chessboardSquare[Random.Range(0, chessboardSquare.Count)];
                         target.DestroyPiece();
                         m_pieces[i].Move(target);
                         m_piecesManager.ChangePlayer();
                         return;
                    }
               }

               for (int i = 0; i < m_pieces.Count; i++)
               {
                    List<ChessboardSquare> chessboardSquare =  m_pieces[i].GetAcceptSquareAi();

                    if (chessboardSquare.Count > 0)
                    {
                         ChessboardSquare target = chessboardSquare[Random.Range(0, chessboardSquare.Count)];
                         target.DestroyPiece();
                         m_pieces[i].Move(target);
                         m_piecesManager.ChangePlayer();
                         return;
                    }
               }
          }
     }
}
