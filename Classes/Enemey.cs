using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace DodgeGame_Project.Classes
{
    internal class Enemey
    {
        //fields
        private Storyboard _rotation = new Storyboard();
        Canvas allpage;
        private Image enemyview;
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
        public Image Enemyview
        {
            get
            {
                return this.enemyview;
            }
            set
            {
                this.enemyview = value;
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
        public Enemey(Canvas canvas,double topPosition, double leftPosition)
        {
            Allpage= canvas;
            Enemyview = new Image();
            Enemyview.Source = new BitmapImage(new Uri("ms-appx:///Assets/UltimateShuriken.png"));
            Enemyview.Width = 80;
            Enemyview.Height = 80;
            Canvas.SetZIndex(Enemyview, 1);
            TopPosition = topPosition;
            Canvas.SetTop(Enemyview, TopPosition);
            LeftPosition = leftPosition;
            Canvas.SetLeft(Enemyview, LeftPosition);
            Isalive = true;
            SpinEffect();
            Allpage.Children.Add(Enemyview);
        }

        //functions that add spin effect to the enemy
        public void SpinEffect()
        {
            Enemyview.Projection = new PlaneProjection();
            DoubleAnimation animation = new DoubleAnimation
            {
                From = 0.0,
                To = 360.0,
                BeginTime = new TimeSpan(0, 0, 0, 0, 10),
                RepeatBehavior = RepeatBehavior.Forever
            };
            Storyboard.SetTarget(animation, Enemyview);
            Storyboard.SetTargetProperty(animation, "(UIElement.Projection).(PlaneProjection.Rotation" + "Z" + ")");
            _rotation.Children.Clear();
            _rotation.Children.Add(animation);
            _rotation.Begin();
        }
        //functions that stop spin effect of the enemy
        public void StopSpining()
        {
            _rotation.Stop();
        }
        //functions that start spin effect of the enemy
        public void StartSpining()
        {
            _rotation.Begin();
        }
        //function that remove the enemy from the canvas
        public void RemoveEnemy()
        {
            Allpage.Children.Remove(Enemyview);
            isalive= false;
        }
    }
}
