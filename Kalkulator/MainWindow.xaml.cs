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

namespace Kalkulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double _currentValue = 0;
        private string _currentOperator = "";
        private bool _isNewEntry = true;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Number_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string number = button.Content.ToString();

            if (_isNewEntry)
            {
                if(int.TryParse(number, out int num)) 
                {
                        Display.Text = number;
                        _isNewEntry = false;
                        History.Text += number;
                                   
                }
                
            }
            else if (double.TryParse(Display.Text, out double result))
            {
                if(Display.Text.Contains(","))
                {
                    Display.Text += number;
                    History.Text += number;
                }
                else if (result != 0)
                {
                    Display.Text += number;
                    History.Text += number;
                }
            }
        }

        private void Dot_Click(object sender, RoutedEventArgs e)
        {
            if (_isNewEntry)
            {
                Display.Text = "0,";
                _isNewEntry = false;
                if (_currentOperator == "")
                {
                    History.Text = "0,";
                }
                else
                {
                    History.Text += "0,";
                }
            }
            else if (!Display.Text.Contains(","))
            {
                Display.Text += ",";
                History.Text += ",";
            }
        }

        private void Operator_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string op = button.Content.ToString();

            if (_isNewEntry == false)
            {
                if (double.TryParse(Display.Text, out double result))
                {
                    if (_currentOperator != "")
                    {
                        Equals_Click(sender, e);
                        _isNewEntry = false;
                        if (double.TryParse(Display.Text, out double result2))
                        {
                            _currentValue = result2;
                        }
                        if (Display.Text == "Błąd, nie dzielimy przez 0")
                        {
                            History.Text = History.Text.Substring(0, History.Text.Length - 1);
                        }
                        _currentOperator = op;
                        History.Text += op;
                        _isNewEntry = true;
                    }
                    else
                    {
                        _currentValue = result;
                        _currentOperator = op;
                        _isNewEntry = true;
                        History.Text += op;
                    }
                }
            }
        }


        private void Equals_Click(object sender, RoutedEventArgs e)
        {
            if (_isNewEntry == false)
            {
                if (!double.TryParse(Display.Text, out double secondValue))
                    return;

                double result = _currentValue;

                switch (_currentOperator)
                {
                    case "+":
                        result += secondValue;
                        break;
                    case "−":
                        result -= secondValue;
                        break;
                    case "×":
                        result *= secondValue;
                        break;
                    case "÷":
                        if (secondValue != 0)
                            result /= secondValue;
                        else
                        {
                            Display.Text = "Błąd, nie dzielimy przez 0";
                            History.Text = History.Text.Substring(0, History.Text.Length - 1);
                            _isNewEntry = true;
                            return;
                        }
                        break;
                }

                Display.Text = result.ToString();
                History.Text = result.ToString();
                _currentOperator = "";
                _isNewEntry = true;
            }
            
        }


        private void ClearAll_Click(object sender, RoutedEventArgs e)
        {
            double t = 0;
            Display.Clear();
            Display.Text += t;
            _currentValue = 0;
            _currentOperator = "";
            _isNewEntry = true;
            History.Clear();
        }

        private void ClearEntry_Click(object sender, RoutedEventArgs e)
        {
            if (!_isNewEntry)
            {
                int length = Display.Text.Length;
                History.Text = History.Text.Substring(0, History.Text.Length - length);
                double t = 0;
                Display.Clear();
                Display.Text += t;
                _isNewEntry = true;
            }
           
            
        }

        private void Backspace_Click(object sender, RoutedEventArgs e)
        {
            if (!_isNewEntry && Display.Text.Length > 0)
            {
                Display.Text = Display.Text.Substring(0, Display.Text.Length - 1);
                History.Text = History.Text.Substring(0, History.Text.Length - 1);
                if (Display.Text == "")
                {
                    double t = 0;
                    Display.Clear();
                    Display.Text += t;
                    _isNewEntry = true;
                }
            }
        }


        private void PlusMinus_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(Display.Text, out double value))
            {
                if (_isNewEntry == false)
                {
                    if (value != 0)
                    {
                        int length = Display.Text.Length;
                        value *= -1;
                        //_currentValue = value;
                        Display.Text = value.ToString();

                        History.Text = History.Text.Substring(0, History.Text.Length - length);
                        History.Text += value;
                    }
                        
                }

                
            }
        }

        private void Percent_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(Display.Text, out double value))
            {
                if (_currentOperator != "" && _isNewEntry == false)
                {
                    int length= Display.Text.Length;
                    value = _currentValue * value / 100.0;
                    Display.Text = value.ToString();
                    History.Text = History.Text.Substring(0, History.Text.Length - length);
                    History.Text += value;
                }
                else if(_currentOperator=="" && _isNewEntry==false)
                {
                    // Jeśli nie wybrano operatora, pokazujemy "suchy" procent
                    value = 0;
                    Display.Text = value.ToString();
                    _isNewEntry = true;
                    History.Clear();
                }
            }
        }

        private void Sqrt_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(Display.Text, out double value))
            {
                if (value >= 0)
                {
                    if (_isNewEntry==false)
                    {
                        int length = Display.Text.Length;
                        value = Math.Sqrt(value);
                        Display.Text = value.ToString();
                        History.Text = History.Text.Substring(0, History.Text.Length - length);
                        History.Text += value;
                    }
                    /*else if (_isNewEntry==true && _currentOperator !="" ) 
                    {
                        int length = Display.Text.Length;
                        value = Math.Sqrt(value);
                        Display.Text = value.ToString();
                        History.Text = History.Text.Substring(0, History.Text.Length - (length-1));
                        History.Text += value;
                    }*/
                }
                else
                {
                    if (_isNewEntry == false)
                    {
                        int length = Display.Text.Length;
                        Display.Text = "Błąd - pierwiastek z l.ujemnej";
                        History.Text = History.Text.Substring(0, History.Text.Length - length);
                        _isNewEntry = true;
                    }
                }
            }
        }

        private void I_x(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(Display.Text, out double value))
            {

                if (_isNewEntry == false)
                {
                    int length = Display.Text.Length;
                    value = 1 / value;
                    Display.Text = value.ToString();
                    History.Text = History.Text.Substring(0, History.Text.Length - length);
                    History.Text += value;
                }

            }
        }



        //Obsługa klawiszy
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            // Obsługa cyfr
            if (e.Key >= Key.D0 && e.Key <= Key.D9)
            {
                string number = (e.Key - Key.D0).ToString();
                SimulateButtonClick(number);
            }
            else if (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
            {
                string number = (e.Key - Key.NumPad0).ToString();
                SimulateButtonClick(number);
            }
            // Obsługa operatorów
            else if (e.Key == Key.Add || e.Key == Key.OemPlus && (Keyboard.Modifiers & ModifierKeys.Shift) != 0)
                SimulateButtonClick("+");
            else if (e.Key == Key.Subtract || e.Key == Key.OemMinus)
                SimulateButtonClick("−");
            else if (e.Key == Key.Multiply)
                SimulateButtonClick("×");
            else if (e.Key == Key.Divide || e.Key == Key.Oem2)
                SimulateButtonClick("÷");
            // Enter = Equals
            else if (e.Key == Key.Enter || e.Key == Key.Return)
                SimulateButtonClick("=");
            // Kropka
            else if (e.Key == Key.OemComma || e.Key == Key.Decimal)
                SimulateButtonClick(".");
        }


        private void SimulateButtonClick(string content)
        {
            foreach (UIElement element in ((Grid)this.Content).Children)
            {
                if (element is Grid innerGrid)
                {
                    foreach (UIElement child in innerGrid.Children)
                    {
                        if (child is Button btn && btn.Content.ToString() == content)
                        {
                            btn.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                            return;
                        }
                    }
                }
            }
        }


    }
}