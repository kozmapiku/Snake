using Snake.Model;
using Snake.Persistence;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Snake.View
{
    public partial class GameForm : Form
    {
        private ISnakeDataAccess _dataAccess; // adatelérés
        private SnakeGameModel _model;
        private Label[,] _tableGrid;

        public GameForm()
        {
            InitializeComponent();
        }
        private void MenuGameEasy_Click(Object sender, EventArgs e)
        {
            _model.GameDifficulty = GameDifficulty.Easy;
            Debug.WriteLine("Level 1 selected");
        }

        private void MenuGameMedium_Click(Object sender, EventArgs e)
        {
            _model.GameDifficulty = GameDifficulty.Medium;
            Debug.WriteLine("Level 2 selected");
        }

        private void MenuGameHard_Click(Object sender, EventArgs e)
        {
            _model.GameDifficulty = GameDifficulty.Hard;
            Debug.WriteLine("Level 3 selected");
        }
        private void SetupMenus()
        {
            _menuGameEasy.Checked = (_model.GameDifficulty == GameDifficulty.Easy);
            _menuGameNormal.Checked = (_model.GameDifficulty == GameDifficulty.Medium);
            _menuGameHard.Checked = (_model.GameDifficulty == GameDifficulty.Hard);
        }
        private async void GameForm_Load(Object sender, EventArgs e)
        {
            // adatelérés példányosítása
            _dataAccess = new SnakeFileDataAccess();

            // modell létrehozása és az eseménykezelők társítása
            _model = new SnakeGameModel(_dataAccess);
            _model.GameAdvanced += new EventHandler<SnakeEventArgs>(Game_GameAdvanced);
            _model.GameOver += new EventHandler<SnakeEventArgs>(Game_GameOver);

            // játéktábla és menük inicializálása
            await _model.NewGame();
            GenerateTable();
            SetupMenus();

            // új játék indítása
            
            SetupTable();

            //_timer.Start();
        }
        private void GenerateTable()
        {
            _tableGrid = new Label[_model.Table.Size, _model.Table.Size];
            
            for (Int32 i = 0; i < _model.Table.Size; i++)
                for (Int32 j = 0; j < _model.Table.Size; j++)
                {
                    _tableGrid[i, j] = new Label();
                    _tableGrid[i, j].Location = new Point(5 + 50 * j, 35 + 50 * i); // elhelyezkedés
                    _tableGrid[i, j].Size = new Size(50, 50); // méret
                    _tableGrid[i, j].FlatStyle = FlatStyle.Flat;

                    _tableGrid[i, j].TabIndex = 100 + i * _model.Table.Size + j; // a gomb számát a TabIndex-ben tároljuk
                    Controls.Add(_tableGrid[i, j]);
                }
        }
        private void DeleteTable()
        {
            foreach (Label label in _tableGrid)
            {
                Controls.Remove(label);
            }
        }
        private void SetupTable()
        {
            for (Int32 i = 0; i < _tableGrid.GetLength(0); i++)
            {
                for (Int32 j = 0; j < _tableGrid.GetLength(1); j++)
                {
                    switch(_model.Table[i,j])
                    {
                        case TableObject.Egg:
                            _tableGrid[i, j].BackColor = Color.Yellow;
                            break;
                        case TableObject.Empty:
                            _tableGrid[i, j].BackColor = Color.White;
                            break;
                        case TableObject.Wall:
                            _tableGrid[i, j].BackColor = Color.Black;
                            break;
                        case TableObject.Snake:
                            _tableGrid[i, j].BackColor = Color.Green;
                            break;
                    }
                    
                }
            }
        }
        private async void MenuFileNewGame_Click(Object sender, EventArgs e)
        {
            _model.ChangeTimers(false);
            await _model.NewGame();
            Debug.WriteLine("Tábla mérete: "+_model.Table.Size);
            DeleteTable();
            GenerateTable();
            SetupTable();
            SetupMenus();

        }
        private void MenuFilePause_Click(Object sender, EventArgs e)
        {
            if(_model.IsPaused)
            {
                _model.IsPaused = false;
                _model.ChangeTimers(true);
            }
            else
            {
                _model.IsPaused = true;
                _model.ChangeTimers(false);
            }
        }
        private void MenuFileExit_Click(Object sender, EventArgs e)
        {
            _model.ChangeTimers(false);
            // megkérdezzük, hogy biztos ki szeretne-e lépni
            if (MessageBox.Show("Biztosan ki szeretne lépni?", "Snake játék", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // ha igennel válaszol
                Close();
            }
            else
            {
                _model.ChangeTimers(true);
            }
        }

        private void ChangeDirection(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Left && !_model.IsPaused)
            {
                _model.LastDirection = (Direction)(Mod(((Int32)_model.LastDirection - 1), 4));
                Debug.WriteLine(_model.LastDirection);
            }
            else if(e.KeyCode == Keys.Right && !_model.IsPaused)
            {
                _model.LastDirection = (Direction)(Mod(((Int32)_model.LastDirection + 1), 4));
                Debug.WriteLine(_model.LastDirection);
            }
            
        }
        Int32 Mod(Int32 x, Int32 m)
        {
            int r = x % m;
            return r < 0 ? r + m : r;
        }
        private void Game_GameAdvanced(Object sender, SnakeEventArgs e)
        {
            SetupTable();
        }
        private void Game_GameOver(Object sender, SnakeEventArgs e)
        {
            MessageBox.Show("Elfogyasztott tojások: " + e.GameEggsCount, "Snake játék",MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
    }
}
