namespace AniTalkApi.CRUD;

public class CrudManager
{
    public ImageCrud ImageCrud { get; }
     
    public CrudManager(
        ImageCrud imageCrud)
    {
        ImageCrud = imageCrud;
    }    
}