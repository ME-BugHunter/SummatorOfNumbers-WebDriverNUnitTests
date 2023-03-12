using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SummatorOfNumberNUnitTests
{
    public class SummatorOfNumbersTests
    {
        private WebDriver driver;
        private string baseUrl = "http://softuni-qa-loadbalancer-2137572849.eu-north-1.elb.amazonaws.com/number-calculator/";

        [SetUp]
        public void Setup()
        {
            this.driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(baseUrl);
        }

        [TearDown]
        public void CloseBrowser()
        {
            driver.Quit();
        }

        [Test]
        public void Test_Summator_CheckValidSum()
        {
            driver.FindElement(By.CssSelector("input#number1")).SendKeys("15");
            driver.FindElement(By.CssSelector("input#number2")).SendKeys("8");
            driver.FindElement(By.Id("operation")).Click();
            driver.FindElement(By.CssSelector("#operation > option:nth-child(2)")).Click();
            driver.FindElement(By.Id("calcButton")).Click();
            var actual = driver.FindElement(By.CssSelector("#result > pre")).Text;

            Assert.That(actual, Is.EqualTo("23"));
        }

        [Test]
        public void Test_Summator_InvalidInput()
        {
            driver.FindElement(By.CssSelector("input#number1")).SendKeys("");
            driver.FindElement(By.CssSelector("input#number2")).SendKeys("Hello");
            driver.FindElement(By.Id("operation")).Click();
            driver.FindElement(By.CssSelector("#operation > option:nth-child(2)")).Click();
            driver.FindElement(By.Id("calcButton")).Click();
            var actual = driver.FindElement(By.CssSelector("#result > i")).Text;

            Assert.That(actual, Is.EqualTo("invalid input"));
        }

        [Test]
        public void Test_Summator_CheckReset()
        {
            //fill the form and assert that fields aren't empty
            driver.FindElement(By.CssSelector("input#number1")).SendKeys("15");
            driver.FindElement(By.CssSelector("input#number2")).SendKeys("8");
            var operation = driver.FindElement(By.Id("operation"));
            operation.Click();
            driver.FindElement(By.CssSelector("#operation > option:nth-child(2)")).Click(); 
            driver.FindElement(By.Id("calcButton")).Click();

            var input1 = driver.FindElement(By.Id("number1")).GetAttribute("value");
            var input2 = driver.FindElement(By.Id("number2")).GetAttribute("value");
            var result = driver.FindElement(By.CssSelector("#result > pre")).Text;

            Assert.IsNotEmpty(result);
            Assert.IsNotEmpty(input1);
            Assert.IsNotEmpty(input2);

            driver.FindElement(By.Id("resetButton")).Click();

            var emptyNum1 = driver.FindElement(By.Id("number1")).GetAttribute("value");
            var emptyNum2 = driver.FindElement(By.Id("number2")).GetAttribute("value");
            
            Assert.IsEmpty(emptyNum1);
            Assert.IsEmpty(emptyNum2);
        }
    }
}