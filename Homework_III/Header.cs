using System;

namespace scoopflix {
	
	// I N T E R F A C E S
	
	// IVideo interface
	
	public interface IVideo {
		
		string        GetTitle();
		int    GetDurationMins();
		IImage      GetArtWork();
		
	}
	
	// IPlayable interface
	
	public interface IPlayable {
		
		void       Play();
		void      Pause();
		void       Stop();
		string GetState();
		string ToString();
		
	}
	
	// IImage interface
	
	public interface IImage {
		
		string GetPath();
		int  GetHeight();
		int   GetWidth();
		
	}
	
	// ArtWork class
	
	public class ArtWork: IImage {
		
		// fields
		protected string   Path;
		protected    int Height;
		protected    int  Width;
		
		// constructor
		public ArtWork(string picTitle) {
			this.Path = "SERVER:/Images/Thumbnails/" + picTitle + ".jpg";
			this.Height = 468;
			this.Width  = 642;
		}
		
		// IImage implementation
		public string GetPath() {
			return this.Path;
		}
		
		public int GetHeight() {
			return this.Height;
		}
		
		public int GetWidth() {
			return this.Width;
		}
		
	}

}