oauth2-jwtprofile-netcore
==================================

This sample calls a oAuth2 Authorization Server to get a JWT token using the JWT Profile flow (https://tools.ietf.org/html/rfc7523).

## How To Run This Sample

### To run this sample you will need:
- .NET Core 2.1

### Usage:
Usage... adfsUrl certificatePath certificatePassword clientId resource

Example:
```
dotnet oAuth2JwtProfile.dll https://sts.test.rb.is/adfs/oauth2/token c:\\cert\\myprivatekey.pfx myprivatekeypass 1add817a-4641-4ab0-a881-679f34421af4 urn:rb.api
```
