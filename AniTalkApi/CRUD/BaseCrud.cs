using AniTalkApi.DataLayer;

namespace AniTalkApi.CRUD;

public class BaseCrud
{
    protected AppDbContext DbContext;

    public BaseCrud(AppDbContext dbContext)
    {
        DbContext = dbContext;
    }

}