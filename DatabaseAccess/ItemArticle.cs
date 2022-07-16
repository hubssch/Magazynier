namespace Magazynier.DatabaseAccess
{
    public class ItemArticle
    {
        public int ArticleID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Amount { get; set; }

        public ItemArticle() : this(0, "", "", 0) { }

        public ItemArticle(int articleID, string name, string description, int amount)
        {
            ArticleID = articleID;
            Name = name;
            Description = description;
            Amount = amount;
        }

        public ItemArticle(DbArticle article)
        {
            ArticleID = article.ArticleID;
            Name = article.Name;
            Description = article.Description;
            Amount = article.Amount;
        }
    }
}
