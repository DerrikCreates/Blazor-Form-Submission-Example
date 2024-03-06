namespace BlazorApp;

public class Posts
{
    public List<Submission> RecentPosts = new();

    private Database _database;

    public Posts(Database database)
    {
        _database = database;
        _database.OnNewPost += OnNewPost;
        RecentPosts = _database.GetPosts(10);
    }

    private void OnNewPost(object? sender, EventArgs e)
    {
        RecentPosts = _database.GetPosts(10);
    }
}