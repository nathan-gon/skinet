using Core.Entities.Identity;

namespace Core.Interfaces
{
  public interface ITokenService
  {
    string CreateToke(AppUser user);
  }
}