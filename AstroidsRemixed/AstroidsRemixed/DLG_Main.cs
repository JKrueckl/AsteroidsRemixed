using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using AstroidsRemixed.GameModels;
using AstroidsRemixed.Etc;
using InputInterfaceLib;
using System.Media;

namespace AstroidsRemixed
{
    // Send object information to debug dialog
    public delegate void SetValDel(object s);

    public partial class DLG_Main : Form
    {
        // Used in debug mode
        public SetValDel setValCallback;

        // debug mode?
        const bool DEBUG_MODE = false;
        DLG_Debug DebugWindow;

        static Random _rnd = new Random();

        // Wave number / calculator
        int _waveNumber = 5;
        WaveCalculator _wc;

        int _playerScore = 0;
        int _playerLives = 3;
        bool _starting = true; 

        // Player
        PlayerInfo _player = new PlayerInfo();

        // Collision testing
        //private LinkedList<Region> _deadShapes = new LinkedList<Region>();

        // testing enemies
        EnemyOne _enemyOne = new EnemyOne();
        EnemyTwo _enemyTwo;
        Rock _testRock;

        // All enemies / rocks
        List<EnemyOne> _enemyOneContainer = new List<EnemyOne>();
        List<EnemyTwo> _enemyTwoContainer = new List<EnemyTwo>();
        List<Rock> _rockContainer = new List<Rock>();

        // Sound testing!
        Dictionary<string, SoundPlayer> _sounds = new Dictionary<string, SoundPlayer>();

        // Control interface
        InputInterface _ii = new InputInterface();

        // background object
        BackgroundImg _generatedBG;      

        #region Inits
        public DLG_Main()
        {
            InitializeComponent();           
        }

        private void DLG_Main_Load(object sender, EventArgs e)
        {
            UI_Menu_Panel.Location = new Point((ClientSize.Width / 2) - (UI_Menu_Panel.Width / 2), 10);
            UI_Pause_Panel.Location = new Point((ClientSize.Width / 2) - (UI_Menu_Panel.Width / 2), 10);
            UI_GameOver_Panel.Location = new Point((ClientSize.Width / 2) - (UI_Menu_Panel.Width / 2), 400);
            UI_Pause_Panel.Hide();
            UI_GameOver_Panel.Hide();

            #region Debug Mode
            if (DEBUG_MODE)
            {
                DebugWindow = new DLG_Debug();
                DebugWindow.Show();

                setValCallback += new SetValDel(DebugWindow.TranslateInformation);
            }
            #endregion

            // Create NEW RANDOM BACKGROUND
            _generatedBG = new BackgroundImg(ClientRectangle);

            // Enemy Two test
            //_enemyTwo = new EnemyTwo(ClientRectangle);

            // Sound testing fun!
            //SoundPlayer lazer = new SoundPlayer();
            //lazer.SoundLocation = "Lazer.wav";

            //_sounds.Add("Lazer", lazer);

            // Boss rock test
            //_testRock = new RockBoss(new PointF(300, 300), Rock.State.boss);

            // Init wave calc
            _wc = new WaveCalculator(_waveNumber);

            // Spawn the first wave
            for(int i = 0; i < _wc.Rocks; i++)
                _rockContainer.Add(new Rock(new PointF(_rnd.Next(0, ClientRectangle.Width), _rnd.Next(0, ClientRectangle.Height)), Rock.State.large));
         
        }
        #endregion

        #region Controls

        private void DLG_Main_KeyDown(object sender, KeyEventArgs e)
        {
            _ii.PushInputToInterface(e, true);

            if (e.KeyCode == Keys.M)
            {
                if (_enemyOne._currentFireMode == EnemyOne._shootingMode.TripleShot)
                    _enemyOne._currentFireMode = EnemyOne._shootingMode.Normal;
                else
                    _enemyOne._currentFireMode = EnemyOne._shootingMode.TripleShot;

                if (_enemyTwo._currentFireMode == EnemyTwo._shootingMode.TripleShot)
                    _enemyTwo._currentFireMode = EnemyTwo._shootingMode.Normal;
                else
                    _enemyTwo._currentFireMode = EnemyTwo._shootingMode.TripleShot;
            }
        }

