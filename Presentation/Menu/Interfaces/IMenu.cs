using Presentation.Commands.Interfaces;

namespace Presentation.Menu.Interfaces
{
    /// <summary>
    /// Интерфейс меню.
    /// </summary>
    public interface IMenu
    {
        /// <summary>
        /// Название раздела меню.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Заголовок меню.
        /// </summary>
        string Label { get; }

        /// <summary>
        /// Все команды в меню.
        /// </summary>
        public IEnumerable<ICommand> Commands { get; }

        /// <summary>
        /// Все разделы в меню.
        /// </summary>
        public IEnumerable<IMenu> Sections { get; }

        /// <summary>
        /// Метод добавления команды в меню.
        /// </summary>
        /// <param name="command"></param>
        public void AddCommand(ICommand command);

        /// <summary>
        /// Метод добавления раздела в меню.
        /// </summary>
        /// <param name="section"></param>
        public void AddSection(IMenu section);

        /// <summary>
        /// Метод отображения меню.
        /// </summary>
        void Show();
    }
}
