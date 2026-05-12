
namespace OTS2026_GrupaA.Models
{

    public enum TileType
    {
        Standard,
        MapBarrier,
        Hidden
    }

    public enum TileContent
    {
        Empty,
        RevealHiddenItem,
        Gold
    }

    public  class Tile
    {
        public TileContent Content { get; set; }
        public TileType Type { get; set; }

        public Tile()
        {
            Content = TileContent.Empty;
            Type = TileType.Standard;
        }

        public Tile(TileContent content)
        {
            Content = content;
        }
    }
}
