namespace GitVisualizationApi.Models;

public class GitContribution
{
    public GitContribution(string userEmail, string repoName, DateTime contributionsAfter)
    {
        UserEmail = userEmail;
        RepoName = repoName;
        ContributionsAfter = contributionsAfter;
    }

    public string UserEmail { get; set; }
    public string RepoName { get; set; }

    public DateTime ContributionsAfter { get; set; }
    public int Count { get; set; }
}