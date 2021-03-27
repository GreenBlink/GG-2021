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
    
    public void LightAcceptSquares(ChessboardSquare startSquare, int[]  massMove, int maxCountSquare, int idPlayer)
    {
        GetAcceptSquares(startSquare, massMove, maxCountSquare, idPlayer).ForEach(x => x.Light(ChessboardSquare.TypeLight.Path));
    }
    
    public void HideSquares()
    {
        m_chessboardSquares.ForEach(x => x.Hide(true));
    }
    
    public List<ChessboardSquare> GetAcceptSquares(ChessboardSquare startSquare, int[]  massMove, int maxCountSquare, int idPlayer)
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
                if (massMove[i] == 7 || massMove[i] == -7 || massMove[i] == 15 || massMove[i] == -15)
                {
                    if ((indexCheck) % 8 == 0 && massMove[i] < 0)
                    {
                        break;
                    }
                    
                    if ((indexCheck + 1) % 8 == 0 && massMove[i] > 0)
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
                        if (!m_chessboardSquares[indexCheck].IsThisPlayer(idPlayer))
                        {
                            acceptSquares.Add(m_chessboardSquares[indexCheck]);
                            countSquare++;
                        }
                        
                        break;
                    }

                    if ((indexCheck + 1) % 8 == 0 || indexCheck % 8 == 0)
                    {
                        break;
                    }
                }
                else if (massMove[i] == 9 || massMove[i] == -9 || massMove[i] == 1 || massMove[i] == -1 || massMove[i] == 17 || massMove[i] == -17)
                {
                    if ((indexCheck) % 8 == 0 && massMove[i] > 0)
                    {
                        break;
                    }
                    
                    if ((indexCheck + 1) % 8 == 0 && massMove[i] < 0)
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
                        if (!m_chessboardSquares[indexCheck].IsThisPlayer(idPlayer))
                        {
                            acceptSquares.Add(m_chessboardSquares[indexCheck]);
                            countSquare++;
                        }
                        
                        break;
                    }

                    if ((indexCheck + 1) % 8 == 0 || indexCheck % 8 == 0)
                    {
                        break;
                    }
                }
                else if (massMove[i] == 8 || massMove[i] == -8)
                {
                    if (m_chessboardSquares[indexCheck].IsClear())
                    {
                        acceptSquares.Add(m_chessboardSquares[indexCheck]);
                        countSquare++;
                    }
                    else
                    {
                        if (!m_chessboardSquares[indexCheck].IsThisPlayer(idPlayer))
                        {
                            acceptSquares.Add(m_chessboardSquares[indexCheck]);
                            countSquare++;
                        }
                        
                        break;
                    }
                }
                else if (massMove[i] == 10 || massMove[i] == -10)
                {
                    if (((indexCheck) % 8 == 0 || (indexCheck - 1) % 8 == 0) && massMove[i] > 0)
                    {
                        break;
                    }
                    
                    if (((indexCheck + 1) % 8 == 0 || (indexCheck) % 8 == 0) && massMove[i] < 0)
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
                        if (!m_chessboardSquares[indexCheck].IsThisPlayer(idPlayer))
                        {
                            acceptSquares.Add(m_chessboardSquares[indexCheck]);
                            countSquare++;
                        }
                        
                        break;
                    }
                }
                else if (massMove[i] == 6 || massMove[i] == -6)
                {
                    if (((indexCheck) % 8 == 0 || (indexCheck - 1) % 8 == 0) && massMove[i] < 0)
                    {
                        break;
                    }
                    
                    if (((indexCheck + 1) % 8 == 0 || (indexCheck) % 8 == 0) && massMove[i] > 0)
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
                        if (!m_chessboardSquares[indexCheck].IsThisPlayer(idPlayer))
                        {
                            acceptSquares.Add(m_chessboardSquares[indexCheck]);
                            countSquare++;
                        }
                        
                        break;
                    }
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
