using democart_automation.Pages;
using democart_automation.Hooks;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;

namespace democart_automation.Steps
{
    [Binding]
    public class CartTestAppSteps
    {
        IWebDriver driver;
        //ChromeDriver driver;
        HomePage home_page;
        ResultPage result_page;
        ProductPage product_page;
        CartPage cart_page;
        CheckoutPage checkout_page;

        String name_product;
        String price_product;

        [Given(@"I am on the Demo Store page")]
        public void GivenIAmOnTheDemoStorePage()
        {
            string expected_title = "Shopping Cart Software & Ecommerce Software Solutions by CS-Cart";

            driver = MainHook.getDriver();
            driver.Manage().Window.Maximize();
            home_page = new HomePage(driver);
            home_page.goToPage();
            home_page.load_complete();

            String result_pageTitle = home_page.getPageTitle();
            Assert.That(expected_title == result_pageTitle);


        }

        [When(@"I search for (.*)")]
        public void WhenSearchFor(string term)
        {
            home_page.search_product(term);
            result_page = new ResultPage(driver);
        }

        [When(@"The page loads the searched product: (.*)")]
        public void WHENLoadsThePageWithSearchedProduct(string p0)
        {
            string expected_title = "Resultados da pesquisa";

            String result_pageTitle = home_page.getPageTitle();
            result_page.load_complete();
            Assert.That(expected_title == result_pageTitle);

            name_product = result_page.getNameProduct();
            result_page.chooseProduct();

            product_page = new ProductPage(driver);
        }

        [When(@"I click on the chosen product and add it to the cart")]
        public void WhenClickOnTheChosenProductAndAddItToTheCart()
        {
            //string expected_title = name_product;

            String result_pageTitle = product_page.getPageTitle();
            product_page.load_complete();
            Assert.That(result_pageTitle.Contains(name_product));

            price_product = product_page.getPriceProduct();
            product_page.addtoTheCart();

            cart_page = new CartPage(driver);
        }


        [When(@"I check this (.*) on cart as well as total price")]
        public void WhenCheckThisOnCartAsWellAsTotalPrice(string p0)
        {
            string expected_title = "Conteúdo do carrinho";
            cart_page.load_complete();
            Assert.That(cart_page.getPageTitle() == expected_title);
            Assert.That(cart_page.getTotal() == price_product);
            cart_page.goTotheCheckout();
            checkout_page = new CheckoutPage(driver);

        }

        [When(@"I do the checkout, fill in the (.*), (.*),(.*),(.*),(.*),(.*),(.*) and select Phone Order Payment by passing (.*)")]
        public void WhenDoTheCheckoutFillInTheAndSelectPhoneOrderPaymentByPassing(string country, string state, string city, string address, string cep, string email, string name, string phone)
        {
            string expected_title = "Checkout";
            checkout_page.load_complete();
            Assert.That(checkout_page.getPageTitle() == expected_title);
            checkout_page.filledOut(country, state, city, address, cep, email, name, phone);
            checkout_page.captchaStep();
        }


        [Then(@"proceed with the order.")]
        public void ThenProceedWithTheOrder()
        {


        }
    }
}
