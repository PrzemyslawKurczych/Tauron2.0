using System;
using System.Collections.Generic;
using System.Linq;
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
using System.IO;
using System.Text.RegularExpressions;

namespace Tauron.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Window1 winOPTIONS = new Window1();
            this.Icon = BitmapFrame.Create(new Uri("images.ico", UriKind.RelativeOrAbsolute));
            imageGEOROX.Source = new BitmapImage(new Uri(System.IO.Path.Combine(Environment.CurrentDirectory, "logo.png")));
            string fileCONFIG = "config.cfg";
            if (System.IO.File.Exists(fileCONFIG))
            {

            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Przed przystąpieniem do konwersji pliku zdefiniuj ścieżki dostępu." + Environment.NewLine + "Czy chcesz przejść do opcji?", "Brak konfiguracji", MessageBoxButton.YesNo);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        winOPTIONS.Show();
                        break;
                    case MessageBoxResult.No:
                        break;

                }
            }


        }

        private void bOPTIONS_Click(object sender, RoutedEventArgs e)
        {
            Window1 winOPTIONS = new Window1();
            winOPTIONS.Show();
        }

        private void bEXIT_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        public void bCREATE_Click(object sender, RoutedEventArgs e)
        {
            string pathDEKODER = null;
            string pathBAZA = null;
            bool checkYEAR = true;
            string fileCONFIG = "config.cfg";
            string pointCODE = null;
            if (Convert.ToBoolean(rbSPD1.IsChecked))
            {
                pointCODE = "O";
            }
            else if (Convert.ToBoolean(rbSPD2.IsChecked))
            {
                pointCODE = "A";
            }
            else if (Convert.ToBoolean(rbSPD3.IsChecked))
            {
                pointCODE = "B";
            }
            else if (Convert.ToBoolean(rbSPD4.IsChecked))
            {
                pointCODE = "D";
            }
            else if (Convert.ToBoolean(rbSPD5.IsChecked))
            {
                pointCODE = "F";
            }
            else if (Convert.ToBoolean(rbSPD6.IsChecked))
            {
                pointCODE = "G";
            }
            else if (Convert.ToBoolean(rbSPD7.IsChecked))
            {
                pointCODE = "I";
            }
            else if (Convert.ToBoolean(rbSPD8.IsChecked))
            {
                pointCODE = "M";
            }
            else if (Convert.ToBoolean(rbSPD9.IsChecked))
            {
                pointCODE = "N";
            }
            else if (Convert.ToBoolean(rbSPD10.IsChecked))
            {
                pointCODE = "X";
            }

            if (System.IO.File.Exists(fileCONFIG))
            {
                string[] optionselements = File.ReadAllLines("config.cfg");
                string[] optionsset = optionselements[0].Split(';');
                pathDEKODER = optionsset[0];
                pathBAZA = optionsset[1];
                checkYEAR = Convert.ToBoolean(optionsset[2]);
            }
            else
            {
                MessageBox.Show("Utwórz ścieżki dostępu do dekodera i bazy");
                Window1 winOPTIONS = new Window1();
                winOPTIONS.Show();
            }
            if (checkYEAR)
            {
                try
                {
                    string[] tbNRSPLIT = tbNR.Text.Split('-');
                    string fileYEAR = tbNRSPLIT[1];
                    pathBAZA = pathBAZA + "\\" + fileYEAR;
                }
                catch (Exception exc)
                {
                    Console.WriteLine(exc.Message);
                    Console.ReadKey();
                }
            }
            string fileDEKODER = pathDEKODER + "\\" + tbNR.Text + "\\pikiety_z_kodami.txt";
            if (System.IO.File.Exists(fileDEKODER))
            {
                List<List<string>> pointsOLD = new List<List<string>>();
                string[] linePOINTSOLD = File.ReadAllLines(fileDEKODER);
                for (int i = 0; i < linePOINTSOLD.Length; ++i)
                {
                    pointsOLD.Add(new List<string>());
                    foreach (string pointELEMENT in Regex.Split(linePOINTSOLD[i], @"\s+"))
                    {
                        pointsOLD[i].Add(pointELEMENT);
                    }
                }
                string newfilename = null;
                if (Directory.Exists(pathBAZA + "\\" + tbNR.Text+"\\Branzowe\\"))
                {
                    
                    if (tbNEWFILENAME != null)
                    {
                        newfilename =  pathBAZA + "\\" + tbNR.Text + "\\Branzowe\\" + tbNEWFILENAME.Text + ".txt";
                    }
                    else
                    {
                        newfilename =  pathBAZA + "\\" + tbNR.Text + "\\Branzowe\\" + tbNR.Text + ".txt";
                    }
                }
                else
                {
                    if (tbNEWFILENAME != null)
                    {
                        newfilename = pathDEKODER + "\\" + tbNR.Text  + tbNEWFILENAME.Text + ".txt";
                    }
                    else
                    {
                        newfilename = pathDEKODER + "\\" + tbNR.Text  + tbNR.Text + ".txt";
                    }

                }
                FileStream newfileSTREAM = new FileStream(newfilename, FileMode.Create, FileAccess.Write);
                StreamWriter newfileWRITE = new StreamWriter(newfileSTREAM);
                for (int i = 0; i < linePOINTSOLD.Length; ++i)
                {
                    string newfileLine = String.Format("{0} {1} {2} {3} {4} {5}", pointsOLD[i][1], pointsOLD[i][3], pointsOLD[i][2], pointsOLD[i][4], tbKERG.Text, pointCODE);
                    newfileWRITE.WriteLine(newfileLine);

                }
                newfileWRITE.Close();
                MessageBox.Show("Plik został przystosowany do wymagań TAURON i zapisany w lokalizacji " + newfilename);
                tbNR.Clear();
                tbKERG.Clear();
                tbNEWFILENAME.Clear();
            }
        }
        private void tbNEWFILENAME_TextChanged(object sender, TextChangedEventArgs e)
        {
            string newfilenametext = tbNEWFILENAME.Text;
            string forbiddentextelement = "\\";
            bool forbiddenvalue = newfilenametext.Contains(forbiddentextelement);
            if (forbiddenvalue == true)
            {
                MessageBox.Show("Znak '\\' nie może być zawarty w nazwie pliku", "Błąd nazwy pliku");
                tbNEWFILENAME.Clear();
            }
        }
    }
}