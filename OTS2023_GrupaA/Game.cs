using OTS2026_GrupaA.Exceptions;
using OTS2026_GrupaA.Models;


namespace OTS2026_GrupaA
{
    public enum Move
    {
        Up,
        Down,
        Left,
        Right,
        Front,
        Back
    }

    public enum Score
    {
        Bad,
        Average,
        Good
    }

    public class Game
    {
        public Player Player { get; set; }
        public Space Map { get; set; }


        public Game(Position playerPosition, Position revealHiddenItemPosition)
        {
            Map = new Space();
            Map.InitializeMap();

            if (!ValidatePositionInsideMap(playerPosition) || !ValidatePositionInsideMap(revealHiddenItemPosition))
            {
                throw new PositionOutsideOfMapException("Positions must be valid!");
            }

            int itemX = revealHiddenItemPosition.X;
            int itemY = revealHiddenItemPosition.Y;
            int itemZ = revealHiddenItemPosition.Z;

            Map.Tiles[itemX, itemY, itemZ].Content = TileContent.RevealHiddenItem;
            Player = new Player(playerPosition);
        }

        public void MovePlayer(Move move)
        {
            Position playerPositionAfterMove = Player.GetPositionAfterMove(move);
            bool positionIsValid = ValidatePosition(playerPositionAfterMove);
            if (positionIsValid)
            {
                Player.MakeMove(move);
            }
        }

        public bool ValidatePosition(Position position)
        {
            int x = position.X;
            int y = position.Y;
            int z = position.Z; 

            if (!ValidatePositionInsideMap(position))
            {
                return false;
            }
            if(Map.Tiles[x, y, z].Type.Equals(TileType.Hidden))
            {
                return Player.CanRevealHidden;
            }
            else
            {
                return true;
            }
        }

        private bool ValidatePositionInsideMap(Position position)
        {
            int x = position.X;
            int y = position.Y;
            int z = position.Z;

            if (x < 0 || x >= Space.MapSize || y < 0 || y >= Space.MapSize || z < 0 || z >= Space.MapSize)
            {
                return false;
            }
            if (Map.Tiles[x, y, z].Type.Equals(TileType.MapBarrier))
            {
                return false;
            }
            return true;
        }

        public void CollectItems()
        {
            int x = Player.Position.X;
            int y = Player.Position.Y;
            int z = Player.Position.Z;

            if (Map.Tiles[x, y, z].Content.Equals(TileContent.Gold))
            {
                if (Map.Tiles[x, y, z].Type.Equals(TileType.Hidden))
                    Player.AmountOfHiddenGold++;
                else
                    Player.AmountOfGold++;
            }
            else if(Map.Tiles[x, y, z].Content.Equals(TileContent.RevealHiddenItem))
            {
                Player.CanRevealHidden = true;
            }

            Map.EmptyTileOnPosition(Player.Position);
        }


        public Score CalculateScore()
        {
            if(Player.AmountOfHiddenGold > 10)
            {
                return Score.Good;
            }
            if(Player.AmountOfGold > 15 && Player.CanRevealHidden)
            {
                if (Player.AmountOfHiddenGold < 5)
                {
                    return Score.Average;
                }
                else
                {
                    return Score.Good;
                }
            }
            return Score.Bad;
        }

    }
}
