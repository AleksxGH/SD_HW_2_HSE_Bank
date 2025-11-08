namespace Presentation.Commands.Interfaces
{
    /// <summary>
    /// Общий интерфейс для всех команд пользовательского меню.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Название команды.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Метод выполнения команды.
        /// </summary>
        void Execute();
    }
}
