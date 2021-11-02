```
dotnet publish -c Release -o Published
docker-compose -f docker-compose.yml up
dotnet Published\MSSQLDockerSample.dll
docker-compose -f docker-compose.yml down
```