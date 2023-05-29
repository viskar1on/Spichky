using MatchGame.Data;
using MatchGame.Models;
using MatchGame.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MatchGame.ViewModels
{
    internal class GameViewModel : ViewModel
    {
        #region Field Observable Collection
        private ObservableCollection<FieldModel> f = new();
        public ObservableCollection<FieldModel> F { get => f; set => Set(ref f, value); }
        #endregion

        #region Field Observable Collection
        private ObservableCollection<FieldModel> f1 = new();
        public ObservableCollection<FieldModel> F1 { get => f1; set => Set(ref f1, value); }
        #endregion

        #region Field Observable Collection
        private ObservableCollection<FieldModel> f2 = new();
        public ObservableCollection<FieldModel> F2 { get => f2; set => Set(ref f2, value); }
        #endregion

        private int matches = 20;

        #region Step
        private string step;
        public string Step { get => step; set => Set(ref step, value); }
        #endregion

        private bool stepForFirst = false;
        public bool StepForFirst {
            get => stepForFirst;
            set {
                Step = stepForFirst ? "первый" : "второй";
                Set(ref stepForFirst, value);
            }
        }

        private int pickupCount;
        public int PickupCount { get => pickupCount; set => Set(ref pickupCount, value); }

        public GameViewModel()
        {
            StartGame();
        }

        public void StartGame()
        {
            Step = "первый";
            PickupCount = 0;
            GenerateField();
        }

        public void RestartGame()
        {
            MessageBoxResult mbr = MessageBox.Show("Начать заново?", "", MessageBoxButton.YesNo);
            if (mbr == MessageBoxResult.Yes)
            {
                StartGame();
            }
            else {
                Application.Current.Shutdown();
            }
        }

        public void MatchClick(FieldModel match)
        {

            if(match.Image == ImagesPaths.Match)
            {
                if (pickupCount < 3)
                {
                    match.Image = ImagesPaths.None;
                    PickupCount++;
                }

            } else
            {
                if (pickupCount > 0)
                {
                    match.Image = ImagesPaths.Match;
                    PickupCount--;
                }
            }
        }

        public void Accept()
        {
            if (PickupCount < 1) return;

            if (StepForFirst)
            {
                int c = GetMatchCount(f1);

                for (int i = 0; i < PickupCount + c; i++)
                {
                    f1[i].Image = ImagesPaths.Match;
                }
            }
            else {
                int c = GetMatchCount(f2);

                for (int i = 0; i < PickupCount + c; i++)
                {
                    f2[i].Image = ImagesPaths.Match;
                }
            }

            PickupCount = 0;
            if (GetMatchCount(f) < 1)
            {
                MessageBox.Show(StepForFirst ? "Выйграл первый игрок" : "Выйграл второй игрок");
                RestartGame();
            }
            else {
                StepForFirst = !StepForFirst;
            }

        }

        public int GetMatchCount(ObservableCollection<FieldModel> f)
        {
            int c = 0;
            for (int i = 0; i < matches; i++) {
                if (f[i].Image == ImagesPaths.Match)
                    c++;
            }
            return c;
        }

        public void GenerateField()
        {
            f.Clear();
            f1.Clear();
            f2.Clear();
            for (int i = 0; i < matches; i++)
            {
                f.Add(new FieldModel() { I = i, Image = ImagesPaths.Match });
                f1.Add(new FieldModel() { I = i, Image = ImagesPaths.None });
                f2.Add(new FieldModel() { I = i, Image = ImagesPaths.None });
            }
        }
    }
}
