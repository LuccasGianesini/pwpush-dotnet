namespace PwPush.Core;

public class GetResponse
{
    public int expire_after_days { get; set; }
    public int expire_after_views { get; set; }
    public bool expired { get; set; }
    public string url_token { get; set; }
    public DateTime created_at { get; set; }
    public DateTime updated_at { get; set; }
    public bool deleted { get; set; }
    public bool deletable_by_viewer { get; set; }
    public bool retrieval_step { get; set; }
    public string expired_on { get; set; }
    public string payload { get; set; }
    public int days_remaining { get; set; }
    public int views_remaining { get; set; }
}