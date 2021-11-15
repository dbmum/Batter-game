using System;
using cse210_batter_csharp.Casting;

namespace cse210_batter_csharp
{
    ///<summary>Define the ball that bounces around the screen</summary>
    public class Ball : Actor
    {
        public Ball(int dx, int dy, int x)
        {
            Point position = new Point(x, Constants.BALL_Y);
            Point velocity = new Point(dx, dy);

            SetImage(Constants.IMAGE_BALL);
            SetWidth(Constants.BALL_WIDTH);
            SetHeight(Constants.BALL_HEIGHT);
            SetPosition(position);
            SetVelocity(velocity);
        }
    }
}