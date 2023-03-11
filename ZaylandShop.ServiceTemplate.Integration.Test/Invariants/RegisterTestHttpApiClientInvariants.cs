namespace ZaylandShop.ServiceTemplate.Integration.Test.Invariants;

public class RegisterTestHttpApiClientInvariants
{
    public const string OptionsSectionPath = "Integration:Test";

    public const string OptionsSectionNotDefined = "Не определена секция с настройками клиента сервиса 'Avito' или эта секция пустая";
    
    public const string OptionNotFoundError = "{0} обязан иметь значение";
    
    public const string OptionsEmptyValue = "Конфигурация AvitoHttpApiClientOptions должена иметь значение";
}