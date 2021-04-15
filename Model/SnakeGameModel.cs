using Snake.Persistence;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Snake.Model
{
    public enum Direction { Up, Right, Down, Left }
    public enum GameDifficulty { Easy, Medium, Hard }
    public class SnakeGameModel
    {
        private GameDifficulty _gameDifficulty; //játék nehézség
        private SnakeTable _table;
        private ISnakeDataAccess _dataAccess; // adatelérés
        private Timer _timer;
        private Int32 _elapsedTime;
        private Direction _lastDirection;
        private Boolean _isPaused;

        private Int32 _collectedEggs; //begyűjtött tojások
        private Int32 _skipSteps; //kihagyásra váró lépések
        public Int32 CollectedEggs { get { return _collectedEggs; } set { _collectedEggs = value; } }
        public Int32 SkipSteps { get { return _skipSteps; } set { _skipSteps = value; } }

        private Queue<Tuple<Int32, Int32>> _snakePositions; //kígyó helye
        private Tuple<Int32, Int32> _head;

        private const String GameEasyName = "Levels/level1.snk";
        private const String GameMediumName = "Levels/level2.snk";
        private const String GameHardName = "Levels/level3.snk";
        private const Int32 SpawnTime = 10;
        private const Int32 MoveTime = 2;


        public event EventHandler<SnakeEventArgs> GameAdvanced;
        public event EventHandler<SnakeEventArgs> GameOver;

        public GameDifficulty GameDifficulty { get { return _gameDifficulty; } set { _gameDifficulty = value; } }
        public Boolean IsPaused { get { return _isPaused; } set { _isPaused = value; } }
        public SnakeTable Table { get { return _table; } }
        public Direction LastDirection { get { return _lastDirection; } set { _lastDirection = value; } }

        public async Task LoadGameAsync(String path)
        {
            if (_dataAccess == null)
                throw new InvalidOperationException("No data access is provided.");

            _table = await _dataAccess.LoadAsync(path);

        }
        public void OnTick(Object source, ElapsedEventArgs e)
        {
            _elapsedTime++;
            if (_elapsedTime % SpawnTime == 0)
            {
                SpawnEgg();
            }
            if (_elapsedTime % MoveTime == 0)
            {
                MoveSnake();
            }
        }
        public SnakeGameModel(ISnakeDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
            _gameDifficulty = GameDifficulty.Easy;
        }
        public void ChangeTimers(bool b)
        {
            if (_timer != null)
            {
                _timer.Enabled = b;
            }
            Debug.WriteLine("Timers changed");

        }
        public void MoveSnake()
        {
            if (SkipSteps != 0)
            {
                if (_table.GetNextObject(_head, _lastDirection) == TableObject.Empty)
                {
                    _head = _table.GetNextXY(_head, _lastDirection);
                    _snakePositions.Enqueue(_head);
                    _table.SetValue(_head.Item1, _head.Item2, TableObject.Snake);

                    SkipSteps--;
                    OnGameAdvanced();
                }
                else if (_table.GetNextObject(_head, _lastDirection) == TableObject.Egg)
                {
                    _head = _table.GetNextXY(_head, _lastDirection);
                    _snakePositions.Enqueue(_head);
                    _table.SetValue(_head.Item1, _head.Item2, TableObject.Snake);
                    CollectedEggs++;
                    SkipSteps++;
                    OnGameAdvanced();
                }
                else
                {
                    //GAME OVER;
                    Debug.WriteLine("Game Over");
                    ChangeTimers(false);
                    OnGameOver();
                }

            }
            else
            {
                if (_table.GetNextObject(_head, _lastDirection) == TableObject.Empty)
                {
                    _head = _table.GetNextXY(_head, _lastDirection);
                    Tuple<Int32, Int32> remove = _snakePositions.Dequeue();
                    _table.SetValue(remove.Item1, remove.Item2, TableObject.Empty);
                    _snakePositions.Enqueue(_head);
                    _table.SetValue(_head.Item1, _head.Item2, TableObject.Snake);
                    OnGameAdvanced();
                }
                else if (_table.GetNextObject(_head, _lastDirection) == TableObject.Egg)
                {
                    _head = _table.GetNextXY(_head, _lastDirection);
                    _snakePositions.Enqueue(_head);
                    _table.SetValue(_head.Item1, _head.Item2, TableObject.Snake);
                    CollectedEggs++;
                    OnGameAdvanced();
                }
                else
                {
                    //GAME OVER;
                    Debug.WriteLine("Game Over");
                    ChangeTimers(false);
                    OnGameOver();
                }
            }
            Debug.WriteLine("Snake Moved");

        }
        public async Task NewGame()
        {
            switch (_gameDifficulty)
            {
                case GameDifficulty.Easy:
                    await LoadGameAsync(GameEasyName);
                    break;
                case GameDifficulty.Medium:
                    await LoadGameAsync(GameMediumName);
                    break;
                case GameDifficulty.Hard:
                    await LoadGameAsync(GameHardName);
                    break;
            }
            _elapsedTime = 0;
            SkipSteps = 4;
            CollectedEggs = 0;
            _timer = new Timer(500);
            _timer.Elapsed += OnTick;
            _timer.AutoReset = true;
            _snakePositions = new Queue<Tuple<Int32, Int32>>();
            int tmp = (Int32)(_table.Size / 2);
            _snakePositions.Enqueue(Tuple.Create(tmp, tmp));
            _head = Tuple.Create(tmp, tmp);
            _table.SetValue(tmp, tmp, TableObject.Snake);

            LastDirection = Direction.Up;

            _isPaused = false;

            ChangeTimers(true);
        }
        public void SpawnEgg()
        {
            Random random = new Random();
            Int32 x, y;
            do
            {
                x = random.Next(_table.Size);
                y = random.Next(_table.Size);
            }
            while (!_table.IsEmpty(x, y));

            _table.SetValue(x, y, TableObject.Egg);
            Debug.WriteLine("Egg spawned");
            OnGameAdvanced();
        }
        private void OnGameAdvanced()
        {
            if (GameAdvanced != null)
            {
                GameAdvanced(this, new SnakeEventArgs(false, CollectedEggs));
                Debug.WriteLine("GameAdvanced Fired");
            }
            else
            {
                Debug.WriteLine("GameAdvanced Not Fired");
            }

        }
        private void OnGameOver()
        {
            if (GameOver != null)
            {
                GameOver(this, new SnakeEventArgs(true, CollectedEggs));
                Debug.WriteLine("GameOver Fired");
            }
            else
            {
                Debug.WriteLine("GameAdvanced Not Fired");
            }
        }
    }
}
