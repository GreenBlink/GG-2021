using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PiecesManager : MonoBehaviour
{
	public UnityEvent m_changePlayerEvent;
	
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
		m_changePlayerEvent.Invoke();
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
					ChoosePiece(piece);
				}
				else
				{
					if (MovePiece(square))
					{
						DestroyPiece(piece);
					}
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
					square.Light(ChessboardSquare.TypeLight.Error);
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
				square.Light(ChessboardSquare.TypeLight.Error);
			}
		}
		
		return false;
	}

	public void DestroyPiece(Piece piece)
	{
		piece.Destroy();
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
			m_chessboard.HideSquares();
			m_choosePieces.CancelChoose();
			m_choosePieces = null;
		}
	}

	public void SetNewMoveRule(Piece.TypePiece typePiece, IMoveRule moveRule)
	{
		List<Piece> pieces = m_piecesWhite;
		pieces.AddRange(m_piecesBlack);
		
		if (typePiece == Piece.TypePiece.Any)
		{
			for (int i = 0; i < pieces.Count; i++)
			{
				pieces[i].SetMoveRule(moveRule);
			}
		}
		else
		{
			for (int i = 0; i < pieces.Count; i++)
			{
				if (pieces[i].GetRule() == typePiece)
				{
					pieces[i].SetMoveRule(moveRule);
				}
			}
		}
	}
	
	public int GetIdPlayer() => m_idCurrentPlayer;
	public Chessboard GetChessboard() =>m_chessboard;
	public bool IsChoosePiece() => m_choosePieces != null;

	private void InitiatePieces(List<Piece> pieces, int idPlayer)
	{
		int i = 0;
		
		pieces[i++].Initiate(this, m_chessboard.GetClearSquare(idPlayer == 0), idPlayer, Piece.TypePiece.Castle, new StraightLinesMove());
		pieces[i++].Initiate(this, m_chessboard.GetClearSquare(idPlayer == 0), idPlayer, Piece.TypePiece.Knight, new KnightMove());
		pieces[i++].Initiate(this, m_chessboard.GetClearSquare(idPlayer == 0), idPlayer, Piece.TypePiece.Bishop, new DiagonalMove());
		pieces[i++].Initiate(this, m_chessboard.GetClearSquare(idPlayer == 0), idPlayer, Piece.TypePiece.King, new OneMove());
		pieces[i++].Initiate(this, m_chessboard.GetClearSquare(idPlayer == 0), idPlayer, Piece.TypePiece.Queen, new QueenMove());
		pieces[i++].Initiate(this, m_chessboard.GetClearSquare(idPlayer == 0), idPlayer, Piece.TypePiece.Bishop, new DiagonalMove());
		pieces[i++].Initiate(this, m_chessboard.GetClearSquare(idPlayer == 0), idPlayer, Piece.TypePiece.Knight, new KnightMove());
		pieces[i++].Initiate(this, m_chessboard.GetClearSquare(idPlayer == 0), idPlayer, Piece.TypePiece.Castle, new StraightLinesMove());

		for (; i < pieces.Count; i++)
		{
			pieces[i].Initiate(this, m_chessboard.GetClearSquare(idPlayer == 0), idPlayer, Piece.TypePiece.Pawn, new ForwardMove());
		}
	}
}
