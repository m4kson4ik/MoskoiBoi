using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace MoskoiBoi
{
    /// <summary>
    /// Логика взаимодействия для TheLayoutOfTheBattlefield.xaml
    /// </summary>
    public partial class TheLayoutOfTheBattlefield : Window
    {
        
        public TheLayoutOfTheBattlefield()
        {
            InitializeComponent();
            Playing_Field.Context().GameDave(gridShips);
        }

        private void gridShips_MouseMove(object sender, MouseEventArgs e)
        {
            //Point pt = e.GetPosition(this);//функция получения координат щелчка мыши
        }

        private void btGo_Click(object sender, RoutedEventArgs e)
        {

            Game game = new Game();
            game.Show();
        }

        private void gridShips_MouseUp(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Point pt = e.GetPosition(this);
            Playing_Field.Context().CreateShips(gridShips, pt, Convert.ToInt32(tbShips.Text));
        }

        private void lbOneShips_MouseUp(object sender, MouseButtonEventArgs e)
        {
            tbShips.Text = lbOneShips.Content.ToString();
        }

        private void lbFourShips_MouseUp(object sender, MouseButtonEventArgs e)
        {
            tbShips.Text = lbFourShips.Content.ToString();
        }

        private void lbTwoShips_MouseUp(object sender, MouseButtonEventArgs e)
        {
            tbShips.Text = lbTwoShips.Content.ToString();
        }

        private void lbThreeShips_MouseUp_1(object sender, MouseButtonEventArgs e)
        {
            tbShips.Text = lbThreeShips.Content.ToString();
        }
    }
}
