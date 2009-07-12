class SquareCoordinate
{
  public int x;

  public int y;

  public SquareCoordinate(int x, int y) {
    this.x = x;
    this.y = y;
  }

  public boolean equals(SquareCoordinate squareCoordinate) {
    if (squareCoordinate == null)
      return false;
    return this.x == squareCoordinate.x && this.y == squareCoordinate.y;
  }
}
