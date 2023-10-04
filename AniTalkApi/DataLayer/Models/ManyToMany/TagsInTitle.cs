namespace AniTalkApi.DataLayer.Models.ManyToMany;

public class TagsInTitle
{
    public int TitleId { get; set; }

    public int TagId { get; set; }

    public int Order { get; set; }

    #region Dependencies
    
    public Tag Tag { get; set; }
    
    public Title Title { get; set; }
    
    #endregion
}