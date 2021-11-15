using System;
using cse210_batter_csharp.Services;
using cse210_batter_csharp.Casting;
using cse210_batter_csharp.Scripting;
using System.Collections.Generic;

namespace cse210_batter_csharp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create the cast
            Dictionary<string, List<Actor>> cast = new Dictionary<string, List<Actor>>();

            // The paddle
            cast["paddle"] = new List<Actor>();

            Paddle paddle = new Paddle();
            cast["paddle"].Add(paddle);
            
            // Bricks
            cast["bricks"] = new List<Actor>();

            for (int x = Constants.BRICK_SPACE; 
                x <= (Constants.MAX_X - Constants.BRICK_SPACE); 
                x+= (Constants.BRICK_WIDTH + Constants.BRICK_SPACE))
            {
                for (int y = Constants.BRICK_SPACE;
                y <= ((Constants.ROWS_OF_BRICKS - 1) * (Constants.BRICK_HEIGHT + (Constants.BRICK_SPACE * 2)));
                y+= (Constants.BRICK_HEIGHT + Constants.BRICK_SPACE))
                {
                    Brick brick = new Brick(x,y);
                    cast["bricks"].Add(brick);
                }
            }

            // The Ball (or balls if desired)
            cast["balls"] = new List<Actor>();

            Ball ball1 = new Ball(Constants.BALL_DX, Constants.BALL_DY, 600);
            cast["balls"].Add(ball1);

            Ball ball2 = new Ball(Constants.BALL_DX - 3, Constants.BALL_DY + 3, 500);
            cast["balls"].Add(ball2);

            cast["scoreboard"] = new List<Actor>();

            Scoreboard scoreboard = new Scoreboard();
            cast["scoreboard"].Add(scoreboard);

            

            // Create the script
            Dictionary<string, List<Action>> script = new Dictionary<string, List<Action>>();

            OutputService outputService = new OutputService();
            InputService inputService = new InputService();
            PhysicsService physicsService = new PhysicsService();
            AudioService audioService = new AudioService();

            script["output"] = new List<Action>();
            script["input"] = new List<Action>();
            script["update"] = new List<Action>();

            DrawActorsAction drawActorsAction = new DrawActorsAction(outputService);
            script["output"].Add(drawActorsAction);

            ControlActorsAction controlActorsAction = new ControlActorsAction(inputService);
            script["update"].Add(controlActorsAction);

            HandleCollisionsAction handleCollisionsAction = new HandleCollisionsAction(audioService, physicsService, scoreboard);
            script["update"].Add(handleCollisionsAction);

            MoveActorsAction moveActorsAction = new MoveActorsAction();
            script["update"].Add(moveActorsAction);

            // TODO: Add additional actions here to handle the input, move the actors, handle collisions, etc.

            // Start up the game
            outputService.OpenWindow(Constants.MAX_X, Constants.MAX_Y, "Batter", Constants.FRAME_RATE);
            audioService.StartAudio();
            audioService.PlaySound(Constants.SOUND_START);

            Director theDirector = new Director(cast, script, scoreboard);
            theDirector.Direct();

            audioService.StopAudio();
        }
    }
}
