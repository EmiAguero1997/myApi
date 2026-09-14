public interface ITokenService
{
    string GenerarToken(string userId, string email, string rol);
}