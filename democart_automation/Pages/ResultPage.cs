using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

namespace democart_automation.Pages
{
    class ResultPage
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        Int32 timeout = 10000;

        public ResultPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            PageFactory.InitElements(driver, this);

        }
        //[FindsBy(How =How.XPath,Using = "//div[@class='ty-column3']//div[@class='ty-grid-list__image']/div")]
        [FindsBySequence]
        [FindsBy(How = How.ClassName, Using = "ty-column3")]
        [FindsBy(How = How.ClassName, Using = "ty-grid-list__item-name")]
        [CacheLookup]
        private IList<IWebElement> product_grid;
        //List<IWebElement> web_results = new List<IWebElement>();

        public String getPageTitle()
        {
            return driver.Title;
        }

        public String getNameProduct()
        {
            return product_grid[0].Text;
        }

        public ProductPage chooseProduct()
        {

            try
            {
                product_grid[0].Click();
                //try to implement randomdly choose a product
            }
            catch (Exception err)
            {
                ScenarioContext.Current[("Error")] = err;
            }
            return new ProductPage(driver);
        }
        public void load_complete()
        {
            wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
        }
    }
}
