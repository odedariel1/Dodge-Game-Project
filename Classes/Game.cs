using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.HumanInterfaceDevice;
using Windows.Gaming.Input;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Imaging;

namespace DodgeGame_Project.Classes
{
    internal class Game
    {
        //fields
        Canvas allpage;
        DispatcherTimer timer = new DispatcherTimer();
        DispatcherTimer savegametimer = new DispatcherTimer();
        DispatcherTimer Enemytouchtimer = new DispatcherTimer();
        Board boardgame;
        Player character;
        Enemey[] enemies;
        Random rnd;
        bool leftkeypressed;
        bool rightkeypressed;
        bool upkeypressed;
        bool downkeypressed;
        //properties
        public Canvas Allpage
        {
            get
            {
                return this.allpage;
            }
            set
            {
                this.allpage = value;
            }
        }
        public DispatcherTimer Timer
        {
            get
            {
                return this.timer;
            }
            set
            {
                this.timer = value;
            }
        }
        public Board Boardgame
        {
            get
            {
                return this.boardgame;
            }
            set
            {
                this.boardgame = value;
            }
        }
        public Player Character
        {
            get
            {
                return this.character;
            }
            set
            {
                this.character = value;
            }
        }
        public Enemey[] Enemies
        {
            get
            {
                return this.enemies;
            }
            set
            {
                this.enemies = value;
            }
        }
        public Random Rnd
        {
            get 
            {
                return this.rnd;
            }
            set 
            {
                this.rnd = value;
            }
        }
        public bool Leftkeypressed
        {
            get
            {
                return this.leftkeypressed;
            }
            set
            {
                this.leftkeypressed = value;
            }
        }
        public bool Rightkeypressed
        {
            get
            {
                return this.rightkeypressed;
            }
            set
            {
                this.rightkeypressed = value;
            }
        }
        public bool Upkeypressed
        {
            get
            {
                return this.upkeypressed;
            }
            set
            {
                this.upkeypressed = value;
            }
        }
        public bool Downkeypressed
        {
            get
            {
                return this.downkeypressed;
            }
            set
            {
                this.downkeypressed = value;
            }
        }
        
        //constractor
        public Game(Canvas canvas)
        {
            Allpage=canvas;
            rnd = new Random();
            Enemies = new Enemey[10];
            CreateAllParticipates();
            Boardgame=new Board(Allpage);
            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
            Window.Current.CoreWindow.KeyUp += CoreWindow_KeyUp1;
            Allpage.KeyDown += OnKeyDown; 
            timer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            timer.Tick += Timer_Tick;
            timer.Start();
            savegametimer.Interval = new TimeSpan(0, 0, 0, 2);
            savegametimer.Tick += Savegametimer_Tick; ;
            
        }

        //Functions:

