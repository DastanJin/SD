using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace SD
{
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
      this.ResizeMode = ResizeMode.NoResize;
      CloseBtn.IsChecked = Properties.Settings.Default.CloseCheckBox;
    }
    public void Shutdown(double hours)
    {
      ProcessStartInfo process = new("shutdown", $"/s /t {hours * 3600}")
      {
        WindowStyle = ProcessWindowStyle.Hidden,
        CreateNoWindow = true
      };
      Process.Start(process);
      if (CloseBtn.IsChecked == true) Close();
    }
    public void CancelShutdown()
    {
      ProcessStartInfo process = new("shutdown", "-a")
      {
        WindowStyle = ProcessWindowStyle.Hidden,
        CreateNoWindow = true
      };
      Process.Start(process);
      if (CloseBtn.IsChecked == true) Close();
    }

    private void Plus_Click(object sender, RoutedEventArgs e)
    {
      if (GetHoursValue() >= 24) return;

      Hours.Content = $"{GetHoursValue() + 0.5:0.0} h";
    }


    public double GetHoursValue() => double.TryParse(s: Hours.Content.ToString()?.Split(" ").FirstOrDefault() ?? "1", out double d) ? d : 1;

    private void Minus_Click(object sender, RoutedEventArgs e)
    {
      if (GetHoursValue() == 0.5) return;

      Hours.Content = $"{GetHoursValue() - 0.5:0.0} h";
    }

    private void Cancel_Click(object sender, RoutedEventArgs e) => CancelShutdown();

    private void Shutdown_Click(object sender, RoutedEventArgs e)
    {
      double h = GetHoursValue();
      Shutdown(h);
    }

    private void Close_Click(object sender, RoutedEventArgs e)
    {
      Properties.Settings.Default.CloseCheckBox = CloseBtn.IsChecked == true;
      Properties.Settings.Default.Save();
    }
    private void Button_MouseEnter(object sender, MouseEventArgs e)
    {
      Button button = (Button)sender;
      button.Background = new SolidColorBrush(Color.FromArgb(100, 100, 20, 20)); // Set a lighter background color
    }

    private void Button_MouseLeave(object sender, MouseEventArgs e)
    {
      Button button = (Button)sender;
      button.Background = new SolidColorBrush(Color.FromArgb(100, 20, 20, 20)); // Restore the original background color
    }

    public new void Close() => base.Close();

  }
}
