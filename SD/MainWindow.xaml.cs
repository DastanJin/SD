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
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
      this.ResizeMode = ResizeMode.NoResize;
    }
    public static void Shutdown(int hours)
    {
      ProcessStartInfo process = new("shutdown", $"/s /t {hours * 3600}")
      {
        WindowStyle = ProcessWindowStyle.Hidden,
        CreateNoWindow = true
      };
      Process.Start(process);
    }
    public static void CancelShutdown()
    {
      ProcessStartInfo process = new("shutdown", "-a")
      {
        WindowStyle = ProcessWindowStyle.Hidden,
        CreateNoWindow = true
      };
      Process.Start(process);
    }

    private void Plus_Click(object sender, RoutedEventArgs e)
    {
      if (GetHoursValue() >= 24) return;

      Hours.Content = $"{GetHoursValue() + 1} h";
    }


    public int GetHoursValue() => int.Parse(s: Hours.Content.ToString().Split(" ").FirstOrDefault());

    private void Minus_Click(object sender, RoutedEventArgs e)
    {
      if (GetHoursValue() == 1) return;

      Hours.Content = $"{GetHoursValue() - 1} h";
    }

    private void Cancel_Click(object sender, RoutedEventArgs e) => CancelShutdown();

    private void Shutdown_Click(object sender, RoutedEventArgs e)
    {
      int h = GetHoursValue();
      Shutdown(h);
      if (Close.IsChecked == true) Close();
    }
  }
}
