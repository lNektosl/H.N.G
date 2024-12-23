public class PathNode {
    public MyTile tile;
    public float GCost { get; set; }
    public float HCost { get; set; }
    public float FCost => GCost + HCost;
    public PathNode Parent { get; set; }

    public PathNode(MyTile tile) {
        this.tile = tile;
        GCost = float.MaxValue;
        HCost = float.MaxValue;
    }

    public override bool Equals(object obj) {
        return obj is PathNode other && tile.Equals(other.tile);
    }

    public override int GetHashCode() {
        return tile.GetHashCode();
    }
}
