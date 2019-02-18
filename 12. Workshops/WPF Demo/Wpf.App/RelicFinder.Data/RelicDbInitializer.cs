namespace RelicFinder.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.EntityFrameworkCore;

    using Models;

    public static class RelicDbInitializer
    {
        public static void Seed(RelicFinderDbContext context)
        {
            context.Database.Migrate();

            if (!context.Relics.Any())
            {
                SeedRelics(context);
            }
        }

        private static void SeedRelics(RelicFinderDbContext context)
        {
            var myRelic = new Relic()
            {
                Name = "The Ark",
                Type = "Container",
                Description = "The Ark of the Covenant (Hebrew: אָרוֹן הַבְּרִית, Modern: Arōn Ha'brēt, Tiberian: ʾĀrôn Habbərîṯ), also known as the Ark of the Testimony, is a gold-covered wooden chest with lid cover described in the Book of Exodus as containing the two stone tablets of the Ten Commandments. According to various texts within the Hebrew Bible, it also contained Aaron's rod and a pot of manna. Hebrews 9:4 describes: \"The ark of the covenant [was] covered on all sides with gold, in which was a golden jar holding the manna, and Aaron's rod which budded, and the tablets of the covenant.\"\r\n\r\nThe biblical account relates that, approximately one year after the Israelites' exodus from Egypt, the Ark was created according to the pattern given to Moses by God when the Israelites were encamped at the foot of biblical Mount Sinai. Thereafter, the gold-plated acacia chest was carried by its staves while en route by the Levites approximately 2,000 cubits (approximately 800 meters or 2,600 feet) in advance of the people when on the march or before the Israelite army, the host of fighting men. When carried, the Ark was always hidden under a large veil made of skins and blue cloth, always carefully concealed, even from the eyes of the priests and the Levites who carried it. God was said to have spoken with Moses \"from between the two cherubim\" on the Ark's cover. When at rest the tabernacle was set up and the holy Ark was placed in it under the veil of the covering, the staves of it crossing the middle side bars to hold it up off the ground. According to the Book of Exodus, God instructed Moses on Mount Sinai during his 40-day stay upon the mountain within the thick cloud and darkness where God was and he was shown the pattern for the tabernacle and furnishings of the Ark to be made of shittim wood to house the Tablets of Stone. Moses instructed Bezalel and Oholiab to construct the Ark. In Deuteronomy, however, the Ark is said to have been built specifically by Moses himself without reference of Bezalel or Oholiab.\r\n\r\nThe Book of Exodus gives detailed instructions on how the Ark is to be constructed. It is to be 2​1⁄2 cubits in length, 1​1⁄2 in breadth, and 1​1⁄2 in height (approximately 131×79×79 cm or 52×31×31 in). Then it is to be gilded entirely with gold, and a crown or molding of gold is to be put around it. Four rings of gold are to be attached to its four corners, two on each side—and through these rings staves of shittim-wood overlaid with gold for carrying the Ark are to be inserted; and these are not to be removed. A golden lid, the kapporet (traditionally \"mercy seat\" in Christian translations) which is covered with 2 golden cherubim, is to be placed above the Ark. Missing from the account are instructions concerning the thickness of the mercy seat and details about the cherubim other than that the cover be beaten out the ends of the Ark and that they form the space where God will appear. The Ark is finally to be placed under the veil of the covering."
            };

            var anotherRelic = new Relic()
            {
                Name = "Crystal Skull",
                Type = "Remains",
                Description = "The crystal skulls are human skull hardstone carvings made of clear or milky white quartz (also called \"rock crystal\"), claimed to be pre-Columbian Mesoamerican artifacts by their alleged finders; however, these claims have been refuted for all of the specimens made available for scientific studies.\r\n\r\nThe results of these studies demonstrated that those examined were manufactured in the mid-19th century or later, almost certainly in Europe during a time when interest in ancient culture was abundant.[1][2] The skulls were crafted in the 19th century in Germany, quite likely at workshops in the town of Idar-Oberstein, which was renowned for crafting objects made from imported Brazilian quartz in the late 19th century.[3] Despite some claims presented in an assortment of popularizing literature, legends of crystal skulls with mystical powers do not figure in genuine Mesoamerican or other Native American mythologies and spiritual accounts.[4]\r\n\r\nThe skulls are often claimed to exhibit paranormal phenomena by some members of the New Age movement, and have often been portrayed as such in fiction. Crystal skulls have been a popular subject appearing in numerous sci-fi television series, novels, films, and video games. "
            };

            var yetAnotherRelic = new Relic()
            {
                Name = "The Holy Grail",
                Type = "Utensil",
                Description = "The Holy Grail is a treasure that serves as an important motif in Arthurian literature. Different traditions describe it as a cup, dish or stone with miraculous powers that provide happiness, eternal youth or sustenance in infinite abundance, often in the custody of the Fisher King. The term \"holy grail\" is often used to denote an elusive object or goal that is sought after for its great significance. A \"grail\", wondrous but not explicitly holy, first appears in Perceval, le Conte du Graal, an unfinished romance written by Chrétien de Troyes around 1190. Here, Chrétien's story attracted many continuators, translators and interpreters in the later 12th and early 13th centuries, including Wolfram von Eschenbach, who perceived the Grail as a stone. In the late 12th century, Robert de Boron wrote in Joseph d'Arimathie that the Grail was Jesus's vessel from the Last Supper, which Joseph of Arimathea used to catch Christ's blood at the Crucifixion. Thereafter, the Holy Grail became interwoven with the legend of the Holy Chalice, the Last Supper cup, a theme continued in works such as the Vulgate Cycle, the Post-Vulgate Cycle, and Le Morte d'Arthur."
            };

            var relics = new List<Relic>();

            relics.Add(myRelic);
            relics.Add(anotherRelic);
            relics.Add(yetAnotherRelic);

            context.Relics.AddRange(relics);
            context.SaveChanges();
        }
    }
}