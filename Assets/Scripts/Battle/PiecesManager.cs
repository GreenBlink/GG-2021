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
	[SerializeField] private SoundControl sControl;

	private List<Piece> m_piecesAll = new List<Piece>();
	private List<Piece> m_piecesKillWhite = new List<Piece>();
	private List<Piece> m_piecesKillBlack = new List<Piece>();
	private AIController m_aiController;
	private Piece m_choosePieces;
	private int m_idCurrentPlayer;

	public void Initiate(AIController aiController)
	{
		m_idCurrentPlayer = 0;
		m_aiController = aiController;
		m_chessboard.Initiate(this);
		InitiatePieces(m_piecesWhite, 0);
		InitiatePieces(m_piecesBlack, 1);
		
		m_piecesAll.Clear();
		m_piecesAll.AddRange(m_piecesWhite);
		m_piecesAll.AddRange(m_piecesBlack);
	}

	public void ChangePlayer()
	{
		m_idCurrentPlayer = m_idCurrentPlayer == 0 ? m_aiController.m_isActive ? 2 : 1 : 0;
		m_changePlayerEvent.Invoke();
	}

	public void SetPlayerAI()
	{
		if (m_idCurrentPlayer != 0)
		{
			m_idCurrentPlayer = m_aiController.m_isActive ? 2 : 1;
		}
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

	public int GetIsKillList()
	{
		if (IsDeathKillList(m_piecesKillWhite))
		{
			return 1;
		}

		if (IsDeathKillList(m_piecesKillBlack))
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
					sControl.playClick();
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
					sControl.playMove();
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
					sControl.playClick();
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
				sControl.playMove();
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
		sControl.playDeath();
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
			for (int i = 0; i < m_piecesAll.Count; i++)
			{
				m_piecesAll[i].SetMoveRule(moveRule);
			}
		}
		else
		{
			for (int i = 0; i < m_piecesAll.Count; i++)
			{
				if (m_piecesAll[i].GetRule() == typePiece)
				{
					m_piecesAll[i].SetMoveRule(moveRule);
				}
			}
		}
	}
	
	public void BuildListKills(Piece.TypePiece typePiece)
	{
		BuildListKill(m_piecesKillWhite,m_piecesWhite, typePiece);
		BuildListKill(m_piecesKillBlack,m_piecesBlack, typePiece);
	} 
	
	public int GetIdPlayer() => m_idCurrentPlayer;
	public Chessboard GetChessboard() => m_chessboard;
	public bool IsChoosePiece() => m_choosePieces != null;
	public List<Piece> GetPieceListAI() => m_piecesBlack;

	private void BuildListKill(List<Piece> pieces, List<Piece> piecesTarget, Piece.TypePiece typePiece)
	{
		pieces.Clear();
		
		for (int i = 0; i < piecesTarget.Count; i++)
		{
			if (piecesTarget[i].GetRule() == typePiece && piecesTarget[i].gameObject.activeSelf)
			{
				pieces.Add(piecesTarget[i]);
			}
		}
	}

	private bool IsDeathKillList(List<Piece> pieces)
	{
		return pieces.Find(x => !x.gameObject.activeSelf) != null;
	}

	private bool IsDeathList(List<Piece> pieces)
	{
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
