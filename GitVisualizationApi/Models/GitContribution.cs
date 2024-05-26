using System.ComponentModel;

namespace GitVisualizationApi.Models;

public class GitUserContributions
{
    public GitUserContributions(string userEmail)
    {
        UserEmail = userEmail;
    }

    public void AddContribution(string repoName, DateTimeOffset date)
    {
        if (!ContributionsPerRepo.ContainsKey(repoName))
        {
            ContributionsPerRepo[repoName] = new List<GitContributionDateCount>();
        }

        var repoContributions = ContributionsPerRepo[repoName];
        UpdateContributionListCount(date, repoContributions);

        UpdateContributionListCount(date, TotalContributions);
    }

    private static void UpdateContributionListCount(DateTimeOffset date, List<GitContributionDateCount> repoContributions)
    {
        var existingContribution = repoContributions.FirstOrDefault(x => x.Date.Date == date.Date);
        if (existingContribution != null)
        {
            existingContribution.numberOfContributions++;
        }
        else
        {
            repoContributions.Add(new GitContributionDateCount { Date = date.Date, numberOfContributions = 1 });
        }
    }

    public string UserEmail { get; set; }
    public Dictionary<string, List<GitContributionDateCount>> ContributionsPerRepo { get; set; } = new Dictionary<string, List<GitContributionDateCount>>();

    public List<GitContributionDateCount> TotalContributions { get; set; } = new List<GitContributionDateCount>();

    public int TotalContributionsCount => TotalContributions.Sum(x => x.numberOfContributions);
}

public class GitContributionDateCount {
    public int numberOfContributions { get; set; }
    public DateTime Date { get; set; }
}