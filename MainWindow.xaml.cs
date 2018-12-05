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

        public MainWindow()
        {
            InitializeComponent();
        }

        private void radioButton_Checked(object sender, RoutedEventArgs e)
        {
            searchByFilm = true;
        }

        private void radioButton1_Checked(object sender, RoutedEventArgs e)
        {
            searchByFilm = false;
        }

        private void radioButton2_Checked(object sender, RoutedEventArgs e)
        {
            searchByImdb = true;
        }

        private void radioButton3_Checked(object sender, RoutedEventArgs e)
        {
            searchByImdb = false;
        }

        private void Button1(object sender, RoutedEventArgs e)
        {
            list.Items.Add(film.Title);
        }

        private void Button2(object sender, RoutedEventArgs e)
        {
            try
            {
                //string index = list.SelectedItem.ToString();
                list.Items.Remove(list.SelectedItems[0]);
            }
            catch
            {
                MessageBox.Show("No item selected for deletion");
            }
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
                        film = film.SearchTitle(textBox1.Text);
                    }
                    else
                    {
                        film = film.SearchID(textBox1.Text);
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
                }
                else
                {
                    if(searchByFilm == true)
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

        public ImdbEntity SearchTitle(string searchText)
        {
            filmNameModified = searchText.Replace(" ", "+");

            string response = httpClient.GetStringAsync(new Uri("http://www.omdbapi.com/?t=" + filmNameModified + "&apikey=8727e147")).Result;
            ImdbEntity omdb = new JavaScriptSerializer().Deserialize<ImdbEntity>(response);

            if (omdb.Title == null)
            {
                omdb.Title = "No such film exists";
            }

            return omdb;
        }

        public ImdbEntity SearchID(string filmID)
        {
            string response = httpClient.GetStringAsync(new Uri("http://www.omdbapi.com/?i=" + filmID + "&apikey=8727e147")).Result;
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

/*<Window x:Class="ACME_Movie_Client.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:ACME_Movie_Client"
        mc:Ignorable="d"
        Title="ACME Movie Client" Height="500" Width="1000">
    <Grid>
        <!--<TextBlock x:Name="textBlock" HorizontalAlignment="Left" TextWrapping="Wrap" Text="Select a message option and then choose the Display button" VerticalAlignment="Top" Margin="107,10,0,0"/>-->
        <RadioButton x:Name="radioButton" GroupName="search" Content="Search by title&#xD;&#xA;" HorizontalAlignment="Left" VerticalAlignment="Top" Margin="762,5,0,0" Checked="radioButton_Checked"  IsChecked="True"/>
        <RadioButton x:Name="radioButton1" GroupName="search" Content="Search by ID&#xD;&#xA;" HorizontalAlignment="Left" VerticalAlignment="Top" Margin="875,5,0,0" Checked="radioButton1_Checked"/>
        <RadioButton x:Name="radioButton2" GroupName="database" Content="Search by Imdb&#xD;&#xA;" HorizontalAlignment="Left" VerticalAlignment="Top" Margin="762,24,0,0" Checked="radioButton2_Checked"  IsChecked="True" Height="26"/>
        <RadioButton x:Name="radioButton3" GroupName="database" Content="Search by Tmdb&#xD;&#xA;" HorizontalAlignment="Left" VerticalAlignment="Top" Margin="875,24,0,0" Checked="radioButton3_Checked"/>
        <!--<Button x:Name="button" Content="Display" HorizontalAlignment="Left" VerticalAlignment="Top" Width="75" Margin="207,79,0,0" Click="button_Click"/>-->
        <!--<ListBox x:Name="list" HorizontalAlignment="Left" VerticalAlignment="Top" Width="75" Margin="207,79,0,0"/>-->
        <StackPanel>
            <TextBlock Height="20" Margin="10,0,673,0" RenderTransformOrigin="0.336,0.9"><Run Text="Type some text into the TextBox and press the Enter key."/></TextBlock>
            <TextBox Height="20" x:Name="textBox1" KeyDown="OnKeyDownHandler" Margin="0,0,243,0"/>
            <TextBlock Width="1250" Height="20" x:Name="textBlock1"/>
            <TextBlock Width="1250" Height="20" x:Name="textBlock2"/>
            <TextBlock Width="1250" Height="20" x:Name="textBlock3"/>
            <TextBlock Width="1250" Height="20" x:Name="textBlock4"/>
            <TextBlock Width="1250" Height="20" x:Name="textBlock5"/>
            <TextBlock Width="1250" Height="20" x:Name="textBlock6"/>
            <TextBlock Width="1250" Height="20" x:Name="textBlock7"/>
            <TextBlock Width="1250" Height="20" x:Name="textBlock8"/>
            <TextBlock Width="1250" Height="20" x:Name="textBlock9"/>
            <TextBlock Width="1250" Height="20" x:Name="textBlock10"/>
            <TextBlock Width="1250" Height="20" x:Name="textBlock11"/>
            <TextBlock Width="1250" Height="20" x:Name="textBlock12"/>
            <TextBlock Width="1250" Height="20" x:Name="textBlock13"/>
            <TextBlock Width="1250" Height="20" x:Name="textBlock14"/>
            <TextBlock Width="1250" Height="20" x:Name="textBlock15"/>
            <TextBlock Width="1250" Height="20" x:Name="textBlock16"/>
            <TextBlock Width="1250" Height="20" x:Name="textBlock17"/>
            <TextBlock Width="1250" Height="20" x:Name="textBlock18"/>
        </StackPanel>

        <ListBox Name="list" Margin="624,351,10,28">

        </ListBox>

        <Button x:Name="button2" Click="Button2" Content="Delete favourite" Margin="880,41,10,403" RenderTransformOrigin="0.5,0.5"></Button>
        <Button x:Name="button1" Click="Button1" Content="Add favourite" Margin="762,41,120,403"/>
    </Grid>
</Window>*/