        //function that check if enemy and player collided
        public bool PlayerAndEnemyDoesCollied(Player a, Enemey b)
        {
            if (((a.LeftPosition >= b.LeftPosition  && a.LeftPosition <= (b.LeftPosition + b.Enemyview.Width)) || ((a.LeftPosition + a.Playerview.Width) >= b.LeftPosition) && ((a.LeftPosition + a.Playerview.Width) <= (b.LeftPosition + b.Enemyview.Width))))
            {
                if (((a.TopPosition >= b.TopPosition && a.TopPosition <= (b.TopPosition + b.Enemyview.Height)) || ((a.TopPosition + a.Playerview.Height) >= b.TopPosition) && ((a.TopPosition + a.Playerview.Height) <= (b.TopPosition + b.Enemyview.Height))))
                {
                    return true;
                }
            }
            return false;

        }
        //function that check if enemy and enemy collided
        public bool EnemyAndEnemyDoesCollied(Enemey a, Enemey b)
        {
            if (((a.LeftPosition >= b.LeftPosition && a.LeftPosition <= (b.LeftPosition + b.Enemyview.Width)) || ((a.LeftPosition + a.Enemyview.Width) >= b.LeftPosition) && ((a.LeftPosition + a.Enemyview.Width) <= (b.LeftPosition + b.Enemyview.Width))))
            {
                if (((a.TopPosition >= b.TopPosition && a.TopPosition <= (b.TopPosition + b.Enemyview.Height)) || ((a.TopPosition + a.Enemyview.Height) >= b.TopPosition) && ((a.TopPosition + a.Enemyview.Height) <= (b.TopPosition + b.Enemyview.Height))))
                {
                    return true;
                }
            }
            return false;
        }
        //function that create the players and enemys into the game without to spawn on each other location
        public void CreateAllParticipates()
        {
            int x, y;
            for (int i = 0; i < Enemies.Length; i++)
            {
               x = rnd.Next(0, 1400);
                y = rnd.Next(0, 700); ;
                Enemies[i] = new Enemey(Allpage, y, x);
                Enemies[i].TopPosition = y;
                Enemies[i].LeftPosition = x;
                for (int j = 0; j < i; j++)
                {
                    if (EnemyAndEnemyDoesCollied(Enemies[i], Enemies[j]))
                    {
                        Enemies[i].LeftPosition = rnd.Next(0, 1400);
                        Canvas.SetLeft(Enemies[i].Enemyview, Enemies[i].LeftPosition);
                        Enemies[i].TopPosition = rnd.Next(0, 700);
                        Canvas.SetTop(Enemies[i].Enemyview, Enemies[i].TopPosition);
                        j = -1;
                    }
                }
            }
            x = rnd.Next(20, 1400);
            y = rnd.Next(20, 700);
            Character = new Player(Allpage,y,x);
            Character.TopPosition = y;
            Character.LeftPosition = x;
            for (int i = 0; i < Enemies.Length; i++)
            {
                if (((Character.LeftPosition >= Enemies[i].LeftPosition && Character.LeftPosition <= (Enemies[i].LeftPosition + Enemies[i].Enemyview.Width + 60)) || ((Character.LeftPosition + Character.Playerview.Width + 60) >= Enemies[i].LeftPosition) && ((Character.LeftPosition + Character.Playerview.Width+60) <= (Enemies[i].LeftPosition + Enemies[i].Enemyview.Width + 60))))
                {
                    if (((Character.TopPosition >= Enemies[i].TopPosition && Character.TopPosition <= (Enemies[i].TopPosition + Enemies[i].Enemyview.Height + 60)) || ((Character.TopPosition + Character.Playerview.Height + 60) >= Enemies[i].TopPosition) && ((Character.TopPosition + Character.Playerview.Height+60) <= (Enemies[i].TopPosition + Enemies[i].Enemyview.Height + 60))))
                    {
                        Character.LeftPosition = rnd.Next(0, 1400);
                        Canvas.SetLeft(Character.Playerview, Character.LeftPosition);
                        Character.TopPosition = rnd.Next(0, 700);
                        Canvas.SetTop(Character.Playerview, Character.TopPosition);
                        i = -1;

                    }

                }
            }
        }
       //function that remove all of the players and enemys in my game
        public void RemoveAllParticipates()
        {
            Character.PlayerRemove();
            for (int i = 0; i < Enemies.Length; i++)
            {
                Enemies[i].RemoveEnemy();
            }
        }
        

    //functions that check the current Key press(down/up)------------------
    private void CoreWindow_KeyUp1(CoreWindow sender, KeyEventArgs args)
    {
        if (args.VirtualKey == Windows.System.VirtualKey.Left)
        {
            Leftkeypressed = false;
        }
        if (args.VirtualKey == Windows.System.VirtualKey.Right)
        {
            Rightkeypressed = false;
        }
        if (args.VirtualKey == Windows.System.VirtualKey.Up)
        {
            Upkeypressed = false;
        }
        if (args.VirtualKey == Windows.System.VirtualKey.Down)
        {
            Downkeypressed = false;
        }

    }

    private void CoreWindow_KeyDown(CoreWindow sender, KeyEventArgs args)

