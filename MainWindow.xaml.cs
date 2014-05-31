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
using Microsoft.Win32;
using System.IO;
using System.Data;

namespace WPF
{
    static class Constants
    {
        public const int PREDMET_LENGTH = 72;
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Run pom;
        Run[,] sched;
        DataClass data;
        int g_pom;
        public MainWindow()
        {
            InitializeComponent();
            addButton.IsEnabled = false;
            data = new DataClass();
            createTable();
        }
//-----------------------------------------------------------------------------------------------------------
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            Nullable<bool> result = openFileDialog1.ShowDialog();
            if (result == true)
            {

                Console.WriteLine("Opening: " + openFileDialog1.FileName);
                data.loadFileData(openFileDialog1.FileName);
                addButton.IsEnabled = true;
            }
            updateCombo();
            
        }

        private void updateCombo()
        {
            foreach (UcitelClass u in data.uc)
            {
                combo1.Items.Add(u.Name);
            }
        }
//-----------------------------------------------------------------------------------------------------------
        private void createTable()
        {
            sched = new Run[14, 5];

            myGrid.ShowGridLines = true;

            RowDefinition rowDef1;
            for (int i = 0; i < 15; i++)
            {
                rowDef1 = new RowDefinition();
                myGrid.RowDefinitions.Add(rowDef1);
            }
            ColumnDefinition colDef1;
            for (int i = 0; i < 6; i++)
            {
                colDef1 = new ColumnDefinition();
                myGrid.ColumnDefinitions.Add(colDef1);
            }
            string[] arr = { "Time\\Day", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };
            TextBlock txt2;
            for (int i = 0; i < 6; i++)
            {
                txt2 = new TextBlock();
                txt2.Text = arr[i];
                Grid.SetRow(txt2, 0);
                Grid.SetColumn(txt2, i);
                myGrid.Children.Add(txt2);
            }
            DateTime date1 = new DateTime(2000, 1, 1, 8, 10, 00);
            for (int i = 1; i < 15; i++)
            {
                txt2 = new TextBlock();
                txt2.Text = date1.ToString("HH:mm");
                Grid.SetRow(txt2, i);
                Grid.SetColumn(txt2, 0);
                myGrid.Children.Add(txt2);
                date1 = date1.AddMinutes(50);
            }
            for (int i = 1; i < 15; i++)
            {
                for (int j = 1; j < 6; j++)
                {
                    sched[i-1, j-1] = new Run();
                    TextBlock blc = new TextBlock(sched[i-1, j-1]);
                    Grid.SetRow(blc, i);
                    Grid.SetColumn(blc, j);
                    myGrid.Children.Add(blc);
                }
            }
            
            /*for(int  k =0;k<15;k++){
                for (int l = 0; l < 6; l++)
                {
                    tabulka.RowGroups[0].Rows[k].Cells[l].Padding = new Thickness(13);
                }
            }*/
        }
//-----------------------------------------------------------------------------------------------------------
        private void showInTable(int cas,int den,string predmet,bool ok)
        {
            //int pom = Constants.PREDMET_LENGTH;
            //int n = pom-predmet.Length;
                if (ok)
                {
                    sched[cas, den].Text = predmet;
                }
                else
                {
                    sched[cas, den].Background = Brushes.Red;
                    sched[cas, den].Text = predmet;
                }
        }
//-----------------------------------------------------------------------------------------------------------
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //add req for teacher
            Window a = new ChildWindow(data.findUcitel(combo1.SelectedValue.ToString()));
            a.Owner = this;
            a.ShowDialog();
            showTeacher(g_pom);
        }
//-----------------------------------------------------------------------------------------------------------
        private void combo1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int pom = 0;
            string pom2 = ((ComboBox) sender).SelectedItem as string;
            for (int i = 0; i < data.uc.Count; i++)
            {
                if (data.uc[i].Name == pom2)
                {
                    pom = i;
                    break;
                }
            }
            showTeacher(pom);
        }
//-----------------------------------------------------------------------------------------------------------
        private void showTeacher(int pom)
        {
            g_pom = pom;
            for (int k = 0; k < 14; k++)
            {
                for (int l = 0; l < 5; l++)
                {
                    showInTable(k,l,data.uc[pom].pred[k,l],!data.uc[pom].nope[k,l]);
                }
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

            //export

            ExportReq.create(data.uc);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            //import

            ImportReq.load(data.uc);
        }
    }
}
