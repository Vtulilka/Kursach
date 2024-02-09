using Minecraft.DataBase;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using static System.Net.WebRequestMethods;


namespace Minecraft.Controllers
{
    public class HtmlController
    {
        private IWebDriver driver;
        private string uri = @"https://minecraft.fandom.com/wiki/Mob";

        public HtmlController()
        {
            InitializeWebDriver();
        }

        private void InitializeWebDriver()
        {
            //driver = new ChromeDriver();
            //driver.Url = @"https://minecraft.fandom.com/wiki/Mob";
            func();
        }
        public void func()
        {
            HtmlWeb htmlWeb = new HtmlWeb();
            HtmlDocument htmlDoc = new HtmlDocument();

            htmlDoc = htmlWeb.Load(uri);
            HtmlNodeCollection hrefs = htmlDoc.DocumentNode.SelectNodes("//table[contains(@data-description, 'mobs')]/tbody/tr[1]/td/a");
            if (hrefs != null)
            {
                foreach (HtmlNode node in hrefs)
                {
                    string aRef = "https://minecraft.fandom.com" + node.Attributes["href"].Value.ToString();
                    htmlDoc = htmlWeb.Load(aRef);

                    string Name = htmlDoc.DocumentNode.SelectSingleNode("//h2[@class='pi-item pi-item-spacing pi-title pi-secondary-background']").InnerText;

                    //string HealthElement = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='pi-item pi-data pi-item-spacing pi-border-color']").InnerText;
                    HtmlNodeCollection divCollection = htmlDoc.DocumentNode.SelectNodes("//div[@class='pi-data-value pi-font']");
                    string Health = divCollection[0].ChildNodes[0].InnerText;

                    foreach (HtmlNode location in divCollection[2].SelectNodes("./a"))
                    {
                        string loc = location.InnerText;
                    }
                    //string LocationContainer = htmlDoc.DocumentNode.SelectSingleNode("../div[@class='pi-data-value pi-font']").InnerText;

                    //string LocationLinks = htmlDoc.DocumentNode.SelectSingleNode(".//a"));
                    //var Location_ = LocationLinks.Select(link => link.Text);

                    //var _location = string.Join(", ", Location_);


                    foreach (HtmlNode dropElement in divCollection[divCollection.Count() - 2].SelectNodes(".//a"))
                    {
                        string drop = dropElement.Attributes["title"].Value;
                    }
                    //var DropContainer = DropElement.FindElement(By.XPath("../div[@class='pi-data-value pi-font']/ul/li"));

                    //var DropLinks = DropContainer.FindElements(By.XPath(".//a"));
                    //var Drop_ = DropLinks.Select(link => link.Text);

                    //var _drop = string.Join(", ", Drop_);
                }
            }
        }
        public async Task ParseData()
        {
            await Task.Delay(3000);

            int fileNumber = 1;
            while (fileNumber <= 1)
            {
                await Task.Delay(1000);
                var filmLink = driver.FindElement(By.XPath($"(//tbody//tr//td/a)[{fileNumber}]"));
                filmLink.Click();
                await Task.Delay(1000);

                var oldNumber = string.Empty;
                var counter = 0;
                while (true)
                {
                    await Task.Delay(3000);
                    counter++;
                    if (counter == 8)
                        break;
                    await Task.Delay(500);
                    var Name = driver.FindElement(By.XPath("//h2[@class='pi-item pi-item-spacing pi-title pi-secondary-background' and contains(text(), 'title')]/ya-tr-span")).GetAttribute("textContent");

                    var HealthElement = driver.FindElement(By.XPath("//div[@class='pi-item pi-data pi-item-spacing pi-border-color' and contains(text(), 'health')]"));
                    var Health = HealthElement.FindElement(By.XPath("../div[@class='pi-data-value pi-font']/ya-tr-span")).GetAttribute("textContent");

                    var LocationElement = driver.FindElement(By.XPath("//div[@class='pi-item pi-data pi-item-spacing pi-border-color' and contains(text(), 'spawn')]"));
                    var LocationContainer = LocationElement.FindElement(By.XPath("../div[@class='pi-data-value pi-font']"));

                    var LocationLinks = LocationContainer.FindElements(By.XPath(".//a"));
                    var Location_ = LocationLinks.Select(link => link.Text);

                    var _location = string.Join(", ", Location_);

                    var DropElement = driver.FindElement(By.XPath("//div[@class='pi-item pi-item-spacing pi-border-color' and contains(text(), 'usableitems')]"));
                    var DropContainer = DropElement.FindElement(By.XPath("../div[@class='pi-data-value pi-font']/ul/li"));

                    var DropLinks = DropContainer.FindElements(By.XPath(".//a"));
                    var Drop_ = DropLinks.Select(link => link.Text);

                    var _drop = string.Join(", ", Drop_);

                    using (var context = new MobsDbContext())
                    {
                        var existingMob = context.Mobs.FirstOrDefault(f => f.MobName == Name);

                        if (existingMob == null)
                        {
                            existingMob = new Models.Mob()
                            {
                                MobName = Name,
                                MobHealth = int.Parse(Health)
                            };
                            context.Mobs.Add(existingMob);
                        }
                        else
                        {
                            existingMob.MobName = Name;
                            existingMob.MobHealth = int.Parse(Health);
                        }

                        var LocationNames = _location.Split(',');
                        foreach (var locationName in LocationNames)
                        {
                            var Location = context.Locations.FirstOrDefault(a => a.SpawnName == locationName.Trim());
                            if (Location == null)
                            {
                                Location = new Models.Location()
                                {
                                    SpawnName = locationName.Trim()
                                };
                                context.Locations.Add(Location);
                            }
                            existingMob.Location.Add(Location);
                        }

                        var DropNames = _drop.Split(',');
                        foreach (var name in DropNames)
                        {
                            var Drop = context.Drops.FirstOrDefault(c => c.DropName == name.Trim());
                            if (Drop == null)
                            {
                                Drop = new Models.Drop()
                                {
                                    DropName = name.Trim()
                                };
                                context.Drops.Add(Drop);
                            }
                            existingMob.Drop.Add(Drop);
                        }
                        context.SaveChanges();
                    }
                }
                driver.Navigate().Back();
                fileNumber++;
            }
            driver.Quit();
        }
    }
}
