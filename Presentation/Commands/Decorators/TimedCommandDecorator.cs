using Presentation.Commands.Interfaces;
using System.Diagnostics;

namespace Presentation.Commands.Decorators
{
    /// <summary>
    /// Декоратор для измерения времени выполнения команд.
    /// </summary>
    public class TimedCommandDecorator : ICommand
    {
        private readonly ICommand _innerCommand;

        public string Name => _innerCommand.Name;

        public TimedCommandDecorator(ICommand command)
        {
            _innerCommand = command;
        }

        public void Execute()
        {
            var stopwatch = Stopwatch.StartNew();

            _innerCommand.Execute();

            stopwatch.Stop();
            Console.WriteLine($"\nВремя выполнения: {stopwatch.ElapsedMilliseconds} мс");
        }
    }
}
