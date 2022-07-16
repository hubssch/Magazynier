using System.Windows;
using Magazynier.DatabaseAccess;
using Magazynier.WinForms;

namespace Magazynier
{
    public partial class MainWindow : Window
    {
        MagDbContext db;

        public MainWindow()
        {
            InitializeComponent();

            db = new MagDbContext();
        }

        private void btn_NewDoc(object sender, RoutedEventArgs e)
        {
            DocumentForm documentWin = new DocumentForm(db);
            documentWin.ShowDialog();
        }

        private void btn_ListDoc(object sender, RoutedEventArgs e)
        {
            DocumentsForm documentsWin = new DocumentsForm(db);
            documentsWin.ShowDialog();
        }

        private void btn_StoreData(object sender, RoutedEventArgs e)
        {
            ArticlesForm articlesWin = new ArticlesForm(db, ArticlesWindowMode.EDIT_LIST);
            articlesWin.ShowDialog();
        }

        private void btn_ListContract(object sender, RoutedEventArgs e)
        {
            ContractsForm contractsWin = new ContractsForm(db, ContractsWindowMode.EDIT_LIST);
            contractsWin.ShowDialog();
        }

        private void btn_Exit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
