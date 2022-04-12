using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CC.Plugins.Subscription.Models
{
    public class MarketSubscriptionResponse
    {
        private const string imagePath = @"~/Themes/GreatClips/Content/Images/";
        public int Id { get; set; }

        public int MarketId { get; set; }

        public string MarketCode { get; set; }

        public string MarketName { get; set; }

        public string ProgramCode { get; set; }

        public string ProgramName { get; set; }

        public string ShortDescription { get; set; }

        public string LongDescription { get; set; }

        public int NumberOfLevels { get; set; }

        public int Level { get; set; }

        public int MaxVolume { get; set; }

        public string IconPath
        {
            get { return string.Format("{0}_Icon.png", ProgramCode); }
        }

        public double Volume
        {
            get { return ((double)Level / (double)NumberOfLevels) * 100; }
        }

        public string PictureDecorationPath
        {
            get
            {
                return string.Format("{0}_line.png", ProgramCode);
            }
        }

        

    }
}