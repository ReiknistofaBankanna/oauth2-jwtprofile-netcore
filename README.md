oauth2-jwtprofile-netcore
==================================

This sample calls a oAuth2 Authorization Server to get a JWT token using the JWT Profile flow (https://tools.ietf.org/html/rfc7523).

## How To Run This Sample

### To run this sample you will need:
- .NET 6

### Usage:
Usage... stsUrl certificatePath certificatePassword clientId resource audience

- **stsUrl**: Url for the authorization server(sts).
- **certificatePath**: Path to the x509 certificate to sign the jwt assertion, must contain a private key, in this example the certificate is in a pem format.
- **certificatePassword**: Password for the certificate private key.
- **clientId**: The ClientId for the oauth2 client.
- **resource**: Identifier for the api being called (can be a url but is just an identifier)
- **audience**: Identifier for the authorization server (sts) (can be a url but is just an identifier)

Example:
```
dotnet oAuth2JwtProfile.dll https://{host}/oauth2/token c:\\cert\\myprivatekey.pfx myprivatekeypass 1add817a-4641-4ab0-a881-679f34421af4 urn:some.api urn:myoauth2audience
```
