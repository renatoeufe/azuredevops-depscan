namespace depscan.Application;

public sealed record Credentials
{
    public string Organization { get; }
    public string Token { get; }

    public Credentials(string user, string accessToken, string organization)
    {
        Organization = organization;
        Token = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{user}:{accessToken}"));
    }
}