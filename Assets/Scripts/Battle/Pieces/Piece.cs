using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Piece : MonoBehaviour
{
	public enum TypePiece
	{
		Pawn,
		Bishop,
		Castle,
		Knight,
		Queen,
		King
	}

	[SerializeField] private List<Sprite> m_spritePiecesWhite;
	[SerializeField] private List<Sprite> m_spritePiecesBlack;
	[SerializeField] private SpriteRenderer m_ico;
	[SerializeField] private SpriteRenderer m_frame;
	[SerializeField] private Transform m_graphics;

	protected TypePiece m_typePiece;
	private PiecesManager m_piecesManager;
	private ChessboardSquare m_currentSquare;
	private IMoveRule m_moveRule;
	private int m_idPlayer;

	public virtual void Initiate(PiecesManager piecesManager, ChessboardSquare startSquare, int idPlayer, TypePiece typePiece, IMoveRule moveRule)
	{
		m_piecesManager = piecesManager;
		transform.position = startSquare.transform.position;

		InitiateType(typePiece, idPlayer);
		SetMoveRule(moveRule);
		CancelChoose();
		SetSquare(startSquare);
	}

	public void InitiateType(TypePiece typePiece, int idPlayer)
	{
		m_idPlayer = idPlayer;
		m_typePiece = typePiece;

		SetSprites(idPlayer, typePiece);
	}

	public void SetMoveRule(IMoveRule moveRule)
	{
		m_moveRule = moveRule;
	}

	public void Choose()
	{
		m_frame.DOFade(1, 0.5f);
		m_graphics.DOLocalMoveY(0.3f, 0.5f);
		
		m_piecesManager.GetChessboard().LightAcceptSquares(m_currentSquare, m_moveRule.GetMassMove(), m_moveRule.GetMaxCountSquare());
	}

	public void BlinkSquare()
	{
		m_currentSquare.Blink();
	}
	
	public void CancelChoose(bool isFast = false)
	{
		m_frame.DOFade(0, isFast ? 0 : 0.5f);
		m_graphics.DOLocalMoveY(0, 0.5f);
	}

	public bool Move(ChessboardSquare targetSquare)
	{
		if (m_moveRule.Move(transform, m_piecesManager.GetChessboard(), m_currentSquare, targetSquare))
		{
			SetSquare(targetSquare);
			return true;
		}
		
		return false;
	}

	public void Destroy()
	{
		Destroy(gameObject);
	}

	public int GetIdPlayer() => m_idPlayer;

	private void SetSprites(int idPlayer, TypePiece typePiece)
	{
		Sprite sprite = idPlayer == 0 ? m_spritePiecesWhite[(int)typePiece] : m_spritePiecesBlack[(int)typePiece];

		m_ico.sprite = sprite;
		m_frame.sprite = sprite;
	}

	private void SetSquare(ChessboardSquare targetSquare)
	{
		ClearCurrentSquare();
		
		targetSquare.SetPiece(this);
		m_currentSquare = targetSquare;
	}

	private void ClearCurrentSquare()
	{
		if (m_currentSquare != null)
		{
			m_currentSquare.Clear();
			m_currentSquare = null;
		}
	}
}
