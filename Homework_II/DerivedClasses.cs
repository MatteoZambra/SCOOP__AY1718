using System;
using scoopflix;

public class Movie : BaseContent, IPlayable, IVideo	{
	
	public string genre {get; set;}
	
	public Movie (string title, int duration, ArtWork art, string genre) : base (title, duration, art) {
		this.genre     =   genre;
		this.metalabel = "Movie";
	}
	
}

public class Documentary : BaseContent, IPlayable, IVideo {
	
	public string topic {get; set;}
	
	public Documentary (string title,int duration, ArtWork art, string topic) : base (title, duration, art) {
		this.topic     =         topic;
		this.metalabel = "Documentary";
	}
	
}

public class SerieEpisode : BaseContent, IPlayable, IVideo {
	
	public string serie    {get; set;}
	public int    season   {get; set;}
	public int    epNumber {get; set;}
	
	public SerieEpisode (string title, int duration, ArtWork art, string serie, int season, int epNumber)
		: base (title, duration, art) {
			this.serie     =           serie;
			this.season    =          season;
			this.epNumber  =        epNumber;
			this.metalabel = "Serie Episode";
		}
	
}