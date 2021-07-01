using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TechTalk.SpecFlow;

namespace democart_automation.Hooks
{
    [Binding]
    class MainHook
    {
        public static IWebDriver driver;
        public static ExtentTest featureName;
        public static ExtentTest scenario;
        public static ExtentReports extent;
        public static dynamic projectPath;
        public static string path_chrome;
        public static int numSteps;
        public static IWebDriver getDriver()
        {
            return driver;
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            //Here this part will get the fullpath of this project (current) and extract only the local
            var path = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            var actualPath = path.Substring(0, path.LastIndexOf("bin"));
            projectPath = new Uri(actualPath).LocalPath;
            Directory.CreateDirectory(projectPath.ToString() + "Reports");

            var reportPath = projectPath + "Reports\\Index.html";

            var htmlReporter = new ExtentHtmlReporter(reportPath);
            htmlReporter.LoadConfig(projectPath + "report-config.xml"); // load based on report-config.xml
            htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);

            //path_chrome = projectPath.ToString();
            path_chrome = @"C:\";
            //path_chrome = Environment.GetEnvironmentVariable("webdriver.chrome.driver");

        }

        [BeforeFeature]
        public static void BeforeFeature()
        {
            //Create dynamic feature name
            featureName = extent.CreateTest<Feature>(FeatureContext.Current.FeatureInfo.Title);
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            extent.Flush();


            //driver = new ChromeDriver();
            //driver.Navigate().GoToUrl(projectPath.ToString() + @"\Reports\index.html");
        }

        [BeforeScenario]
        public static void BeforeScenario()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("no-sandbox");
            options.AddArgument("--lang = pt");
            //ChromeDriver driver = new ChromeDriver(ChromeDriverService.CreateDefaultService(), options, TimeSpan.FromMinutes(3));
            //driver.Manage().Timeouts().PageLoad.Add(System.TimeSpan.FromSeconds(30));

            driver = new ChromeDriver(path_chrome);


            //driver = new ChromeDriver();
            scenario = featureName.CreateNode<Scenario>(ScenarioContext.Current.ScenarioInfo.Title);
            
        }

        [AfterScenario]
        public static void AfterScenario()
        {
            //var scenarioType = ScenarioContext.Current.ScenarioInfo.ScenarioAndFeatureTags.Length;
            //Console.WriteLine(">>>>>>>>>>>>>AfterScenario"+scenarioType+"<<<<<<<<<<<");
            //if(ScenarioContext.Current.TestError != null)
            //{
            //    scenario.Fail(ScenarioContext.Current.TestError.InnerException);
            //}
            //Console.WriteLine(">>>>>>>>>>>>" + ScenarioContext.Current.TestError + "<<<<<<<<<<<");
            
            driver.Quit();
            numSteps = 0;
        }

        [AfterStep]
        public void InsertReportingSteps()
        {
            numSteps++;
            var stepType = ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString();

            if (ScenarioContext.Current.ScenarioExecutionStatus == null)
            {
                if (stepType == "Given")
                    scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text);
                else if (stepType == "When")
                    scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text);
                else if (stepType == "Then")
                    scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text);
                else if (stepType == "And")
                    scenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text);
            }
/*-->>>>>>*/else if (ScenarioContext.Current.TestError != null)
            {
                if (stepType == "Given")
                    scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message);
                else if (stepType == "When")
                    scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message); //InnerException
                else if (stepType == "Then")
                    scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message);
                else if (stepType == "And")
                    scenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message);
            }

            if (ScenarioContext.Current.ToString() == "StepDefinitionPending")
            {
                if (stepType == "Given")
                    scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
                else if (stepType == "When")
                    scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
                else if (stepType == "Then")
                    scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
                else if (stepType == "And")
                    scenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
            }

            
        }

    }
}
