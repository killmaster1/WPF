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

namespace WPF
{
    /// <summary>
    /// Interaction logic for ChildWindow.xaml
    /// </summary>
    public partial class ChildWindow : Window
    {
        public CheckBox[,] chb;
        private DataClass data;
        public ChildWindow(DataClass data_)
        {
            data = data_;
            InitializeComponent();
            myGrid.ShowGridLines = true;
            foreach (UcitelClass u in data.uc)
            {
                combo1.Items.Add(u.Name);
            }
            chb = new CheckBox[14, 5];
            CheckBox pom;
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
                        pom = new CheckBox();
                        Grid.SetRow(pom, i);
                        Grid.SetColumn(pom, j);
                        myGrid.Children.Add(pom);
                        chb[i - 1, j - 1] = pom;
                    }
                }

            /*ColumnDefinition colDef1 = new ColumnDefinition();
            ColumnDefinition colDef2 = new ColumnDefinition();
            myGrid.ColumnDefinitions.Add(colDef1);
            myGrid.ColumnDefinitions.Add(colDef2);
            RowDefinition rowDef1 = new RowDefinition();
            RowDefinition rowDef2 = new RowDefinition();
            myGrid.RowDefinitions.Add(rowDef1);
            myGrid.RowDefinitions.Add(rowDef2);
            TextBlock txt2 = new TextBlock();
            txt2.Text = "Quarter 1";
            txt2.FontSize = 12;
            txt2.FontWeight = FontWeights.Bold;
            Grid.SetRow(txt2, 1);
            Grid.SetColumn(txt2, 0);
            myGrid.Children.Add(txt2);*/
            
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < data.uc.Count; i++)
            {
                if (combo1.SelectedValue.ToString() == data.uc[i].Name)
                {
                    for (int k = 0; k < 14; k++)
                    {
                        for (int l = 0; l < 5; l++)
                        {
                            data.uc[i].nope[k, l] = new Boolean();
                            data.uc[i].nope[k, l] = chb[k, l].IsChecked.Value;
                        }
                    }
                }
            }
            this.Close();
        }
    }
}
