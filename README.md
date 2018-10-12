# BlackDice

Each developer should always stay up-to-date on our Ways of Working (in progress).

## Ways of Working

Branching, Commits, and Pull Requests

```
Branches shall be named Feature/Issue_xxx, where xxx is the issue # in question, unless you are working on a technical task within an issue, which will then be called Issue_xxx_technical.

Commits should be clear, to the point, and reference the issue being worked on. They should not be written from a personal POV. '#554 Add unit tests to Character class' rather than 'I added unit tests to Character class'

Pull Requests need 2 reviewers to approve the pull request before being accepted into master. Only code that meets the following criteria should ever be marked as approved: (closes the issue in question + adequate testing + passing build). 
We can make exceptions to these rules in 2 cases: 
    1) Technical story is complete independently of its containing user story.
    2) Critical bug-fix.
```

## Development Tools

```
Unity Editor Personal: https://store.unity.com/download?ref=personal
Visual Studio Enterprise: https://visualstudio.microsoft.com/vs/enterprise/ (License found when you click 'go here': https://aits.encs.concordia.ca/aits/public/top/MSDNAA/)
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

Dependencies could not be found

```
From the Unity Editor: NuGet -> Restore Packages
```

## DB setup

```
All the instructions will be posted below and I will update them as the time comes.
```
To install docker on your system please choose system:
### Windows:
		https://docs.docker.com/toolbox/toolbox_install_windows/
### Mac:
		https://docs.docker.com/docker-for-mac/install/

To get Mongo DB 4.0:
```
docker pull mongo:4.0
```
To run docker container:
```
docker run -p 27017:27017 --name mongo -v ~/data:/data -w /data -it mongo:4.0 
```
When we will have data we will be using mongo-restore and mongo-dump and I will provide assistance for that. That is all for now.

## Server SSH key generation

```
All the instructions will be posted below and I will update them as the time comes.
```
### Windows:
```
To generate an SSH key please download Putty application:
https://www.chiark.greenend.org.uk/~sgtatham/putty/latest.html
```
### Mac:
```
You can use the terminal command ssh-keygen. The manual page will have more then enough information. 
Here is a howto page also if needed:

https://docs.typo3.org/typo3cms/ContributionWorkflowGuide/Appendix/OSX/SSHKeyOSX.html
```
Some requirements:
```
- RSA format must be used
- minimum bits: 2048
```
