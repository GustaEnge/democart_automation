using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace democart_automation.Pages
{
    class ProductPage
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        Int32 timeout = 10000;
        private string id_product;


        public ProductPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            PageFactory.InitElements(driver, this);

        }

        public String getPageTitle()
        {
            return driver.Title;
        }

        public void getIdProduct()
        {
            string pattern = @"(\d+)";
            id_product = (Regex.Match(id_prod_element.GetAttribute("id"), pattern)).Value;
        }

        public string getPriceProduct()
        {
            getIdProduct();
            return driver.FindElement(By.Id($"sec_discounted_price_{id_product}")).Text;
        }
        [FindsBy(How = How.XPath, Using = "//div[contains(@class,'-wrapper')]//div[contains(@class,'cm-reload-')]")]
        private IWebElement id_prod_element;

        //[FindsBy(How = How.Id, Using = "button_cart_11")]
        [FindsBy(How = How.CssSelector, Using = "button[id *= 'button_cart']")]
        private IWebElement addCart;

        [FindsBy(How = How.Id, Using = "sw_dropdown_8")]
        private IWebElement buttonMyCart;


        /*[FindsBySequence]
        [FindsBy(How = How.ClassName, Using = "dropdown_8")]
        [FindsBy (How=How.ClassName, Using = "ty-float-left")]
        For some reason the popup didn't work from ClassName method, it didn't find it*/
        //[FindsBy(How=How.XPath,Using = "//*[@id='dropdown_8']/div/div[2]/div[1]")]
        //private IWebElement cart_page;

        //[FindsBy(How = How.ClassName, Using = "notification-body-extended")]
        //private IWebElement popUp_element;
        public CartPage addtoTheCart()
        {

            //cart_page.Click();
            try
            {


                addCart.Click();
                IWebElement popUp_element = wait.Until(a => a.FindElement(By.ClassName("notification-body-extended")));

                //async_delay(10000); here the method will not affect the main thread, that's why i didn't work
                //Thread.Sleep(7000); // here the main thread is affected/frozen untill the method has ended
                //wait.Until(ExpectedConditions.StalenessOf(popUp_element));
                //Thread.Sleep(6000);
                wait.Until(ExpectedConditions.StalenessOf(popUp_element));
                buttonMyCart.Click();

                IWebElement cart_page = wait.Until(a => a.FindElement(By.XPath("//*[@id='dropdown_8']/div/div[2]/div[1]")));
                cart_page.Click();

            }
            catch (Exception err)
            {
                ScenarioContext.Current[("Error")] = err;
            }

            return new CartPage(driver);
        }
        async void async_delay(int num)
        {
            await Task.Delay(num);
        }


        public void load_complete()
        {
            wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
        }
    }
}

