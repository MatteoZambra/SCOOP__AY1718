using System;

class Documentary {
	
	// C A M P I  /  P R O P R I E T A'
	
	private string     title {get; set;}
	private string     topic {get; set;}
	private string metalabel {get; set;}
	private int   duration;
	
	public int Duration {
		get {return duration;}
		set {duration = 60 * value;} // conversione in secondi
	}
	
	// C O S T R U T T O R E
	
	public Documentary (string title, string topic, string tag) {
		this.title   = title;
		this.topic   = topic;
		this.metalabel = tag;
	}
	
	// M E T O D I
	
	public override string ToString() {
		string ToReturn = "Titolo: " + this.title + "\nTematica: " + this.topic + "\nDurata (sec): " + this.duration + "\n";
		return ToReturn;
	}
	
	public string Meta () {
		return this.metalabel;
	}
}