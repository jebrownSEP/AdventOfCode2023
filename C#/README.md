## Running app

To ensure you have the necessary NuGet packages installed, first run `dotnet restore` in the `src` folder.

`dotnet run`

If you see any issues with dev certs when running the app, trust the dev cert by running `dotnet dev-certs https --trust`

## Running tests

To ensure you have the necessary NuGet packages installed, first run `dotnet restore` in the `tests` folder.

Run `dotnet test` in the `tests` folder to run the tests.  The results will be shown in the console.
If you want to only run certain tests, you can use the `--filter` flag like `dotnet test --filter Name~TestPartNumber`.  See https://learn.microsoft.com/en-us/dotnet/core/testing/selective-unit-tests?pivots=mstest for more details.