    {
        if (args.VirtualKey == Windows.System.VirtualKey.Left)
        {
            Leftkeypressed = true;
        }
        if (args.VirtualKey == Windows.System.VirtualKey.Right)
        {
            Rightkeypressed = true;
        }
        if (args.VirtualKey == Windows.System.VirtualKey.Up)
        {
            Upkeypressed = true;
        }
        if (args.VirtualKey == Windows.System.VirtualKey.Down)
        {
            Downkeypressed = true;
        }
    }   
    //--------------------------------------------------------------------------
    //function that do remake the game when the user press 'enter' after win/lose
    private void OnKeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter && Boardgame.EndGame == true)//OnWork
            {
                RemoveAllParticipates();
                CreateAllParticipates();
                timer.Start();
            }
        }
    
        //function that make the enemy to move to go after the player
        private void EnemeysMoveToPlayer(Enemey a)
        {
            if (Canvas.GetLeft(a.Enemyview) <= Canvas.GetLeft(Character.Playerview) && Boardgame.IsPaused==false)//enemy right
            {
                a.LeftPosition = (double)Canvas.GetLeft(a.Enemyview);
                a.LeftPosition= a.LeftPosition + 2;

                Canvas.SetLeft(a.Enemyview, a.LeftPosition);
            }
            if (Canvas.GetLeft(a.Enemyview) >= Canvas.GetLeft(Character.Playerview) && Boardgame.IsPaused == false)//enenmy left
            {
                a.LeftPosition = (double)Canvas.GetLeft(a.Enemyview);
                a.LeftPosition = a.LeftPosition - 2;

                Canvas.SetLeft(a.Enemyview, a.LeftPosition);
            }
            if (Canvas.GetTop(a.Enemyview) <= Canvas.GetTop(Character.Playerview) && Boardgame.IsPaused == false)//enemy down
            {
                a.TopPosition = (double)Canvas.GetTop(a.Enemyview);
                a.TopPosition = a.TopPosition + 2;

                Canvas.SetTop(a.Enemyview, a.TopPosition);
            }
            if (Canvas.GetTop(a.Enemyview) >= Canvas.GetTop(Character.Playerview) && Boardgame.IsPaused == false)//enenmy top
            {
                a.TopPosition = (double)Canvas.GetTop(a.Enemyview);
                a.TopPosition = a.TopPosition - 2;

                Canvas.SetTop(a.Enemyview, a.TopPosition);
            }
        }
        //while the timer in touchtimer do the tick it remove the touched enemy from the game
        private void Touchtimer_Tick(object sender, object e)
        {
            for(int i=0;i<Enemies.Length;i++)
            {
                if (Enemies[i].Isalive==false)
                    Allpage.Children.Remove(Enemies[i].Enemyview);
            }
        }
        //function that make the enemy to explode and then remove it when it touch other enemy
        private void EnemyDown(Enemey a, Enemey b)
        {
            
            if (EnemyAndEnemyDoesCollied(a,b) && a.Isalive==true)
            {
                a.Enemyview.Source = new BitmapImage(new Uri("ms-appx:///Assets/Explode.png"));
                a.Isalive = false;
                Enemytouchtimer.Interval = new TimeSpan(0, 0, 0, 0, 60);
                Enemytouchtimer.Tick += Touchtimer_Tick;
                Enemytouchtimer.Start();
            }
        }
        //check if all enemy are dead
        private void IsAllEnemysDead(Enemey[] enemies)
        {
            int count = 0;
            for(int i = 0;i< enemies.Length;i++)
            {
                if (enemies[i].Isalive == false)
                {
                    count++;
                }
            }
            if (count >= 9)
            {
                timer.Stop();
                Boardgame.GameWinScreen();
            }
        }
        //function that check every step of the enemys if the player and the enemy collied if it is open the lose screen and stop the game
        private void EnemyTouchPlayer(Enemey a)
        {
            if (PlayerAndEnemyDoesCollied(character,a)==true&&a.Isalive==true)
            {
                timer.Stop();
                Boardgame.GameOverScreen();
            }
        }
        //function that run the functions in it every (10 mili second). every move in the game.
        private void Timer_Tick(object sender, object e)
        {
            if (leftkeypressed == true && Canvas.GetLeft(Character.Playerview) >= 0 && Boardgame.IsPaused == false)//player move left
            {
                Character.LeftPosition = Character.LeftPosition - 10;
                Canvas.SetLeft(Character.Playerview, Character.LeftPosition);
            }
            if (Rightkeypressed && Canvas.GetLeft(Character.Playerview) <= 1440 && Boardgame.IsPaused == false)//player move right
            {
                Character.LeftPosition = Character.LeftPosition + 10;
                Canvas.SetLeft(Character.Playerview, Character.LeftPosition);
            }
            if (Upkeypressed && Canvas.GetTop(Character.Playerview) >= 20 && Boardgame.IsPaused == false)//player move up
            {
                Character.TopPosition = Character.TopPosition - 10;
                Canvas.SetTop(Character.Playerview, Character.TopPosition);
            }
            if (Downkeypressed && Canvas.GetTop(Character.Playerview) <= 740 && Boardgame.IsPaused == false)//player move down
            {
                Character.TopPosition = Character.TopPosition + 10;
                Canvas.SetTop(Character.Playerview, Character.TopPosition);
            }
            IsAllEnemysDead(Enemies);//check if all enemys are dead
            //check if new game clicked
            if (Boardgame.NewGame==true)
            {
                NewGame();
                Boardgame.NewGame=false;
            }
            //check if save game clicked
            if (Boardgame.IsSaved == true)
            {
                SaveGame();
                savegametimer.Start();
                Boardgame.IsSaved = false;
            }
            //check if load game clicked
            if (Boardgame.IsLoad == true)
            {
                LoadGame();
                Boardgame.IsLoad = false;
            }
            for (int i = 0; i < Enemies.Length; i++)
            {
                EnemeysMoveToPlayer(Enemies[i]);
                EnemyTouchPlayer(Enemies[i]);
                for(int j=0;j<Enemies.Length;j++)
                {
                    if (i != j && Enemies[j].Isalive == true)
                    { 
                        EnemyDown(Enemies[i], enemies[j]);

                    }
                }
            }
        }
        //Functions that remove the Save the game screen
        private void Savegametimer_Tick(object sender, object e)
        {
            Allpage.Children.Remove(Boardgame.Messagebackground);
            Allpage.Children.Remove(Boardgame.Message);
            Allpage.Children.Remove(Boardgame.Closingbutton);
            savegametimer.Stop();
        }
        //Function that make new game
        public void NewGame()
        {
            Boardgame.NewGame = false;
            RemoveAllParticipates();
            CreateAllParticipates();
        }
        //Function that save the game
        public void SaveGame()
        {
            string filePath = $@"{Windows.Storage.ApplicationData.Current.LocalFolder.Path}\SaveFile.txt";
            string saving = $"{Character.LeftPosition} {Character.TopPosition} ";

            for(int i=0;i < Enemies.Length;i++)
            {
                saving += $"{Enemies[i].LeftPosition} {Enemies[i].TopPosition} {Enemies[i].Isalive} ";
            }
            File.WriteAllText(filePath, saving);
        }
        //Function that Load the game
        public void LoadGame()
        {
            timer.Stop();
            string filePath = $@"{Windows.Storage.ApplicationData.Current.LocalFolder.Path}\SaveFile.txt";
            string text= File.ReadAllText(filePath);
            string[] itemsString = text.Split(' ');
            int place = 0;

                    Character.LeftPosition = double.Parse(itemsString[0]);
                    Canvas.SetLeft(Character.Playerview, Character.LeftPosition);
                    Character.TopPosition = double.Parse(itemsString[1]);
                    Canvas.SetTop(Character.Playerview, Character.TopPosition);


            for (int i = 0; i<Enemies.Length;i++)
            {
                if(Enemies[i].Isalive == true)
                    Enemies[i].RemoveEnemy();
                Enemies[i].Enemyview.Source = new BitmapImage(new Uri("ms-appx:///Assets/UltimateShuriken.png"));
            }

            for (int i = 2; i < itemsString.Length-2; i+=3)
            {
                Enemies[place].LeftPosition = double.Parse(itemsString[i]);
                Canvas.SetLeft(Enemies[place].Enemyview, Enemies[place].LeftPosition);
                Enemies[place].TopPosition = double.Parse(itemsString[i + 1]);
                Canvas.SetTop(Enemies[place].Enemyview, Enemies[place].TopPosition);
                Enemies[place].Isalive = bool.Parse(itemsString[i + 2]);
                if (Enemies[place].Isalive==true)
                {
                    Allpage.Children.Add(Enemies[place].Enemyview);
                }

                place++;
            }
            timer.Start();
        }
    }
}
