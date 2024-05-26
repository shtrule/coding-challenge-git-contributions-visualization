using GitVisualizationApi.Models;
using LibGit2Sharp;

namespace GitVisualizationApi.Services;

public class RepoParser
{
    public static Dictionary<string, GitUserContributions> GetUserContributionsForRepo(Repository repo, int numberOfDays)
    {
        var userContributions = new Dictionary<string, GitUserContributions>();

        foreach (var commit in repo.Commits.Where(c => c.Author.When > DateTime.Now.AddDays(-numberOfDays)))
        {
            if (!userContributions.ContainsKey(commit.Author.Email))
            {
                userContributions[commit.Author.Email] = new GitUserContributions(commit.Author.Email);
            }

            userContributions[commit.Author.Email].AddContribution(repo.Info.WorkingDirectory, commit.Author.When);
        }

        return userContributions;
    }
}