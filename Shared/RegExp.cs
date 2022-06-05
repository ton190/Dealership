using System.Text.RegularExpressions;

namespace Shared.RegEx;

public static class RegExp
{
    public static readonly Regex BusinessName = new Regex(
        @"^[0-9a-zA-Z àâäèéêëîïôœùûüÿçÀÂÄÈÉÊËÎÏÔŒÙÛÜŸÇ/\)\(.-]+$");

    public static readonly Regex NumbersAndLetters = new Regex(
        @"^[a-zA-Z0-9]+$");

    public static readonly Regex Name = new(
        "^[a-zA-ZàâäèéêëîïôœùûüÿçÀÂÄÈÉÊËÎÏÔŒÙÛÜŸÇ]+$");

    public static readonly Regex PhoneNumber = new(@"^\d{10}$");

    public static readonly Regex AddressNumber = new("^[0-9a-zA-Z ]+$");
    public static readonly Regex StreetName = new(
        @"^[a-zA-Z\u0080-\u024F]+(?:. |-| |')*([1-9a-zA-Z\u0080-\u024F]+(?:. |-| |'))*[a-zA-Z0-9\u0080-\u024F]*$");

    public static readonly Regex PostalCode = new(
        "^(?!.*[DFIOQU])[A-VXY][0-9][A-Z] ?[0-9][A-Z][0-9]$");

    public static readonly Regex City = new(
        @"^[a-zA-Z\u0080-\u024F]+(?:. |-| |')*([1-9a-zA-Z\u0080-\u024F]+(?:. |-| |'))*[a-zA-Z\u0080-\u024F]*$");
}
