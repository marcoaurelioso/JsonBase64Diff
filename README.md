# JsonBase64Diff
Provide 2 Api Rest that accepts JSON base64 encoded binary data on both endpoints and a third to return the differences between them.

### How it works:
Provide 2 Rest Api endpoints that receive JSON base64 encoded binary data on both endpoints;
```
[POST] http://localhost:64017/v1/diff/1/left
[Payload]
{
  "data": "YXNkZmFzZGZhc2RmYXNkZmFzZGY="
}
[Result] 201
{
  "message":"ok"
}
[POST] http://localhost:64017/v1/diff/1/right
[Payload]
{
  "data": "YXNkZmFzZGZhc2RmYXNkZmFzZGY="
}
[Result] 201
{
  "message":"ok"
}
```
Provide a endpoint for diff comparison between them.
- Get: {host}/v1/diff/{id}
- The results provide the following info in JSON format:
- Jsons are equal
- Jsons are same size
- Jsons are different size
- Jsons are same size but with differences
```
[GET] http://localhost:64017/v1/diff/1
[Result] 200
{
  "message":"The data is the same",
}
```

### Techonologies
- Aspnet Core 1.1, WebApi
- EntityFramework Core InMemory
- Swagger for documentation
- DDD
- UnitTest

### Suggestion to improve
- Change InMemory database for a relational or noSql database
- Put a cache in the Api layer (Redis, memcache)
- Distribute the application in containers Docker
- Apply Unit, AutoMapper, SOLID techniques
- Use an Api Gateway or create an Oauth server for Authentication and Authentication
