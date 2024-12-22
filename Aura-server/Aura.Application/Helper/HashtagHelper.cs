using System.Text.RegularExpressions;

namespace Aura.Application.Helper;
public static class HashtagHelper
{
    public static List<string> GetHashtags(string postText)
    {
        var hashtagPattern = new Regex(@"#\w+");
        var matches = hashtagPattern.Matches(postText)
            .Select(match => match.Value.TrimEnd('.', ',', '!', '?').ToLower())
            .Distinct()
            .ToList();

        return matches;
    }
