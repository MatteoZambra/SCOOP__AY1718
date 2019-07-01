using System;
using scoopflix;

// all classes previously defined, are stored in a single namespace, in order
// to be more easily callable from the playlist namespace and module

namespace NSPClasses{
	
	//   B A S E _ C L A S S 
	
	public class BaseContent: IPlayable, IVideo {
		
		// F I E L D S
		public string title     {get; set;}
		public string metalabel {get; set;}
		protected int             duration;
		public           ArtWork thumbnail;
		
		protected static string          State = "instantiated";
		protected static DateTime    timeStart;
		protected static DateTime   timeAction;
		protected static DateTime  timeElapsed;
		
		
		// C O N S T R U C T O R
		public BaseContent (string title, int duration, ArtWork art) {
			this.title     =         title;
			this.duration  = duration * 60;  // convert to seconds
			this.thumbnail =           art;
		}
		
		// method to display duration
		public int Duration() {
			return (int) this.duration;
		}
		
		
		// I N T E R F A C E S _ M E T H O D S _ I M P L E M E N T A T I O N
		
		// IImage implemented in ArtWork class yet (cfr. Header.cs file)
		
		// IVideo implementation
		
		public string GetTitle() {
			return (string) this.title;
		}
		
		public int GetDurationMins() {
			return (int) this.duration / 60;
		}
		
		public IImage GetArtWork() {
			return this.thumbnail; 
		}
		
		
		// IPlayable implementation
		// messages commented, since for the sake of this assignment 
		// a over verbose terminal messaging is redundant
		
		public void Play() {
			if (State == "instantiated") {
				State = "Playing";
				timeStart = DateTime.Now;
				//Console.WriteLine(GetState() + ". Reproduction Started at {0}", timeStart);
			} else if (State == "Playing") {
				//Console.WriteLine("Content Playing yet.");
			} else if (State == "Paused") {
				State = "Playing";
				//Console.WriteLine(GetState() + ". Reproduction Resumed at {0}. Reproduction Time Elapsed so far (sec): {1}", DateTime.Now, (int)(DateTime.Now - timeAction).TotalSeconds);
			} else if (State == "Stopped") {
				State = "Playing";
				//Console.WriteLine(GetState() + ". Reproduction Restarted at " + DateTime.Now + ". Previous Reproduction Arrested at {0}", timeAction);
				timeStart = DateTime.Now;
			}
		}
		
		public void Pause() {
			if (State == "instantiated") {
				//Console.WriteLine("Content not Started yet cannot be Paused. Consider Playing it first.");
			} else if (State == "Playing") {
				State = "Paused";
				timeAction = DateTime.Now;
				//Console.WriteLine(GetState() + ". Reproduction Time Elapsed so far (sec) : {0}", (int)(timeAction - timeStart).TotalSeconds);
			} else if (State == "Paused") {
				//Console.WriteLine("Content Paused yet.");
			} else if (State == "Stopped") {
				//Console.WriteLine("Content Stopped cannot be Paused. Consider Playing it first.");
			}
		}
		
		public void Stop() {
			if (State == "instantiated") {
				//Console.WriteLine("Content not Started yet cannot be Stopped. Consider Playing it first.");
			} else if (State == "Playing" || State == "Paused") {
				State = "Stopped";
				timeAction = DateTime.Now;
				//Console.WriteLine(GetState() + " at {0}", timeAction + ". Reproduction Time before Stop (sec) : " + (int)(DateTime.Now - timeStart).TotalSeconds);
			} else if (State == "Stopped") {
				//Console.WriteLine("Content Stopped yet. Consider Starting it again.");
			}
		}
		
		public string GetState() {
			string contentState = "State";
			if (State == "Playing") {
				contentState = "Content State: Playing";
			}
			else if (State == "Paused") {
				contentState = "Content State: Paused";
			}
			else if (State == "Stopped") {
				contentState = "Content State: Stopped";
			}
			return contentState;
		}
		
		public override string ToString() {
			return (string) this.GetTitle() + "#" + this.Duration();
		}
	
	}
	
	// D E R I V E D _ C L A S S E S
	
	// ToString method overridden because it is necessary to get 
	// such information from a PlayList DLL
	
	public class Movie : BaseContent, IPlayable, IVideo	{
		
		public string genre {get; set;}
		
		public Movie (string title, int duration, ArtWork art, string genre) : base (title, duration, art) {
			this.genre     =   genre;
			this.metalabel = "Movie";
		}
				
		public override string ToString() {
			return (string) "M#" + this.GetTitle() + "#" + this.Duration() + "#" + this.genre;
		}
		
	}
	
	public class Documentary : BaseContent, IPlayable, IVideo {
		
		public string topic {get; set;}
		
		public Documentary (string title,int duration, ArtWork art, string topic) : base (title, duration, art) {
			this.topic     =         topic;
			this.metalabel = "Documentary";
		}
		
		public override string ToString() {
			return (string) "D#" + this.GetTitle() + "#" + this.Duration() + "#" + this.topic;
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
			
		public override string ToString() {
			return (string) "S#" + this.GetTitle() + "#" + this.Duration() + "#" + this.serie + "#" + this.season + "#" + this.epNumber;
		}
		
	}
	
}