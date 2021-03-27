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

	private List<Piece> m_pieces = new List<Piece>();
	private Piece m_choosePieces;
	private int m_idCurrentPlayer;

	public void Initiate()
	{
		m_idCurrentPlayer = 0;
		m_chessboard.Initiate(this);
		InitiatePieces(m_piecesWhite, 0);
		InitiatePieces(m_piecesBlack, 1);
		
		m_pieces.Clear();
		m_pieces.AddRange(m_piecesWhite);
		m_pieces.AddRange(m_piecesBlack);
	}

	public void ChangePlayer()
	{
		m_idCurrentPlayer = m_idCurrentPlayer == 0 ? 1 : 0;
		m_changePlayerEvent.Invoke();
	}

	public int GetIsKillAll()
	{
		if (IsDeathList(m_piecesWhite))
		{
			return 1;
		}

		if (IsDeathList(m_piecesBlack))
		{
			return 0;
		}

		return -1;
	}
	
	public int GetIsKillKing()
	{
		if (IsDeathList(m_piecesWhite))
		{
			return 1;
		}

		if (IsDeathList(m_piecesBlack))
		{
			return 0;
		}

		return -1;
	}
	
	public int GetIsKillPawn()
	{
		if (IsDeathList(m_piecesWhite))
		{
			return 1;
		}

		if (IsDeathList(m_piecesBlack))
		{
			return 0;
		}

		return -1;
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
						ChangePlayer();
					}
				}
			}
			else
			{
				if (MovePiece(square))
				{
					ChangePlayer();
				}
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
		if (typePiece == Piece.TypePiece.Any)
		{
			for (int i = 0; i < m_pieces.Count; i++)
			{
				m_pieces[i].SetMoveRule(moveRule);
			}
		}
		else
		{
			for (int i = 0; i < m_pieces.Count; i++)
			{
				if (m_pieces[i].GetRule() == typePiece)
				{
					m_pieces[i].SetMoveRule(moveRule);
				}
			}
		}
	}
	
	public int GetIdPlayer() => m_idCurrentPlayer;
	public Chessboard GetChessboard() =>m_chessboard;
	public bool IsChoosePiece() => m_choosePieces != null;

	private bool IsDeathList(List<Piece> pieces)
	{
		Piece piece = pieces.Find(x => x.gameObject.activeSelf);
		return pieces.Find(x => x.gameObject.activeSelf) == null;
	}

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
