using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

public class WebDriverManager
{
    private static IWebDriver driver;
    private static WebDriverWait wait;

    private WebDriverManager()
    {
        // Constructor privado para evitar instanciación externa
    }

    public static void InitializeDriverAndWait()
    {
        if (driver == null)
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }
    }

    public static IWebDriver GetDriver()
    {
        InitializeDriverAndWait();
        return driver;
    }

    public static WebDriverWait GetWait()
    {
        InitializeDriverAndWait();
        return wait;
    }

    public static void QuitDriver()
    {
        if (driver != null)
        {
            driver.Quit();
            driver = null;
            wait = null;
        }
    }
}
