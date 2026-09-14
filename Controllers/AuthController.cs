using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using myApi.Data;
using myApi.Models;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly MyApiContext _context;
    private readonly ITokenService _tokenService;

    public AuthController(MyApiContext context, ITokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegistrarUsuarioDto dto)
    {
        if (await _context.Usuarios.AnyAsync(u => u.Correo == dto.Email))
            return BadRequest("El correo ya está registrado.");

        var usuario = new Usuario
        {
            Nombre = dto.Nombre,
            Correo = dto.Email,
            Contrasena = BCrypt.Net.BCrypt.HashPassword(dto.Password), // 👈 Hash seguro
            Rol = dto.Rol
        };

        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();

        return Ok(new { mensaje = "Usuario registrado con éxito" });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Correo == dto.Email);
        if (usuario == null)
            return Unauthorized("Credenciales inválidas.");

        // 👈 Verificar hash de contraseña
        bool passwordValida = BCrypt.Net.BCrypt.Verify(dto.Password, usuario.Contrasena);
        if (!passwordValida)
            return Unauthorized("Credenciales inválidas.");

        var token = _tokenService.GenerarToken(usuario.Id.ToString(), usuario.Correo, usuario.Rol);

        return Ok(new { token });
    }
}