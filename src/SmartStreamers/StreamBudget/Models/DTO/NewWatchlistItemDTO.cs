namespace StreamBudget.Models.DTO
{
    public class NewWatchlistItemDTO
    {
        public int CurWatchlistId {get; set;}
        public string Title { get; set;} 
        public string ImdbId { get; set;}
        public int FirstYear { get; set;}
        public int Runtime { get; set;}
        public int TotalEpisodeCount { get; set;}
    }
}
