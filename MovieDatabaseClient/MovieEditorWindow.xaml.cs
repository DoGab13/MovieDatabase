using System;
using System.Collections.Generic;
using System.Linq;
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

namespace MovieDatabaseClient
{
    /// <summary>
    /// Interaction logic for MovieEditorWindow.xaml
    /// </summary>
    public partial class MovieEditorWindow : Window
    {
        public MovieEditorWindow()
        {
            InitializeComponent();
        }
        public Movie? Movie { get; set; }
        private void Saveclick(object sender, RoutedEventArgs e)
        {
            Movie = new Movie();
            Movie.Title = TB_Title.Text.Trim();
            Movie.Star = TB_Star.Text.Trim();
            Movie.Category = TB_Category.Text.Trim();
            Movie.Type = TB_Type.Text.Trim();
            Movie.Year = DP_Year.SelectedDate;
            DialogResult = true;
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if(Movie != null) 
            {
                TB_Title.Text = Movie.Title;
                TB_Star.Text = Movie.Star;
                TB_Category.Text = Movie.Category;
                TB_Type.Text = Movie.Type;
                DP_Year.SelectedDate = Movie.Year;
            }
        }

        private void Property_Changed(object sender, EventArgs e)
        {
            BTN_Save.IsEnabled = !string.IsNullOrWhiteSpace(TB_Title.Text) && !string.IsNullOrWhiteSpace(TB_Star.Text) && !string.IsNullOrWhiteSpace(TB_Category.Text) && !string.IsNullOrWhiteSpace(TB_Type.Text) && DP_Year.SelectedDate != null;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape: Close(); break;
                case Key.Enter: Saveclick(sender, e); break;    
            }
        }

        private void TB_GotFocus(object sender, RoutedEventArgs e)
        {
            if(sender is TextBox tb)
            tb.CaretIndex = tb.Text.Length;
        }
    }
}
