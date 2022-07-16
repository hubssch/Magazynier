using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

using Magazynier.DatabaseAccess;

namespace Magazynier.WinForms
{

    public partial class DocumentsForm : Window
    {
        private MagDbContext dbContext;

        public DocumentsForm(MagDbContext db)
        {
            InitializeComponent();

            this.dbContext = db;

            this.Loaded += DocumentsForm_Loaded;
        }

        private void DocumentsForm_Loaded(object sender, RoutedEventArgs e) => dg_docs_list.ItemsSource = LoadDocumentsData();

        private List<ItemDocument> LoadDocumentsData()
        {
            var docs_list =
                from doc in dbContext.Documents
                join contract in dbContext.Contracts
                on doc.ContractID equals contract.ContractID
                select new
                {
                    DocID = doc.DocID,
                    Signature = doc.Signature,
                    DocType = doc.Operation == 'W' ? "WZ" : "PZ",
                    ContractData = String.Format("{0}, {1}, {2} {3}, NIP: {4}", contract.Name, contract.Street, contract.Code, contract.Post, contract.NIP),
                    Date = doc.Date
                };
            List<ItemDocument> list = new List<ItemDocument>();
            foreach (var doc_item in docs_list)
            {
                list.Add(new ItemDocument(doc_item.DocID, doc_item.Signature, doc_item.DocType, doc_item.ContractData, doc_item.Date));
            }
            return list;
        }
        private void RefreshGridDataSource()
        {
            dg_docs_list.ItemsSource = null;
            dg_docs_list.ItemsSource = LoadDocumentsData();
        }

        private void btn_CancelDocumentsWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_RemoveDocument(object sender, RoutedEventArgs e)
        {
            ItemDocument item = (ItemDocument) dg_docs_list.SelectedItem;
            DbDocument doc_item = dbContext.Documents.FirstOrDefault(d => d.DocID == item.DocID);
            if (doc_item == null) return;
            dbContext.Remove(doc_item);
            dbContext.SaveChanges();
            RefreshGridDataSource();
        }

        private void lb_dbl_click_DocList(object sender, MouseButtonEventArgs e)
        {
            ItemDocument item = (ItemDocument)dg_docs_list.SelectedItem;
            DocumentForm documentWin = new DocumentForm(dbContext, DocumentWindowMode.VIEW, item);
            documentWin.ShowDialog();
        }
    }
}
