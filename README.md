# ExchangeSystem
## What is it

**ExchangeSystem** are networking protocols that can exchange information between two sides without network protection or use aes+rsa (and custom) encription. Under the hood is used a TCP/IP stable connection which guarantees that data will received.

## Where can it used

This is project was developed for use in non-commercial applications likes online chat and message wall.

## Roadmap

In the future I'll make a desktop (mobile) client and nice (hopefully) web-site. But today I'm finalizing and cleaning up codebase to use this solution without problems. Here is what I want to implement:

* Desktop client (WPF)
  * Online-chat
  * Message wall
  * User profile
  * Solve a specific problem
* Web-site (TypeScript / Bootstrap)
  * Use OpenGl
  * Minimal interface
  * Download installer and read the application description

## Code Examples

[Client](https://github.com/Sparrow1488/ExchangeSystem/tree/main/src/Exchange.Sample.Client) with Dependency Injection:

```C#
// Startup
public async Task RunAsync()
{
    _logger.LogInformation($"{nameof(Startup)} running");
    await _authorization.AuthorizeAsync();
    if (_authorization.IsSuccess())
    {
        var authToken = _authorization.GetToken();
        _logger.LogInformation("Auth token => " + authToken.ToString());
    }
}
```

```C#
// AuthorizationService
public async Task AuthorizeAsync(string login, string password)
{
    var sendler = new NewRequestSendler(_connection);
    _logger.LogDebug("GET => " + "Authorization");
    var response = await sendler.SendRequestAsync(CreateAuthorizationRequest());
    _logger.LogInformation("STATUS => " + response.Report.Status.ToString());
    _logger.LogInformation($"MESSAGE => {response.Report.Message}");

    if (response.Report.Status.Equals(AuthorizationStatus.Success))
    {
        var correctResponse = response as Response<Guid>;
        Ex.ThrowIfNull(correctResponse);
        _logger.LogDebug("GUID => " + correctResponse.Content.ToString());
        _context.IsSuccess = true;
        _context.Token = correctResponse.Content;
    }
    else if (response.Report.Status.Equals(AuthorizationStatus.Failed))
    {
        _logger.LogWarning("Authorization Failed");
    }
    else
    {
        _logger.LogError("Error");
    }
}
```

