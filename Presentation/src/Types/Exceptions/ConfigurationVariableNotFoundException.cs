namespace Presentation.src.Types.Exceptions
{
    internal class ConfigurationVariableNotFoundException(string variableName) : Exception($"{variableName} свойство не найдено в конфигурации приложения");
}
