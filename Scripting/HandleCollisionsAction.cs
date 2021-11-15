using System.Collections.Generic;
using cse210_batter_csharp.Casting;
using cse210_batter_csharp.Services;

namespace cse210_batter_csharp
{
    /// <summary>Handle the collisions of the ball with the paddle and bricks</summary>
    public class HandleCollisionsAction : Action
    {
        AudioService _audioService;
        PhysicsService _physicsService;
        Scoreboard _scoreboard;
        Point _ballVelocity;
        public HandleCollisionsAction(AudioService audioService, PhysicsService physicsService, Scoreboard scoreboard)
        {
            _audioService = audioService;
            _physicsService = physicsService;
            _scoreboard = scoreboard;
            
        }
        /// <summary>Check for ball collisions and remove bricks, dead balls and bounce off of walls</summary>
        ///<param name="cast">

        public override void Execute(Dictionary<string, List<Actor>> cast)
        {
            List<Actor> bricks = cast["bricks"];
            List<Actor> balls = cast["balls"];
            Actor paddle = cast["paddle"][0];
            List<Brick> bricksToRemove = new List<Brick>(); 
            List<Ball> ballsToRemove = new List<Ball>();

            foreach (Ball ball in balls)
            {
                WallBounce(ball);

                if (BallIsGone(ball))
                {
                    ballsToRemove.Add(ball);
                }

                foreach (Brick brick in bricks)
                {
                    if (_physicsService.IsCollision(ball, brick))
                    {
                        _audioService.PlaySound(Constants.SOUND_BOUNCE);
                        bricksToRemove.Add(brick);

                        int brickX = brick.GetX();
                        int brickY = brick.GetY();
                        Point velocity = ball.GetVelocity();
                        
                        for (int i = 0; i < Constants.BRICK_HEIGHT / 2; i++)
                        {
                            if (ball.GetTopEdge() == brickY + Constants.BRICK_HEIGHT - i
                                || ball.GetBottomEdge() == brickY + i)
                            {
                                velocity = HorizontalBounce(velocity);
                                break;
                            }

                            else if (ball.GetLeftEdge() == brickX + Constants.BRICK_WIDTH - i
                                || ball.GetRightEdge() == brickX + i)
                            {
                                velocity = VerticalBounce(velocity);
                                break;
                            }
                        }
                        ball.SetVelocity(velocity);   
                    }
                }
                if (_physicsService.IsCollision(paddle, ball))
                {
                    Point velocity = HorizontalBounce(ball.GetVelocity());
                    ball.SetVelocity(velocity);
                    _audioService.PlaySound(Constants.SOUND_BOUNCE);
                }
            }

            foreach (Brick reomve in bricksToRemove)
            {
                cast["bricks"].Remove(reomve);
            }

            foreach (Ball ballx in ballsToRemove)
            {
                cast["balls"].Remove(ballx);
                Ball ball = new Ball(_ballVelocity.GetX(), -_ballVelocity.GetY(), Constants.BALL_X);
                cast["balls"].Add(ball);
                _audioService.PlaySound(Constants.SOUND_OVER);
                _scoreboard.UpdateLives();
            }
        }

        /// <summary>Bounce the ball continuously off of the walls</summary>
        ///<param name="actor actor">
        private void WallBounce(Actor actor)
        {
            Point velocity = actor.GetVelocity();
            int dx = velocity.GetX();
            int dy = velocity.GetY();

            Point position = actor.GetPosition();
            int x = position.GetX();
            int y = position.GetY();


            if (x >= Constants.MAX_X - Constants.BALL_WIDTH || x <= Constants.BALL_WIDTH)
            {
                velocity = VerticalBounce(velocity);
            }

            if (y <= Constants.BALL_HEIGHT)
            {
                velocity = HorizontalBounce(velocity);
            }
            
            Point newVelocity = velocity;

            actor.SetVelocity(newVelocity);
        }

        /// <summary>Check to see if the ball has gone out of the bottom, 
        ///get velocity of ball for new ball creation</summary>
        ///<param name="actor ball">
        ///<returns></returns>
        private bool BallIsGone(Actor ball)
        {
            if (ball.GetY() >= Constants.MAX_Y - Constants.BALL_HEIGHT)
            {
                _ballVelocity = ball.GetVelocity();
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>Simulate a vertical bounce</summary>
        ///<param name="Point velocity">
        ///<returns>Point Velocity</returns> 
        private Point VerticalBounce(Point velocity)
        {
            int dx = velocity.GetX();
            int dy = velocity.GetY();

            dx = -dx;

            Point newVelocity = new Point(dx,dy);
            return newVelocity;
        }

        /// <summary>Simulate a horizontal bounce</summary>
        ///<param name="Point velocity"> 
        ///<returns>Point Velocity</returns>
        private Point HorizontalBounce(Point velocity)
        {
            int dx = velocity.GetX();
            int dy = velocity.GetY();

            dy = -dy;

            Point newVelocity = new Point(dx,dy);
            return newVelocity;
        }
    }
}