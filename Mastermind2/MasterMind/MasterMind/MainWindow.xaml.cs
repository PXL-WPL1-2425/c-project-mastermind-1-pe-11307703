using System.Drawing;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Mastermind
{

    public partial class MainWindow : Window
    {
        private List<string> secretCode;
        private List<string> colors = new List<string> { "Red", "Yellow", "Orange", "White", "Green", "Blue" };
        int attempts = 1;

        public MainWindow()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            secretCode = new List<string>();
            Random color = new Random();
            secretCode.Clear();
            for (int i = 0; i < 4; i++)
            {
                int colorNumber = color.Next(1, 7);
                secretCode.Add(colors[colorNumber - 1]);
            }
            Toggledebug();

        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            ComboBoxItem selectedItem = comboBox.SelectedValue as ComboBoxItem;
            string selectedColor = selectedItem.Content.ToString();

            if (sender == comboBox1 && comboBox1.SelectedItem != null)
            {
                ComboBoxPicker(comboBox1, kleur1, selectedColor);
            }
            if (sender == comboBox2 && comboBox2.SelectedItem != null)
            {
                ComboBoxPicker(comboBox2, kleur2, selectedColor);
            }
            if (sender == comboBox3 && comboBox3.SelectedItem != null)
            {
                ComboBoxPicker(comboBox3, kleur3, selectedColor);
            }
            if (sender == comboBox4 && comboBox4.SelectedItem != null)
            {
                ComboBoxPicker(comboBox4, kleur4, selectedColor);
            }
        }

        private void ComboBoxPicker(ComboBox combobox, Label labelToColor, string selectedColor)
        {
            SolidColorBrush color = ColorFromString(selectedColor);
            labelToColor.Background = color;
        }

        private SolidColorBrush ColorFromString(string color)
        {
            SolidColorBrush mediaColor = Brushes.Transparent; 
            switch (color) 
            { case "Red": 
                    mediaColor = Brushes.Red; 
                    break; 
              case "Orange":
                    mediaColor = Brushes.Orange; 
                    break; 
              case "White": 
                    mediaColor = Brushes.White; 
                    break; 
              case "Green":
                    mediaColor = Brushes.Green;
                    break; 
              case "Blue":
                    mediaColor = Brushes.Blue; 
                    break; 
              case "Yellow": 
                    mediaColor = Brushes.Yellow;
                    break; 
            }
            return mediaColor;
        }

        private void ControlButton_Click(object sender, RoutedEventArgs e)
        {
            
            
            


            List<string> playerGuess = new List<string>
        {
            (comboBox1.SelectedItem as ComboBoxItem)?.Content.ToString(),
            (comboBox2.SelectedItem as ComboBoxItem)?.Content.ToString(),
            (comboBox3.SelectedItem as ComboBoxItem)?.Content.ToString(),
            (comboBox4.SelectedItem as ComboBoxItem)?.Content.ToString()
        };
            if (playerGuess.Contains(null))
            {
                MessageBox.Show("Selecteer vier kleuren!","Foutief",MessageBoxButton.YesNo);
                
                return;
            }
            ResetLabelBorders();
            

            int correctPosition = 0;
            int correctColor = 0;

            List<string> tempSecretCode = new List<string>(secretCode);
            List<string> tempPlayerGuess = new List<string>(playerGuess);

            for (int i = 0; i < 4; i++)
            {
                if (tempPlayerGuess[i] == tempSecretCode[i])
                {
                    correctPosition++;
                    tempSecretCode[i] = null;
                    tempPlayerGuess[i] = null;

                    Label label = GetLabelByIndex(i);
                    label.BorderBrush = Brushes.DarkRed;
                    label.BorderThickness = new Thickness(6);
                    
                }
               
            }
            
            for (int i = 0; i < 4; i++)
            {
                if (tempPlayerGuess[i] != null && tempSecretCode.Contains(tempPlayerGuess[i]))
                {
                    correctColor++;
                    tempSecretCode[tempSecretCode.IndexOf(tempPlayerGuess[i])] = null;

                    Label label = GetLabelByIndex(i);
                    label.BorderBrush = Brushes.Wheat;
                    label.BorderThickness = new Thickness(6);
                }
                
            }
           

            if (tempPlayerGuess != null && tempSecretCode != (tempPlayerGuess))
            {
                attempts += 1;
            }


                if (correctPosition == 4)
            {
                MessageBox.Show("Gefeliciteerd! Je hebt de code gekraakt!", "Gewonnen");
                attempts += 1;
                InitializeGame();
                ResetAllColors();

            }
            
           




        }

        private void ResetAllColors()
        {
            kleur1.Background = Brushes.Transparent;
            kleur2.Background = Brushes.Transparent;
            kleur3.Background = Brushes.Transparent;
            kleur4.Background = Brushes.Transparent;
        }

        private void ResetLabelBorders()
        {
            kleur1.BorderBrush = Brushes.Transparent;
            kleur2.BorderBrush = Brushes.Transparent;
            kleur3.BorderBrush = Brushes.Transparent;
            kleur4.BorderBrush = Brushes.Transparent;

            kleur1.BorderThickness = new Thickness(0);
            kleur2.BorderThickness = new Thickness(0);
            kleur3.BorderThickness = new Thickness(0);
            kleur4.BorderThickness = new Thickness(0);
        }

        private Label GetLabelByIndex(int index)
        {
            return index switch
            {
                0 => kleur1,
                1 => kleur2,
                2 => kleur3,
                3 => kleur4,
                _ => null
            };
        }

        private void Toggledebug()
        {
            cheatCode.Text = $"{secretCode}";
            
        }

        
         private void cheatCode_KeyDown(object sender, KeyEventArgs e)
         {
            if (e.KeyboardDevice.Modifiers == ModifierKeys.Control && e.Key == Key.F12)
            {
                
                cheatCode.Visibility = Visibility.Visible;
                
            }
         }
    }
    
}