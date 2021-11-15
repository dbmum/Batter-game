using cse210_batter_csharp.Casting;
using System;

namespace cse210_batter_csharp
{
    public class Scoreboard : Actor
    {
        private int _lives;
        public Scoreboard()
        {
            _lives = Constants.STARTING_LIVES;

            Point position = new Point(Constants.SCOREBOARD_X, Constants.SCOREBOARD_Y);
            Point velocity = new Point(0, 0);

            SetText($"Lives: {_lives}");
            
            SetWidth(Constants.SCOREBOARD_WIDTH);
            SetHeight(Constants.SCOREBOARD_HEIGHT);
            SetPosition(position);
            SetVelocity(velocity);
        }

        public void UpdateLives()
        {
            _lives -= 1;
            SetText($"Lives: {_lives}");
        }

        public bool IsGameOver()
        {
            if (_lives <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}