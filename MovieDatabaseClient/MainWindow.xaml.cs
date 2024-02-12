using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MovieDatabaseClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private const string MovieDBPath = "movie.csv";

        private void NewMovie_Click(object sender, RoutedEventArgs e)
        {
            MovieEditorWindow window = new MovieEditorWindow();
            if (window.ShowDialog() == true) 
            {
                LB_Movies.Items.Add(window.Movie);
                Save();
            }
        }

        private void DeleteMovie_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Biztos, hogy törölni szeretnéd a kiválasztott filmet?", "Film törlése", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
            if(result == MessageBoxResult.Yes)
            {
                LB_Movies.Items.Remove(LB_Movies.SelectedItem);
                Save();
            }

            
        }

        private void EditMovie_Click(object sender, RoutedEventArgs e)
        {
            if (LB_Movies.SelectedItem != null) 
            {
                MovieEditorWindow window = new MovieEditorWindow();
                window.Movie = (Movie)LB_Movies.SelectedItem;
                if (window.ShowDialog() == true) 
                {

                    int index = LB_Movies.SelectedIndex;
                    LB_Movies.Items.RemoveAt(index);
                    LB_Movies.Items.Insert(index, window.Movie);
                    Save();
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
                if (File.Exists(MovieDBPath))
                {
                    string[] Lines = File.ReadAllLines(MovieDBPath);
                foreach (string Line in Lines)
                    {
                    Movie movie = Movie.FromCsv(Line);
                    LB_Movies.Items.Add(movie);
                    }
                }
                else 
                {
                File.Create(MovieDBPath);
                }

        }

        private void Save() 
        {
            List<string> op = new List<string>();
            foreach (Movie item in LB_Movies.Items)
            {
                op.Add(item.ToCsv());
            }
            File.WriteAllLines(MovieDBPath, op);
        }

        private void LB_Movies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BTN_EditMovie.IsEnabled = LB_Movies.SelectedItem != null;
            BTN_DeleteMovie.IsEnabled = LB_Movies.SelectedItem != null;
        }

        private void LB_Movies_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            EditMovie_Click(sender, e);
        }

        private void LB_Movie_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                EditMovie_Click(sender, e);
            }
            else if (e.Key == Key.Delete)
            {
                DeleteMovie_Click(sender, e);
            }
            
        }

    }
}