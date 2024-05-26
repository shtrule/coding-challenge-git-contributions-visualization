namespace GitVisualizationApi.Models;

public class GitUserContributions
{
    public GitUserContributions(string userEmail)
    {
        UserEmail = userEmail;
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