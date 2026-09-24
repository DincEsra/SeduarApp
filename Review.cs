namespace SeduarApp;

public class Review
{
    public string UserName { get; set; }
    public string CommentText { get; set; }
    public int Rating { get; set; } // Örn: 1 ile 5 arası yıldız
    public string Date { get; set; } = DateTime.Now.ToShortDateString();
}