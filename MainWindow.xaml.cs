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

namespace WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Run pom;
        Run[,] sched;
        DataClass data;
        public MainWindow()
        {
            InitializeComponent();
            addButton.IsEnabled = false;
            tabulka.RowGroups.Add(new TableRowGroup());
            tabulka.RowGroups[0].Rows.Add(new TableRow());
            TableRow currentRow = tabulka.RowGroups[0].Rows[0];
            data = new DataClass();

            // Global formatting for the title row.
            currentRow.Background = Brushes.Silver;
            currentRow.FontSize = 20;
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

            

            DateTime dateN = new DateTime(2000, 1, 1, 19, 10,00);
            int i = 0;
            sched = new Run[14, 5];
            for (DateTime date1 = new DateTime(2000, 1, 1, 8, 10,00);
                date1<dateN;
                date1=date1.AddMinutes(50))
            {
                TableRow tr = new TableRow();
                tabulka.RowGroups[0].Rows.Add(tr);
                pom = new Run(date1.ToString("HH:mm"));
                tr.Cells.Add(new TableCell(new Paragraph(pom)));
                //sched[0, 0]  = new Run("volaco");
                //tr.Cells.Add(new TableCell(new Paragraph(sched[0, 0])));
                for (int j = 0; j < 5; j++)
                {
                    sched[i, j] = new Run(date1.ToString(i.ToString() + " " + j.ToString()));
                    sched[i, j].Text = i.ToString() + " " + j.ToString();
                    tr.Cells.Add(new TableCell(new Paragraph(sched[i,j])));
                }
                i++;//toto az na koniec
            }
            
                //tabulka.RowGroups[0].Rows.Add(new TableRow());
            //currentRow = tabulka.RowGroups[0].Rows[1];
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Stream myStream = null;
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
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Window a = new ChildWindow(data);
            a.Owner = this;
            a.Show();
        }
    }
}
