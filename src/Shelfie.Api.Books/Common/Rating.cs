using System.Globalization;

namespace Shelfie.Api.Books.Common;

public struct Rating
{
    private const int MinRating = 0;
    private const int MaxRating = 10;

    private readonly double _value;

    public Rating(int rating)
    {
        _value = GetRating(rating);
    }

    public Rating(double rating)
    {
        _value = GetRating(rating);
    }

    private static double GetRating(double value)
    {
        return value > MaxRating
            ? MaxRating
            : value < MinRating
                ? MinRating
                : value;
    }

    public override string ToString() => ToString(0);

    public string ToString(int decimalPlaces)
    {
        return Math.Round(_value, decimalPlaces).ToString(CultureInfo.InvariantCulture);
    }
}
