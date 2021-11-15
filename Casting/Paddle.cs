using cse210_batter_csharp.Casting;
using System;

namespace cse210_batter_csharp
{
    ///<summary>Define the Paddle that the player moves</summary>
    
    public class Paddle : Actor
    {
        public Paddle()
        {
            Point position = new Point(Constants.PADDLE_X, Constants.PADDLE_Y);
            Point velocity = new Point(0, 0);

            SetImage(Constants.IMAGE_PADDLE);
            SetWidth(Constants.PADDLE_WIDTH);
            SetHeight(Constants.PADDLE_HEIGHT);
            SetPosition(position);
            SetVelocity(velocity);
        }
    }
}