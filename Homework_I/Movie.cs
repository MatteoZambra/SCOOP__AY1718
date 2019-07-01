using System;

class Movie {
	
	// P r o p r i e t à;
	
	private string      title {get; set;} 
	private string      genre {get; set;}
	private string  metalabel {get; set;}
	private int    duration;
	
	public int Duration {
		get {return duration;}
		set {duration = 60 * value;} // conversione in secondi
	}
	
	// C O S T R U T T O R E
	/*
	public Movie (string title, string genre, int duration) {
		this.title    =    title;
		this.genre    =    genre;
		this.duration = duration;
	}
	*/
	
	// C O S T R U T T O R E  2

	public Movie (string title, string genre, string tag) {
		this.title   = title;
		this.genre   = genre;
		this.metalabel = tag;
	}
	
	
	
	// ,,ToString''  m e t h o d
	
	public override string ToString() {
		string ToReturn = "Titolo: " + this.title + "\nGenere: " + this.genre + "\nDurata (sec): " + this.duration + "\n";
		return ToReturn;
	}
	
	// M e t a l a b e l  method
	
	public string Meta () {
		return this.metalabel;
	}
}