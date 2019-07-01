using System;
using System.IO;
using scoopflix;
using NSPClasses;

namespace PlayListActions {
	
	public class PlayList {
		
		// definition of the DLLNode class (only propetries)
		public class PlayListItem {
			public IPlayable Content {get; set;}
			public PlayListItem Next {get; set;}
			public PlayListItem Prev {get; set;}
		}
		
		// fields of PlayList
		private PlayListItem head;
		private PlayListItem tail;
		private PlayListItem Curr;
		private          int    n;
		
		// constructor
		public PlayList() {
			this.head = new PlayListItem();
			this.tail = new PlayListItem();
			this.Curr = new PlayListItem();
			this.head.Next = this.tail;
			this.head.Prev =      null;
			this.tail.Next =      null;
			this.tail.Prev = this.head;
			this.n         =         0;
		}
		
		
		// methods
		public void Add(IPlayable content) {
		
			
			PlayListItem newContent = new PlayListItem();
			newContent.Content      = content;
			
			newContent.Next     =      this.tail;
			newContent.Prev     = this.tail.Prev;
			this.tail.Prev.Next =     newContent;
			this.tail.Prev      =     newContent;
			
			this.n++;
		}
		
		public void Next() {
			
			try 
			{
				this.Curr.Content.Stop();
				this.Curr    = Curr.Next;
				this.Curr.Content.Play();
			}
			catch (Exception e) 
			{
				Console.WriteLine("No more content is available in the current PlayList: {0}", e);
			}
			
		}
		
		
		public IPlayable GetCurrentContent() {
			return this.Curr.Content;
		}
		
		public bool HasMoreContents() {
			return (Curr.Next != null);
		}
		
		public void Rewind() {
			Curr = this.head.Next;
		}
		
	}
	
	
	
	
	public class PlayListIO {
		
		public static PlayList LoadPlayListFromFile(string filePath) {
			
			
			PlayList pl = new PlayList();
			StreamReader sr = new StreamReader(filePath);
			
			while (sr.Peek() >= 0) {
				
				string Entry = sr.ReadLine();
				string [] EntryField = Entry.Split('#');
				
				if (EntryField[0] == "D") {
					int duration       = Convert.ToInt32(EntryField[2]);
					Documentary doc    = new Documentary(EntryField[1], duration/60, new ArtWork(EntryField[1]), EntryField[3]);
					IPlayable toadd    = (IPlayable) doc;
					pl.Add(toadd);
				} else if (EntryField[0] == "M") {
					int duration       = Convert.ToInt32(EntryField[2]);
					Movie film         = new Movie(EntryField[1], duration/60, new ArtWork(EntryField[1]), EntryField[3]);
					IPlayable toadd    = (IPlayable) film;
					pl.Add(toadd);
				} else if (EntryField[0] == "S") {
					int duration       = Convert.ToInt32(EntryField[2]);
					int season         = Convert.ToInt32(EntryField[4]);
					int episode        = Convert.ToInt32(EntryField[5]);
					SerieEpisode serie = new SerieEpisode(EntryField[1], duration/60, new ArtWork(EntryField[1]), EntryField[3], season, episode);
					IPlayable toadd    = (IPlayable) serie;
					pl.Add(toadd);
				}
				
			}
			
			return pl;
			
		}
		
		
		public static void SavePlayListToFile(PlayList p, string filePath) {
			
			StreamWriter sw = new StreamWriter(filePath);
			
			p.Rewind();
			
			
			while (p.HasMoreContents()) {
				
				if (p.GetCurrentContent() is Documentary) {
					sw.Write(p.GetCurrentContent().ToString() + Environment.NewLine);
					// Console.WriteLine(p.GetCurrentContent().ToString());
				} else if (p.GetCurrentContent() is Movie) {
					sw.Write(p.GetCurrentContent().ToString() + Environment.NewLine);
					// Console.WriteLine(p.GetCurrentContent().ToString());
				} else if (p.GetCurrentContent() is SerieEpisode) {
					sw.Write(p.GetCurrentContent().ToString() + Environment.NewLine);
					// Console.WriteLine(p.GetCurrentContent().ToString());
				}
				
				p.Next();
			}
			
			sw.Close();			
		} 
		
		
	} 
	
}