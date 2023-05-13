using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using System.Drawing;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Input;
using System.Numerics;
using System.Threading;

namespace MoskoiBoi
{

    interface IPlaying
    {
        public void GameDave(Grid grid);
        public void CreateShips(Grid grid, System.Windows.Point pt, int ships);
        public bool GuningPlayer(Grid grid, System.Windows.Point pt);
        public void Guning(Grid grid);
        public void OtrisovkaShipsPlayer(Grid grid);
        public void GenerationOfShips();
        public void OtrisovkaShipsBots(Grid grid);
        public bool CheckInPeresechenie(Grid grid, System.Windows.Point pt);

    }

    class Playing_Field : IPlaying
    {
        public static IPlaying Context()
        {
            Playing_Field playing_ = new Playing_Field();
            return playing_;
        }
        public static int[,] array = new int[10, 10];
        public static int[,] arrayBot = new int[10, 10];
        public static int[,] arrayHisortBot = new int[10, 10];
        public static int[,] arrayHisortPlayer = new int[10, 10];
        public Playing_Field()
        {
        }
        public void GameDave(Grid grid)
        {
            for (byte i = 0; i <= 10; i++)

            {
                //построение горизонтальные и вертикальные линии на поле игрока

                Line y = new Line();

                y.X1 = i * 40;

                y.X2 = i * 40;

                y.Y1 = 0;

                y.Y2 = grid.Width;

                y.Stroke = Brushes.Gray;
                grid.Children.Add(y);



                Line x = new Line();

                x.X1 = 0;

                x.X2 = grid.Width;

                x.Y1 = i * 40;

                x.Y2 = i * 40;

                x.Stroke = Brushes.Gray;

                grid.Children.Add(x);



                //строит горизонтальные линии на поле компьютера

                Line y_с = new Line();

                y_с.X1 = i * 40;

                y_с.X2 = i * 40;

                y_с.Y1 = 0;

                y_с.Y2 = grid.Width;

                y_с.Stroke = Brushes.Black;

                grid.Children.Add(y_с);


                Line x_с = new Line();

                x_с.X1 = 0;

                x_с.X2 = grid.Width;

                x_с.Y1 = i * 40;

                x_с.Y2 = i * 40;

                x_с.Stroke = Brushes.Black;

                grid.Children.Add(x_с);
            }
        } // Отрисовка сетки

