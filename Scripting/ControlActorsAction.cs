using System.Collections.Generic;
using cse210_batter_csharp.Casting;
using cse210_batter_csharp.Services;

namespace cse210_batter_csharp 
{
    ///<summary>An action to interpret inputs into moving the paddle</summary>
    public class ControlActorsAction : Action
    {
        InputService _inputService;
        public ControlActorsAction(InputService inputService)
        {
            _inputService = inputService;
        }

        ///<summary>Use input to set paddle velocity, and handle its wall collision</summary>
        public override void Execute(Dictionary<string, List<Actor>> cast)
        {
            Point direction = _inputService.GetDirection();
            
            Actor paddle = cast["paddle"][0];

            if (_inputService.IsLeftPressed() || _inputService.IsRightPressed())
            {
                Point velocity = direction.Scale(Constants.PADDLE_SPEED);
                paddle.SetVelocity(velocity);

                //handle left wall colision
                if (paddle.GetX() <= Constants.PADDLE_SPEED ) 
                {
                    StopActor(paddle);
                    int x = paddle.GetX();

                    Point newPosition = new Point(Constants.PADDLE_SPEED + 1, Constants.PADDLE_Y);
                    paddle.SetPosition(newPosition);
                }

                // Handle right wall colision
                if (paddle.GetX() >= (Constants.MAX_X - Constants.PADDLE_WIDTH))
                {
                    StopActor(paddle);
                    int x = paddle.GetX();

                    Point newPosition = new Point((Constants.MAX_X - Constants.PADDLE_WIDTH - 1), Constants.PADDLE_Y);
                    paddle.SetPosition(newPosition);
                }
            }
            else 
            {
                StopActor(paddle);
            }    
        }

        ///<summary>Return actor velocity to 0 so that it does not move continuously</summary>
        ///<param name="actor actor">
        private void StopActor(Actor actor)
        {
            Point velocity = new Point(0,0);
            actor.SetVelocity(velocity);
        }
    }
}