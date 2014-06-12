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
            g_pom = 0;
        }
//-----------------------------------------------------------------------------------------------------------
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //load sched
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
            combo1.Items.Clear();
            foreach (UcitelClass u in data.uc)
            {
                combo1.Items.Add(u.Name);
            }
        }
//-----------------------------------------------------------------------------------------------------------
        private void createTable()
        {
            tabulka.RowGroups.Add(new TableRowGroup());
            tabulka.RowGroups[0].Rows.Add(new TableRow());
            TableRow currentRow = tabulka.RowGroups[0].Rows[0];

            currentRow.Background = Brushes.Silver;
            currentRow.FontSize = 12;
            currentRow.FontWeight = System.Windows.FontWeights.Bold;

            // Add the header row with content, 
            pom = new Run("Time\\Day");

            currentRow.Cells.Add(new TableCell(new Paragraph(pom)));
            pom = new Run("Monday");
            currentRow.Cells.Add(new TableCell(new Paragraph(pom)));
            pom = new Run("Tuesday");
            currentRow.Cells.Add(new TableCell(new Paragraph(pom)));
            pom = new Run("Wednesday");
            currentRow.Cells.Add(new TableCell(new Paragraph(pom)));
            pom = new Run("Thursday");
            currentRow.Cells.Add(new TableCell(new Paragraph(pom)));
            pom = new Run("Friday");
            currentRow.Cells.Add(new TableCell(new Paragraph(pom)));


            DateTime dateN = new DateTime(2000, 1, 1, 19, 10, 00);
            int i = 0;
            sched = new Run[14, 5];
            for (DateTime date1 = new DateTime(2000, 1, 1, 8, 10, 00);
                date1 < dateN;
                date1 = date1.AddMinutes(50))
            {
                TableRow tr = new TableRow();
                tr.FontSize = 10;
                if (i % 2 == 1)
                {
                    tr.Background = Brushes.Silver;
                }

                tabulka.RowGroups[0].Rows.Add(tr);
                pom = new Run(date1.ToString("HH:mm"));
                tr.Cells.Add(new TableCell(new Paragraph(pom)));
                for (int j = 0; j < 5; j++)
                {
                    sched[i, j] = new Run();
                    TableCell asd = new TableCell(new Paragraph(sched[i,j]));
                    tr.Cells.Add(asd);
                    showInTable(i, j, i.ToString() + " " + j.ToString(), true);
                }
                i++;//toto az na koniec
            }
        }
//-----------------------------------------------------------------------------------------------------------
        private void showInTable(int cas,int den,string predmet,bool ok)
        {
            //int pom = Constants.PREDMET_LENGTH;
            //int n = pom-predmet.Length;
                if (ok)
                {
                    if (cas % 2 == 1)
                    {
                        tabulka.RowGroups[0].Rows[cas+1].Cells[den+1].Background = Brushes.Silver;
                    }
                    else
                    {
                        tabulka.RowGroups[0].Rows[cas + 1].Cells[den + 1].Background = Brushes.White;
                    }
                    sched[cas, den].Text = predmet;
                }
                else
                {
                    tabulka.RowGroups[0].Rows[cas + 1].Cells[den + 1].Background = Brushes.Red;
                    sched[cas, den].Text = predmet;
                }
        }
//-----------------------------------------------------------------------------------------------------------
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //add req for teacher
            if (combo1.SelectedIndex == -1)
            {
                return;
            }
            string pom = combo1.SelectedValue.ToString();
            Console.WriteLine("bla:"+pom);

            Window a = new ChildWindow(data.findUcitel(pom));
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
            Console.WriteLine("pom je " + pom);
            g_pom = pom;
            if (data.uc.Count <= pom)
            {
                return;    // ziadas zleho ucitela
            }
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
            showTeacher(g_pom);
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            //unload schedule

            data = new DataClass();
            combo1.Items.Clear();
        }
    }
}
