using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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

namespace WTF.AppMain
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
    }
    public partial class test : ObservableRecipient
    {
        [ObservableProperty]
        private FlowDocument document1 = new FlowDocument();

        public test()
        {
            Run run = new Run() { Text = "hello", Foreground = new SolidColorBrush(Color.FromArgb(0, 255, 0, 0)) };

            Paragraph myParagraph = new Paragraph();
            myParagraph.Inlines.Add(run);
            Document1.Blocks.Add(myParagraph);
        }

        [RelayCommand]
        public void Run() {

            Run run = new Run() { Text = "hello", Foreground = new SolidColorBrush(Color.FromArgb(0, 255, 0, 0)) };

            Paragraph myParagraph = new Paragraph();
            myParagraph.Inlines.Add(run);
            Document1.Blocks.Add(myParagraph);


        }
    }
    public class Stextbox : RichTextBox
    {


        public new FlowDocument Document
        {
            get { return (FlowDocument)GetValue(DocumentProperty); }
            set { SetValue(DocumentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Document.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DocumentProperty =
            DependencyProperty.Register("Document", typeof(FlowDocument), typeof(Stextbox), new FrameworkPropertyMetadata(null, new PropertyChangedCallback(OnDucumentChanged)));
        private static void OnDucumentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RichTextBox rtb = (RichTextBox)d;
            rtb.Document = (FlowDocument)e.NewValue;
        }
    }
}