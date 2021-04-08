using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace democart_automation.Pages
{
    class CartPage
    {

        private IWebDriver driver;
        private WebDriverWait wait;
        Int32 timeout = 10000;

        public CartPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            PageFactory.InitElements(driver, this);

        }

        public String getPageTitle()
        {
            return driver.Title;
        }

        [FindsBy(How = How.ClassName, Using = "ty-price")]
        [CacheLookup]
        private IList<IWebElement> total_cart;

        [FindsBy(How = How.XPath, Using = "(//div[@class='ty-float-right ty-cart-content__right-buttons'])[1]")]
        [CacheLookup]
        private IWebElement button_checkout;

        public String getTotal()
        {

            //with currency -> return total_cart[0].Text+" "+total_cart[1].Text;
            return total_cart[1].Text;
        }

        public CheckoutPage goTotheCheckout()
        {
            button_checkout.Click();
            return new CheckoutPage(driver);
        }
        public void load_complete()
        {
            wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
        }
    }
}
