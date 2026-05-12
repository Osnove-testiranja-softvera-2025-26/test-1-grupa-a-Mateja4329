

namespace OTS2026_GrupaA.Models
{
    public class Space
    {
        public Tile[,,] Tiles { get; set; }
        public static readonly int MapSize = 30;

        public Space()
        {
            Tiles = new Tile[MapSize, MapSize, MapSize];    
        }

        public void InitializeMap()
        {
            for (int i = 0; i < MapSize; i++)
            {
                for (int j = 0; j < MapSize; j++)
                {
                    for (int k = 0;  k < MapSize; k++)
                    {
                        Tiles[i, j, k] = new Tile();
                    }
                }
            }

            CreateBarrier(5, 5, 0, 5, 20, 10);
            CreateBarrier(20, 5, 0, 5, 20, 10);
        }

        public void CreateBarrier(int upperLeftCornerX, int upperLeftCornerY, int upperLeftCornerZ, int width, int height, int length)
        {
            for(int i=upperLeftCornerX; i< upperLeftCornerX + width; i++)
            {
                for(int j=upperLeftCornerY; j< upperLeftCornerY + height; j++)
                {
                    for (int k = upperLeftCornerZ; k< upperLeftCornerZ + length; k++)
                    {
                        Tiles[i, j, k].Type = TileType.MapBarrier;
                    }
                }
            }
        }

        public void AddTile(TileType type, TileContent content, int x, int y, int z)
        {
            if (!type.Equals(TileType.MapBarrier))
            {
                Tiles[x, y, z].Type = type;
                Tiles[x, y, z].Content = content;
            }
        }

        public void EmptyTileOnPosition(Position position)
        {
            int x = position.X;
            int y = position.Y;
            int z = position.Z;
            Tiles[x, y, z].Content = TileContent.Empty;
        }

    }
}
