using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using SeleniumExtras.PageObjects;
using System.Collections.Generic;
using System.Text;

namespace democart_automation.Pages
{
    class HomePage
    {
        string url = "http://demo.alt-team.com/4_demo3/index.php";
        private IWebDriver driver;
        private WebDriverWait wait;
        Int32 timeout = 10000;

        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            PageFactory.InitElements(driver, this);

        }

        [FindsBy(How = How.Name, Using = "hint_q")]
        [CacheLookup]
        private IWebElement elem_search;

        [FindsBy(How = How.Id, Using = "bp_off_bottom_panel")]
        [CacheLookup]
        private IWebElement hide_panel;

        public void goToPage()
        {
            driver.Navigate().GoToUrl(url);
        }

        public String getPageTitle()
        {
            return driver.Title;
        }

        public ResultPage search_product(string term)
        {
            hide_panel.Click();
            elem_search.SendKeys(term);
            elem_search.Submit();
            return new ResultPage(driver);
        }
        public void load_complete()
        {
            wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
        }
    }
}

