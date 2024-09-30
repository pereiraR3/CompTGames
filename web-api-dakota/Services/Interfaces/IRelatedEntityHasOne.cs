namespace web_api_dakota.Services.Interfaces;

public interface IRelatedEntityHasOne<TParent>
{
    void SetParent(TParent parent);
    
}