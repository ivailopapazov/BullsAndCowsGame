namespace BullsAndCows.Web.ViewModels
{
    using BullsAndCows.Services.DTO;
    using System;
    using System.Linq.Expressions;

    public class HighScoreViewModel
    {
        public static Expression<Func<HighScoreDto, HighScoreViewModel>> FromHighScoreDto
        {
            get
            {
                return hs => new HighScoreViewModel()
                {
                    UserName = hs.UserName,
                    WinCount = hs.WinCount,
                    LoseCount = hs.LoseCount
                };
            }
        }

        public string UserName { get; set; }

        public int WinCount { get; set; }

        public int LoseCount { get; set; }
    }
}