# BlackDice

Each developer should always stay up-to-date on our Ways of Working

## Ways of Working

Branching, Commits, and Pull Requests

```
Branches shall be named issue_xxx, where xxx is the issue number in question.

Commits should be clear, to the point, and reference the issue being worked on. They should not be written from a personal POV. '#554 Add unit tests to Character class' rather than '#554 I added unit tests to Character class'

Pull Requests' names should describe the feature being implemented and needs 2 reviewers to approve the pull request before being accepted into master. Only code that meets the following criteria should ever be marked as approved: (closes the issue in question + adequate testing + passing build). 
We can make exceptions to these rules in 2 cases: 
    1) Technical story is complete independently of its containing user story.
    2) Critical bug-fix.
```

## Development Tools

```
Unity Editor Personal: https://store.unity.com/download?ref=personal
Visual Studio Enterprise: https://visualstudio.microsoft.com/vs/enterprise/ (License found when you click 'go here': https://aits.encs.concordia.ca/aits/public/top/MSDNAA/)
Visual Studio Code: https://code.visualstudio.com/
```

## Running Unit Tests

For now, the only way to run unit tests is by going to the following destination. A future goal is to add unit test support inside Microsoft Visual Studio.

```
From the Unity Editor: Window -> General -> Test Runner -> Run All
```

## Troubleshooting

Unity complains about C# version

```
Edit -> Project Settings -> Player -> Other Settings -> Set 'Scripting Runtime Version' to '.NET 4.x Equivalent' and 'Api Compatibility Level*' to .NET 4.x
```

Microsoft Visual Studio complains about C# version

```
Select the error message and use the IDEs suggested fix and save. You may need to close and reopen Unity if there is a compiler error showing up in Unity and not Visual Studio.
```
## Website

Our website can be found at the following address: www.blackdice.ca

