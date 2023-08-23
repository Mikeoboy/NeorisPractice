using NUnit.Framework;
using OpenQA.Selenium;
using System.Net.Http.Json;

namespace Neoris3
{
    [TestFixture]
    public class Saucedemotests
    {
        private HttpClient client;
        private IWebDriver driver;


        [SetUp]
        public void Setup()
        {
            driver = WebDriverManager.GetDriver();
            driver.Manage().Window.Maximize();

            client = new HttpClient
            {
                BaseAddress = new Uri("https://api.demoblaze.com")
            };
        }

        [TearDown]
        public void TearDown()
        {
            WebDriverManager.QuitDriver();
        }

        [Test]
        public void TestSaucedemoScenario()
        {
            PageObjectModel pom = new PageObjectModel();
            driver.Navigate().GoToUrl("https://www.saucedemo.com/v1/");
            pom.inputCredentialsAndLogin("standard_user", "secret_sauce");
            pom.chooseProductGivenIndex(1, 3);
            pom.clickOnCartButton();
            Assert.IsTrue(pom.isCartContentVisible());
            pom.clickOnCheckoutButton();
            pom.fillForm("Miguel", "Gutierrez", "99630");
            Assert.AreEqual(pom.cartitems(), 2);
            pom.clickFinishButton();
            Assert.IsTrue(pom.isMessageThanksVisible());


        }

        [Test]
        public async Task createUserTestApi()
        {
            //Aqui cree el username con la fecha para que cada que corra el testcase cree un nuevo usuario actual

            var requestData = new
            {
                username = $"newUser_{DateTime.Now.AddSeconds}",
                password = "password"
            };
            HttpResponseMessage response = await client.PostAsJsonAsync("/signup", requestData);
            Assert.IsTrue(response.IsSuccessStatusCode);
        }

        [Test]
        public async Task CreateExistentUserTestApi()
        {
            var requestData = new
            {
                username = $"Miguel",
                password = "Trestres"
            };
            HttpResponseMessage response = await client.PostAsJsonAsync("/signup", requestData);

            string responseContent = await response.Content.ReadAsStringAsync();

            Assert.IsTrue(response.IsSuccessStatusCode);
            Assert.IsTrue(responseContent.Contains("This user already exist."));
        }        
        
        [Test]
        public async Task loginToPageApi()
        {
            /*aqui como comentario la prueba funciona como deberia pero testeando manual la pagina y abriendo 
            las devtools revise que aun cuando mande un user o psw incorrecto regresa 200, entonces esta mal la pagina */
            
            var requestData = new
            {
                username = $"NeorisTest",
                password = "NeorisTest"
            };
            HttpResponseMessage response = await client.PostAsJsonAsync("/login", requestData);
            Assert.IsTrue(response.IsSuccessStatusCode);
        }


        [Test]
        public async Task loginToPageWithIncorrectDataApi()
        {
            var requestData = new
            {
                username = $"NeorisTestNOTREAL",
                password = "NOTREALDATA"
            };

            HttpResponseMessage response = await client.PostAsJsonAsync("/login", requestData);
            string responseContent = await response.Content.ReadAsStringAsync();
            
            Assert.IsTrue(responseContent.Contains("User does not exist."));
        }




    }
}

