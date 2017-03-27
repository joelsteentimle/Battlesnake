using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace WcfServer
{
    public enum BoardCellType
    {
        Empty = 0,
        Food = 1,
        Me = 2,
        Enemy = 3,
        Dead = 4,
        Wall = 5
    }

    public class Board
    {
        private readonly BoardCellType[,] grid;

        public int MyHeadX { get; private set; }
        public int MyHeadY { get; private set; }

        public BoardCellType GetCellType(int x, int y)
        {
            return grid[x, y];
        }

        public Board(string myId, int width, int height, int[][] food, Snake[] deadSnakes, Snake[] livingSnakes)
        {
            var boardWidth = width + 2;
            var boardHeight = height + 2;

            grid = new BoardCellType[boardWidth, boardHeight]; // Make room for walls

            SetPointArray(food, BoardCellType.Food);

            for (int x = 0; x < boardWidth; x++)
            {
                grid[x, 0] = BoardCellType.Wall;
                grid[x, boardHeight - 1] = BoardCellType.Wall;
            }

            for (int y = 0; y < boardHeight; y++)
            {
                grid[0, y] = BoardCellType.Wall;
                grid[boardWidth - 1, y] = BoardCellType.Wall;
            }

            if (deadSnakes != null)
            {
                foreach (var deadSnake in deadSnakes)
                {
                    SetPointArray(deadSnake.coords, BoardCellType.Dead);
                }
            }

            foreach (var snake in livingSnakes)
            {
                
                var coords = snake.coords;

                BoardCellType boardCellType;
                if (snake.id == myId)
                {
                    boardCellType = BoardCellType.Me;
                    MyHeadX = coords[0][0] + 1;
                    MyHeadY = coords[0][1] + 1;
                }
                else
                { boardCellType = BoardCellType.Enemy; }

                SetPointArray(coords, boardCellType);
            }
        }

        private void SetPointArray(int[][] points, BoardCellType boardCellType)
        {
            if (points == null)
            {
                return;
            }
            
            foreach (var point in points)
            {
                SetBoardPositionType(point, boardCellType);
            }
        }

        private void SetBoardPositionType(int[] foodPosition, BoardCellType cellType)
        {
            var posX = foodPosition[0] + 1;
            var posY = foodPosition[1] + 1;

            grid[posX, posY] = cellType;
        }
    }

    [ServiceContract]
    public interface ISnakeService
    {
        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/start")]
        StartResponse Start(string width, string height, string game_id);

        [OperationContract]
        [WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
                   BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/")]
        string Ping();

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/move")]
        MoveResponse Move(int[][] food, string game_id, int height, int width, Snake[] snakes, Snake[] dead_snake, int turn,
            string you);
    }

    public class SnakeService : ISnakeService
    {
        private int currentDirection = 0;

        private string[] direction = {"up", "right", "down", "left"};
        private int[][] directionOffset = { new[] { 0, -1 }, new[] { 1, 0 }, new[] { 0, 1 }, new[] { -1, 0 } };

        public string CurrentGameId { get; private set; }
        public string GameWidth { get; private set; }
        public string GameHeight { get; private set; }

        public string Ping()
        {
            return "Pong";
        }

        public StartResponse Start(string width, string height, string game_id)
        {
            CurrentGameId = game_id;
            GameWidth = width;
            GameHeight = height;

            string color = "#FF0000";
            string secondary_color = "#00FF00";
            string head_url = "http://placecage.com/c/100/100";
            string name = "lbsa71 Snake";
            string taunt = "OH GOD NOT THE BEES";
            string head_type = "pixel";
            string tail_type = "pixel";

            return new StartResponse(color, secondary_color, head_url, name, taunt, head_type, tail_type);
        }

        public MoveResponse Move(int[][] food, string game_id, int height, int width, Snake[] snakes, Snake[] dead_snake, int turn, string you)
        {
            var board = new Board(you, width, height, food, dead_snake, snakes);

            string move = OnMove(board);

            string taunt = "Oi!";

            return new MoveResponse(move, taunt);
        }

        private string OnMove(Board board)
        {
            int potentialDirection = 0;

            for (var i = 0; i < 4; i++)
            {
                

                potentialDirection = ( i + currentDirection + 1 ) % 4;

                var potentialHeadX = board.MyHeadX + directionOffset[potentialDirection][0];
                var potentialHeadY = board.MyHeadY + directionOffset[potentialDirection][1];

                var potentialBoardType = board.GetCellType(potentialHeadX, potentialHeadY);

                if (potentialBoardType == BoardCellType.Empty || potentialBoardType == BoardCellType.Food)
                {
                    break;
                }
            }

            return direction[potentialDirection];
        }
    }

    [DataContract]
    public class MoveResponse
    {
        [DataMember]
        public string move { get; set; }

        [DataMember]
        public string taunt { get; set; }

        public MoveResponse(string move, string taunt)
        {
            this.move = move;
            this.taunt = taunt;
        }
    }

    [DataContract]
    public class StartResponse
    {
        [DataMember]
        public string color { get; set; }

        [DataMember]
        public string secondary_color { get; set; }

        [DataMember]
        public string head_url { get; set; }

        [DataMember]
        public string name { get; set; }

        [DataMember]
        public string taunt { get; set; }

        [DataMember]
        public string head_type { get; set; }

        [DataMember]
        public string tail_type { get; set; }

        public StartResponse(string color, string secondaryColor, string headUrl, string name, string taunt, string headType, string tailType)
        {
            this.color = color;
            secondary_color = secondaryColor;
            head_url = headUrl;
            this.name = name;
            this.taunt = taunt;
            head_type = headType;
            tail_type = tailType;
        }
    }

    [DataContract]
    public class Snake
    {
        [DataMember]
        public int[][] coords { get; set; }

        [DataMember]
        public int health_points { get; set; }

        [DataMember]
        public string id { get; set; }

        [DataMember]
        public string name { get; set; }

        [DataMember]
        public string taunt { get; set; }
    }
}
