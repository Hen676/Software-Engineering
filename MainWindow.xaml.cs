using System;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Net.Http;
using System.Web.Script.Serialization;

using Newtonsoft.Json;
using TMDbLib.Client;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.Search;

namespace ACME_Movie_Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ImdbEntity film = new ImdbEntity();
        TmdbEntity film2 = new TmdbEntity();

        bool searchByFilm = true;
        bool searchByImdb = true;
        bool searchShortPlot = true;

        string[] name;
        string[] comment;
        int name_sum = 0;

        public MainWindow()
        {
            InitializeComponent();

            try
            {
                name = System.IO.File.ReadAllLines("Favorite_name.txt");
                name_sum = name.Length;

                int x = 0;
                while (x < name_sum)
                {
                    List.Items.Add(name[x]);
                    x++;
                }

            }
            catch { }
            try
            {
                comment = System.IO.File.ReadAllLines("Favorite_comment.txt");
            }
            catch { }
        }

        void Button3(object sender, RoutedEventArgs e)
        {
            if (button3.Content.ToString() == "Search by: Title")
            {
                button3.Content = "Search by: ID";
                searchByFilm = false;
            }
            else
            {
                button3.Content = "Search by: Title";
                searchByFilm = true;
            }
        }

        void Button4(object sender, RoutedEventArgs e)
        {
            if (button4.Content.ToString() == "Database: IMDB")
            {
                button4.Content = "Database: TMDB";
                searchByImdb = false;
            }
            else
            {
                button4.Content = "Database: IMDB";
                searchByImdb = true;
            }
        }

        void Button5(object sender, RoutedEventArgs e)
        {
            if (button5.Content.ToString() == "Short plot")
            {
                button5.Content = "Long plot";
                searchShortPlot = false;

            }
            else
            {
                button5.Content = "Short plot";
                searchShortPlot = true;
            }
        }

        void Button6(object sender, RoutedEventArgs e)
        {
            ImdbEntity movie = new ImdbEntity();
            Random rnd = new Random();
            bool repeatLoop = true;

            while (repeatLoop)
                if (searchByImdb == true)
                {
                    int one = rnd.Next(0, 10);
                    int two = rnd.Next(0, 10);
                    int three = rnd.Next(0, 10);
                    int four = rnd.Next(0, 10);
                    int five = rnd.Next(0, 10);
                    int six = rnd.Next(0, 10);
                    int seven = rnd.Next(0, 10);

                    string id = "tt" + one + two + three + four + five + six + seven;
                    movie = film.SearchID(id, searchShortPlot);

                    if (movie.Title != "No such ID exists")
                    {
                        textBox1.Text = id;

                        film = film.SearchID(id, searchShortPlot);

                        textBlock1.Text = "Title: " + film.Title;
                        textBlock2.Text = "Year: " + film.Year;
                        textBlock3.Text = "Rated: " + film.Rated;
                        textBlock3.Text = "Released: " + film.Released;
                        textBlock4.Text = "Runtime: " + film.Runtime;
                        textBlock5.Text = "Genre: " + film.Genre;
                        textBlock6.Text = "Director: " + film.Director;
                        textBlock7.Text = "Writer: " + film.Writer;
                        textBlock8.Text = "Actors: " + film.Actors;
                        textBlock9.Text = "Plot: " + film.Plot;
                        textBlock10.Text = "Language: " + film.Language;
                        textBlock11.Text = "Country: " + film.Country;
                        textBlock12.Text = "Awards: " + film.Awards;
                        textBlock13.Text = "Poster: " + film.Poster;
                        textBlock14.Text = "Metascore: " + film.Metascore;
                        textBlock15.Text = "imdb rating: " + film.imdbRating;
                        textBlock16.Text = "imdb votes: " + film.imdbVotes;
                        textBlock17.Text = "imdb ID: " + film.imdbID;
                        textBlock18.Text = "Type: " + film.Type;

                        repeatLoop = false;
                    }
                }
        }

        //private void radioButton_Checked(object sender, RoutedEventArgs e)
        //{
        //    searchByFilm = true;
        //}

        //private void radioButton1_Checked(object sender, RoutedEventArgs e)
        //{
        //    searchByFilm = false;
        //}

        //private void radioButton2_Checked(object sender, RoutedEventArgs e)
        //{
        //    searchByImdb = true;
        //}

        //private void radioButton3_Checked(object sender, RoutedEventArgs e)
        //{
        //    searchByImdb = false;
        //}

        private void Button1(object sender, RoutedEventArgs e)
        {
            string newString = textBlock1.Text;
            try
            {
                List.Items.Add(newString.Remove(0, 7));

                name = List.Items.OfType<string>().ToArray();

                System.IO.File.WriteAllLines("Favorite_name.txt", name);
            }
            catch
            {
                MessageBox.Show("No film to add");
            }

        }

        private void Button2(object sender, RoutedEventArgs e)
        {
            try
            {
                List.Items.Remove(List.SelectedItem);

                name = List.Items.OfType<string>().ToArray();

                System.IO.File.WriteAllLines("Favorite_name.txt", name);
            }
            catch
            {
                MessageBox.Show("No film selected for deletion");
            }

        }

        void PrintText(object sender, SelectionChangedEventArgs args)
        {
            try
            {
                string curItem = List.SelectedItem.ToString();

                film = film.SearchTitle(curItem, searchShortPlot);

                textBlock1.Text = "Title: " + film.Title;
                textBlock2.Text = "Year: " + film.Year;
                textBlock3.Text = "Rated: " + film.Rated;
                textBlock3.Text = "Released: " + film.Released;
                textBlock4.Text = "Runtime: " + film.Runtime;
                textBlock5.Text = "Genre: " + film.Genre;
                textBlock6.Text = "Director: " + film.Director;
                textBlock7.Text = "Writer: " + film.Writer;
                textBlock8.Text = "Actors: " + film.Actors;
                textBlock9.Text = "Plot: " + film.Plot;
                textBlock10.Text = "Language: " + film.Language;
                textBlock11.Text = "Country: " + film.Country;
                textBlock12.Text = "Awards: " + film.Awards;
                textBlock13.Text = "Poster: " + film.Poster;
                textBlock14.Text = "Metascore: " + film.Metascore;
                textBlock15.Text = "imdb rating: " + film.imdbRating;
                textBlock16.Text = "imdb votes: " + film.imdbVotes;
                textBlock17.Text = "imdb ID: " + film.imdbID;
                textBlock18.Text = "Type: " + film.Type;

                try
                {
                    wrapper.Children.Clear();
                    Image img = new Image();
                    BitmapImage image = new BitmapImage();
                    image.BeginInit();
                    image.UriSource = new Uri(film.Poster, UriKind.Absolute);
                    image.EndInit();

                    img.Source = image;
                    wrapper.Children.Add(img);
                }
                catch { }

            }
            catch { }
        }

        //private void button_Click(object sender, RoutedEventArgs e)
        //{
        //    if (radioButton.IsChecked == true)
        //    {
        //        MessageBox.Show("Hello.");
        //    }
        //    else if (radioButton1.IsChecked == true)
        //    {
        //        MessageBox.Show("Goodbye.");
        //    }
        //}

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                if (searchByImdb == true)
                {
                    if (searchByFilm == true)
                    {
                        try
                        {
                            film = film.SearchTitle(textBox1.Text, searchShortPlot);
                        }
                        catch
                        {
                            MessageBox.Show("Invalid film name");
                            return;
                        }
                    }
                    else
                    {
                        film = film.SearchID(textBox1.Text, searchShortPlot);
                    }
                    textBlock1.Text = "Title: " + film.Title;
                    textBlock2.Text = "Year: " + film.Year;
                    textBlock3.Text = "Rated: " + film.Rated;
                    textBlock3.Text = "Released: " + film.Released;
                    textBlock4.Text = "Runtime: " + film.Runtime;
                    textBlock5.Text = "Genre: " + film.Genre;
                    textBlock6.Text = "Director: " + film.Director;
                    textBlock7.Text = "Writer: " + film.Writer;
                    textBlock8.Text = "Actors: " + film.Actors;
                    textBlock9.Text = "Plot: " + film.Plot;
                    textBlock10.Text = "Language: " + film.Language;
                    textBlock11.Text = "Country: " + film.Country;
                    textBlock12.Text = "Awards: " + film.Awards;
                    textBlock13.Text = "Poster: " + film.Poster;
                    textBlock14.Text = "Metascore: " + film.Metascore;
                    textBlock15.Text = "imdb rating: " + film.imdbRating;
                    textBlock16.Text = "imdb votes: " + film.imdbVotes;
                    textBlock17.Text = "imdb ID: " + film.imdbID;
                    textBlock18.Text = "Type: " + film.Type;

                    try
                    {
                        wrapper.Children.Clear();
                        Image img = new Image();
                        BitmapImage image = new BitmapImage();
                        image.BeginInit();
                        image.UriSource = new Uri(film.Poster, UriKind.Absolute);
                        image.EndInit();

                        img.Source = image;
                        wrapper.Children.Add(img);
                    }
                    catch
                    {

                    }

                }
                else
                {
                    if (searchByFilm == true)
                    {
                        //Add search by film functionality
                    }
                    else
                    {
                        Movie desiredFilm = film2.SearchID(textBox1.Text);
                        textBlock1.Text = "Title: " + desiredFilm.Title;
                        textBlock2.Text = "Release date: " + desiredFilm.ReleaseDate.ToString();
                        textBlock3.Text = "Popularity: " + desiredFilm.Popularity.ToString();
                        textBlock4.Text = "Runtime: " + desiredFilm.Runtime.ToString();
                        textBlock5.Text = "Budget: " + desiredFilm.Budget.ToString();
                        textBlock6.Text = "Poster link: " + desiredFilm.PosterPath;
                        textBlock7.Text = "Revenue: " + desiredFilm.Revenue.ToString();
                        textBlock8.Text = "Overview: " + desiredFilm.Overview;
                        textBlock9.Text = "Original language: " + desiredFilm.OriginalLanguage;
                        textBlock10.Text = "Keywords: " + desiredFilm.Keywords;
                        textBlock11.Text = "Adult: " + desiredFilm.Adult.ToString();
                        textBlock12.Text = "Vote average: " + desiredFilm.VoteAverage.ToString();
                        textBlock13.Text = "Vote count: " + desiredFilm.VoteCount.ToString();
                        textBlock14.Text = "";
                        textBlock15.Text = "";
                        textBlock16.Text = "";
                        textBlock17.Text = "";
                        textBlock18.Text = "";
                    }
                }

            }
        }
    }
    public class ImdbEntity
    {
        HttpClient httpClient = new HttpClient();

        public string filmNameModified;

        public string Title;
        public string Year;
        public string Rated;
        public string Released;
        public string Runtime;
        public string Genre;
        public string Director;
        public string Writer;
        public string Actors;
        public string Plot;
        public string Language;
        public string Country;
        public string Awards;
        public string Poster;
        public string Metascore;
        public string imdbRating;
        public string imdbVotes;
        public string imdbID;
        public string Type;

        public ImdbEntity SearchTitle(string searchText, bool shortPlot)
        {
            filmNameModified = searchText.Replace(" ", "+");
            string response;

            if (shortPlot == true)
                response = httpClient.GetStringAsync(new Uri("http://www.omdbapi.com/?t=" + filmNameModified + "&apikey=8727e147")).Result;
            else
                response = httpClient.GetStringAsync(new Uri("http://www.omdbapi.com/?t=" + filmNameModified + "&plot=full&apikey=8727e147")).Result;

            ImdbEntity omdb = new JavaScriptSerializer().Deserialize<ImdbEntity>(response);

            if (omdb.Title == null)
            {
                omdb.Title = "No such film exists";
            }

            return omdb;
        }

        public ImdbEntity SearchID(string filmID, bool shortPlot)
        {
            string response;

            if (shortPlot == true)
                response = httpClient.GetStringAsync(new Uri("http://www.omdbapi.com/?i=" + filmID + "&apikey=8727e147")).Result;
            else
                response = httpClient.GetStringAsync(new Uri("http://www.omdbapi.com/?i=" + filmID + "&plot=full&apikey=8727e147")).Result;
            ImdbEntity omdb = new JavaScriptSerializer().Deserialize<ImdbEntity>(response);

            if (omdb.Title == null)
            {
                omdb.Title = "No such ID exists";
            }

            return omdb;
        }
    }

    class TmdbEntity
    {
        Movie movie;
        public Movie SearchID(string searchText)
        {
            try
            {
                int id = Convert.ToInt32(searchText);
                TMDbClient client = new TMDbClient("1206a7b3fff9fa9951f843442e161c6a");
                movie = client.GetMovieAsync(id).Result;
            }
            catch
            {
                MessageBox.Show("ID contains no letters");
            }
            return movie;
        }
    }
}