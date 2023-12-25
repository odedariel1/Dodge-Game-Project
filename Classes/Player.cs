using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace DodgeGame_Project.Classes
{
    internal class Player
    {
        //fields
        Canvas allpage;
        private Image playerview;
        private double topPosition;
        private double leftPosition;
        private bool isalive;
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
        public Image Playerview
        {
            get
            {
                return this.playerview;
            }
            set
            {
                this.playerview = value;
            }
        }
        public double TopPosition
        {
            get
            {
              return this.topPosition;
            }
            set
            {
                this.topPosition = value;
            }
        }
        public double LeftPosition
        {
            get
            {
                return this.leftPosition;
            }
            set
            {
                this.leftPosition = value;
            }
        }
        public bool Isalive
        {
            get
            {
                return this.isalive;
            }
            set
            {
                this.isalive = value;
            }
        }
        //constractor
        public Player(Canvas canvas,double topPosition, double leftPosition)
        {
            Allpage = canvas;
            Playerview = new Image();
            Playerview.Source = new BitmapImage(new Uri("ms-appx:///Assets/Character2.png"));
            Playerview.Width = 80;
            Playerview.Height = 100;
            Isalive = true;
            Canvas.SetZIndex(Playerview, 1);
            TopPosition = topPosition;
            Canvas.SetTop(Playerview, this.TopPosition);
            LeftPosition = leftPosition;
            Canvas.SetLeft(Playerview, this.LeftPosition);
            Allpage.Children.Add(Playerview);
        }
        //function to remove player
        public void PlayerRemove()
        {
            Allpage.Children.Remove(Playerview);
            Isalive= false;
        }
    }
}
