using System;
using System.IO;
using scoopflix;
using NSPClasses;
using PlayListActions;


// M A I N _ C L A S S 

public class MainApp {
	
	
	static void Main () {
		
		// setting the input and output directories
		
		string DirPathIn = "Data/Source";
		if (!Directory.Exists(DirPathIn)) {
			Directory.CreateDirectory(DirPathIn);
		}
		string pathIn = DirPathIn + "/videos.txt";
		
		string DirPathOut = "Data/Drop";
		if (!Directory.Exists(DirPathOut)) {
			Directory.CreateDirectory(DirPathOut);
		}
		string pathOut = DirPathOut + "/PlayList_Full.dat";
		
		// istantiating a new playlist object
		
		PlayList p = new PlayList();	
		
		// playlist import and print on file
		
		p = PlayListIO.LoadPlayListFromFile(pathIn);
		
		PlayListIO.SavePlayListToFile(p, pathOut);
		
		// creation of other playlists containing separated kinds of video contents
		
		PlayList pDoc = new PlayList();
		PlayList pMov = new PlayList();
		PlayList pSer = new PlayList();
		
		string pOutDoc = DirPathOut + "/Documentary_PlayList.dat";
		string pOutMov = DirPathOut + "/Movie_PlayList.dat";
		string pOutSer = DirPathOut + "/SerieEpisode_PlayList.dat";
		
		p.Rewind();
		while (p.HasMoreContents()) {
			if (p.GetCurrentContent() is Documentary) {
				pDoc.Add(p.GetCurrentContent());
			} else if (p.GetCurrentContent() is Movie) {
				pMov.Add(p.GetCurrentContent());
			} else if (p.GetCurrentContent() is SerieEpisode) {
				pSer.Add(p.GetCurrentContent());
			}
			p.Next();
		}
		
		PlayListIO.SavePlayListToFile(pDoc, pOutDoc);
		PlayListIO.SavePlayListToFile(pMov, pOutMov);
		PlayListIO.SavePlayListToFile(pSer, pOutSer);
		
		Console.WriteLine("PRESS ANY KEY TO CONTINUE");
		Console.ReadKey();
	}
	
}

