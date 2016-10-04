using System;
using System.Collections.Generic;
using System.IO;
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
using System.Xml.Serialization;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            List<string> list = new List<string>() { "a", "ab", "abc", "abcd", "b", "c", "d" };
            var result =
                (from n in list
                 where n.Contains("bc")
                 select n.Length)
                .Sum();
            Random random = new Random();
            var result2 =
                from n in Enumerable.Range(0, 10000)
                 select new MyItem() { Age = random.Next(10, 100), Name = Guid.NewGuid().ToString() };
            new XmlSerializer(typeof(List<MyItem>)).Serialize(new FileStream("large_file.xml", FileMode.Create), result2.ToList());
            result2.ForEach(r => richTextBox.AppendText(Environment.NewLine + r.ToString()));
        }
    }
    
    public class MyItem
    {
        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute]
        public int Age { get; set; }
    }

    public static class MyEnumertable
    {
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
            {
                action(item);
            }
        }
    }
}
