Run tests:
  dotnet test ./BananaMacro.Tests.Unit
  dotnet test ./BananaMacro.Tests.Integration
  dotnet test ./BananaMacro.Tests.UI

Notes:
  - Integration tests create temporary directories and clean up.
  - TestUtilities contains lightweight helpers used across test projects.