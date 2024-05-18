using LibGit2Sharp;

Console.WriteLine("Enter the path to the repository: ");
string repoPath = Console.ReadLine();

using (var repo = new Repository(repoPath))
{
    foreach (var commit in repo.Commits)
    {
        Console.WriteLine($"Commit message: {commit.Message}");
    }
}
