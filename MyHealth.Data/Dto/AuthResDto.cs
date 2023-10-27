namespace MyHealth.Data.Dto
{
    /// <summary>
    /// Результат авторизации
    /// </summary>
    public class AuthResDto
    {
        public string? AccessToken { get; set; }

        public DateTime? AccessTokenExpiresAt { get; set; }

        public string? RefreshToken { get; set; }

        public DateTime? RefreshTokenExpiresAt { get; set; }

        public Guid? UserID { get; set; }
    }
}
