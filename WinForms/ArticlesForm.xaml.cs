using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

using Magazynier.DatabaseAccess;

namespace Magazynier.WinForms
{
    public enum ArticlesWindowMode
    {
        EDIT_LIST,
        SELECT_ARTICLE
    }

    public partial class ArticlesForm : Window
    {
        private MagDbContext dbContext;

        private ArticlesWindowMode mode;

        public delegate void selection_callback(ItemArticle article);
        public event selection_callback selected_getData_CallBack;

        public ArticlesForm(MagDbContext db, ArticlesWindowMode mode = ArticlesWindowMode.SELECT_ARTICLE)
        {
            InitializeComponent();
            this.dbContext = db;
            this.mode = mode;
            this.Loaded += Articles_Loaded;
            this.selected_getData_CallBack += DefaultCallBack;
        }

        private void DefaultCallBack(ItemArticle i) { }

        private void Articles_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshGridDataSource();
        }

        private void lb_dbl_click(object sender, MouseButtonEventArgs e)
        {
            DbArticle selectedArticle = (DbArticle) dg_list.SelectedItem;
            ItemArticle selectedItemArticle = new ItemArticle(selectedArticle);

            if (mode == ArticlesWindowMode.SELECT_ARTICLE)
            {
                this.Close();
                selectedItemArticle.Amount = 0; /* wybrany towar zawsze zwracam bez ilosci (stan magazynowy mozna zmienic z widoku listy, badz zmieni sie on po zapisaniu dokumentu */
                selected_getData_CallBack(selectedItemArticle);
            }
            else
            {
                ArticleForm articleEditWin = new ArticleForm(ArticleWindowMode.EDIT, selectedItemArticle);
                articleEditWin.getData_CallBack += ArticleUpdate_getData_CallBack;
                articleEditWin.ShowDialog();
            }
        }

        private void RefreshGridDataSource()
        {
            this.dg_list.ItemsSource = null;
            this.dg_list.ItemsSource = dbContext.Articles.OrderBy(a => a.Name).ToList();
        }

        private void ArticleAddition_getData_CallBack(ItemArticle article)
        {
            DbArticle added_article = new DbArticle { Name = article.Name, Description = article.Description, Amount = article.Amount };
            dbContext.Add(added_article);
            dbContext.SaveChanges();
            RefreshGridDataSource();
        }

        private void ArticleUpdate_getData_CallBack(ItemArticle article)
        {
            DbArticle updated_article = dbContext.Articles.FirstOrDefault(a => a.ArticleID == article.ArticleID);
            if (updated_article == null) return;
            updated_article.Name = article.Name;
            updated_article.Description = article.Description;
            updated_article.Amount = article.Amount;
            dbContext.Update(updated_article);
            dbContext.SaveChanges();
            RefreshGridDataSource();
        }

        private void btn_CancelArticleWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_AddArticle(object sender, RoutedEventArgs e)
        {
            ArticleForm articleWin = new ArticleForm();
            articleWin.getData_CallBack += ArticleAddition_getData_CallBack;
            articleWin.ShowDialog();
        }

        private void btn_RemoveArticle(object sender, RoutedEventArgs e)
        {
            DbArticle selectedArticle = (DbArticle) dg_list.SelectedItem;
            if (selectedArticle != null)
            {
                dbContext.Articles.Remove(selectedArticle);
                dbContext.SaveChanges();
                RefreshGridDataSource();
            }
        }
    }
}
