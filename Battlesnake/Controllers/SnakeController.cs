using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BattleSnake.Core;

namespace Battlesnake.Controllers
{

    public class SnakeController : ApiController
    {
        [Route("start")]
        [HttpPost]
        public SnakeSettings Start(GameSettings game)
        {
            return Snake.Create(game);
        }

        [Route("")]
        [HttpGet]
        public string Ping()
        {
            return "Pong!";
        }

        [Route("move")]
        [HttpPost]
        public SnakeAction Move(GameStatus state)
        {
            var response = new SnakeAction();
            var snake = state.Snakes.FirstOrDefault(s => s.Id == state.You);
            if (snake != null)
            {
                response.Move = snake.Update(state);
            }
            return response;
        }
    }
}
