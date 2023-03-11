using Microsoft.Extensions.Options;
using ZaylandShop.ServiceTemplate.Integration.Test.Invariants;
using ZaylandShop.ServiceTemplate.Integration.Test.Options;

namespace ZaylandShop.ServiceTemplate.Integration.Test.Extensions;

public static class TestHttpApiClientOptionsExtensions
{
    public static void Validate(this TestHttpApiClientOptions options)
    {
        if (options == default)
        {
            throw new ArgumentNullException(
                nameof(options), 
                RegisterTestHttpApiClientInvariants.OptionsEmptyValue);
        }

        List<string> errors = new();
        if (string.IsNullOrWhiteSpace(options.BaseUrl))
        {
            errors.Add(string.Format(
                RegisterTestHttpApiClientInvariants.OptionNotFoundError, 
                nameof(options.BaseUrl)));
        }

        if (errors.Count != 0)
        {
            throw new OptionsValidationException(
                nameof(TestHttpApiClientOptions), 
                typeof(TestHttpApiClientOptions), 
                errors);
        }
    }
}