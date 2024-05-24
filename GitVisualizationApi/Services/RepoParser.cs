using LibGit2Sharp;

namespace GitVisualizationApi.Services;

public class RepoParser
{
    public static Dictionary<string, int> GetCurrentUserStatsForRepo(Repository repo, int numberOfDays)
    {
        var dict = new Dictionary<string, int>();

        foreach (var commit in repo.Commits.Where(c => c.Author.When > DateTime.Now.AddDays(-numberOfDays)))
        {
            if (dict.ContainsKey(commit.Author.Email))
            {
                dict[commit.Author.Email]++;
            }
            else
            {
                dict[commit.Author.Email] = 1;
            }
        }

        return dict;
    }
}