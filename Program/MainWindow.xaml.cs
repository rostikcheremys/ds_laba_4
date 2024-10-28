using System.Windows;

namespace Program
{
    public partial class MainWindow : Window
    {
        delegate double MathOp(double a, double b);

        public MainWindow()
        {
            InitializeComponent();
        }
        
        private double Add(double a, double b) => a + b;
        private double Divide(double a, double b) => b != 0 ? a / b : double.NaN;
        private double Multiply(double a, double b) => a * b;
        
        private void AddButton_Click(object sender, RoutedEventArgs e) => StartThreadOperation(Add);
        private void DivideButton_Click(object sender, RoutedEventArgs e) => StartThreadOperation(Divide);
        private void MultiplyButton_Click(object sender, RoutedEventArgs e) => StartThreadOperation(Multiply);
        
        private void StartThreadOperation(MathOp operation)
        {
            if (double.TryParse(InputA.Text, out double a) && double.TryParse(InputB.Text, out double b))
            {
                Thread thread = new Thread(() =>
                {
                    double result = operation(a, b);
                    Dispatcher.Invoke(() => ResultsBox.Text = $"{result:N}\n");
                });
                
                thread.Start();
            }
            else
            {
                MessageBox.Show("Введіть коректні числові значення для A та B!");
            }
        }
    }
}