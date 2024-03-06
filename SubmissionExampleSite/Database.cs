using System.Data;
using Dapper;
using Microsoft.Data.Sqlite;

namespace BlazorApp;

public class Database
{
    private IDbConnection _connection;

    public EventHandler OnNewPost;


    private Posts _posts;

    public Database(IConfiguration configuration)
    {
        _connection = new SqliteConnection(configuration.GetConnectionString("PostsDbConnection"));
        _connection.Execute(CREATE_POST_TABLE);
        _connection.Execute(CREATE_DATE_INDEX);
    }

    private const string CREATE_POST_TABLE = @"
    CREATE TABLE IF NOT EXISTS Posts(
Id INTEGER PRIMARY KEY,
Name TEXT,
Post TEXT,
Date INTEGER
)
";

    private const string CREATE_DATE_INDEX = @"
CREATE UNIQUE INDEX IF NOT EXISTS DateIndex ON Posts(Date);
";

    private const string INSERT_POST = @"
    
INSERT INTO Posts (Id,Name,Post,Date)
VALUES (NULL,@Name,@Post,@Date)
    

";

    private const string GET_POSTS = @"
SELECT * FROM Posts ORDER BY Date DESC LIMIT @RowCount 
";

    public void InsertPost(Submission submission)
    {
        _connection.Execute(INSERT_POST,
            new { submission.Name, Post = submission.Post, Date = submission.Date });
        OnNewPost.Invoke(this, EventArgs.Empty);
    }

    public List<Submission> GetPosts(int count)
    {
      return  _connection.Query<Submission>(GET_POSTS, new { RowCount = count }) as List<Submission>;
    }
}