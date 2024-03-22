namespace RealEstateWebApp.Business.Identity
{
    public interface ISessionUserResolver
    {
        bool IsAuthorized { get; }
        
        SessionUserModel User { get; }
    }
}