        private void OtrisovkaKorabl(double x, double y, Grid grid) // Отрисовка выбора корабля
        {
            System.Windows.Shapes.Rectangle cube = new System.Windows.Shapes.Rectangle();
            Thickness mrgn = new Thickness(x, y, 0, 0);
            Image img = new Image();
            img.Margin = mrgn;
            img.Source = new BitmapImage(new Uri("D.png", UriKind.Relative));
            img.Width = 38;
            img.Height = 38;
            grid.Children.Add(img);
        }
        public void OtrisovkaPopal(double x, double y, Grid grid)
        {
            Thickness mrgn = new Thickness(x, y, 0, 0);
            Image img = new Image();
            img.Margin = mrgn;
            img.Source = new BitmapImage(new Uri("krest.png", UriKind.Relative));
            img.Width = 40;
            img.Height = 40;
            grid.Children.Add(img);
        }
        public void OtrisovkaPromox(double x, double y, Grid grid) // Отрисовка промоха
        {
            Thickness mrgn = new Thickness(x, y, 0, 0);
            Image img = new Image();
            img.Margin = mrgn;
            img.Source = new BitmapImage(new Uri("tockha.png", UriKind.Relative));
            img.Width = 35;
            img.Height = 35;
            grid.Children.Add(img);
        }
        public void OtrisovkaShipsPlayer(Grid grid)
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (array[i, j] == 1)
                    {
                        int x_k = 0, y_k = 0;
                        x_k = -360 + i * 80;
                        y_k = -360 + j * 80;
                        OtrisovkaKorabl(x_k, y_k, grid);
                    }
                }
            }
        }

        public void OtrisovkaShipsBots(Grid grid)
        {
            Random random = new Random();
            tp:
            int j = 2;
            int kolvo = 1;
            for (int i = 0; i <= j;)
            {
                if (i < j)
                {
                    while (!CheckingShipsBot(random.Next(1, 8), random.Next(1, 8), kolvo, grid))
                    {
                        
                    }
                    i++;
                }
                else
                {
                    kolvo++;
                    j--;
                    i = 0;
                }
            }

        }

        public void ClearShips(double x, double y, Grid grid)
        {
            Brush color = new SolidColorBrush(Colors.Gray);

            Canvas ell = new Canvas();

            Thickness mrgn = new Thickness(x, y, 0, 0);

            ell.Margin = mrgn;

            ell.Width = 39;

            ell.Height = 39;

            ell.Background = color;
            grid.Children.Add(ell);
        }




        private int game_zone(int x, int y, int a) // Заполнение массива при выборе кораблей
        {
            array[x, y] = a;
            return a;
        }


        private int game_zone_bot(int x, int y, int a = 1) // Заполнение массива для бота
        {
            arrayBot[x, y] = a;
            return a;
        }

        private bool check_massiv_bot(int x, int y, int a = 1)
        {
            if (arrayBot[x, y] == a)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool GuningPlayer(Grid grid, System.Windows.Point pt) // Ход игрока
        {
            int b = 0;
            int x = Convert.ToInt32(pt.X), y = Convert.ToInt32(pt.Y);
            int x_k = 0, y_k = 0;
            x = (x - (int)grid.Margin.Left) / 40;
            y = (y - (int)grid.Margin.Top) / 40;
            x_k = -360 + x * 80;
            y_k = -360 + y * 80;

            if (arrayBot[x, y] == 1)
            {
                OtrisovkaPopal(x_k, y_k, grid);
                return true;
            }
            else
            {
                OtrisovkaPromox(x_k, y_k, grid);
                return false;
            }
        }
        public void CreateShips(Grid grid, System.Windows.Point pt, int ships) // Создание кораблей
        {
            int x = Convert.ToInt32(pt.X), y = Convert.ToInt32(pt.Y);
            int x_k = 0, y_k = 0;
            x = (x - (int)grid.Margin.Left) / 40;
            y = (y - (int)grid.Margin.Top) / 40;
            x_k = -360 + x * 80;
            y_k = -360 + y * 80;
            if (array[x, y] != 1)
            {
                if (CheckingShips(x, y, ships, grid))
                {
                    game_zone(x, y, 1);
                    //OtrisovkaKorabl(x_k, y_k, grid);
                }
            }
            else
            {
                ClearShips(x_k, y_k, grid);
                game_zone(x, y, 0);
            }

        }
        public static int[] ships = new int[4]; 
        private bool CheckingShips(int x, int y, int paluba, Grid grid)
        {
            try
            {
                switch (paluba)
                {
                    case 1:
                        if (array[x + 1, y] != 1 && array[x + 1, y + 1] != 1 && array[x, y + 1] != 1 && array[x - 1, y] != 1 && array[x - 1, y + 1] != 1 && array[x + 1, y - 1] != 1 && array[x - 1, y - 1] != 1 && array[x, y - 1] != 1 && ships[paluba] < 4)
                        {
                            ships[paluba]++;
                            game_zone(x, y, 1);
                            int x_k = -360 + x * 80;
                            int y_k = -360 + y * 80;
                            OtrisovkaKorabl(x_k, y_k, grid);
                            return true;
                        }
                        break;
                    case 2:
                        if (array[x + 1, y] != 1 && array[x + 1, y + 1] != 1 && array[x, y + 1] != 1 && array[x - 1, y] != 1 && array[x - 1, y + 1] != 1 && array[x + 1, y - 1] != 1 && array[x - 1, y - 1] != 1 && array[x, y - 1] != 1 && ships[paluba] < 3)
                        {
                            int x_k = -360 + x * 80;
                            int y_k = -360 + y * 80;
                            game_zone(x, y, 1);
                            OtrisovkaKorabl(x_k, y_k, grid);
                            if (array[x + 1, y] != 1 && array[x + 1, y + 1] != 1 && array[x, y + 1] != 1 && array[x - 1, y] != 1 && array[x - 1, y + 1] != 1 && array[x + 1, y - 1] != 1 && array[x - 1, y - 1] != 1 && array[x, y - 1] != 1)
                            {
                                ships[paluba]++;
                                y++;
                                y_k = -360 + y * 80;
                                game_zone(x, y, 1);
                                OtrisovkaKorabl(x_k, y_k, grid);
                                return true;
                            }
                        }
                        break;
                    case 3:
                        bool isValid = false;
                        if ((y == 9 && x == 9) || (y == 9 && x == 0))
                        {
                            MessageBox.Show(" ");
                            return false;
                        }

                        if (x == 9)
                        {
                            // если горизонтально, то ошибка
                            if (array[x - 1, y] != 1 && array[x - 1, y + 1] != 1 && ships[paluba] < 2)
                            {
                                isValid = true;
                                int x_k = -360 + x * 80;
                                int y_k = -360 + y * 80;
                                game_zone(x, y, 1);
                                OtrisovkaKorabl(x_k, y_k, grid);
                                if (array[x, y + 1] != 1 && array[x - 1, y] != 1 && array[x - 1, y + 1] != 1 && array[x - 1, y - 1] != 1 && array[x, y - 1] != 1)
                                {
                                    y++;
                                    y_k = -360 + y * 80;
                                    game_zone(x, y, 1);
                                    OtrisovkaKorabl(x_k, y_k, grid);
                                    if (array[x, y + 1] != 1 && array[x - 1, y] != 1 && array[x - 1, y + 1] != 1 && array[x - 1, y - 1] != 1)
                                    {
                                        ships[paluba]++;
                                        y++;
                                        y_k = -360 + y * 80;
                                        game_zone(x, y, 1);
                                        OtrisovkaKorabl(x_k, y_k, grid);
                                    }
                                }
                            }
                        }
                        if (!isValid && (array[x + 1, y] != 1 && array[x + 1, y + 1] != 1 && array[x - 1, y] != 1 && array[x - 1, y + 1] != 1 && array[x + 1, y - 1] != 1 && array[x - 1, y - 1] != 1 && array[x, y - 1] != 1 && array[x, y + 1] != 1 && ships[paluba] < 2))
                        {
                            int x_k = -360 + x * 80;
                            int y_k = -360 + y * 80;
                            game_zone(x, y, 1);
                            OtrisovkaKorabl(x_k, y_k, grid);
                            if (array[x + 1, y] != 1 && array[x + 1, y + 1] != 1 && array[x, y + 1] != 1 && array[x - 1, y] != 1 && array[x - 1, y + 1] != 1 && array[x - 1, y - 1] != 1 && array[x, y - 1] != 1)
                            {
                                y++;
                                y_k = -360 + y * 80;
                                game_zone(x, y, 1);
                                OtrisovkaKorabl(x_k, y_k, grid);
                                if (array[x + 1, y] != 1 && array[x + 1, y + 1] != 1 && array[x, y + 1] != 1 && array[x - 1, y] != 1 && array[x - 1, y + 1] != 1 && array[x - 1, y - 1] != 1 && array[x + 1, y - 1] != 1)
                                {
                                    ships[paluba]++;
                                    y++;
                                    y_k = -360 + y * 80;
                                    game_zone(x, y, 1);
                                    OtrisovkaKorabl(x_k, y_k, grid);
                                }
                            }
                        }
                        break;
                }
                return false;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }
        public static int[] shipsBot = new int[4];
        private bool CheckingShipsBot(int x, int y, int paluba, Grid grid)
        {
            try
            {
                switch (paluba)
                {
                    case 1:
                        if (arrayBot[x + 1, y] != 1 && arrayBot[x + 1, y + 1] != 1 && arrayBot[x, y + 1] != 1 && arrayBot[x - 1, y] != 1 && arrayBot[x - 1, y + 1] != 1 && arrayBot[x + 1, y - 1] != 1 && arrayBot[x - 1, y - 1] != 1 && arrayBot[x, y - 1] != 1 && shipsBot[paluba] < 4)
                        {
                            shipsBot[paluba]++;
                            game_zone_bot(x, y, 1);
                            int x_k = -360 + x * 80;
                            int y_k = -360 + y * 80;
                            OtrisovkaKorabl(x_k, y_k, grid);
                            //MessageBox.Show("OK");
                            return true;
                        }
                        break;
                    case 2:
                        if (arrayBot[x + 1, y] != 1 && arrayBot[x + 1, y + 1] != 1 && arrayBot[x, y + 1] != 1 && arrayBot[x - 1, y] != 1 && arrayBot[x - 1, y + 1] != 1 && arrayBot[x + 1, y - 1] != 1 && arrayBot[x - 1, y - 1] != 1 && arrayBot[x, y - 1] != 1 && shipsBot[paluba] < 3)
                        {
                            int x_k = -360 + x * 80;
                            int y_k = -360 + y * 80;
                            game_zone_bot(x, y, 1);
                            OtrisovkaKorabl(x_k, y_k, grid);
                            if (arrayBot[x + 1, y] != 1 && arrayBot[x + 1, y + 1] != 1 && arrayBot[x, y + 1] != 1 && arrayBot[x - 1, y] != 1 && arrayBot[x - 1, y + 1] != 1 && arrayBot[x + 1, y - 1] != 1 && arrayBot[x - 1, y - 1] != 1 && arrayBot[x, y - 1] != 1)
                            {
                                shipsBot[paluba]++;
                                y++;
                                y_k = -360 + y * 80;
                                game_zone_bot(x, y, 1);
                                OtrisovkaKorabl(x_k, y_k, grid);
                                return true;
                            }
                        }
                        break;
                    case 3:

                        if (arrayBot[x + 1, y] != 1 && arrayBot[x + 1, y + 1] != 1 && arrayBot[x - 1, y] != 1 && arrayBot[x - 1, y + 1] != 1 && arrayBot[x + 1, y - 1] != 1 && arrayBot[x - 1, y - 1] != 1 && arrayBot[x, y - 1] != 1 && arrayBot[x, y + 1] != 1 && shipsBot[paluba] < 2)
                        {
                            int x_k = -360 + x * 80;
                            int y_k = -360 + y * 80;
                            game_zone_bot(x, y, 1);
                            OtrisovkaKorabl(x_k, y_k, grid);
                            if (arrayBot[x + 1, y] != 1 && arrayBot[x + 1, y + 1] != 1 && arrayBot[x, y + 1] != 1 && arrayBot[x - 1, y] != 1 && arrayBot[x - 1, y + 1] != 1 && arrayBot[x - 1, y - 1] != 1 && arrayBot[x, y - 1] != 1)
                            {
                                y++;
                                y_k = -360 + y * 80;
                                game_zone_bot(x, y, 1);
                                OtrisovkaKorabl(x_k, y_k, grid);
                                if (arrayBot[x + 1, y] != 1 && arrayBot[x + 1, y + 1] != 1 && arrayBot[x, y + 1] != 1 && arrayBot[x - 1, y] != 1 && arrayBot[x - 1, y + 1] != 1 && arrayBot[x - 1, y - 1] != 1 && arrayBot[x + 1, y - 1] != 1)
                                {
                                    shipsBot[paluba]++;
                                    y++;
                                    y_k = -360 + y * 80;
                                    game_zone_bot(x, y, 1);
                                    OtrisovkaKorabl(x_k, y_k, grid);
                                }
                            }
                        }
                        break;
                    default:
                        return false;
                }
                return false;
            }
            catch(Exception e)
            {
                //MessageBox.Show(e.Message);
                return false;
            }
        }


        // Генерация кораблей для компьютера
        //  однопалуб = 4
        //  двухпалуб = 3
        //  трехпалуб = 2
        //  четырехпалуб = 1
        public void GenerationOfShips()
        {
         //   MessageBox.Show("Генерация корабликов");
         //   Random random = new Random();
         //   //Однопалубные = 4
         //   for (int i = 0; i < 4; i++)
         //   {
         //       int rand, rand2;
         //       rand = random.Next(0, 10);
         //       rand2 = random.Next(0, 10);
         //       if (check_massiv_bot(rand, rand2))
         //       {
         //           game_zone_bot(rand, rand2, 1);
         //       }
         //   }
         //   //двухпалуб = 3
         //   for (int i = 0; i < 3; i++)
         //   {
         //       int rand, rand2;
         //       int ranoms = random.Next(1, 4);
         //       rand = random.Next(0, 10);
         //       rand2 = random.Next(rand, 10);
         //       for (int j = 0; j < 2; j++)
         //       {
         //           if (check_massiv_bot(rand, rand2))
         //           {
         //               game_zone_bot(rand, rand2, 1);
         //           }
         //           if (ranoms == 1)
         //           {
         //               rand2++;
         //           }
         //           else if (ranoms == 2)
         //           {
         //               rand2 = rand2 - 2;
         //           }
         //           else if (ranoms == 3)
         //           {
         //
         //           }
         //       }
         //   }
        }
        public int prov(int x, int y, int b, int vert)
        {
            int otv = 0;
            if (vert == 0)
            {
                switch (b)

                {

                    case 1: if (array[x, y] != 0) otv = 1; break;

                    case 2: if (array[x, y] != 0 || array[x + 1, y] != 0) otv = 1; break;

                    case 3: if (array[x, y] != 0 || array[x + 1, y] != 0 || array[x + 2, y] != 0) otv = 1; break;

                    case 4: if (array[x, y] != 0 || array[x + 1, y] != 0 || array[x + 2, y] != 0 || array[x + 3, y] != 0) otv = 1; break;

                    default: otv = 0; break;

                }

            }

            else

            {

                switch (b)

                {

                    case 1: if (array[x, y] != 0) otv = 1; break;

                    case 2: if (array[x, y] != 0 || array[x, y + 1] != 0) otv = 1; break;

                    case 3: if (array[x, y] != 0 || array[x, y + 1] != 0 || array[x, y + 2] != 0) otv = 1; break;

                    case 4: if (array[x, y] != 0 || array[x, y + 1] != 0 || array[x, y + 2] != 0 || array[x, y + 3] != 0) otv = 1; break;

                    default: otv = 0; break;
                }
            }
            return otv;
        }
        public void CorrectnesShips()
        {

        }

        public void ChekingDeadShips(Grid grid)
        {
            //   int x = 2;
            //   int y = 2;
            //   int x_k = 0, y_k = 0;
            //   if (arrayHisortPlayer[x,y] == 1 && arrayBot[x,y] == 1)
            //   {
            //       x--;
            //       x_k = -360 + x * 80;
            //       y_k = -360 + y * 80;
            //       for (int i = 0; i < 8; i++)
            //       {
            //           OtrisovkaPromox(x_k,y_k, grid);
            //       }
            //   }
        }

        public void Guning(Grid grid)
        {
            Random rand = new Random();
            int x = rand.Next(0, 10);
            int y = rand.Next(0, 10);
            Thread.Sleep(600);
            int x_k = 0, y_k = 0;
            x_k = -360 + x * 80;
            y_k = -360 + y * 80;

            if (arrayHisortBot[x, y] == 0)
            {
                if (array[x, y] == 1)
                {
                    OtrisovkaPopal(x_k, y_k, grid);
                    MessageBox.Show("Компьютер попал! Он ходит еще раз!");
                    Guning(grid);
                    arrayHisortBot[x, y] = 2;
                }
                else
                {
                    OtrisovkaPromox(x_k, y_k, grid);
                    arrayHisortBot[x, y] = 1;
                }
            }
        }

        // Хранение убитых кораблей
        public void StorageOfDeadShips()
        {

        }

        // Хранение информации о ходах
        public void StoringInformationAboutMoves(int x, int y)
        {
            //  array[]
        }

        // Проверка количества кораблей
        public void CheckingTheNumberOfShips()
        {

        }

        public bool CheckInPeresechenie(Grid grid, System.Windows.Point pt)
        {
            int x = Convert.ToInt32(pt.X), y = Convert.ToInt32(pt.Y);
            int x_k = 0, y_k = 0;
            x = (x - (int)grid.Margin.Left) / 40;
            y = (y - (int)grid.Margin.Top) / 40;
            x_k = -360 + x * 80;
            y_k = -360 + y * 80;

            if (arrayHisortBot[x, y] == 0)
            {
                if (arrayBot[x, y] == 0)
                {
                    arrayHisortBot[x, y] = 2;
                    return true;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                MessageBox.Show("Уже были тута");
                return false;
            }
        }
    }
}
