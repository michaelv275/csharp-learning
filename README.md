# csharp-learning

A sandbox repository for learning and experimenting with **C#** and the **.NET SDK** on macOS using **VS Code**.

## 🚀 Getting Started

1. **Install .NET SDK**  
   - [Download here](https://dotnet.microsoft.com/download) or install via Homebrew:  
     ```bash
     brew install --cask dotnet-sdk
     ```
   - Verify:  
     ```bash
     dotnet --info
     ```

2. **Add useful VsCode extensions**  
   - C# Dev Kit & C# — Core C# experience
   - IntelliCode for C# — Optional, smarter completions
   - NuGet Package Manager — Browse/install packages
   - Roslynator — Analyzers & refactorings
   - EditorConfig — Consistent formatting
   - Error Lens — Inline diagnostics
   - GitLens — Repo insights

## Building

1. **All projects**
  ```bash
    cd examples

    dotnet build   
  ```
2. **specific projects**
  ```bash
    cd examples

    dotnet build [PATH_TO_PROJECT_FILE].csproj
  ```

## Running an example
```bash
    cd examples

    dotnet run --project [PATH_TO_PROJECT_FILE].csproj
    # ex: dotnet run --project AccessLevels/AccessLevels.csproj 

    # or

    dotnet run CsharpExamples.sln --project [PROJECT_NAME]
    # ex: dotnet run CsharpExamples.sln --project AccessLevels 
```
