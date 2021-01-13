$env:ASPNETCORE_ENVIRONMENT="test"
cd tests/Library.Tests
dotnet test
cd ../..
cd tests/Library.Tests.EndToEnd
dotnet test
cd ../..
$env:ASPNETCORE_ENVIRONMENT="Development"