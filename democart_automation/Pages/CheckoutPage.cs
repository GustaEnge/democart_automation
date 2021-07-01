using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace democart_automation.Pages
{
    class CheckoutPage
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        Int32 timeout = 10000;
        private string id_product;
        private Actions actions;
        public CheckoutPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            PageFactory.InitElements(driver, this);
            actions = new Actions(driver);

        }

        [FindsBy(How = How.Id, Using = "litecheckout_country")]
        [CacheLookup]
        private IWebElement country_field;


        [FindsBy(How = How.Id, Using = "litecheckout_state")]
        [CacheLookup]
        private IWebElement state_field;


        [FindsBy(How = How.Id, Using = "litecheckout_city")]
        [CacheLookup]
        private IWebElement city_field;

        [FindsBy(How = How.Id, Using = "shipping_rates_list")]
        [CacheLookup]
        private IWebElement update_shipping;

        [FindsBy(How = How.Id, Using = "litecheckout_s_address")]
        [CacheLookup]
        private IWebElement address_field;

        [FindsBy(How = How.Id, Using = "litecheckout_s_zipcode")]
        [CacheLookup]
        private IWebElement zipcode_field;

        [FindsBy(How = How.Id, Using = "litecheckout_fullname")]
        [CacheLookup]
        private IWebElement name_field;

        [FindsBy(How = How.Id, Using = "litecheckout_email")]
        [CacheLookup]
        private IWebElement email_field;

        //[FindsBy(How = How.ClassName, Using = "litecheckout__shipping-method__title")]
        //[CacheLookup]
        //private IWebElement phone_ordering;
        public String getPageTitle()
        {
            return driver.Title;
        }

        public void filledOut(string country, string state, string city, string address, string cep, string email, string name, string phone)
        {


            var selectCountry = new SelectElement(country_field);

            selectCountry.SelectByText(country);

            if (state_field.Selected)
            {
                var selectState = new SelectElement(state_field);
                selectCountry.SelectByText(country);
            }

            state_field.SendKeys(state);
            city_field.Clear();
            city_field.SendKeys(city);

            wait.Until(ExpectedConditions.ElementToBeClickable(update_shipping));
            update_shipping.Click();
            //wait.Until(ExpectedConditions.ElementToBeClickable(update_shipping));
            Thread.Sleep(4000);//usar um wait aqui esperar o elemento carregar completamente

            actions.MoveToElement(address_field);

            //address_field.Click();
            address_field.SendKeys(address);
            zipcode_field.Clear();
            zipcode_field.SendKeys(cep);
            name_field.SendKeys(name);
            email_field.SendKeys(email);

            IWebElement phone_ordering = wait.Until(e => e.FindElement(By.Id("payments_2")));
            phone_ordering.Click();

            IWebElement phone_number = wait.Until(e => e.FindElement(By.Id("customer_phone")));
            IWebElement checkbox_terms = wait.Until(e => e.FindElement(By.Name("accept_terms")));

            actions.MoveToElement(checkbox_terms);
        
            phone_number.SendKeys(phone);
            checkbox_terms.Click();

        }

        public void captchaStep()
        {

            //driver.SwitchTo().Frame(1);
            string name_class = "cm-required cm-recaptcha ty-captcha__label";
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            string iframe_ref = Convert.ToString(jse.ExecuteScript($"return (document.getElementsByClassName(\"{name_class}\")[0]).getAttribute(\"for\")"));
            //Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>"+iframe_ref);
            IWebElement captcha = wait.Until(a => a.FindElement(By.Id(iframe_ref)));
            actions.MoveToElement(captcha);
            captcha.Click();

        }

        public void load_complete()
        {
            wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
        }
    }
}
