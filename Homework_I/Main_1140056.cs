using System;

/*
	Vengono scritte tre classi, atte a contenere informazioni.
	sui contenuti di un'azienda di servizi media..............
	Le tre classi sono:                                       
		-        Film ;                                      
		-    Serie TV ;
		- Documentari ;
	ognuna avente i rispettivi identificativi.................
	Ognuna di esse viene attrezzata con un metodo ,,ToString''
	che serve per scrivere a schermo gli identificativi di ...
	ognuna delle istanze di ogni classe (solo una istanza)....
	Le suddette classi vengono smistate in files diversi......
*/



class MainApp {
	
	public static void Main () {
		
		Console.WriteLine("\n\n***********************************\nOFFERTA  CONTENUTI  MEDIA\n");
		Console.WriteLine("");
		
		// Movie
		Console.WriteLine("___________________________________\n");
		Movie film = new Movie("Play it again, Sam", "Commedia", "Movie"); 
		film.Duration = 85; // in minuti
		Console.WriteLine("- Contenuto   (i): " + "[" + film.Meta() + "] \n" + film.ToString());
		Console.WriteLine("___________________________________\n");
		
		// Documentary
		Console.WriteLine("___________________________________\n");
		Documentary doc = new Documentary("Inside Job", "Economics", "Documentary");
		doc.Duration = 108; // in minuti
		Console.WriteLine("- Contenuto  (ii): " + "[" + doc.Meta() +  "] \n"  + doc.ToString());
		Console.WriteLine("___________________________________\n");
		
		// Serie Episode
		Console.WriteLine("___________________________________\n");
		SerieEpisode eps = new SerieEpisode("Amore", "Il Commissario Montalbano", 1, 12, "Serie Episode");
		eps.EpDuration = 110; // in minuti
		Console.WriteLine("- Contenuto (iii): " + "[" + eps.Meta() +  "] \n"  + eps.ToString());
		Console.WriteLine("___________________________________\n");
		
		Console.WriteLine("\n\n***********************************\n\n");
		
		Console.WriteLine("Premere un qualsiasi tasto per interrompere l'esecuzione.");
		Console.ReadKey();
		
	}
}