        private void DLG_Main_KeyUp(object sender, KeyEventArgs e)
        {
            _ii.PushInputToInterface(e, false);
        }

        private void UI_B_Start_Click(object sender, EventArgs e)
        {
            UI_Menu_Panel.Hide();
            UI_GameOver_Panel.Hide();
            Focus();

            if(_playerLives == 0)
            {
                _waveNumber = 1;
                _playerScore = 0;
                _playerLives = 3;
                _player = new PlayerInfo();
                _rockContainer.Clear();
                
                // Init wave calc
                _wc = new WaveCalculator(_waveNumber);

                // Spawn the first wave
                for (int i = 0; i < _wc.Rocks; i++)
                    _rockContainer.Add(new Rock(new PointF(_rnd.Next(0, ClientRectangle.Width), _rnd.Next(0, ClientRectangle.Height)), Rock.State.large));

                _generatedBG = new BackgroundImg(ClientRectangle);
            }

            _starting = false;
        }

        #endregion

        // Game tick loop
        private void Game_Timer_Tick(object sender, EventArgs e)
        {
            if(_playerLives == 0)
            {
                //_ii = new InputInterface();
                UI_GameOver_Panel.Show();
                UI_Menu_Panel.Show();
                _starting = true;
            }

            if (_ii.Pause)
            {
                UI_Pause_Panel.Show();                
            }
            else
            {
                #region Debug Mode
                if (DEBUG_MODE)
                {
                    Text = string.Format("DEBUG MODE - Player pos - X: {0:F2} Y: {1:F2}", _player._pos.X, _player._pos.Y);
                    if (DebugWindow.currentSelection == "Player")
                        setValCallback.Invoke(_player);
                    else if (DebugWindow.currentSelection == "EnemyOne")
                        setValCallback.Invoke(_enemyOne);
                    else if (DebugWindow.currentSelection == "EnemyTwo")
                        setValCallback.Invoke(_enemyTwo);
                }
                #endregion

                if(_starting && _ii.Shoot)
                {
                    UI_B_Start_Click(null, null);
                    _starting = false;
                }                   

                UI_Pause_Panel.Hide();               

                using (BufferedGraphicsContext bgc = new BufferedGraphicsContext())
                {
                    using (BufferedGraphics bg = bgc.Allocate(CreateGraphics(), ClientRectangle))
                    {
                        // Draw stars / planet ----
                        _generatedBG.imgStars.ForEach(x => bg.Graphics.FillPath(new SolidBrush(x.starColor), x.GetPath()));

                        if (_generatedBG.hasPlanets)
                            _generatedBG.imgPlanets.ForEach(x => bg.Graphics.FillPath(new SolidBrush(x.planetCol), x.GetPath()));
                        // ------------------------

                        if (!_starting)
                        {
                            // draw all dead shapes (collisions)
                            //foreach (var hit in _deadShapes)
                            //bg.Graphics.FillRegion(new SolidBrush(Color.Yellow), hit);

                            #region Player Calcs

                            // player boost
                            if (_player._boosting)
                                _player._particles.Add(new ExhaustParticle(_player));
                            foreach (ExhaustParticle ep in _player._particles)
                            {
                                ep.Tick();
                                bg.Graphics.FillPath(new SolidBrush(ep.myColor), ep.GetPath());
                            }

                            // player Bullets
                            if (_player._shooting)
                            {
                                _player._bullets.Add(new BulletParticle(_player));
                                //_sounds["Lazer"].Play();
                            }
                            foreach (BulletParticle bp in _player._bullets)
                            {
                                bp.Tick();
                                bg.Graphics.FillPath(new SolidBrush(BulletParticle.myColor), bp.GetPath());
                            }

                            _player.Tick(_ii, ClientRectangle);
                            bg.Graphics.FillPath(new SolidBrush(Color.FromArgb(_player._alpha, Color.Red)), _player.GetPath());

                            #endregion

                            // All rocks
                            foreach (Rock r in _rockContainer)
                            {
                                r.Tick(ClientSize);
                                r.Render(bg, ClientSize);
                            }

                            #region Enemy One & Enemy Two & Boss calc example
                            //~~~~Enemy one
                            //_enemyOne.Tick(_player);
                            //if (_enemyOne._shooting)
                            //    _enemyOne._bullets.Add(new BulletParticle(_enemyOne));

                            //foreach (BulletParticle bp in _enemyOne._bullets)
                            //{
                            //    bp.Tick();
                            //    bg.Graphics.FillPath(new SolidBrush(BulletParticle.myColor), bp.GetPath());
                            //}
                            //bg.Graphics.FillPath(new SolidBrush(Color.Blue), _enemyOne.GetPath());
                            //CleanUp(_enemyOne);


                            //// ~~~~ Enemy two
                            //_enemyTwo.Tick(_player);
                            //if (_enemyTwo._shooting)
                            //{
                            //    _enemyTwo._bullets.Add(new BulletParticle(_enemyTwo, 0));
                            //    _enemyTwo._bullets.Add(new BulletParticle(_enemyTwo, 60));
                            //    _enemyTwo._bullets.Add(new BulletParticle(_enemyTwo, 120));
                            //    _enemyTwo._bullets.Add(new BulletParticle(_enemyTwo, 180));
                            //    _enemyTwo._bullets.Add(new BulletParticle(_enemyTwo, 240));
                            //    _enemyTwo._bullets.Add(new BulletParticle(_enemyTwo, 300));
                            //}
                            //foreach (BulletParticle bp in _enemyTwo._bullets)
                            //{
                            //    bp.Tick();
                            //    bg.Graphics.FillPath(new SolidBrush(BulletParticle.myColor), bp.GetPath());
                            //}
                            //bg.Graphics.FillPath(new SolidBrush(Color.Green), _enemyTwo.GetPath());
                            //CleanUp(_enemyTwo);


                            // ~~~~ BOSS ROCK
                            //_testRock.Tick(ClientSize);
                            //_testRock.Render(bg);
                            #endregion

                            #region UI

                            bg.Graphics.DrawString("Wave: " + _waveNumber, new Font("Times New Roman", 24), new SolidBrush(Color.Red), 10, 10);
                            bg.Graphics.DrawString("Score: " + _playerScore, new Font("Times New Roman", 24), new SolidBrush(Color.Red), 10, 44);

                            for (int i = 0; i < _playerLives; i++)
                            {
                                bg.Graphics.FillPath(new SolidBrush(Color.Red), _player.GetPathForLifeSymbol(25 + (50 * i), 110));
                            }

                            #endregion

                            foreach (BulletParticle bp in _player._bullets)
                            {
                                List<Rock> possibleHits = (
                                    from shape in _rockContainer
                                    where Math.Sqrt(Math.Pow(Math.Abs(shape.Pos.X - bp.location.X), 2) + Math.Pow(Math.Abs(shape.Pos.Y - bp.location.Y), 2)) < 10000 // Mirroring?????????
                                select shape).ToList();

                                // itterate through possible hit list
                                for (int i = 0; i < possibleHits.Count(); i++)
                                {
                                    // regions of intersections
                                    Region r1 = new Region(possibleHits[i].GetPath(ClientSize));
                                    Region r2 = new Region(bp.GetPath());

                                    // create intersection of the two shapes that are close
                                    r1.Intersect(r2);

                                    // they hit
                                    if (!r1.IsEmpty(CreateGraphics()))
                                    {
                                        // add region to linked list of hits
                                        //_deadShapes.AddFirst(r1);

                                        if (possibleHits[i] is RockBoss && (possibleHits[i] as RockBoss)._health > 1)
                                            (possibleHits[i] as RockBoss)._health--;
                                        else
                                            possibleHits[i].IsMarkedForDeath = true;

                                        bp.KILLMENOW = true;
                                    }
                                }
                            }

                            if (!UI_Menu_Panel.Visible)
                            {
                                List<Rock> possiblePlayerHits = (
                                from shape in _rockContainer
                                where Math.Sqrt(Math.Pow(Math.Abs(shape.Pos.X - _player._pos.X), 2) + Math.Pow(Math.Abs(shape.Pos.Y - _player._pos.Y), 2)) < 250 // boss is big (might be a problem)
                            select shape).ToList();

                                // itterate through possible hit list
                                for (int i = 0; i < possiblePlayerHits.Count(); i++)
                                {
                                    if ((possiblePlayerHits[i]._alpha == 255) && (_player._alpha == 255))
                                    {
                                        // regions of intersections
                                        Region r1 = new Region(possiblePlayerHits[i].GetPath(ClientSize));
                                        Region r2 = new Region(_player.GetPath());

                                        // create intersection of the two shapes that are close
                                        r1.Intersect(r2);

                                        // they hit
                                        if (!r1.IsEmpty(CreateGraphics()))
                                        {
                                            possiblePlayerHits[i].IsMarkedForDeath = true;
                                            _player._gotHit = true;
                                        }
                                    }
                                }
                            }


                            List<Rock> SmallerRocksToAdd = new List<Rock>();

                            //_rockContainer.RemoveAll(x => x.IsMarkedForDeath);       
                            foreach (Rock r in _rockContainer)
                            {
                                // 25, 45, 65
                                if (r.IsMarkedForDeath)
                                {
                                    if (r._tileSize == (int)Rock.State.medium)
                                    {
                                        SmallerRocksToAdd.Add(new Rock(r.Pos, Rock.State.small));
                                        SmallerRocksToAdd.Add(new Rock(r.Pos, Rock.State.small));
                                        SmallerRocksToAdd.Add(new Rock(r.Pos, Rock.State.small));

                                        _playerScore += 250;
                                    }
                                    else if (r._tileSize == (int)Rock.State.large)
                                    {
                                        SmallerRocksToAdd.Add(new Rock(r.Pos, Rock.State.medium));
                                        SmallerRocksToAdd.Add(new Rock(r.Pos, Rock.State.medium));

                                        _playerScore += 500;
                                    }
                                    else
                                    {
                                        _playerScore += 100;
                                    }
                                }
                            }

                            CleanUp(_player);

                            _rockContainer.RemoveAll(x => x.IsMarkedForDeath);
                            SmallerRocksToAdd.ForEach(x => _rockContainer.Add(x));

                            if (_player._gotHit)
                            {
                                if (_playerLives > 0)
                                {
                                    _playerLives--;
                                    _player.ResetOnHit();
                                }
                            }

                        }

                        bg.Render();
                    }
                }

                if (!_starting)
                {

                    if (_rockContainer.Count == 0)
                    {
                        _playerScore += _waveNumber * 1000;
                        _playerLives++;

                        _wc = new WaveCalculator(_waveNumber++);

                        if (_wc.BossWave)
                        {
                            _rockContainer.Add(new RockBoss(new PointF(_rnd.Next(0, ClientRectangle.Width), _rnd.Next(0, ClientRectangle.Height)), Rock.State.boss));
                        }
                        else
                        {
                            for (int i = 0; i < _wc.Rocks; i++)
                                _rockContainer.Add(new Rock(new PointF(_rnd.Next(0, ClientRectangle.Width), _rnd.Next(0, ClientRectangle.Height)), Rock.State.large));
                        }
                    }
                }
            }

            
        }

        // Debug Method (OutDated)
        public List<object> SendGameObjects()
        {
            List<object> allExistingItems = new List<object>();

            // TEMP BUT WORKS - CREATE LIST OF GAME OBJECTS LATER SOMETIME

            allExistingItems.Add(_player);
            allExistingItems.Add(_enemyOne);
            allExistingItems.Add(_enemyTwo);

            return allExistingItems;
        }
        
        // Manual clean up
        public void CleanUp(object gameObject)
        {
            if(gameObject is PlayerInfo)
            {
                (gameObject as PlayerInfo)._bullets.RemoveAll(x => x.KILLMENOW);
                (gameObject as PlayerInfo)._particles.RemoveAll(x => x.KILLMENOW);
            }
            else if(gameObject is EnemyOne)
            {
                (gameObject as EnemyOne)._bullets.RemoveAll(x => x.KILLMENOW);
            }
            else if(gameObject is EnemyTwo)
            {
                (gameObject as EnemyTwo)._bullets.RemoveAll(x => x.KILLMENOW);
            }
        }
    }
}
