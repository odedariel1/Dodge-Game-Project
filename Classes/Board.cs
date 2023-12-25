using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Printing3D;
using Windows.Networking.NetworkOperators;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.Media.Core;

namespace DodgeGame_Project.Classes
{
    internal class Board
    {
        //field
        private Canvas allpage;
        MediaElement audio;
        private Image background;
        private Canvas pausescreen;
        private Button continuebutton;
        private Button newgamebutton;
        private Button savegamebutton;
        private Button loadgamebutton;
        private TextBlock message;
        private Border messagebackground;
        private Button closingbutton;
        private bool isPaused;
        private bool isSaved;
        private bool isLoad;
        private bool endGame;
        private bool newGame;
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
        public MediaElement Audio
        {
            get
            { 
                return this.audio; 
            }
            set
            {
                this.audio = value;
            }
        }
        public Image Background
        {
            get
            {
                return this.background;
            }
            set
            {
                this.background = value;
            }
        }
        public Canvas Pausescreen
        {
            get
            {
                return this.pausescreen;
            }
            set
            {
                this.pausescreen = value;
            }
        }
        public Button Continuebutton
        {
            get
            {
                return this.continuebutton;
            }
            set
            {
                this.continuebutton = value;
            }
        }
        public Button Newgamebutton
        {
            get
            {
                return this.newgamebutton;
            }
            set
            {
                this.newgamebutton = value;
            }
        }
        public Button Savegamebutton
        {
            get
            {
                return this.savegamebutton;
            }
            set
            {
                this.savegamebutton = value;
            }
        }
        public Button Loadgamebutton
        {
            get
            {
                return this.loadgamebutton;
            }
            set
            {
                this.loadgamebutton = value;
            }
        }
        public TextBlock Message
        {
            get
            {
                return this.message;
            }
            set
            {
                this.message = value;
            }
        }
        public Border Messagebackground
        {
            get
            {
                return this.messagebackground;
            }
            set
            {
                this.messagebackground = value;
            }
        }
        public Button Closingbutton
        {
            get
            {
                return this.closingbutton;
            }
            set
            {
                this.closingbutton = value;
            }
        }
        public bool IsPaused
        {
            get
            {
                return this.isPaused;
            }
            set
            {
                this.isPaused = value;
            }
        }
        public bool IsSaved
        {
            get
            {
                return this.isSaved;
            }
            set
            {
                this.isSaved = value;
            }
        }
        public bool IsLoad
        {
            get
            {
                return this.isLoad;
            }
            set
            {
                this.isLoad = value;
            }
        }
        public bool EndGame
        {
            get
            {
                return this.endGame;
            }
            set
            {
                this.endGame = value;
            }
        }
        public bool NewGame
        {
            get
            {
                return this.newGame;
            }
            set
            {
                this.newGame = value;
            }
        }
        //constractor
        public Board(Canvas canvas)
        {
            EndGame = false;
            isPaused = true;
            IsSaved = false;
            Allpage = canvas;
            //opening audio
            Audio = new MediaElement();
            Audio.Source = new Uri("ms-appx:///Assets/Tengu.mp3");
            Audio.Volume = 0.5;
            Audio.Play();
            Allpage.Children.Add(Audio);
            
            //start img
            Background = new Image();
            Background.Name = "startpage";
            Background.Source = new BitmapImage(new Uri("ms-appx:///Assets/StartPageOpening.jpg"));
            Background.Width = 1700;
            Background.Height = 900;
            Canvas.SetZIndex(Background, 30);
            Canvas.SetLeft(Background, -100);
            Canvas.SetTop(Background, -5);
            Allpage.Children.Add(Background);
            //NewGame button
            Newgamebutton = new Button();
            Newgamebutton.Name = "newgamebutton";
            Newgamebutton.FontSize = 50;
            Newgamebutton.Width = 300;
            Newgamebutton.Height = 100;
            Newgamebutton.Content = "Start Game";
            Newgamebutton.Background = new SolidColorBrush(Color.FromArgb(123, 255, 255, 255));
            Canvas.SetZIndex(Newgamebutton, 31);
            Canvas.SetLeft(Newgamebutton, 600);
            Canvas.SetTop(Newgamebutton, 320);
            this.newgamebutton.Click += Newgamebutton_Click;
            Allpage.Children.Add(Newgamebutton);
        }
        //function opening new game button result
        private void Newgamebutton_Click(object sender, RoutedEventArgs e)
        {
            Allpage.Children.Remove(audio);
            Allpage.Children.Remove(Background);
            Allpage.Children.Remove(Newgamebutton);
            
            Audio.Source = new Uri("ms-appx:///Assets/NarutoSound.mp3");
            Audio.Volume = 0.5;
            Audio.Play();
            Allpage.Children.Add(Audio);
            //add background image
            Background = new Image();
            Background.Name = "gameplaypage";
            Background.Source = new BitmapImage(new Uri("ms-appx:///Assets/DojoBackGround.jpg"));
            Background.Width = 2000;
            Background.Height = 900;
            Canvas.SetZIndex(Background, 0);
            Canvas.SetLeft(Background, -250);
            Allpage.KeyDown += PauseGame_KeyDown;
            Allpage.Children.Add(Background);
            EndGame = false;
            isPaused = false;
        }
        //function on keydown events for screens.
        private void PauseGame_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Escape && isPaused == false)
            {
                PauseGameScreen();
                IsPaused = true;
            }
            else
            {
                UnPauseGameScreen();
                IsPaused = false;
            }
        }
        //function gameover screen 
        public void GameOverScreen()
        {
            Allpage.Children.Remove(Background);
            Allpage.Children.Remove(Audio);
            //game audio
            Audio.Source = new Uri("ms-appx:///Assets/OnePieceOvertakenSound.mp3");
            Audio.Volume = 1;
            Audio.Play();
            Allpage.Children.Add(Audio);
            //image background
            Background = new Image();
            Background.Name = "gameoverpage";
            Background.Source = new BitmapImage(new Uri("ms-appx:///Assets/GameOver.jpg"));
            Background.Width = 1700;
            Background.Height = 900;
            Canvas.SetZIndex(background, 20);
            Canvas.SetLeft(background, -100);
            Canvas.SetTop(background, -5);
            Allpage.Children.Add(Background);
            //message
            Message = new TextBlock();
            Message.Name = "Message";
            Message.Text = "Press Enter To Continue";
            Message.FontSize = 40;
            Message.Foreground = new SolidColorBrush(Color.FromArgb(225, 255, 255, 255));
            Canvas.SetZIndex(Message, 21);
            Canvas.SetLeft(Message, 552);
            Canvas.SetTop(Message, 730);
            Allpage.KeyDown += Message_KeyDown;
            Allpage.Children.Add(Message);
            EndGame = true;
        }
        //function game win screen 
        public void GameWinScreen()
        {
            Allpage.Children.Remove(Background);
            Allpage.Children.Remove(Audio);
            //game audio
            Audio.Source = new Uri("ms-appx:///Assets/CheersSoundEffect.mp3");
            Audio.Volume = 1;
            Audio.Play();
            Allpage.Children.Add(Audio);
            //game background
            Background = new Image();
            Background.Name = "gamewinpage";
            Background.Source = new BitmapImage(new Uri("ms-appx:///Assets/WinnerScreen.jpg"));
            Background.Width = 1700;
            Background.Height = 900;
            Canvas.SetZIndex(background, 20);
            Canvas.SetLeft(background, -100);
            Canvas.SetTop(background, -5);
            Allpage.Children.Add(Background);
            //message
            Message = new TextBlock();
            Message.Name = "Message";
            Message.Text = "Press Enter To Continue";
            Message.FontSize = 40;
            Message.Foreground = new SolidColorBrush(Color.FromArgb(225, 255, 255, 255));
            Canvas.SetZIndex(Message, 21);
            Canvas.SetLeft(Message, 552);
            Canvas.SetTop(Message, 730);
            Allpage.KeyDown += Message_KeyDown;
            Allpage.Children.Add(Message);
            EndGame= true;
        }
        //function on win or lose screen on keydown event
        private void Message_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter && EndGame == true)
            {
                EndGame = false;
                isPaused = false;
                Allpage.Children.Remove(Background);
                Allpage.Children.Remove(Message);
                Allpage.Children.Remove(Audio);
                //game audio
                Audio.Source = new Uri("ms-appx:///Assets/NarutoSound.mp3");
                Audio.Volume = 0.5;
                Audio.Play();
                Allpage.Children.Add(Audio);
                //game background
                Background = new Image();
                Background.Name = "gameplaypage";
                Background.Source = new BitmapImage(new Uri("ms-appx:///Assets/DojoBackGround.jpg"));
                Background.Width = 2000;
                Background.Height = 900;
                Canvas.SetZIndex(Background, 0);
                Canvas.SetLeft(Background, -250);
                Allpage.Children.Add(Background);
            }
        }
        //function on pause game screen
        public void PauseGameScreen()
        {
            Audio.Pause();
            //canvas background
            Pausescreen = new Canvas();
            Pausescreen.Name = "pausepage";
            Pausescreen.Width = 1700;
            Pausescreen.Height = 900;
            Pausescreen.Background = new SolidColorBrush(Color.FromArgb(200, 0, 0, 0));
            Canvas.SetLeft(Pausescreen, -100);
            Canvas.SetTop(Pausescreen, -5);
            Canvas.SetZIndex(Pausescreen, 15);
            Allpage.Children.Add(Pausescreen);
            //continue button
            Continuebutton = new Button();
            Continuebutton.Name = "pausecontinuebutton";
            Continuebutton.FontSize = 50;
            Continuebutton.Width = 300;
            Continuebutton.Height = 100;
            Continuebutton.Content = "Continue";
            Continuebutton.Background = new SolidColorBrush(Color.FromArgb(123, 255, 255, 255));
            Canvas.SetZIndex(Continuebutton, 16);
            Canvas.SetLeft(Continuebutton, 600);
            Canvas.SetTop(Continuebutton, 166);
            Continuebutton.Click += PauseContinuegamebutton_Click;
            Allpage.Children.Add(Continuebutton);
            //NewGame button
            Newgamebutton = new Button();
            Newgamebutton.Name = "pausenewgamebutton";
            Newgamebutton.FontSize = 50;
            Newgamebutton.Width = 300;
            Newgamebutton.Height = 100;
            Newgamebutton.Content = "New Game";
            Newgamebutton.Background = new SolidColorBrush(Color.FromArgb(123, 255, 255, 255));
            Canvas.SetZIndex(Newgamebutton, 16);
            Canvas.SetLeft(Newgamebutton, 600);
            Canvas.SetTop(Newgamebutton, 322);
            Newgamebutton.Click += PauseNewgamebutton_Click;
            Allpage.Children.Add(Newgamebutton);
            //SaveGame button
            Savegamebutton = new Button();
            Savegamebutton.Name = "savegamebutton";
            Savegamebutton.FontSize = 50;
            Savegamebutton.Width = 300;
            Savegamebutton.Height = 100;
            Savegamebutton.Content = "Save Game";
            Savegamebutton.Background = new SolidColorBrush(Color.FromArgb(123, 255, 255, 255));
            Canvas.SetZIndex(Savegamebutton, 16);
            Canvas.SetLeft(Savegamebutton, 600);
            Canvas.SetTop(Savegamebutton, 471);
            Savegamebutton.Click += PauseSavegamebutton_Click; ;
            Allpage.Children.Add(Savegamebutton);
            //LoadGame button
            Loadgamebutton = new Button();
            Loadgamebutton.Name = "pauseloadgamebutton";
            Loadgamebutton.FontSize = 50;
            Loadgamebutton.Width = 300;
            Loadgamebutton.Height = 100;
            Loadgamebutton.Content = "Load Game";
            Loadgamebutton.Background = new SolidColorBrush(Color.FromArgb(123, 255, 255, 255));
            Canvas.SetZIndex(Loadgamebutton, 16);
            Canvas.SetLeft(Loadgamebutton, 600);
            Canvas.SetTop(Loadgamebutton, 620);
            Loadgamebutton.Click += Loadgamebutton_Click;
            Allpage.Children.Add(this.Loadgamebutton);
            IsPaused = true;
        }

        //function to Unpausegamescreen
        public void UnPauseGameScreen()
        {
            Audio.Play();
            Allpage.Children.Remove(Pausescreen);
            Allpage.Children.Remove(Continuebutton);
            Allpage.Children.Remove(Newgamebutton);
            Allpage.Children.Remove(Savegamebutton);
            Allpage.Children.Remove(Loadgamebutton);
            IsPaused = false;
        }
        //function pause screen continue button result
        private void PauseContinuegamebutton_Click(object sender, RoutedEventArgs e)
        {
            Audio.Play();
            Allpage.Children.Remove(Pausescreen);
            Allpage.Children.Remove(Continuebutton);
            Allpage.Children.Remove(Newgamebutton);
            Allpage.Children.Remove(Savegamebutton);
            Allpage.Children.Remove(Loadgamebutton);
            IsPaused = false;
            EndGame = false;
        }
        //function pause screen new game button result
        private void PauseNewgamebutton_Click(object sender, RoutedEventArgs e)
        {
            Audio.Play();
            Allpage.Children.Remove(Pausescreen);
            Allpage.Children.Remove(Continuebutton);
            Allpage.Children.Remove(Newgamebutton);
            Allpage.Children.Remove(Savegamebutton);
            Allpage.Children.Remove(Loadgamebutton);
            IsPaused = false;
            NewGame = true;
        }
        //function pause screen save game button result
        private void PauseSavegamebutton_Click(object sender, RoutedEventArgs e)
        {
            if (IsSaved == false)
            {
                IsSaved = true;
                //background
                Messagebackground = new Border();
                Messagebackground.Background = new SolidColorBrush(Color.FromArgb(255, 170, 160, 160));
                Messagebackground.Height = 200;
                Messagebackground.Width = 880;
                Canvas.SetZIndex(Messagebackground, 21);
                Canvas.SetLeft(Messagebackground, 288);
                Canvas.SetTop(Messagebackground, 310);
                Allpage.Children.Add(Messagebackground);
                //text
                Message = new TextBlock();
                Message.Name = "GameSavedMessage";
                Message.Text = "Your Game Have Saved";
                Message.FontSize = 80;
                Message.Foreground = new SolidColorBrush(Color.FromArgb(225, 255, 255, 255));
                Canvas.SetZIndex(Message, 21);
                Canvas.SetLeft(Message, 322);
                Canvas.SetTop(Message, 350);
                Allpage.Children.Add(Message);
                //button
                Closingbutton = new Button();
                Closingbutton.Content = "X";
                Closingbutton.Height = 56;
                Closingbutton.Width = 52;
                Closingbutton.Foreground = new SolidColorBrush(Color.FromArgb(225, 255, 255, 255));
                Closingbutton.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
                Closingbutton.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 190, 178, 178));
                Canvas.SetZIndex(Closingbutton, 21);
                Canvas.SetLeft(Closingbutton, 1116);
                Canvas.SetTop(Closingbutton, 310);
                Closingbutton.Click += Closingbutton_Click;
                Allpage.Children.Add(Closingbutton);
            }
        }
        //function to close the save game scrern
        private void Closingbutton_Click(object sender, RoutedEventArgs e)
        {
            IsSaved= false;
            Allpage.Children.Remove(Messagebackground);
            Allpage.Children.Remove(Message);
            Allpage.Children.Remove(Closingbutton);
        }
        //function to load game
        private void Loadgamebutton_Click(object sender, RoutedEventArgs e)
        {
            IsLoad = true;
            Audio.Play();
            Allpage.Children.Remove(Pausescreen);
            Allpage.Children.Remove(Continuebutton);
            Allpage.Children.Remove(Newgamebutton);
            Allpage.Children.Remove(Savegamebutton);
            Allpage.Children.Remove(Loadgamebutton);
        }
    }   
    
}


