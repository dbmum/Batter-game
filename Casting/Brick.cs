using cse210_batter_csharp.Casting;
using System;

namespace cse210_batter_csharp
{
    ///<summary>Define the bricks that are on the top to be broken</summary>

    public class Brick : Actor
    {
        public Brick(int x, int y)
        {
            Point position = new Point(x, y);
            Point velocity = new Point(0, 0);

            SetImage(Constants.IMAGE_BRICK);
            SetWidth(Constants.BRICK_WIDTH);
            SetHeight(Constants.BRICK_HEIGHT);
            SetPosition(position);
            SetVelocity(velocity);
        }
    }
}