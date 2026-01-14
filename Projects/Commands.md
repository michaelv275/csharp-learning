# Dotnet CLI Commands

This file contains useful dotnet CLI commands for working with the TriviaBot solution.

## 1. Building the Project

To build the entire solution:

```bash
dotnet build TriviaBotSolution.sln
```

To build a specific project:

```bash
dotnet build TriviaBot/TriviaBot.csproj
```

To build in Release configuration:

```bash
dotnet build TriviaBotSolution.sln --configuration Release
```

## 2. Creating a New Project and Adding it to the Solution

To create a new console project and add it to the solution:

```bash
# Create a new console project in a new directory
dotnet new console -n YourProjectName

# Add the new project to the solution
dotnet sln TriviaBotSolution.sln add YourProjectName/YourProjectName.csproj
```

To create a different type of project (e.g., class library):

```bash
# Create a new class library
dotnet new classlib -n YourLibraryName

# Add it to the solution
dotnet sln TriviaBotSolution.sln add YourLibraryName/YourLibraryName.csproj
```

## Additional Useful Commands

### Running the Project

```bash
dotnet run --project TriviaBot/TriviaBot.csproj
```

### Cleaning Build Artifacts

```bash
dotnet clean TriviaBotSolution.sln
```

### Restoring NuGet Packages

```bash
dotnet restore TriviaBotSolution.sln
```

### Adding a Project Reference

```bash
# Add a reference from one project to another
dotnet add TriviaBot/TriviaBot.csproj reference YourProjectName/YourProjectName.csproj
```

### Adding a NuGet Package

```bash
# Add a NuGet package to a specific project
dotnet add TriviaBot/TriviaBot.csproj package PackageName
```

### Listing Projects in Solution

```bash
dotnet sln TriviaBotSolution.sln list
```

### Removing a Project from Solution

```bash
dotnet sln TriviaBotSolution.sln remove YourProjectName/YourProjectName.csproj
```
