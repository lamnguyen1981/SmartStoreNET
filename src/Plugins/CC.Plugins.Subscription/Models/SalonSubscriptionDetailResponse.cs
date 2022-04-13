using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CC.Plugins.Subscription.Models
{
    public class SalonSubscriptionDetailResponse
    {
        private const string imagePath = @"~/Themes/GreatClips/Content/Images/";
        public int Id { get; set; }

        public int SalonId { get; set; }

        public string SalonCode { get; set; }

        public string SalonName { get; set; }

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

        public decimal Volume
        {
            get { return ((decimal)Level / (decimal)NumberOfLevels) * 100; }
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