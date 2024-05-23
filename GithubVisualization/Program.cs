using LibGit2Sharp;//TODO: Replace this with your own implementation after implementing the Git client

Console.WriteLine("Enter the path to the repository: ");
var repoPath = Console.ReadLine();

if (string.IsNullOrWhiteSpace(repoPath) || !Directory.Exists(repoPath))
{
    Console.WriteLine("Invalid path.");
    return;
}

Console.WriteLine("Enter the number of days to look back for commits: ");
var numberOfDaysRaw = Console.ReadLine();

if (!int.TryParse(numberOfDaysRaw, out var numberOfDays))
{
    Console.WriteLine("Invalid number of days.");
    return;
}


if (Repository.IsValid(repoPath))
{
    using (var repo = new Repository(repoPath))
    {
        var repoStats = GetCurrentUserStatsForRepo(repo, numberOfDays);
        PrintCurrentUserStatsForRepo(repoStats, repo.Info.WorkingDirectory);
    }
    return;
}

var dict = new Dictionary<string, int>();

foreach (var subDir in Directory.GetDirectories(repoPath))
{
    if (Repository.IsValid(subDir))
    {
        using (var repo = new Repository(subDir))
        {
            var repoStats = GetCurrentUserStatsForRepo(repo, numberOfDays);
            foreach (var entry in repoStats)
            {
                if (dict.ContainsKey(entry.Key))
                {
                    dict[entry.Key] += entry.Value;
                }
                else
                {
                    dict[entry.Key] = entry.Value;
                }
            }
            PrintCurrentUserStatsForRepo(repoStats, repo.Info.WorkingDirectory);
        }
    }
}

PrintCurrentUserStatsForRepo(dict, "Total contributions");

Dictionary<string, int> GetCurrentUserStatsForRepo(Repository repo, int numberOfDays)
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

void PrintCurrentUserStatsForRepo(Dictionary<string, int> dict, string repoName)
{
    if (dict.Count > 0)
    {
        foreach (var entry in dict)
        {
            Console.WriteLine($"Number of commits by {entry.Key} in repo {repoName} the last {numberOfDays} days: {entry.Value}");
        }
    }
}