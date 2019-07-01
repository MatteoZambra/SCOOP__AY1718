using System;

class SerieEpisode {
	
	// C A M P I   E   P R O P R I E T A'
	
	private string   serie_title {get; set;}
	private string episode_title {get; set;}
	private    int     ep_number {get; set;}
	private    int  serie_number {get; set;}
	private string     metalabel {get; set;}
	private   int   ep_duration;
	
	public int EpDuration {
		get {return ep_duration;}
		set {ep_duration = 60 * value;}
	}
	
	// C O S T R U T T O R E
	
	public SerieEpisode (string ep_title, string serie_title, int ep_number, int serie_number, string tag) {
		this.episode_title =     ep_title;
		this.serie_title   =  serie_title;
		this.ep_number     =    ep_number;
		this.serie_number  = serie_number;
		this.metalabel     =          tag;
	}
	
	// M E T O D I
	
	public override string ToString () {
		string ToReturn = "Serie: " + this.serie_title + "\nEpisodio: " + this.episode_title + "\nStagione: " + this.serie_number + "\nEpisodio: " + this.ep_number + "\nDurata (sec): " + this.ep_duration;
		return ToReturn;
	}
	
	public string Meta() {
		return this.metalabel;
	}
}