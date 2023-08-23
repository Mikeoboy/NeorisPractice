using OpenQA.Selenium;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;

public class PageObjectModel
{
    IWebDriver driver = WebDriverManager.GetDriver();
    WebDriverWait wait = WebDriverManager.GetWait();

    protected By userInput = By.Id("user-name");
    protected By pswInput = By.Id("password");
    protected By loginButton = By.Id("login-button");
    protected By listOfProducts = By.XPath("//*[@class='inventory_container']//*[@class='inventory_item']//button");
    protected By removeButton = By.XPath("//*[text()='REMOVE']");
    protected By cartButton = By.CssSelector("[data-icon='shopping-cart']");
    protected By cartContent = By.Id("cart_contents_container");
    protected By checkoutButton = By.LinkText("CHECKOUT");
    protected By firstNameInput = By.Id("first-name");
    protected By lastNameInput = By.Id("last-name");
    protected By postalCodeInput = By.Id("postal-code");
    protected By continueButton = By.CssSelector("[value='CONTINUE']");
    protected By cartItemsToPay = By.XPath("//*[@class='cart_list']//*[@class='cart_item']");
    protected By finishButton = By.LinkText("FINISH");
    protected By thanksForOrderText = By.XPath("//*[text()='THANK YOU FOR YOUR ORDER']");




    public void inputCredentialsAndLogin(string user, string psw)
    {
        driver.FindElement(userInput).SendKeys(user);
        driver.FindElement(pswInput).SendKeys(psw);
        driver.FindElement(loginButton).Click();
    }

    public void chooseProductGivenIndex(int numberOfProductOne, int numberOfProductTwo)
    {
        IReadOnlyList<IWebElement> listOfProduct = driver.FindElements(listOfProducts);
        listOfProduct[numberOfProductOne].Click();
        listOfProduct[numberOfProductTwo].Click();

        //Agrego este para que despues de seleccionar dos elementos existan 2 botones de remove, asi verifico que se hayan seleccionado
        IReadOnlyList<IWebElement> removeButtons = driver.FindElements(removeButton);
        Assert.AreEqual(removeButtons.Count, 2);
    }

    public int cartitems()
    {
        IReadOnlyList<IWebElement> listOfItems = driver.FindElements(cartItemsToPay);
        return listOfItems.Count;
    }

    public void fillForm(string firstName, string lastName, string zipcode)
    {
        driver.FindElement(firstNameInput).SendKeys(firstName);
        driver.FindElement(lastNameInput).SendKeys(lastName);
        driver.FindElement(postalCodeInput).SendKeys(zipcode);
        driver.FindElement(continueButton).Click();
    }

    public void clickOnCartButton() { driver.FindElement(cartButton).Click(); }
    public void clickOnCheckoutButton() { driver.FindElement(checkoutButton).Click(); }
    public void clickFinishButton() { driver.FindElement(finishButton).Click(); }
    public bool isCartContentVisible() {return elementIsPresent(driver, cartContent); }
    public bool isMessageThanksVisible() {return elementIsPresent(driver, thanksForOrderText); }



    public bool elementIsPresent(IWebDriver driver, By locator)
    {
        try
        {
            wait.Until(driver => driver.FindElement(locator));
            return true;
        }
        catch { }
        return false;
    }



}
