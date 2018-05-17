using TechTalk.SpecFlow;

namespace AGLWebServiceTest.Environment
{
    [Binding]
    public static class ScenarioHttpClient
    {
        [BeforeScenario(Order = 1)]
        public static void Setup()
        {
            ScenarioContext.Current[nameof(ScenarioHttpClient)] = CreateHttpClient();
        }

        public static HttpClient GetHttpClient(this ScenarioContext scenarioContext)
        {
            return (HttpClient)scenarioContext[nameof(ScenarioHttpClient)];
        }

        [AfterScenario]
        public static void Teardown()
        {
            ScenarioContext.Current[nameof(ScenarioHttpClient)] = null;
        }

        public static HttpClient CreateHttpClient()
        {
            return HttpClient.Instance;
        }
    }
}
