using System.Windows;
using System.Windows.Controls;
using AutomationCommon;
using ObservableCollectionExtention;

namespace AutomationClient
{
    /// <summary>
    /// Interaction logic for SerachUserControl.xaml
    /// </summary>
    public partial class SerachUserControl : UserControl
    {
        public string Path { get; set; }

        public ObservableCollectionExt<string> _filteredList = new ObservableCollectionExt<string>();

        public SerachUserControl()
        {
            InitializeComponent();
            _filteredList = new ObservableCollectionExt<string>();
            DataContext = this;
        }

        private string _selectedFile;

        public string SelectedFile
        {
            get { return _selectedFile; }
            set
            {
                _selectedFile = value;
            }
        }

        public ObservableCollectionExt<string> FilteredList
        {
            get { return _filteredList; }
            set { _filteredList = value; }
        }

        private string _searchedFile;

        public string SearchedFile
        {
            get { return _searchedFile; }
            set { _searchedFile = value; }
        }

        public event RoutedEventHandler OpenSelectedFileClick;

        public event RoutedEventHandler RunFilterClick;

        private void clearBtn_Click(object sender, RoutedEventArgs e)
        {
            FilteredList.Clear();
        }

        private void scriptSerachBtn_Click(object sender, RoutedEventArgs e)
        {
            FilteredList.Clear();
            Path = StaticFields.SCRIPT_PATH;
            OpenFile();
        }

        private void testSerachBtn_Click(object sender, RoutedEventArgs e)
        {
            FilteredList.Clear();
            Path = StaticFields.TEST_PATH;
            OpenFile();
        }

        private void suiteSerachBtn_Click(object sender, RoutedEventArgs e)
        {
            FilteredList.Clear();
            Path = StaticFields.SUITE_PATH;
            OpenFile();
        }

        private void OpenFile()
        {
            _searchedFile = searchTxb.Text;
            if (RunFilterClick != null)
            {
                RunFilterClick(this, new RoutedEventArgs());
            }
        }

        private void searchesDataGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (OpenSelectedFileClick != null)
            {
                OpenSelectedFileClick(this, new RoutedEventArgs());
            }
        }
    }
}