using Minecraft.DataBase;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using static System.Net.WebRequestMethods;
using Minecraft.Models;
using System.Windows.Documents;
using System.Collections.Generic;


namespace Minecraft.Controllers
{
    public class HtmlController
    {
        private int count = 0;
        private List<Models.Location> UniqueLocation = new List<Models.Location>();
        private List<string> UniqueDrop = new List<string>();
        private string uri = @"https://minecraft.fandom.com/wiki/Mob";

        public HtmlController()
        {
            InitializeWebDriver();
        }

        private void InitializeWebDriver()
        {
            Parse();
        }
        public async Task Parse()
        {
            HtmlWeb htmlWeb = new HtmlWeb();
            HtmlDocument htmlDoc = new HtmlDocument();

            try
            {
                htmlDoc = htmlWeb.Load(uri);
            }
            catch
            {
                throw;
            }

            using (DataBase.MobsDbContext db = new MobsDbContext())
            {
                HtmlNodeCollection hrefs = htmlDoc.DocumentNode.SelectNodes("//table[contains(@data-description, 'mobs')]/tbody/tr[1]/td/a");
                if (hrefs != null)
                {
                    foreach (HtmlNode node in hrefs)
                    {
                        if (count <= 20)
                        {
                            string aRef = "https://minecraft.fandom.com" + node.Attributes["href"].Value.ToString();
                            htmlDoc = htmlWeb.Load(aRef);

                            string Name = htmlDoc.DocumentNode.SelectSingleNode("//h2[@class='pi-item pi-item-spacing pi-title pi-secondary-background']").InnerText;

                            HtmlNodeCollection divCollection = htmlDoc.DocumentNode.SelectNodes("//div[@class='pi-data-value pi-font']");


                            string Health = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='pi-item pi-data pi-item-spacing pi-border-color' and @data-source='health']/div[@class='pi-data-value pi-font']").FirstChild.InnerText; //divCollection[0].ChildNodes[0].InnerText;

                            Models.Mob mob = new Models.Mob
                            {
                                MobName = Name,
                                MobHealth = int.Parse(Health),
                            };
                            db.Mobs.Add(mob);

                            HtmlNodeCollection locationCollection = htmlDoc.DocumentNode.SelectNodes("//div[@class='pi-item pi-data pi-item-spacing pi-border-color' and @data-source='spawn']/div/a");
                            string loc = "unknown";
                            if (locationCollection != null)
                            {
                                foreach (HtmlNode location in locationCollection)
                                {
                                    loc = location.InnerText;
                                }
                                if (!db.Locations.Any(a => a.SpawnName == loc))
                                {
                                    Models.Location location = new Location
                                    {
                                        SpawnName = loc,
                                    };
                                    db.Locations.Add(location);
                                    mob.Location.Add(location);
                                }
                            }
                            else
                            {

                            }

                            HtmlNodeCollection dropCollection = htmlDoc.DocumentNode.SelectNodes("//div[@class='pi-item pi-data pi-item-spacing pi-border-color' and @data-source='usableitems']/div/ul/.//a | //div[@class='pi-item pi-data pi-item-spacing pi-border-color' and @data-source='usableitems']/div//a");
                            string drop = "unknown";
                            if (dropCollection != null)
                            {
                                foreach (HtmlNode dropElement in dropCollection)
                                {
                                    drop = dropElement.Attributes["title"].Value;
                                }
                                if (!db.Drops.Any(c => c.DropName == drop))
                                {
                                    Models.Drop drop1 = new Drop
                                    {
                                        DropName = drop,
                                    };
                                    db.Drops.Add(drop1);
                                    mob.Drop.Add(drop1);
                                }
                            }
                            else
                            {

                            }
                            count++;
                            db.SaveChanges();

                        }



                    }




                }
            }
        }
    }
}
