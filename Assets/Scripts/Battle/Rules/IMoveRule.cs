using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveRule
{
    public bool Move(Transform transform, Chessboard chessboard, ChessboardSquare startSquare, ChessboardSquare targetSquare, int idPlayer);
    public int[] GetMassMove(int idPlayer);
    public int GetMaxCountSquare();
}
