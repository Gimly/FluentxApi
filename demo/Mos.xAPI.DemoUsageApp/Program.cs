using Mos.xApi;
using Mos.xApi.Actors;
using Mos.xApi.Client;
using Mos.xApi.Objects;
using System;
using System.Threading.Tasks;

namespace Mos.xAPI.DemoUsageApp
{
    public class Program
    {
        public static async Task MainAsync()
        {
            DisplayXapiSpecExampleStatements();
        }

        public static void Main()
        {
            MainAsync().Wait();
        }

        private static Statement CompleteStatement()
        {
            var statement =
                Statement.Create(
                    Actor.CreateGroup("Team PB")
                         .Add(Actor.CreateAgent("Andrew Downes").WithAccount("13936749", "http://www.example.com"))
                         .Add(Actor.CreateAgent("Toby Nichols").WithOpenId("http://toby.openid.example.org/"))
                         .Add(Actor.CreateAgent("Ena Hills").WithHashedMailBox("ebd31e95054c018b10727ccffd2ef2ec3a016ee9"))
                         .WithMailBox("teampb@example.com"),
                    Verb.Create("http://adlnet.gov/expapi/verbs/attended")
                        .AddDisplay("en-GB", "attended")
                        .AddDisplay("en-US", "attended"),
                    StatementObject
                        .CreateActivity("http://www.example.com/meetings/occurances/34534")
                        .WithActivityType("http://adlnet.gov/expapi/activities/meeting")
                        .AddName("en-GB", "example meeting")
                        .AddName("en-US", "example meeting")
                        .AddDescription("en-GB", "An example meeting that happened on a specific occasion with certain people present.")
                        .AddDescription("en-US", "An example meeting that happened on a specific occasion with certain people present.")
                        .AddExtension("http://example.com/profiles/meetings/activitydefinitionextensions/room", @"{""name"": ""Kilby"", ""id"" : ""http://example.com/rooms/342""}")
                        .WithMoreInfo("http://virtualmeeting.example.com/345256"))
                   .WithResult(Result.Create()
                                     .WithSuccess(true)
                                     .WithCompletion(true)
                                     .WithResponse("We agreed on some example actions.")
                                     .WithDuration(TimeSpan.FromHours(1))
                                     .AddExtension("http://example.com/profiles/meetings/resultextensions/minuteslocation", @"X:\meetings\minutes\examplemeeting.one"))
                   .WithTimeStamp(new DateTime(2015, 12, 18, 12, 17, 00))
                   .WithContext(Context.Create()
                                       .WithRegistration(new Guid("ec531277-b57b-4c15-8d91-d292c5b2b8f7"))
                                       .AddParent(StatementObject.CreateActivity("http://www.example.com/meetings/series/267").Build())
                                       .AddCategory(StatementObject.CreateActivity("http://www.example.com/meetings/categories/teammeeting")
                                                                   .AddName("en", "team meeting")
                                                                   .AddDescription("en", "A category of meeting used for regular team meetings.")
                                                                   .WithActivityType("http://example.com/expapi/activities/meetingcategory"))
                                       .AddOther(StatementObject.CreateActivity("http://www.example.com/meetings/occurances/34257"))
                                       .AddOther(StatementObject.CreateActivity("http://www.example.com/meetings/occurances/3425567"))
                                       .WithInstructor(Actor.CreateAgent("Andrew Downes").WithAccount("13936749", "http://www.example.com"))
                                       .WithTeam(Actor.CreateGroup("Team PB").WithMailBox("teampb@example.com"))
                                       .WithPlatform("Example virtual meeting software")
                                       .WithLanguage("tlh")
                                       .WithStatementReference(new Guid("6690e6c9-3ef0-4ed3-8b37-7f3964730bee")))
                   .WithAuthority(Actor.CreateAgent().WithAccount("anonymous", "http://cloud.scorm.com"))
                   .Build();
            return statement;
        }

        private static void CompletionStatement()
        {
            var statement =
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
            var json = statement.ToJson(true);
            Console.WriteLine(json);
        }

        private static void DisplayXapiSpecExampleStatements()
        {
            Console.WriteLine(SimpleStatement().ToJson(true));
            Console.ReadKey();

            Console.Clear();
            CompletionStatement();
            Console.ReadKey();

            Console.Clear();
            Console.WriteLine(CompleteStatement().ToJson(true));
            Console.ReadKey();

            Console.Clear();
            SubStatement();
            Console.ReadKey();
        }
        private static Statement SimpleStatement()
        {
            var statement =
                Statement.Create(
                    Actor.CreateAgent("Project Tin Can API").WithMailBox("user@example.com"),
                    Verb.Create("http://example.com/xapi/verbs#sent-a-statement")
                        .AddDisplay("en-US", "sent"),
                    StatementObject.CreateActivity("http://example.com/xapi/activity/simplestatement")
                                   .AddName("en-US", "simple statement")
                                   .AddDescription("en-US", "A simple Experience API statement. Note that the LRS does not need to have any prior information about the Actor(learner), the verb, or the Activity / object.")).Build();

            return statement;
        }

        private static void SubStatement()
        {
            var statement =
                Statement.Create(
                    Actor.CreateAgent().WithMailBox("test@example.com"),
                    Verb.Create("http://example.com/planned").AddDisplay("en-US", "planned"),
                    StatementObject.CreateSubStatement(
                        Actor.CreateAgent().WithMailBox("test@example.com"),
                        Verb.Create("http://example.com/visited").AddDisplay("en-US", "will visit"),
                        StatementObject.CreateActivity("http://example.com/website").AddName("en-US", "Some Awesome Website"))).Build();
            var json = statement.ToJson(true);
            Console.WriteLine(json);
        }
    }
}