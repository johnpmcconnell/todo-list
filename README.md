# Todo List

A sample code project demonstrating a basic web application.

# Development

This project is designed for development in an environment with the following tools:

  * Visual Studio Code (<https://code.visualstudio.com/>) (with C# extension <https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp>)
  * .NET 5.0 SDK (<https://dotnet.microsoft.com/download>)

## Commands

Common tasks are mostly performed in PowerShell. Before the commands below can be used,
you must initialize the environment by invoking the `env.ps1` script:

```
PS> .\env.ps1
```

This will set up the required environment variables and push the web app source directory
onto the location stack (setting the current directory).

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
