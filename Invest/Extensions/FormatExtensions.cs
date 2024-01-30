using System.Globalization;

namespace Invest.Extensions;

public static class FormatExtensions
{
    public static string ToCustomDateFormat(this DateTime dateTime)
    {
        return dateTime.ToString("dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture);
    }

    public static string ToAmountFormat(this decimal quantity)
    {
        return quantity.ToString("0.##", CultureInfo.InvariantCulture);
    }

    public static string ToPercentageFormat(this decimal percentage)
    {
        var basicFormat = percentage.ToString("0.00", CultureInfo.InvariantCulture);
        var advancedFormat = percentage.ToString("0.####", CultureInfo.InvariantCulture);
        var basicDecimalPlaces = basicFormat.SkipWhile(c => c != '.').Count();
        var advancedDecimalPlaces = advancedFormat.SkipWhile(c => c != '.').Count();
        return advancedDecimalPlaces > basicDecimalPlaces ? advancedFormat : basicFormat;
    }

    public static string ToCurrencyFormat(this decimal amount)
    {
        return amount.ToString("C", new CultureInfo("en-US"));
    }
}
