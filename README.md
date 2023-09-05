# FluentxApi
A fluent .Net Standard library to create xApi statements and communicate with a LRS

![.NET Core](https://github.com/MindOnSite/FluentxApi/workflows/.NET%20Core/badge.svg)
[![NuGet](https://img.shields.io/nuget/v/Mos.xApi.svg)](https://www.nuget.org/packages/Mos.xApi)

## Installation
Add the NuGet package to your project using the Package Manager Console.

```powershell
PM> Install-Package Mos.xApi
```

## Getting started

### Creating a statement
Use the `Statement.Create` static method to start the Statement building.
All the complex objects then have a fluent builder that simplifies the creation of
the `Statement` object.

```C#
var newStatement =
    Statement.Create(
        Actor.CreateAgent("Example Learner").WithMailBox("example.learner@adlnet.gov"),
        Verb.Create("http://adlnet.gov/expapi/verbs/attempted").AddDisplay("en-US", "attempted"),
        StatementObject.CreateActivity("http://example.adlnet.gov/xapi/example/simpleCBT")
                        .AddName("en-US", "simple CBT course")
                        .AddDescription("en-US", "A fictious example CBT course"))
        .WithResult(Result.Create()
                            .WithScore(new Score(0.95))
                            .WithSuccess(true)
                            .WithCompletion(true)
                            .WithDuration(TimeSpan.FromSeconds(1234)))
        .Build();
```

Once the Statement has been created, it can be serialized to json using the `ToJson` method.
This method takes a boolean argument setting if it should be pretty printed or not.

```C#
var json = statement.ToJson(true);
Console.WriteLine(json);
```

### Communication with a LRS
An LRS Client is also implemented in the `Mos.xApi.LrsClient` namespace. It implements an interface
called `ILrsClient` and uses asynchronous methods.

```C#
using Mos.xApi.LrsClient;
...

ILrsClient lrsClient = new LrsClient("http://www.example.com/mylrs");

Statement statement = await lrsClient.GetStatementAsync(new Guid("d0371e17-4e91-46ba-924f-e78168bf0f02"));

var statementResult = await lrsClient.FindStatement(new StatementQuery{ActivityId = new Uri("http://adlnet.gov/expapi/verbs/completed")});

var statements = statementResult.Statements;

if(statements.More != null){
    var moreStatementResult = await lrsClient.FindMoreStatements(statements.More);
}

await lrsClient.SendStatementAsync(newStatement);

```

### Parsing Statements from json

A static method `Statement.FromJson` allows to deserialize a statement defined as a Json string.

```C#
string jsonStatement = "...";

var statement = Statement.FromJson(jsonStatement);
```

## Contributing
Please send your pull requests to the master branch (ideally from a  feature branch in your own fork).

You can add bugs or feature requests by [creating a new item](https://github.com/MindOnSite/FluentxApi/issues/new) on the [issues page](https://github.com/MindOnSite/FluentxApi/issues/).

## Credits

This project uses the following open source components

- [Newtonsoft.Json](https://github.com/JamesNK/Newtonsoft.Json/blob/master/LICENSE.md)

## Mos MindOnSite

This project is brought to you by [MOS MindOnSite](http://www.mindonsite.com/en/) _Smart learning solutions_.

We assist you in implementing your fully customised learning environment, with innovative and smart solutions and ready-to-use and custom-made portals.

We create a digital and unified learning experience that fits you best and where you decide the objectives and the degree of customisation.

With our solutions, you inform, train, follow, evaluate and certify your ecosystem: employees, external partners, distributors, resellers and clients.

Take a look at our [solutions](http://www.mindonsite.com/en/our-solutions/) and our [offer](http://www.mindonsite.com/en/our-offer/).
