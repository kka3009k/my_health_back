namespace MyHealth.Data.Dto
{
    /// <summary>
    /// Данные изменения пароля
    /// </summary>
    public class ChangePasswordPar
    {
        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Новый пароль
        /// </summary>
        public string NewPassword { get; set; }
    }
}
