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


if (!Repository.IsValid(repoPath))
{
    Console.WriteLine("Repository is not valid.");
    return;
}

using (var repo = new Repository(repoPath))
{
    var config = repo.Config;

    var username = config.Get<string>("user.name")?.Value;
    var email = config.Get<string>("user.email")?.Value;
    Console.WriteLine($"Username: {username}, Email: {email}");

    foreach (var commit in repo.Commits.Where(c => c.Author.When > DateTime.Now.AddDays(-numberOfDays) && c.Author.Email == email))
    {
        Console.WriteLine($"Commit message: {commit.Message}, when: {commit.Author.When}");
    }
}
