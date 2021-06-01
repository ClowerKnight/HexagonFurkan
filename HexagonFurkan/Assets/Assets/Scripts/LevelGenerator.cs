using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utility;
using Random = UnityEngine.Random;

namespace Hexagon
{

    #region FillBoard-GetExcludedHextypes-GetRandomIndex
    public class LevelGenerator : MonoBehaviour
    {
        public void FillBoard()
        {
            HexPositionCalculator positionCalculator = GameManager.instance.positionCalculator;

            // Cift sutunları doldurma
            for (int r = 0; r < positionCalculator.boardSize.y * 2; r+=2)
            {
                for (int c = 0; c < positionCalculator.boardSize.x; c += 2)
                {
                    GameManager.instance.CreateHexGameObject(new Vector2Int(c, r));
                }
            }
            // Sutunlari oranli doldurma.
            for (int r = 1; r < positionCalculator.boardSize.y * 2; r+=2)
            {
                for (int c = 1; c < positionCalculator.boardSize.x; c += 2)
                {
                    Hex hex = GameManager.instance.CreateHexGameObject( new Vector2Int(c, r));
                    int randomIndex = GetRandomIndex(GetExcludedHextypes(hex));
                    hex.Initialize(new Vector2Int(c,r),  randomIndex);
                }
            }
        }

        public HashSet<int> GetExcludedHextypes(Hex hex)
        {
            HexagonEdges[] checkMatchingTriangle = new[]
            {
                HexagonEdges.BottomLeft, HexagonEdges.TopLeft, HexagonEdges.Top, HexagonEdges.TopRight
            };
            HashSet<int> excluding = new HashSet<int>();

            // Tek sayıdaki sütunlarda ayni renkte 2 - Komşu olup olmadigini kontrol edin.
            //Eger oyleyse, o anda olusturulan Hex'te rastgele sayi oluşturmada o rengi dahil etmeyin.

            foreach (var edge in checkMatchingTriangle)
            {
                // If has those neighbors
                if (hex.HasNeighborHex(edge) && hex.HasNeighborHex(edge.Next()))
                {
                    // If those neighbors are the same type
                    Hex neighbor1 = hex.GetNeighbor(edge),
                        neighbor2 = hex.GetNeighbor(edge.Next());
                    if (neighbor1.hexType == neighbor2.hexType)
                    {
                        excluding.Add(neighbor1.hexType);
                    }
                }
            }

            return excluding;
        }
        
        int GetRandomIndex(HashSet<int> exclude)
        {
            var range = Enumerable.Range(0, GameManager.instance.hexSprites.Count).Where(i => !exclude.Contains(i));
            int index = Random.Range(0, GameManager.instance.hexSprites.Count - exclude.Count);
            return range.ElementAt(index);
        }
    }
    #endregion
}