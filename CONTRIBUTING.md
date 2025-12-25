# Contributor's Guide

## Pull Requests
NBuilder accepts pull requests and they are very much appreciated! However, there are certain things you can do to make pull requests easier to accept.

1. Take your fork from the `develop` branch. NBuilder is currently being developed using the 
    GitFlow branching strategy. `develop` represents "work in progress." Your changes are easier
    to integrate if they done alongside the other features and changes we are about to release.
2. Make sure your work is covered by unit tests. If we have to add test coverage to your proposed change,
   it means more work for us, and longer wait times for you to see your changes integrated into the solution.
3. Make sure an issue exists on the NBuilder issues page and label your commits with the issue number.
4. Make sure your changes take effect in each version of the framework that NBuilder provides.
5. Use the `Invoke-EmulateAppVeyor.ps1` script to validate your work prior to submitting your pull request.

```powershell
    .\Invoke-EmulateAppVeyor.ps1 -Build -Test -Pack
```

6. Any new tests must follow this naming convention: `MethodName_Scenario_Expectation()`.
7. Every class must have a single responsibility. (SOLID Principles)
8. Every new test must be in Arrange Act Assert form. If touching an existing test in record/replay, please convert it to AAA syntax unless it is too time consuming.
9. The "Foo Bar" convention is not permitted anywhere.
10. American English spellings should be used not British English.