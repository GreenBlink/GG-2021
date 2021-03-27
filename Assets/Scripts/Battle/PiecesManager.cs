using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiecesManager : MonoBehaviour
{
	[SerializeField] private List<Piece> m_piecesWhite;
	[SerializeField] private List<Piece> m_piecesBlack;
	[SerializeField] private Chessboard m_chessboard;
	
	private Piece m_choosePieces;
	private int m_idCurrentPlayer;

	public void Initiate()
	{
		m_idCurrentPlayer = 0;
		m_chessboard.Initiate(this);
		InitiatePieces(m_piecesWhite, 0);
		InitiatePieces(m_piecesBlack, 1);
	}

	public void ChangePlayer()
	{
		m_idCurrentPlayer = m_idCurrentPlayer == 0 ? 1 : 0;
	}

	public void MoveParty(ChessboardSquare square, Piece piece)
	{
		if (IsChoosePiece())
		{
			if (piece != null)
			{
				if (piece == m_choosePieces)
				{
					CancelChoosePiece();
				}
				else if (piece.GetIdPlayer() == GetIdPlayer())
				{
					piece.BlinkSquare();
				}
				else
				{
					//DestroyPiece(square);
				}
			}
			else
			{
				MovePiece(square);
			}
		}
		else
		{
			if (piece != null)
			{
				if (piece.GetIdPlayer() == GetIdPlayer())
				{
					ChoosePiece(piece);
				}
				else
				{
					square.Blink();
				}
			}
		}
	}

	public bool MovePiece(ChessboardSquare square)
	{
		if (m_choosePieces != null)
		{
			if (m_choosePieces.Move(square))
			{
				CancelChoosePiece();
				ChangePlayer();
				return true;
			}
			else
			{
				square.Blink();
			}
		}
		
		return false;
	}

	public void DestroyPiece(ChessboardSquare square)
	{
		if (MovePiece(square))
		{
			square.DestroyPiece();
		}
	}

	public void ChoosePiece(Piece piece)
	{
		CancelChoosePiece();

		m_choosePieces = piece;
		piece.Choose();
	}

	public void CancelChoosePiece()
	{
		if (m_choosePieces != null)
		{
			m_choosePieces.CancelChoose();
			m_choosePieces = null;
		}
	}
	
	public int GetIdPlayer() => m_idCurrentPlayer;
	public Chessboard GetChessboard() =>m_chessboard;
	public bool IsChoosePiece() => m_choosePieces != null;

	private void InitiatePieces(List<Piece> pieces, int idPlayer)
	{
		int i = 0;
		
		pieces[i++].Initiate(this, m_chessboard.GetClearSquare(idPlayer == 0), idPlayer, Piece.TypePiece.Castle, new FreeMove());
		pieces[i++].Initiate(this, m_chessboard.GetClearSquare(idPlayer == 0), idPlayer, Piece.TypePiece.Knight, new FreeMove());
		pieces[i++].Initiate(this, m_chessboard.GetClearSquare(idPlayer == 0), idPlayer, Piece.TypePiece.Bishop, new FreeMove());
		pieces[i++].Initiate(this, m_chessboard.GetClearSquare(idPlayer == 0), idPlayer, Piece.TypePiece.King, new FreeMove());
		pieces[i++].Initiate(this, m_chessboard.GetClearSquare(idPlayer == 0), idPlayer, Piece.TypePiece.Queen, new QueenMove());
		pieces[i++].Initiate(this, m_chessboard.GetClearSquare(idPlayer == 0), idPlayer, Piece.TypePiece.Bishop, new FreeMove());
		pieces[i++].Initiate(this, m_chessboard.GetClearSquare(idPlayer == 0), idPlayer, Piece.TypePiece.Knight, new FreeMove());
		pieces[i++].Initiate(this, m_chessboard.GetClearSquare(idPlayer == 0), idPlayer, Piece.TypePiece.Castle, new FreeMove());

		for (; i < pieces.Count; i++)
		{
			pieces[i].Initiate(this, m_chessboard.GetClearSquare(idPlayer == 0), idPlayer, Piece.TypePiece.Pawn, new ForwardMove());
		}
	}
}
