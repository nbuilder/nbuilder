# Contributor's Guide

## Pull Requests
NBuilder does accept pull requests and they are very much appreciated, however there are certain things you can do to make
pull requests easier to accept.

1. Take your fork from the "develop" branch. NBuilder is currently being developed using the 
    git flow branching strategy. "develop" represents "work in progress." Your changes are easier
    to integrate if they done alongside the other features and changes we are about to release.
2. Make sure your work is covered by unit tests. If we have to add test coverage to your proposed change,
   it means more work for us, and longer wait times for you to see your changes integrated into the 
   solution.
3. Make sure an issue exists on the NBuilder issues page and label your commits with the issue number.
4. Make sure your change takes effect in each version of the framework that NBuilder provides.
5. Use the `Invoke-EmulateAppVeyor.ps1` script to validate your work prior to submitting your pull request.
