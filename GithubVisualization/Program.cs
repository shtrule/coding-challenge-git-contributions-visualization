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

if (!int.TryParse(numberOfDaysRaw, out var numberOfDays)) {
    Console.WriteLine("Invalid number of days.");
    return;
}


if (Repository.IsValid(repoPath))
{
    PrintCurrentUserStatsForRepo(repoPath, numberOfDays);
    return;
}

foreach (var subDir in Directory.GetDirectories(repoPath))
{
    if (Repository.IsValid(subDir))
    {
        PrintCurrentUserStatsForRepo(subDir, numberOfDays);
    }
}

void PrintCurrentUserStatsForRepo(string repoPath, int numberOfDays)
{
    var dict = new Dictionary<string, int>();
    using (var repo = new Repository(repoPath))
    {
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

        if (dict.Count > 0) {
            var repoName = new DirectoryInfo(repo.Info.WorkingDirectory).Name;
            foreach (var entry in dict)
            {
                Console.WriteLine($"Number of commits by {entry.Key} in repo {repoName} the last {numberOfDays} days: {entry.Value}");
            }
        }
    }
}
