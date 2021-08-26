# Todo List

A sample code project demonstrating a basic web application.

# Development

This project is designed for development in an environment with the following tools:

  * Visual Studio Code (<https://code.visualstudio.com/>) (with C# extension <https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp>)
  * .NET 5.0 SDK (<https://dotnet.microsoft.com/download>)
  * PowerShell 5 or greater (may be used within VS Code or outside it)

## Setup

Before the application can be run or any commands can be executed, you must initialize
the environment by invoking the `env.ps1` script in PowerShell:

```
PS> .\env.ps1
```

This performs several necessary tasks:

  * Sets environment variables the application requires (such as database connection
    string)
  * Sets the current working directory to the web application project, allowing
    `dotnet` commands to find the project by default. (`Push-Location` is used to
    change the working directory, so `Pop-Location` may be used to return to the
    previous working directory.)
  * Creates the `.env` file required by the application launch configuration.

## Commands

Common tasks are mostly performed in PowerShell.

### Database

Note that the database location is being read from the `TODOLISTDB` environment variable,
configured from the `env.ps1` script.

  * To create and deploy changes to the database, use EF's `update` command:

    ```
    dotnet ef database update
    ```

  * To create migrations, modify the database code models (in the `DbModels` namespace)
    and then use EFs `migrations` command:

    ```
    dotnet ef migrations add [migration-name]
    ```

## Debugging

To debug the application, ensure that the database is up to date (using the commands above)
and then use VS Code's built in debugger for C#. A `build` task and `launch` configuration
are already included in the project.
