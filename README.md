# Longest Increasing Subsequence

This program reads a list of numbers separated by spaces and finds the longest part of the list where each number is bigger than the one before it. That part must be connected — you cannot skip numbers.

**Example:** `6 1 5 9 2` → `1 5 9`

For example, in the list `6 1 5 9 2`, the numbers `1 5 9` go up one after another without any gap, so that is the answer. The number `6` at the start is bigger than `1`, so the run breaks there. The `2` at the end is smaller than `9`, so it also breaks the run.



## Algorithm

The program goes through the list of numbers once, from left to right. It remembers where the current group of increasing numbers started and where the longest group so far started.

1. Look at each number one by one.
2. If the number is **bigger** than the one before it, the group keeps growing. If this group is now the longest one seen, remember it.
3. If the number is **equal or smaller**, the group has ended. Start a new group from this number.
4. After reaching the end, return the longest group that was found.

Because the program only reads the list once and remembers a few positions, it is fast even for very long lists.

- **Time complexity:** O(n) — the list is read only once.
- **Space complexity:** O(1) — only a few extra variables are used, no matter how long the list is.

## Project Structure

```
SubsequenceFinder/                          # Console app
    Program.cs                              # Entry point — reads input in a loop until 'exit'
    ISubsequenceFinderService.cs            # Interface defining the contract
    SubsequenceFinderService.cs             # Core algorithm (implements ISubsequenceFinderService)
SubsequenceFinder.Tests/                    # xUnit test project
    SubsequenceFinderTests.cs               # Unit tests
    TestData/
        testcases.json                      # File-driven test cases
.github/
    workflows/
        ci.yml                              # CI pipeline (build, test, coverage, Docker)
.editorconfig                               # Code style rules (enforced at build time)
coverage.runsettings                        # Excludes Program.cs from code coverage
Dockerfile                                  # Multi-stage Docker build
SubsequenceFinder.sln                                # Solution file
```

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://www.docker.com/) (optional, for containerised run)

## How to Verify the Solution

### 1. Run the unit tests

From the repository root:

```bash
dotnet test
```

Expected output:

```
Test summary: total: 24, failed: 0, succeeded: 24, skipped: 0
```

This runs:
- **13 inline tests** — happy-path cases, null/whitespace validation, and non-integer validation
- **11 file-driven tests** — loaded from `SubsequenceFinder.Tests/TestData/testcases.json`

### 2. Run the console app manually

```bash
dotnet run --project SubsequenceFinder
```

The app loops until you type `exit`:

```
Enter integers separated by spaces (or 'exit' to quit): 6 1 5 9 2
1 5 9
Enter integers separated by spaces (or 'exit' to quit): exit
```

Additional examples to try:

| Input | Expected output |
|-------|----------------|
| `6 1 5 9 2` | `1 5 9` |
| `1 2 3 4` | `1 2 3 4` |
| `4 3 2 1` | `4` |
| `1 2 1 2 3` | `1 2 3` |
| `923 11613 30483 19569 24201 13461 1189 30793 8848 16914 16053 21700 22116 3852 20909 5231 31469 3862 16353 22813 28735` | `3862 16353 22813 28735` |

### 3. Run tests with code coverage

```bash
dotnet test --settings coverage.runsettings --collect:"XPlat Code Coverage"
```

Coverage is measured on `SubsequenceFinderService` only (`Program.cs` is excluded via `coverage.runsettings`).

To generate an HTML report (requires [ReportGenerator](https://github.com/danielpalme/ReportGenerator)):

```bash
dotnet tool install -g dotnet-reportgenerator-globaltool
reportgenerator -reports:"**/coverage.cobertura.xml" -targetdir:"coverage" -reporttypes:Html
```

Open `coverage/index.html` in a browser to view line-by-line coverage.

### 4. CI (GitHub Actions)

Every push and pull request triggers the workflow defined in [.github/workflows/ci.yml](.github/workflows/ci.yml), which:
1. Builds the solution
2. Runs all 24 tests with coverage
3. Publishes a Markdown coverage summary to the GitHub Actions run page
4. Uploads an HTML coverage report as a build artifact
5. Builds the Docker image

## Input Validation

The algorithm throws `ArgumentException` for:
- Empty or whitespace-only input
- Non-integer tokens (e.g. `"1 two 3"`, `"1.5 2.5"`)

The console app catches this and writes the error to stderr.

## Code Style

An `.editorconfig` at the repo root defines C# style rules (indentation, naming, braces, null-checks, etc.). `<EnforceCodeStyleInBuild>true</EnforceCodeStyleInBuild>` is set in both project files, so violations are reported as build warnings.

## Adding New Test Cases

Add entries to `SubsequenceFinder.Tests/TestData/testcases.json`:

```json
[
  {
    "description": "descriptive name shown in test output",
    "input": "space separated integers input",
    "expected": "space separated integers expected output"
  }
]
```
