using System;
using scoopflix;


// M A I N _ C L A S S 

public class MainApp {
	
	// a simple method to keep CPU busy in order to 
	// appreciate content reproduction time and to_
	// test effectiveness and consistency of the __
	// Play(), Pause() and Stop() methods _________
	
	static void ContentPlayForAWhile () {
		
		long N = 100000000;
		
		for (long i = 0; i < N; i++) {
			// do nothing
		}
		
		for (int i = 0; i < N; i++) {
			// keep doing nothing
		}
		
		for (int j = 0; j < 10; j++) {
			for (int i = 0; i < N; i++) {
				// still hold on
			}
		}
	} 
	
	static void Main () {
		
		
		// Print the list of the available contents
		Console.WriteLine("Contents:\n\n");
		
		Movie film = new Movie ("Play it again, Sam", 85, new ArtWork("play_it_again_Sam"), "Comedy");
		Console.WriteLine("Content: {0}, Time: {1}, Genre: {2}", film.title, film.Duration(), film.genre);
		
		Documentary doc = new Documentary ("Inside Job", 108, new ArtWork("inside_job"), "Economics");
		Console.WriteLine("Content: {0}, Time: {1}, Topic: {2}", doc.title, doc.Duration(), doc.topic);
		
		SerieEpisode serieep = new SerieEpisode("Amore", 120, new ArtWork("comm_Montalbano_S12E1"), "Il Commissario Montalbano", 12, 1);
		Console.WriteLine("Content: {0}, Time: {1}, Serie: {2}, Season: {3}, Episode Number: {4}", serieep.title, serieep.Duration(), serieep.serie, serieep.season, serieep.epNumber);

		
		// assume we choose to watch at this awesome Woody Allen masterpiece
		// details of the contents are reported by invoking proper methods
		Console.WriteLine("\n\nContent analysed: " + film.metalabel + ". Title: " + film.GetTitle());
		Console.WriteLine("Content duration (mins): {0}", film.GetDurationMins());
		
		IImage art = (IImage) film.GetArtWork();
		Console.WriteLine("ArtWork Details: File Path: {0}, Thumbnail Height and Width respectively (pixels) : {1}; {2}", art.GetPath(), art.GetHeight(), art.GetWidth());
		
		
		// content is to be actioned upon by playing, pausing, stopping it.
		// details about time elapsed are also shown.
		
		film.Stop();
		film.Pause();
		
		film.Play();
		
		ContentPlayForAWhile();
		
		film.Pause();
		
		ContentPlayForAWhile();
		
		film.Play();
		
		ContentPlayForAWhile();
		
		film.Play();
		
		ContentPlayForAWhile();
		
		film.Stop();
		
		ContentPlayForAWhile();
		
		film.Stop();
		
		ContentPlayForAWhile();
		
		film.Play();
		
		ContentPlayForAWhile(); ContentPlayForAWhile();
		
		film.Pause();
		
		ContentPlayForAWhile();
		
		film.Pause();
		
		Console.WriteLine("\n\nPress any Key to quit.");
		Console.ReadKey();
		
	}
	
}

