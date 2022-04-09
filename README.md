# ExchangeSystem
## Introduction

**ExchangeSystem** are networking protocols that can exchange information between two sides without network protection or use aes+rsa (and custom) encription. Under the hood is used a TCP/IP stable connection which guarantees that data will received.

In this solution, you can extend an existing one, or write your own protocols using the `RequestSender` auxiliary class to work with `TcpClient`, or use your own solutions using the open `IRequestSender` interface (`IRequestSender<TRequest, TResponse>`). 

As an example, you can see `AdvancedRequestSender` and `AdvancedAesRsaSender` using Aes256 and RSA1024 encryption algorithms implemented in the .NET standard library.

```C#
IRequestSender sender = new AdvancedRequestSender(connection);
IRequestSender protectedSender = new AdvancedAesRsaSender(connection);
```

To work with your own protocols, you can use [Exchange.Server](https://github.com/Sparrow1488/ExchangeSystem/tree/main/src/Exchange.Server) in which requests are processed using controllers from the MVC pattern.

```C#
public class AuthorizationController : Controller
{
    public AuthorizationController() : base() { }
    public AuthorizationController(RequestContext context) : base(context) { }

    public virtual Response Authorization(HashedUserPassport passport)
    {
        Response response;
        ThrowIfPassportInvalid(passport);
        
        CompleteUserAuthorization(passport);
        if (IsAuthSuccess())
            response = CreateSuccessAuthResponse();
        else response = CreateFailedAuthResponse();
        return response;
    }
    .....
}
```

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

## Dependencies

* For .NET Core 3.1
  * [Encryptors](https://github.com/Sparrow1488/Encryptors) : .NET Core 3.1
  * Newtonsoft.Json : v.13.0.1
  * Polly : v.7.2.3

## Examples

* [Client with DI](https://github.com/Sparrow1488/ExchangeSystem/tree/main/src/Exchange.Sample.Client)

## .NET Core Usage

Sample usage on client side:

```C#
var connection = new ConnectionSettings("127.0.0.1", 88);
var sender = new AdvancedRequestSender(connection);
var response = await sender.SendRetryRequestAsync(CreateAuthRequest());
if(response.Report.Status == ResponseStatus.Success){
    var responseWithToken = response as Response<Guid>;
    ThrowIfNull(responseWithToken);
    return responseWithToken.Content; // Guid
} 
```

```C#
private UserPassport _passport = new UserPassport("asd", "1234");

private Request<HashedUserProfile> CreateAuthRequest(){
    var request = new Request<HashedUserPassport>("Authorization", 											ProtectionType.Default);
    var hashedPassport = HashedUserPassport.CreateHashed(passport);
    request.Body = new Body<HashedUserPassport>(hashedPassport);
    return request;
}
```

