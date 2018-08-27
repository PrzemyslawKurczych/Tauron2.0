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
using System.Windows.Shapes;

using Microsoft.Win32;    //FileDialog
using WinForms = System.Windows.Forms;   //FlderDialog
using System.IO;  //Folder, Directory
using System.Diagnostics;    //Debug.WrtieLine

namespace Tauron.App
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            string fileCONFIG = "config.cfg";
            if (System.IO.File.Exists(fileCONFIG))
            {
                string [] optionselements  = File.ReadAllLines("config.cfg");
                string [] optionsset = optionselements[0].Split(';');
                    tbDEKODER.Text = optionsset[0];
                    tbDEKODER.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF3B811B"));
                    tbBAZA.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF3B811B"));
                    tbBAZA.Text = optionsset[1];
                    cbBAZAYEAR.IsChecked = Convert.ToBoolean(optionsset[2]);
            }
               
        }

        
        
private void bDEKODER_Click(object sender, RoutedEventArgs e)
        {


            WinForms.FolderBrowserDialog folderDialogDEKODER = new WinForms.FolderBrowserDialog();
            folderDialogDEKODER.ShowNewFolderButton = false;
            folderDialogDEKODER.SelectedPath = System.AppDomain.CurrentDomain.BaseDirectory;
            WinForms.DialogResult resultDEKODER = folderDialogDEKODER.ShowDialog();
            if (resultDEKODER == WinForms.DialogResult.OK)
            {
                // Selected Folder
                //Selected Path
                String sPathDekoder = folderDialogDEKODER.SelectedPath;
                tbDEKODER.Text = sPathDekoder;
                tbDEKODER.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF3B811B"));

            }
        }

        private void bBAZA_Click(object sender, RoutedEventArgs e)
        {
            WinForms.FolderBrowserDialog folderDialogBAZA = new WinForms.FolderBrowserDialog();
            folderDialogBAZA.ShowNewFolderButton = false;
            folderDialogBAZA.SelectedPath = System.AppDomain.CurrentDomain.BaseDirectory;
            WinForms.DialogResult resultBAZA = folderDialogBAZA.ShowDialog();
            if (resultBAZA == WinForms.DialogResult.OK)
            {
                // Selected Folder
                //Selected Path
                String sPathDekoder = folderDialogBAZA.SelectedPath;
                tbBAZA.Text = sPathDekoder;
                tbBAZA.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF3B811B"));

            }
        }

        private void bBACK_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void bSAVE_Click(object sender, RoutedEventArgs e)
        {
            FileStream fs = new FileStream("config.cfg", FileMode.Create, FileAccess.Write);
            try
            {
                StreamWriter swCONFIG = new StreamWriter(fs);
                swCONFIG.WriteLine(tbDEKODER.Text + ";" + tbBAZA.Text + ";" + cbBAZAYEAR.IsChecked);
                
                swCONFIG.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }
            this.Close();
        }
    }
}
