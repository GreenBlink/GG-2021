using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chessboard : MonoBehaviour
{
    [SerializeField] private List<ChessboardSquare> m_chessboardSquares;

    public void Initiate(PiecesManager piecesManager)
    {
        for (int i = 0; i < m_chessboardSquares.Count; i++)
        {
            m_chessboardSquares[i].Initiate(piecesManager);
        }
    }

    public ChessboardSquare GetClearSquare(bool isLeft)
    {
        if (isLeft)
        {
            for (int i = 0; i < m_chessboardSquares.Count; i++)
            {
                if (m_chessboardSquares[i].IsClear())
                {
                    return m_chessboardSquares[i];
                }
            }
        }
        else
        {
            for (int i =  m_chessboardSquares.Count - 1; i >= 0; i--)
            {
                if (m_chessboardSquares[i].IsClear())
                {
                    return m_chessboardSquares[i];
                }
            }
        }

        return null;
    }

    public void LightAcceptSquares(ChessboardSquare startSquare, int[]  massMove, int maxCountSquare)
    {
        GetAcceptSquares(startSquare, massMove, maxCountSquare).ForEach(x => x.Light());
    }
    
    public List<ChessboardSquare> GetAcceptSquares(ChessboardSquare startSquare, int[]  massMove, int maxCountSquare)
    {
        List<ChessboardSquare> acceptSquares = new List<ChessboardSquare>();
        int indexStartSquare = m_chessboardSquares.IndexOf(startSquare);
        int indexCheck = indexStartSquare;
        int countSquare = 0;
        int i = 0;

        while (massMove[i] != 0)
        {
            indexCheck = indexStartSquare + massMove[i];
            countSquare = 0;
            
            while (indexCheck < m_chessboardSquares.Count && indexCheck > 0 && !IsMaxCountSquares(countSquare, maxCountSquare))
            {
                if (indexCheck % 8 == 0 && massMove[i] < 0)
                {
                    break;
                }
                
                if (m_chessboardSquares[indexCheck].IsClear())
                {
                    acceptSquares.Add(m_chessboardSquares[indexCheck]);
                    countSquare++;
                }
                else
                {
                    break;
                }

                if (indexCheck + 1 % 8 == 0)
                {
                    break;
                }
                
                indexCheck += massMove[i];
            }

            i++;
        }

        return acceptSquares;
    }

    private bool IsMaxCountSquares(int countSquare, int maxCountSquare)
    {
        return maxCountSquare > -1 && maxCountSquare <= countSquare;
    }
}
