using System;

namespace scoopflix {
	
	// I N T E R F A C E S
	
	// IVideo interface
	/*
		This interface provides title, duration and artwork 
		return methods signatures _________________________
	*/
	public interface IVideo {
		
		string        GetTitle();
		int    GetDurationMins();
		IImage      GetArtWork();
		
	}
	
	// IPlayable interface
	/*
		This interface provide signatures of action methods,
		eventually to be performed on the content chosen ___
	*/
	public interface IPlayable {
		
		void       Play();
		void      Pause();
		void       Stop();
		string GetState();
		
	}
	
	// IImage interface
	/*
		Here we provide mehods for accessing artwork details
	*/
	public interface IImage {
		
		string GetPath();
		int  GetHeight();
		int   GetWidth();
		
	}
	
	// ArtWork class
	/*
		This class implements IImage interface, since it's
		designed to provide the content chosen the proper_
		artwork, i.e. the preview of the content__________
		It's to be successively used as a field in the ___
		BaseContent base class; thus it's a common feature
		for all the contents which inherit from the base _
	*/
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