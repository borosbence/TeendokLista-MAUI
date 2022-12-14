using JwtSecurity.Models;

namespace TeendokLista.API.DTOs
{
    public class FelhasznaloDTO
    {
        public int Id { get; set; }
        public string? FelhasznaloNev { get; set; }
        public string? Szerepkor { get; set; }
    }

    public class LoginDTO : FelhasznaloDTO
    {
        public LoginDTO(int id, string felhasznalonev, string szerepkor, JwtToken jwtToken)
        {
            Id = id;
            FelhasznaloNev = felhasznalonev;
            Szerepkor = szerepkor;
            Access_Token = jwtToken.Access_Token;
            Refresh_Token = jwtToken.Refresh_Token;
        }

        public string? Access_Token { get; set; }
        public string? Refresh_Token { get; set; }
    }
}
