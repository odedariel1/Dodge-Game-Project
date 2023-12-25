using DodgeGame_Project.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DodgeGame_Project
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Game b;
        public MainPage()
        {
            this.InitializeComponent();
            ApplicationView.GetForCurrentView().TryEnterFullScreenMode();
            Window.Current.Content = GamePage;
            b = new Game(GamePage);
        }

        private void StartAndPause_Click(object sender, RoutedEventArgs e)
        {
            if (b.Boardgame.IsPaused == false)
            {
                b.Boardgame.IsPaused = true;
                b.Timer.Stop();
            }
            else
            {
                b.Boardgame.IsPaused = false;
                b.Timer.Start();
            }

        }
        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            b.NewGame();
        }
        private void SaveGame_Click(object sender, RoutedEventArgs e)
        {
            b.SaveGame();
        }
        private void LoadGame_Click(object sender, RoutedEventArgs e)
        {
            b.LoadGame();
        }
        private void ExitGame_Click(object sender, RoutedEventArgs e)
        {
            CoreApplication.Exit();
        }

       
    }
}
