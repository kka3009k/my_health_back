namespace MyHealth.Data.Dto
{
    /// <summary>
    /// Данные авторизации через почту
    /// </summary>
    public class AuthPar
    {
        /// <summary>
        /// Логин(телефон или почта)
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; }
    }
}
