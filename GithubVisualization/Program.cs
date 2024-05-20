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
    using (var repo = new Repository(repoPath))
    {
        var userEmail = repo.Config.Get<string>("user.email")?.Value;
        var numberOfCommits = repo.Commits.Count(c => c.Author.When > DateTime.Now.AddDays(-numberOfDays) && c.Author.Email == userEmail);
        
        Console.WriteLine($"Number of commits by {userEmail} in the last {numberOfDays} days: {numberOfCommits}");
    }
}
